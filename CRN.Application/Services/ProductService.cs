using CRN.Application.DTOs;
using CRN.Application.Interfaces;
using CRN.Domain.Entities;
using CRN.Infrastructure.Repositories;

namespace CRN.Application.Services;

public class ProductService : IProductService
{
    private readonly UnitOfWork _unitOfWork;

    public ProductService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAllAsync()
    {
        var products = await _unitOfWork.Products.GetAllAsync();

        return products.Select(x => new ProductResponseDto
        {
            Id = x.Id,
            ProductName = x.ProductName,
            CreatedBy = x.CreatedBy,
            CreatedOn = x.CreatedOn,
            ModifiedBy = x.ModifiedBy,
            ModifiedOn = x.ModifiedOn
        });
    }

    public async Task<ProductResponseDto?> GetByIdAsync(int id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);

        if (product == null)
            return null;

        return new ProductResponseDto
        {
            Id = product.Id,
            ProductName = product.ProductName,
            CreatedBy = product.CreatedBy,
            CreatedOn = product.CreatedOn,
            ModifiedBy = product.ModifiedBy,
            ModifiedOn = product.ModifiedOn
        };
    }

    public async Task<ProductResponseDto> CreateAsync(ProductCreateDto dto)
    {
        var product = new Product
        {
            ProductName = dto.ProductName,
            CreatedBy = dto.CreatedBy,
            CreatedOn = DateTime.Now
        };

        await _unitOfWork.Products.AddAsync(product);
        await _unitOfWork.SaveAsync();

        return new ProductResponseDto
        {
            Id = product.Id,
            ProductName = product.ProductName,
            CreatedBy = product.CreatedBy,
            CreatedOn = product.CreatedOn
        };
    }

    public async Task<bool> UpdateAsync(int id, ProductUpdateDto dto)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);

        if (product == null)
            return false;

        product.ProductName = dto.ProductName;
        product.ModifiedBy = dto.ModifiedBy;
        product.ModifiedOn = DateTime.Now;

        _unitOfWork.Products.Update(product);
        await _unitOfWork.SaveAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);

        if (product == null)
            return false;

        _unitOfWork.Products.Delete(product);
        await _unitOfWork.SaveAsync();

        return true;
    }
}