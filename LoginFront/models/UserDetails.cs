using System.ComponentModel.DataAnnotations;

namespace LoginFront.Clients;

public class UserDetails
{
    public int Id { get; set; }
    public string Nickname { get; set; }
    public string Email { get; set; }
}