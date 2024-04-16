using System.ComponentModel.DataAnnotations;

namespace SPASite.BlazorWebAssembly.Client.Domain;

public class RegisterMemberAndSignInCommand
{
    [Required]
    [StringLength(150)]
    [EmailAddress(ErrorMessage = "Please use a valid email address")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string DisplayName { get; set; } = string.Empty;

    [Required]
    [StringLength(300, MinimumLength = 8)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}
