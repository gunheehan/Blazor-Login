using Login.Api.Entities;

namespace Login.Api.Repositories;

public interface ISignRepository
{
    IEnumerable<LoginInfo> GetAll();
    LoginInfo Get(int id);
    void Create(LoginInfo newInfo);
    void Update(LoginInfo updateInfo);
    void Delete(int id);
}