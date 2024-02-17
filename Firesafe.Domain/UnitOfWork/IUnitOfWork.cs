using Firesafe.Domain.Repositories;
using Firesafe.Domain.Repositories.EventStore;

namespace Firesafe.Domain.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    // Entities
    public IUserRepository UserRepository { get; }

    public ISupplierRepository SupplierRepository { get; }

    public IRoleRepository RoleRepository { get; }

    public IProductRepository ProductRepository { get; }

    public IProductImageRepository ProductImageRepository { get; }

    public IProductCategoryRepository ProductCategoryRepository { get; }

    public IOriginRepository OriginRepository { get; }

    public IUserRoleRepository UserRoleRepository { get; }

    public IProvinceRepository ProvinceRepository { get; }

    public INewspaperRepository NewspaperRepository { get; }

    public INewspaperImageRepository NewspaperImageRepository { get; }

    public INewspaperCategoryRepository NewspaperCategoryRepository { get; }

    public IUserDeviceRepository UserDeviceRepository { get; }

    // Stored Events
    public IExceptionStoreRepository ExceptionStoreRepository { get; }

    public void Commit();

    public Task CommitAsync();
}