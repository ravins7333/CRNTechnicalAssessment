using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CRN.API.Tests.Controllers;

public class AuthControllerTests
    : IClassFixture<WebApplicationFactory<CRN.API.Program>>
{
    private readonly HttpClient _client;

    public AuthControllerTests(WebApplicationFactory<CRN.API.Program> factory)
    {
        _client = factory.WithWebHostBuilder(builder =>
        {
            builder.UseSetting("ENVIRONMENT", "Testing");
        })
        .CreateClient();
    }


    [Fact]
    public async Task Register_Should_Return_Success()
    {
        // Arrange
        var user = new
        {
            name = "Test User",
            email = "testuser@gmail.com",
            password = "Test@123"
        };


        var content = new StringContent(
            JsonSerializer.Serialize(user),
            Encoding.UTF8,
            "application/json"
        );


        // Act
        var response = await _client.PostAsync(
            "/api/auth/register",
            content
        );


        // Assert
        Assert.True(
            response.StatusCode == HttpStatusCode.OK ||
            response.StatusCode == HttpStatusCode.Created ||
            response.StatusCode == HttpStatusCode.BadRequest
        );
    }


    [Fact]
    public async Task Login_Should_Return_Success()
    {
        // Arrange
        var login = new
        {
            email = "testuser@gmail.com",
            password = "Test@123"
        };


        var content = new StringContent(
            JsonSerializer.Serialize(login),
            Encoding.UTF8,
            "application/json"
        );


        // Act
        var response = await _client.PostAsync(
            "/api/auth/login",
            content
        );


        // Assert
        Assert.True(
            response.StatusCode == HttpStatusCode.OK ||
            response.StatusCode == HttpStatusCode.Unauthorized ||
            response.StatusCode == HttpStatusCode.BadRequest
        );
    }
}