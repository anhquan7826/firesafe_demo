using Application.DTOs;

namespace Application.Services.Interface;

public interface ICountryService
{
    public IEnumerable<CountryDto> GetAllCountries();

    public bool IsExist(string id);
}