using System.ComponentModel.DataAnnotations;

namespace Blazor.Learner.Shared.Models
{
  public class RegisterRequest
  {
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    [Compare(nameof(Password), ErrorMessage = "Passwords does not match!")]
    public string PasswordConfirm { get; set; }
  }
}