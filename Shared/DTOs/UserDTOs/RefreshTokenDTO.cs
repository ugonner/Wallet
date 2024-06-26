using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.UserDTOs;
public class RefreshTokenDTO
{
    
    [Required(ErrorMessage = "Token is requi")]
    public string Token {get; set;}
    
    [Required(ErrorMessage = "RefreshToken is requi")]
    public string RefreshToken {get; set;}
}