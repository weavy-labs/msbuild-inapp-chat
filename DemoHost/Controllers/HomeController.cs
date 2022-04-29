using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using TestHost.Models;
using Jose;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TestHost.Controllers;

[Authorize]
[Route("")]
public class HomeController : Controller {
    private readonly IConfiguration _config;
    private readonly ILogger<HomeController> _logger;

    public HomeController(IConfiguration config, ILogger<HomeController> logger) {
        _config = config;
        _logger = logger;
    }

    [HttpGet("")]
    public IActionResult Index() {
        return View();
    }

    [HttpGet("token")]
    public IActionResult GetToken() {
        var payload = new Dictionary<string, object>(){
            { "iss", _config["ClientId"] },
            { "sub", User.Identity.Name},
            { "username", User.Identity.Name},
            { "exp", DateTimeOffset.UtcNow.AddMinutes(5).ToUnixTimeSeconds()}
        };
        var token = Jose.JWT.Encode(payload, Encoding.UTF8.GetBytes(_config["ClientSecret"]), JwsAlgorithm.HS256);
        return Content(token);
    }

    [AllowAnonymous]
    [HttpGet("login")]
    public IActionResult Login() {
        var model = new LoginModel();
        return View(model);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginModel model) {

        if (ModelState.IsValid) {

            var claims = new List<Claim> { new Claim("username", model.Username) };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, "username", "role");

            var user = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);

            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout() {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction(nameof(Index));
    }

    [AllowAnonymous]
    [Route("error")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
        return View(new ErrorModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
