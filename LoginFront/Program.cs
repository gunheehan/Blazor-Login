using LoginFront.Clients;
using LoginFront.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

var userApiUrl = builder.Configuration["userApiUrl"] ??
    throw new Exception("UserApiUrl is not set");

builder.Services.AddHttpClient<UserClient>(client => client.BaseAddress = new Uri(userApiUrl));

builder.Services.AddSingleton<UserClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
