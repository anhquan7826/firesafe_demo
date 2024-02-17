using Application.DTOs;
using Application.Middlewares;
using Application.Services.Interface;
using AutoMapper;
using Firesafe.Domain.Commands;
using Firesafe.Domain.Core.Mediator;
using Firesafe.Domain.Entities;
using Firesafe.Domain.UnitOfWork;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Impl;

public class UserService(IMediatorHandler mediatorHandler, IUnitOfWork uow, ICloudStorage cloudStorage, IMapper mapper)
    : BaseService, IUserService
{
    public UserDto GetUserFromFirebaseId(string firebaseId)
    {
        var user = uow.UserRepository.GetByFirebaseId(firebaseId.Trim());
        if (user == null) NotifyError("User is not found!");
        return MapToDto(user!);
    }

    public async Task AddUser(string firebaseId)
    {
        var user = uow.UserRepository.GetByFirebaseId(firebaseId.Trim());
        if (user != null) NotifyError("User is already exist!");
        var success = await mediatorHandler.SendCommand(new AddNewUserCommand(firebaseId.Trim()));
        if (!success) NotifyError("Error while saving to database!");
    }

    public async Task SetRoles(Guid userId, List<string> roles)
    {
        var user = uow.UserRepository.GetById(userId)!;
        var success = await mediatorHandler.SendCommand(new SetUserRolesCommand
        {
            UserId = user.UserId,
            Roles = roles
        });
        if (!success) NotifyError("Error while saving to database!");
    }

    public async Task EditProfile(Guid userId, string? name, IFormFile? avatar, IFormFile? banner)
    {
        var result = await mediatorHandler.SendCommand(new EditUserProfileCommand
        {
            UserId = userId,
            Name = name?.Trim()
        });
        cloudStorage.UserStorage.UploadUserImages(userId, avatar, banner);
    }

    public async Task AddDevice(Guid userId, string fcmToken)
    {
        if (!uow.UserDeviceRepository.IsExist(userId, fcmToken))
        {
            var result = await mediatorHandler.SendCommand(new RegisterFcmCommand
            {
                UserId = userId,
                Token = fcmToken
            });
            if (!result) NotifyError("Error while saving to database!");
        }
    }

    public async Task RemoveDevice(Guid userId, string fcmToken)
    {
        if (uow.UserDeviceRepository.IsExist(userId, fcmToken))
        {
            var result = await mediatorHandler.SendCommand(new UnregisterFcmCommand
            {
                UserId = userId,
                Token = fcmToken
            });
            if (!result) NotifyError("Error while saving to database!");
        }
    }

    private UserDto MapToDto(User user)
    {
        var dto = mapper.Map<UserDto>(user);
        dto.Roles = user.Roles.Select(r => r.Type).ToList();
        dto.Avatar = cloudStorage.UserStorage.GetUserAvatarUrl(user.UserId);
        dto.Banner = cloudStorage.UserStorage.GetUserBannerUrl(user.UserId);
        return dto;
    }
}