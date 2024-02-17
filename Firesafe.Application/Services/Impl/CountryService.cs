using Application.DTOs;
using Application.Services.Interface;
using Firesafe.Domain.UnitOfWork;

namespace Application.Services.Impl;

public class CountryService(IUnitOfWork uow) : BaseService, ICountryService
{
    public IEnumerable<CountryDto> GetAllCountries()
    {
        var origins = uow.OriginRepository.GetAllSorted();
        return origins.Select(o => new CountryDto
        {
            Id = o.OriginId,
            Name = o.Name
        });
    }

    public bool IsExist(string id)
    {
        return uow.OriginRepository.Get(id.Trim()) != null;
    }
}