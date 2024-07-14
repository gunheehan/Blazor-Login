using LoginFront.models;

namespace LoginFront.Clients;

public class UserClient
{
    private readonly List<UserSummary> users =
    [
        new()
        {
            Id = 1,
            Nickname = "gun",
            Email = "gungun@gun.com"
        },
        new()
        {
            Id = 2,
            Nickname = "hea",
            Email = "heahea@hea.com"
        }
    ];

    public UserSummary[] GetUsers => users.ToArray();

    public void AddUser(UserDetails user)
    {
        var userSummary = new UserSummary()
        {
            Id = users.Count + 1,
            Nickname = user.Nickname,
            Email = user.Email
        };
        
        users.Add(userSummary);
    }

    public UserDetails GetUser(int id)
    {
        UserSummary? user = users.Find(user => user.Id == id);
        ArgumentNullException.ThrowIfNull(user);

        return new UserDetails()
        {
            Id = user.Id,
            Nickname = user.Nickname,
            Email = user.Email
        };
    }
}