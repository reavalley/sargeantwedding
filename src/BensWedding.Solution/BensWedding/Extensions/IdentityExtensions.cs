using System.Security.Claims;
using System.Security.Principal;

namespace BensWedding.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetDisplayName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("DisplayName");
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}