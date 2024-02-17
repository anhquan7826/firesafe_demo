namespace Application.Services.Interface;

public interface ICategoryService
{
    public IEnumerable<string> GetProductCategories();

    public bool IsProductCategoryExist(string category);

    public List<string> GetNewspaperCategories();

    public bool IsNewspaperCategoryExist(string category);
}