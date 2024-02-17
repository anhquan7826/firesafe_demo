using Application.DTOs;
using Firesafe.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Interface;

public interface IUserService
{
    public UserDto GetUserFromFirebaseId(string firebaseId);

    public Task AddUser(string firebaseId);

    public Task SetRoles(Guid userId, List<string> roles);
    
    public Task EditProfile(Guid userId, string? name, IFormFile? avatar, IFormFile? banner);

    public Task AddDevice(Guid userId, string fcmToken);
    
    public Task RemoveDevice(Guid userId, string fcmToken);
}