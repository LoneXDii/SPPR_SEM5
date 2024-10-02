using Microsoft.AspNetCore.Mvc;
using WEB_253505_PAVLOVICH.UI.Models;
using WEB_253505_PAVLOVICH.UI.Services.Authentication;

namespace WEB_253505_PAVLOVICH.UI.Controllers;

public class AccountController : Controller
{
    private readonly IAuthService _authService;

    public AccountController(IAuthService authService)
    {
        _authService = authService;
    }

    public IActionResult Register()
    {
        return View(new RegisterUserViewModel());
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Register(RegisterUserViewModel user, [FromServices] IAuthService authService)
    {
        if (ModelState.IsValid)
        {
            if (user == null)
            {
                return BadRequest();
            }
            var result = await authService.RegisterUserAsync(user.Email, user.Password, user.Avatar);
            if (result.Result)
            {
                return Redirect(Url.Action("Index", "Home"));
            }
            else return BadRequest(result.ErrorMessage);
        }
        return View(user);
    }
}
