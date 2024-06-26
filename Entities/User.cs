
namespace Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    
    public String RefreshToken {get; set;}


    [ForeignKey(nameof(Role))]
    public int RoleId {get; set;}

    public Role Role {get; set;}
}
