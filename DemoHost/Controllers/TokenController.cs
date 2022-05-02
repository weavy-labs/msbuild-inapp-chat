using Jose;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace TestHost.Controllers;

[Authorize]
[Route("token")]
public class TokenController : Controller {
    private readonly IConfiguration _config;
    public TokenController(IConfiguration config) {
        _config = config;
    }

    /// <summary>
    /// Create and return the JWT token for Weavy.
    /// </summary>
    /// <returns></returns>
    [HttpGet("")]
    public IActionResult GetToken() {
        Random rnd = new Random();
        var payload = new Dictionary<string, object>(){
            { "iss", _config["ClientId"] },
            { "sub", User.Identity.Name},
            { "username", User.Identity.Name},
            { "picture", User.FindFirst("avatar").Value },
            { "dir", User.FindFirst("directory").Value },
            { "exp", DateTimeOffset.UtcNow.AddMinutes(5).ToUnixTimeSeconds()}
        };
        var token = JWT.Encode(payload, Encoding.UTF8.GetBytes(_config["ClientSecret"]), JwsAlgorithm.HS256);
        return Content(token);
    }
}
