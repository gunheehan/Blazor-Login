using System.ComponentModel.DataAnnotations;

namespace LoginFront.Clients;

public class UserDetails
{
    public int Id { get; set; }
    [Required][StringLength(8)]
    public required string Nickname { get; set; }
    [Required(ErrorMessage = "The Email is required")][EmailAddress]
    public required string Email { get; set; }
}