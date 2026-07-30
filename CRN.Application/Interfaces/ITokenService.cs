using CRN.Application.Authentication;

namespace CRN.Application.Interfaces
{
    public interface ITokenService
    {
        LoginResponse GenerateToken(string userName);
    }
}
