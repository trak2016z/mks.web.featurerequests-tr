using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.FeatureRequests.Model
{
    /// <summary>
    /// Various application constants.
    /// Never use magic strings.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Global user roles from auth service.
        /// TODO: move to common git module
        /// </summary>
        public static class RoleNames
        {
            /// <summary>
            /// Normal user
            /// </summary>
            public const string User = nameof(User);

            /// <summary>
            /// Administrator
            /// </summary>
            public const string Administrator = nameof(Administrator);


            /// <summary>
            /// Global service administrator.
            /// </summary>
            public const string Superadministrator = nameof(Superadministrator);
        }
    }
}
