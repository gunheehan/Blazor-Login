using Login.Api.Entities;
using Login.Api.Repositories;

namespace Login.Api.EndPoints;

public static class LoginEndpoints
{
    const string GetLoginInfoEndPoint = "GetInfo";
    
    public static RouteGroupBuilder MapSignEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/Logins")
            .WithParameterValidation();
 
        group.MapGet("/", (ISignRepository repository) => repository.GetAll().Select(user => user.AsDto()));

        group.MapGet("/{id}", (ISignRepository repository, int id) =>
        {
            LoginInfo? userinfo = repository.Get(id);

            if (userinfo is null)
                return Results.NotFound("Not Found Info for : " + id);

            return Results.Ok(userinfo.AsDto());
        }).WithName(GetLoginInfoEndPoint);

        group.MapPost("/", (ISignRepository repository, CreateUserDto userDto) =>
        {
            LoginInfo loginInfo = new()
            {
                Nickname = userDto.Nickname,
                Email = userDto.Email
            };
            
            repository.Create(loginInfo);

            return Results.CreatedAtRoute(GetLoginInfoEndPoint, new { id = loginInfo.Id }, loginInfo);
        });

        group.MapPut("/{id}", (ISignRepository repository, int id, UpdateUserDto updateInfoDto) =>
        {
            LoginInfo? existingInfo = repository.Get(id);

            if (existingInfo is null)
                return Results.NotFound("Not Found Info for : " + id);

            existingInfo.Nickname = updateInfoDto.Nickname;
            existingInfo.Email = updateInfoDto.Email;
            
            repository.Update(existingInfo);

            return Results.Ok(existingInfo);
        });

        group.MapDelete("/{id}", (ISignRepository repository, int id) =>
        {
            LoginInfo? userinfo = repository.Get(id);

            if (userinfo is null)
                return Results.NotFound("Not Found Info for : " + id);

            repository.Delete(id);

            return Results.Ok();
        });

        return group;
    }
}