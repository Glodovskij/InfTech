using AutoMapper;
using InfTech.Services.CatalogApi.Domain.DTOs;
using InfTech.Services.CatalogApi.Domain.Entities;

namespace InfTech.Services.CatalogApi.Infrastructure.Configuration
{
    public class CatalogMapperProfile : Profile
    {
        public CatalogMapperProfile()
        {
            CreateMap<ProductDto, Product>().ReverseMap();
        }
    }
}
