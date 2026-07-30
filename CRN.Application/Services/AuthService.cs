using CRN.Application.Authentication;
using CRN.Application.DTOs;
using CRN.Domain.Entities;
using CRN.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CRN.Application.Services;

public class AuthService
{
    private readonly IJwtTokenService _jwtTokenService;
    private readonly ApplicationDbContext _context;

    public AuthService(
        IJwtTokenService jwtTokenService,
        ApplicationDbContext context)
    {
        _jwtTokenService = jwtTokenService;
        _context = context;
    }


    // Register
    public string Register(RegisterRequestDto request)
    {
        var user = new User
        {
            UserName = request.Name,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            CreatedOn = DateTime.UtcNow
        };

        _context.Users.Add(user);

        _context.SaveChanges();

        return "User Registered Successfully";
    }


    // Login
    public AuthResponseDto Login(LoginRequestDto request)
    {
        var dbUser = _context.Users
            .FirstOrDefault(x =>
                x.Email.ToLower() == request.Email.ToLower());


        if (dbUser == null)
        {
            return new AuthResponseDto
            {
                Token = "",
                RefreshToken = ""
            };
        }


        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(
            request.Password,
            dbUser.PasswordHash);


        if (!isPasswordValid)
        {
            return new AuthResponseDto
            {
                Token = "",
                RefreshToken = ""
            };
        }


        var token = _jwtTokenService.GenerateToken(dbUser);

        var refreshToken = _jwtTokenService.GenerateRefreshToken();


        var refreshTokenEntity = new RefreshToken
        {
            Token = refreshToken,
            ExpiryDate = DateTime.UtcNow.AddDays(7),
            CreatedDate = DateTime.UtcNow,
            IsRevoked = false,
            UserId = dbUser.Id
        };


        _context.RefreshTokens.Add(refreshTokenEntity);

        _context.SaveChanges();


        return new AuthResponseDto
        {
            Token = token,
            RefreshToken = refreshToken
        };
    }


    // Refresh Token
    public AuthResponseDto RefreshToken(RefreshTokenRequest request)
    {
        var token = _context.RefreshTokens
            .FirstOrDefault(x => x.Token == request.RefreshToken);


        if (token == null)
        {
            return new AuthResponseDto
            {
                Token = "",
                RefreshToken = ""
            };
        }


        if (token.IsRevoked)
        {
            return new AuthResponseDto
            {
                Token = "",
                RefreshToken = ""
            };
        }


        if (token.ExpiryDate < DateTime.UtcNow)
        {
            return new AuthResponseDto
            {
                Token = "",
                RefreshToken = ""
            };
        }


        var user = _context.Users
            .FirstOrDefault(x => x.Id == token.UserId);


        if (user == null)
        {
            return new AuthResponseDto
            {
                Token = "",
                RefreshToken = ""
            };
        }


        var newAccessToken = _jwtTokenService.GenerateToken(user);

        var newRefreshToken = _jwtTokenService.GenerateRefreshToken();


        token.IsRevoked = true;


        var refreshTokenEntity = new RefreshToken
        {
            Token = newRefreshToken,
            UserId = user.Id,
            CreatedDate = DateTime.UtcNow,
            ExpiryDate = DateTime.UtcNow.AddDays(7),
            IsRevoked = false
        };


        _context.RefreshTokens.Add(refreshTokenEntity);

        _context.SaveChanges();


        return new AuthResponseDto
        {
            Token = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }


    // Logout
    public string Logout(RefreshTokenRequest request)
    {
        var token = _context.RefreshTokens
            .FirstOrDefault(x => x.Token == request.RefreshToken);


        if (token == null)
        {
            return "Refresh Token Not Found";
        }


        token.IsRevoked = true;

        _context.SaveChanges();


        return "Logout Successfully";
    }
}