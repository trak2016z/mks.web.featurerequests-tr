using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.Data.FeatureRequests.Model
{
    /// <summary>
    /// "Clone" from the user service.
    /// Provides a single place for cascade deletions in case
    /// of user unregistering from the site/app and some room for "caching" in the future
    /// </summary>
    public class User
    {
        public string Id { get; set; }
    }
}
