using Login.Api.Data;
using Login.Api.EndPoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRespositories(builder.Configuration);

var app = builder.Build();

app.Services.InitializeDb();

app.MapSignEndpoints();

app.Run();
