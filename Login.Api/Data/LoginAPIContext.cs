using Login.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Login.Api.Data;

public class LoginAPIContext : DbContext
{
    public LoginAPIContext(DbContextOptions<LoginAPIContext> options) : base(options)
    { }

    public DbSet<LoginInfo> LoginInfos => Set<LoginInfo>();
}