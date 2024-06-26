using System.ComponentModel.DataAnnotations;

namespace Entities;

public class Role
{
    [Key]
    public int Id {get; set;}

    
    [Required(ErrorMessage = "Role Name required")]
    public string RoleName {get; set;}

    public ICollection<User> Users {get; set;}

    
}