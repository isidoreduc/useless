using System.Linq;
using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string ReturnEmailFromPrincipal(this ClaimsPrincipal user) => 
            user?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;

    }
}