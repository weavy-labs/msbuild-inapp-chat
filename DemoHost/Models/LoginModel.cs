using System.ComponentModel.DataAnnotations;

namespace TestHost.Models;

public class LoginModel {
    [Required]
    public string Username { get; set; }
    
    public string Password { get; set; }
}
