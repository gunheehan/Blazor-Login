using Login.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Login.Api.Data;

public static class DataExtensions
{
    public static void InitializeDb(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<LoginAPIContext>();
        dbContext.Database.Migrate();
    }

    public static IServiceCollection AddRespositories(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connString = configuration.GetConnectionString("LoginAPIContext");
        services.AddSqlServer<LoginAPIContext>(connString)
            .AddScoped<ISignRepository, EntityFrameworkLoginsRepository>();

        return services;
    }
}