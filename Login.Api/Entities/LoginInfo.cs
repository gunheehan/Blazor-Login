using System.ComponentModel.DataAnnotations;

namespace Login.Api.Entities;

public class LoginInfo
{
    public int Id { get; set; }
    [Required][StringLength(8)]
    public string Nickname { get; set; }
    [Required][EmailAddress]
    public string Email { get; set; }
}