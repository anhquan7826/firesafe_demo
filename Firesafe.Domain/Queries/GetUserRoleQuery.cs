using Firesafe.Domain.Core.Query;

namespace Firesafe.Domain.Queries;

public class GetUserRoleQuery : Query<string>
{
    public Guid UserId { get; set; }
}