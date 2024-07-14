namespace LoginFront.models;

public class UserSummary
{
    public int Id { get; set; }
    public required string Nickname { get; set; }
    public required string Email { get; set; }
}