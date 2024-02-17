using Application.DTOs;
using Application.Services.Interface;
using AutoMapper;
using Firesafe.Domain.UnitOfWork;

namespace Application.Services.Impl;

public class ProvinceService(
    IMapper mapper,
    IUnitOfWork uow) : BaseService, IProvinceService
{
    public List<ProvinceDto> GetAll()
    {
        return uow.ProvinceRepository.GetAll().ToList().Select(mapper.Map<ProvinceDto>).ToList();
    }

    public ProvinceDto? Get(int id)
    {
        var province = uow.ProvinceRepository.GetById(id);
        return province == null ? null : mapper.Map<ProvinceDto>(province);
    }
}