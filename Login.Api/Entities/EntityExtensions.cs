namespace Login.Api.Entities;

public static class EntityExtensions
{
    public static UserDto AsDto(this LoginInfo user)
    {
        return new UserDto(
            user.Id,
            user.Nickname,
            user.Email
        );
    }
}