using System.ComponentModel.DataAnnotations;

namespace SPASite.BlazorWebAssembly.Client.Domain;

public class SignMemberInCommand
{
    [Required]
    [EmailAddress(ErrorMessage = "Please use a valid email address")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}
