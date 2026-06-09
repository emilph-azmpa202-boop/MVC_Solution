using Microsoft.AspNetCore.Identity;
using solution.ViewModels.Auth;

namespace solution.Services.Interfaces
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterAsync(RegisterVM model);
        Task<bool> LoginAsync(LoginVM model);
        Task LogoutAsync();
    }
}