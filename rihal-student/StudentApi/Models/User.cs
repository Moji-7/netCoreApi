
namespace StudentApi.Models;
using System.ComponentModel.DataAnnotations;

public class User
{

    public int ID { get; set; }
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }
}