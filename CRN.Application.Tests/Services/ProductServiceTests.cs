using CRN.Application.DTOs;
using CRN.Application.Services;
using CRN.Infrastructure.Data;
using CRN.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CRN.Application.Tests.Services;

public class ProductServiceTests
{
    private ProductService GetService()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new ApplicationDbContext(options);

        var unitOfWork = new UnitOfWork(context);

        return new ProductService(unitOfWork);
    }


    [Fact]
    public async Task Create_Product_Should_Return_Product()
    {
        var service = GetService();

        var dto = new ProductCreateDto
        {
            ProductName = "Laptop",
            CreatedBy = "Ravin"
        };

        var result = await service.CreateAsync(dto);

        Assert.NotNull(result);
        Assert.Equal("Laptop", result.ProductName);
        Assert.Equal("Ravin", result.CreatedBy);
    }



    [Fact]
    public async Task GetAll_Products_Should_Return_List()
    {
        var service = GetService();

        await service.CreateAsync(new ProductCreateDto
        {
            ProductName = "Mobile",
            CreatedBy = "Ravin"
        });


        await service.CreateAsync(new ProductCreateDto
        {
            ProductName = "Laptop",
            CreatedBy = "Ravin"
        });


        var result = await service.GetAllAsync();


        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }



    [Fact]
    public async Task GetById_Product_Should_Return_Product()
    {
        var service = GetService();

        var createdProduct = await service.CreateAsync(new ProductCreateDto
        {
            ProductName = "Keyboard",
            CreatedBy = "Ravin"
        });


        var result = await service.GetByIdAsync(createdProduct.Id);


        Assert.NotNull(result);
        Assert.Equal("Keyboard", result.ProductName);
    }



    [Fact]
    public async Task Update_Product_Should_Return_True()
    {
        var service = GetService();

        var createdProduct = await service.CreateAsync(new ProductCreateDto
        {
            ProductName = "Mouse",
            CreatedBy = "Ravin"
        });


        var updateDto = new ProductUpdateDto
        {
            ProductName = "Wireless Mouse",
            ModifiedBy = "Ravin"
        };


        var result = await service.UpdateAsync(
            createdProduct.Id,
            updateDto
        );


        Assert.True(result);


        var updatedProduct = await service.GetByIdAsync(createdProduct.Id);

        Assert.NotNull(updatedProduct);
        Assert.Equal("Wireless Mouse", updatedProduct.ProductName);
        Assert.Equal("Ravin", updatedProduct.ModifiedBy);
    }



    [Fact]
    public async Task Delete_Product_Should_Return_True()
    {
        // Arrange
        var service = GetService();

        var createdProduct = await service.CreateAsync(new ProductCreateDto
        {
            ProductName = "Printer",
            CreatedBy = "Ravin"
        });


        // Act
        var result = await service.DeleteAsync(createdProduct.Id);


        // Assert
        Assert.True(result);


        var deletedProduct = await service.GetByIdAsync(createdProduct.Id);

        Assert.Null(deletedProduct);
    }
}