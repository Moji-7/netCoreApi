
namespace StudentApi.Models;
using System.ComponentModel.DataAnnotations;

public class User
{

    public int ID { get; set; }

    [Required]
    public string UserName { get; set; }
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string Password { get; set; }
}