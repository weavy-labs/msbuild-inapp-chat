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
    private Dictionary<string,string> directories = new Dictionary<string, string>(){
        {"rickard", "Directory A"},
        {"nina", "Directory A"},
        {"lydia", "Directory A"},
        {"johan", "Directory B"},
        {"magnus", "Directory B"},
        {"dave", "Directory B"}
    };

    private Dictionary<string,string> avatars = new Dictionary<string, string>(){
        {"rickard", "https://www.weavy.com/hubfs/MSBuild/user1.svg"},
        {"nina", "https://www.weavy.com/hubfs/MSBuild/user2.svg"},
        {"lydia", "https://www.weavy.com/hubfs/MSBuild/user6.svg"},
        {"johan", "https://www.weavy.com/hubfs/MSBuild/user5.svg"},
        {"magnus", "https://www.weavy.com/hubfs/MSBuild/user7.svg"},
        {"dave", "https://www.weavy.com/hubfs/MSBuild/user3.svg"}
    };
  
    public HomeController(IConfiguration config, ILogger<HomeController> logger) {
        _config = config;
        _logger = logger;
    }

    /// <summary>
    /// Displays the index page.
    /// </summary>
    /// <returns></returns>
    [HttpGet("")]
    public IActionResult Index() {
        return View();
    }

    /// <summary>
    /// Displays the login page.
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet("login")]
    public IActionResult Login() {
        var model = new LoginModel();
        return View(model);
    }

    /// <summary>
    /// Signs in the user and sets auth cookie.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginModel model) {

        var strDirectory = "Directory C";
        var strAvtar = "https://www.weavy.com/hubfs/MSBuild/user4.svg";

        if (directories.ContainsKey(model.Username)) {
            strDirectory = directories[model.Username];
            strAvtar = avatars[model.Username];
        } 

        if (ModelState.IsValid) {
            var claims = new List<Claim> { 
                new Claim("username", model.Username),
                new Claim("directory", strDirectory),
                new Claim("avatar", strAvtar)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, "username", "role");
            var user = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    /// <summary>
    /// Logs out the user.
    /// </summary>
    /// <returns></returns>
    [HttpGet("logout")]
    public async Task<IActionResult> Logout() {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Handles errors.
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [Route("error")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
        return View(new ErrorModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
