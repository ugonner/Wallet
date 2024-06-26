
namespace Entities;
using System.ComponentModel.DataAnnotations;
public class User
{
    [Key]
    public int UserId {get; set;}
    [Required(ErrorMessage = "username is required")]
    public string UserName {get; set;}

    [EmailAddress]
    [Required(ErrorMessage = "email is required")]
    public string Email {get; set;}
    
    [Required(ErrorMessage = "password is required")]
    public string Password {get; set;}
    
    public string? RefreshToken {get; set;}
}
