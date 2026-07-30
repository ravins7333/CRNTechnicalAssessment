using CRN.Domain.Entities;

namespace CRN.Application.Authentication;

public interface IJwtTokenService
{
    string GenerateToken(User user);

    string GenerateRefreshToken();
}