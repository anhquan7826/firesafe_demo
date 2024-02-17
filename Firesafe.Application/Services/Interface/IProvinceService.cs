using Application.DTOs;

namespace Application.Services.Interface;

public interface IProvinceService
{
    public List<ProvinceDto> GetAll();

    public ProvinceDto? Get(int id);
}