using Business.Features.Users.Dtos;

namespace Business.Features.Users.Services;

public interface IUserService
{
    Task CreateAsync(CreateUserDto dto);
}