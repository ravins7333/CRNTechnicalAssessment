using AutoMapper;
using CRN.Application.DTOs;
using CRN.Domain.Entities;

namespace CRN.Application.Mapping;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        // Create DTO -> Entity
        CreateMap<ProductCreateDto, Product>();

        // Update DTO -> Entity
        CreateMap<ProductUpdateDto, Product>();

        // Entity -> Response DTO
        CreateMap<Product, ProductResponseDto>();
    }
}