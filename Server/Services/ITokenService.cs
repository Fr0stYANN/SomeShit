using Server.GraphQl;
using System.Security.Claims;

namespace Server.Services
{
    public interface ITokenService
    {
        TokenGenerationResponse GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
