using Login.Api.Entities;

namespace Login.Api.Repositories;

public class InMemSignRepository : ISignRepository
{
    static readonly List<LoginInfo> info = new()
    {
        new LoginInfo()
        {
            Id = 1,
            Nickname = "gun",
            Email = "gungun@gun.com"
        },
        new LoginInfo()
        {
            Id = 2,
            Nickname = "hea",
            Email = "heahea@hea.com"
        }
    };

    public IEnumerable<LoginInfo> GetAll()
    {
        return info;
    }

    public LoginInfo Get(int id)
    {
        return info.Find(info => info.Id == id);
    }

    public LoginInfo GetUser(string email)
    {
        return info.Find(info => info.Email == email);
    }

    public void Create(LoginInfo newInfo)
    {
        newInfo.Id = info.Max(loginInfo => loginInfo.Id) + 1;
        info.Add(newInfo);
    }

    public void Update(LoginInfo updateInfo)
    {
        var index = info.FindIndex(info => info.Id == updateInfo.Id);
        info[index] = updateInfo;
    }

    public void Delete(int id)
    {
        var index = info.FindIndex(info => info.Id == id);
        info.RemoveAt(index);
    }
}