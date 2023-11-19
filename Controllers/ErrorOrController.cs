using BaseWebApp.Models;
using BaseWebApp.Services.Abstract;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Controllers;

[ApiController]
[Route("[controller]")]
public class ErrorOrController : ControllerBase
{
    private IUserService _userService;

    public ErrorOrController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public IActionResult Register(string username, string password)
    {
        var registrationResult = RegisterUser(username, password);

        if (registrationResult.IsError)
        {
            return BadRequest(registrationResult.FirstError);
        }

        return Ok(registrationResult.Value);
    }
    private ErrorOr<User> RegisterUser(string username, string password)
    {
        if (string.IsNullOrEmpty(username))
            return Error.Failure(code: "UsernameIsRequired", description: "Username is required.");

        if (UserExists(username))
            return Error.Failure(code: "UsernameAlreadyExists", description: "Username already exists.");

        // Hata Yoksa Kullanıcı Kaydı İşlemi Yapılır.
        var user = new User { UserName = username, Password = password };
        _userService.AddUser(user);
        return user;
    }

    private bool UserExists(string username)
    {
       User? user = _userService.GetUserByUserName(username);
       return !(user is null);
    }
}
