using Business.Features.Users.Constants;
using Business.Features.Users.Dtos;
using CrossCuttingConcerns.Exceptions.Types;
using DataAccess.Abstract;
using Domain.Entities;
using Security.Hashing;

namespace Business.Features.Users.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task CreateAsync(CreateUserDto dto)
    {
        await EnsureEmailAddressDoesNotExist(dto.EmailAddress);
        HashingHelper.CreatePasswordHash(dto.Password, out var passwordHash, out var passwordSalt);
        var user = new User
        {
            EmailAddress = dto.EmailAddress,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Status = true
        };

        await _userRepository.AddAsync(user);
    }

    #region Rules
    private async Task EnsureEmailAddressDoesNotExist(string emailAddress)
    {
        if (await _userRepository.AnyAsync(i => i.EmailAddress == emailAddress, enableTracking: false))
            throw new BusinessException(UserMessages.EmailAddressAlreadyTaken);
    }
    #endregion
}