using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MKS.Web.FeatureRequests.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.FeatureRequests.Controllers.MVC
{
    /// <summary>
    /// Service testing and config.
    /// </summary>
    [Authorize(Roles = Constants.RoleNames.Superadministrator)]
    public class ConfigController : Controller
    {
        public IActionResult Claims()
        {
            return Ok(User.Claims);
        }
    }
}
