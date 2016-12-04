using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace MKS.Web.Common
{
    /// <summary>
    /// Utilities for user's identity in the application.
    /// </summary>
    public static class IdentityHelper
    {
        /// <summary>
        /// Extract user id from claims.
        /// </summary>
        /// <param name="principal">principal e.g. HttpContext.User available as just User in the controllers' actions</param>
        /// <returns></returns>
        public static string GetUserId(this IPrincipal principal)
        {
            var claimsIdentity = (ClaimsIdentity)principal.Identity;
            var claim = claimsIdentity.FindFirst("sub");
            return claim.Value;
        }
    }
}
