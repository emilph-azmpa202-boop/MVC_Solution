using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using solution.Services.Interfaces;
using solution.ViewModels.Auth;

public class AccountController : Controller
{
    private readonly IAuthService _accountService;

    public AccountController(IAuthService authService)
    {
        _accountService = authService;
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        return View("~/Views/Auth/Login/Index.cshtml");
    }

    [HttpGet("register")]
    public IActionResult Register()
    {
        return View("~/Views/Auth/Register/Index.cshtml");
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterVM model)
    {
        if (!ModelState.IsValid)
            return View("~/Views/Auth/Register/Index.cshtml", model);

        var result = await _accountService.RegisterAsync(model);

        if (!result.Succeeded)
        {
            foreach (var err in result.Errors)
            {
                ModelState.AddModelError("", err.Description);
            }
            return View("~/Views/Auth/Register/Index.cshtml", model);
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginVM model)
    {
        if (!ModelState.IsValid)
            return View("~/Views/Auth/Login/Index.cshtml", model);

        var result = await _accountService.LoginAsync(model);

        if (!result)
        {
            ModelState.AddModelError("", "Login failed");
            return View("~/Views/Auth/Login/Index.cshtml", model);
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _accountService.LogoutAsync();
        return RedirectToAction("Login");
    }
}