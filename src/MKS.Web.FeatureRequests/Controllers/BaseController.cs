using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MKS.Web.Common.Extensions;

namespace MKS.Web.FeatureRequests.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// Returns validation errors as json object:
        /// { property: [error1,error2,...] }
        /// </summary>
        /// <returns></returns>
        protected IActionResult BadRequestValidationErrors()
        {
            return BadRequest(ModelState.GetValidationErrors()
                .ToCamelCasedKeys());
        }
    }
}
