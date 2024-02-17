using Firesafe.Domain.Entities;
using Firesafe.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserDeviceRepository(DbContext dbContext) : Repository<UserDevice>(dbContext), IUserDeviceRepository
{
    public bool IsExist(Guid userId, string token)
    {
        return DbSet.FirstOrDefault(ud => ud.UserId == userId && ud.FcmToken == token) != null;
    }

    public void AddToken(Guid userId, string token)
    {
        DbSet.Add(new UserDevice
        {
            UserId = userId,
            FcmToken = token
        });
    }

    public void RemoveToken(Guid userId, string token)
    {
        var entry = DbSet.FirstOrDefault(ud => ud.UserId == userId && ud.FcmToken == token);
        if (entry != null) DbSet.Remove(entry);
    }
}