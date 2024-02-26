using System.ComponentModel.DataAnnotations;

namespace AnimalShelterApi.Models
{
  public class SignInDto
  {
   [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
  }
}