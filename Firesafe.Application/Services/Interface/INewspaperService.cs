using Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Interface;

public interface INewspaperService
{
    public bool IsExist(Guid id);

    public NewspaperDto GetNewspaper(Guid id);

    public Task AddNewspaper(string title, IFormFile thumbnail, IFormFile html, List<IFormFile> images, List<string> categories);

    public Task EditNewspaper(Guid id, string? title = null, IFormFile? thumbnail = null, IFormFile? html = null, List<IFormFile>? images = null);

    public Task DeleteNewspaper(Guid id);

    public List<NewspaperShortDto> GetAllNewspapers(int page, DateTime timestamp);
    
    public List<NewspaperShortDto> GetNewNewspapers(int page, DateTime timestamp);
    
    public List<NewspaperShortDto> GetNewspapersByCategory(int page, DateTime timestamp);
    
    public List<NewspaperShortDto> GetAllNewspapersByField(int page, DateTime timestamp);
}