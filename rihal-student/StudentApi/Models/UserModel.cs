
namespace StudentApi.Models;
using System.ComponentModel.DataAnnotations;

public class UserModel
{
    [Required]
    public string UserName { get; set; }
    
    [Required]
    public string Password { get; set; }
}