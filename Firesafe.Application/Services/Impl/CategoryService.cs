using Application.Services.Interface;
using Firesafe.Domain.UnitOfWork;

namespace Application.Services.Impl;

public class CategoryService(IUnitOfWork uow) : BaseService, ICategoryService
{
    public IEnumerable<string> GetProductCategories()
    {
        var categories = uow.ProductCategoryRepository.GetAll();
        return categories.Select(c => c.CategoryId);
    }

    public bool IsProductCategoryExist(string category)
    {
        return uow.ProductCategoryRepository.IsExist(category);
    }

    public List<string> GetNewspaperCategories()
    {
        return uow.NewspaperCategoryRepository.GetAll().Select(nc => nc.NewspaperCategoryId).ToList();
    }

    public bool IsNewspaperCategoryExist(string category)
    {
        return uow.NewspaperCategoryRepository.IsExist(category);
    }
}