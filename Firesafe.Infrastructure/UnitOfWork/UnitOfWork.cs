using Firesafe.Domain.Repositories;
using Firesafe.Domain.Repositories.EventStore;
using Firesafe.Domain.UnitOfWork;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Infrastructure.Repositories.EventStore;

namespace Infrastructure.UnitOfWork;

public class UnitOfWork(DatabaseContext dbContext, EventStoreContext eventStoreContext) : IUnitOfWork
{
    // Entities
    private IProductCategoryRepository? _categoryRepo;
    private IOriginRepository? _originRepo;
    private IProductImageRepository? _productImageRepo;
    private IProductRepository? _productRepo;
    private IRoleRepository? _roleRepo;
    private ISupplierRepository? _supplierRepo;
    private IUserRepository? _userRepo;
    private IUserRoleRepository? _userRoleRepo;
    private IProvinceRepository? _provinceRepo;
    private INewspaperRepository? _newspaperRepo;
    private INewspaperImageRepository? _newspaperImageRepo;
    private INewspaperCategoryRepository? _newspaperCategoryRepo;
    private IUserDeviceRepository? _userDeviceRepo;

    // Stored Events
    private IExceptionStoreRepository? _exceptionStoreRepo;

    // Entities
    public IUserRepository UserRepository
    {
        get { return _userRepo ??= new UserRepository(dbContext); }
    }

    public ISupplierRepository SupplierRepository
    {
        get { return _supplierRepo ??= new SupplierRepository(dbContext); }
    }

    public IRoleRepository RoleRepository
    {
        get { return _roleRepo ??= new RoleRepository(dbContext); }
    }

    public IProductRepository ProductRepository
    {
        get { return _productRepo ??= new ProductRepository(dbContext); }
    }

    public IProductImageRepository ProductImageRepository
    {
        get { return _productImageRepo ??= new ProductImageRepository(dbContext); }
    }

    public IProductCategoryRepository ProductCategoryRepository
    {
        get { return _categoryRepo ??= new ProductCategoryRepository(dbContext); }
    }

    public IOriginRepository OriginRepository
    {
        get { return _originRepo ??= new OriginRepository(dbContext); }
    }

    public IUserRoleRepository UserRoleRepository
    {
        get { return _userRoleRepo ??= new UserRoleRepository(dbContext); }
    }

    public IProvinceRepository ProvinceRepository
    {
        get { return _provinceRepo ??= new ProvinceRepository(dbContext); }
    }

    public INewspaperRepository NewspaperRepository
    {
        get { return _newspaperRepo ??= new NewspaperRepository(dbContext); }
    }

    public INewspaperImageRepository NewspaperImageRepository
    {
        get { return _newspaperImageRepo ??= new NewspaperImageRepository(dbContext); }
    }

    public INewspaperCategoryRepository NewspaperCategoryRepository
    {
        get { return _newspaperCategoryRepo ??= new NewspaperCategoryRepository(dbContext); }
    }

    public IUserDeviceRepository UserDeviceRepository
    {
        get { return _userDeviceRepo ??= new UserDeviceRepository(dbContext); }
    }

    // Stored Events
    public IExceptionStoreRepository ExceptionStoreRepository
    {
        get { return _exceptionStoreRepo ??= new ExceptionStoreRepository(eventStoreContext); }
    }

    public void Commit()
    {
        dbContext.SaveChanges();
        eventStoreContext.SaveChanges();
    }

    public async Task CommitAsync()
    {
        await dbContext.SaveChangesAsync();
        await eventStoreContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        dbContext.Dispose();
        GC.SuppressFinalize(this);
    }
}