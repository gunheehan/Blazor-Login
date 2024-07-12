using Login.Api.EndPoints;
using Login.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ISignRepository, InMemSignRepository>();

var app = builder.Build();

app.MapSignEndpoints();

app.Run();
