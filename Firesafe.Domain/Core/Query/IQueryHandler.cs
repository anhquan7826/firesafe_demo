using MediatR;

namespace Firesafe.Domain.Core.Query;

public interface IQueryHandler<in T, TR> : IRequestHandler<T, TR>
    where T : Query<TR>
{
}