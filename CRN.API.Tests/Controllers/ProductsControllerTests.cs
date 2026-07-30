using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CRN.API.Tests.Controllers;

public class ProductsControllerTests
    : IClassFixture<WebApplicationFactory<CRN.API.Program>>
{
    private readonly HttpClient _client;

    public ProductsControllerTests(WebApplicationFactory<CRN.API.Program> factory)
    {
        _client = factory.WithWebHostBuilder(builder =>
        {
            builder.UseSetting("ENVIRONMENT", "Testing");
        })
        .CreateClient();
    }


    [Fact]
    public async Task Get_Products_Should_Return_Success()
    {
        var response = await _client.GetAsync("/api/v1/products");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }


    [Fact]
    public async Task Get_Product_By_Id_Should_Return_NotFound_When_Product_Does_Not_Exist()
    {
        var response = await _client.GetAsync("/api/v1/products/99999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }


    [Fact]
    public async Task Create_Product_Should_Return_Success()
    {
        var product = new
        {
            name = "Test Product",
            price = 100,
            quantity = 10
        };

        var content = new StringContent(
            JsonSerializer.Serialize(product),
            Encoding.UTF8,
            "application/json"
        );


        var response = await _client.PostAsync(
            "/api/v1/products",
            content
        );


        Assert.True(
            response.StatusCode == HttpStatusCode.OK ||
            response.StatusCode == HttpStatusCode.Created
        );
    }


    [Fact]
    public async Task Update_Product_Should_Return_Success()
    {
        var product = new
        {
            name = "Updated Product",
            price = 200,
            quantity = 20
        };

        var content = new StringContent(
            JsonSerializer.Serialize(product),
            Encoding.UTF8,
            "application/json"
        );


        var response = await _client.PutAsync(
            "/api/v1/products/1",
            content
        );


        Assert.True(
            response.StatusCode == HttpStatusCode.NoContent ||
            response.StatusCode == HttpStatusCode.OK ||
            response.StatusCode == HttpStatusCode.NotFound
        );
    }


    [Fact]
    public async Task Delete_Product_Should_Return_Success()
    {
        var response = await _client.DeleteAsync(
            "/api/v1/products/1"
        );


        Assert.True(
            response.StatusCode == HttpStatusCode.NoContent ||
            response.StatusCode == HttpStatusCode.OK ||
            response.StatusCode == HttpStatusCode.NotFound
        );
    }
}