using System.ComponentModel.DataAnnotations;

namespace Login.Api;

    public record UserDto(
        int Id,
        string Nickname,
        string Email
    );
    
    public record CreateUserDto(
        [Required][StringLength(8)]string Nickname,
        [Required][EmailAddress]string Email
        );
    
    public record UpdateUserDto(
        [Required][StringLength(8)]string Nickname, 
        [Required][EmailAddress]string Email
        );