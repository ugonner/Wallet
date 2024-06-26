namespace Shared.DTOs.UserDTOs;

using System.ComponentModel.DataAnnotations;
public class LoginDTO
{
    [Required(ErrorMessage = "Password is required")]
    public string Password {get; set;}
    
    [EmailAddress(ErrorMessage = "valid Email is required")]
    public string Email {get; set;}

    public string UserName {get; set;}




}