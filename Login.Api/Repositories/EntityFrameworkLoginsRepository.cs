using Login.Api.Data;
using Login.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Login.Api.Repositories;

public class EntityFrameworkLoginsRepository : ISignRepository
{
    private readonly LoginAPIContext dbContext;
    
    public EntityFrameworkLoginsRepository(LoginAPIContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public IEnumerable<LoginInfo> GetAll()
    {
        return dbContext.LoginInfos.AsNoTracking().ToList();
    }
    
    public LoginInfo Get(int id)
    {
        return dbContext.LoginInfos.Find(id);
    }

    public LoginInfo GetUser(string email)
    {
        return dbContext.LoginInfos.Find(email);
    }

    public void Create(LoginInfo newInfo)
    {
        dbContext.LoginInfos.Add(newInfo);
        dbContext.SaveChanges();
    }

    public void Update(LoginInfo updateInfo)
    {
        dbContext.LoginInfos.Update(updateInfo);
        dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        dbContext.LoginInfos.Where(info => info.Id == id)
            .ExecuteDelete();
    }
}