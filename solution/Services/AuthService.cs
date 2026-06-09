using Microsoft.AspNetCore.Identity;
using solution.Models;
using solution.Services.Interfaces;
using solution.ViewModels.Auth;

namespace solution.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterVM model)
        {
            AppUser user = new AppUser
            {
                FullName = model.FirstName,
                UserName = model.LastName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return result;

            await _userManager.AddToRoleAsync(user, "User");

            await _signInManager.SignInAsync(user, false);

            return IdentityResult.Success;
        }

        public async Task<bool> LoginAsync(LoginVM model)
        {
            AppUser user;

            if (model.UserNameOrEmail.Contains("@"))
                user = await _userManager.FindByEmailAsync(model.UserNameOrEmail);
            else
                user = await _userManager.FindByNameAsync(model.UserNameOrEmail);

            if (user == null)
                return false;

            var result = await _signInManager.CheckPasswordSignInAsync(
                user,
                model.Password,
                false);

            return result.Succeeded;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}