using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json.Linq;
using System.Dynamic;

namespace MKS.Web.Common.Extensions
{
    public static class ModelStateDictionaryExtensions
    {
        /// <summary>
        /// Transforms ModelState to a simple dictionary of property errors which can be sent as json to the client.
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns>Dict: property => [errorMsg1, errorMsg2...]</returns>
        public static Dictionary<string, List<string>> GetValidationErrors(this ModelStateDictionary modelState)
        {
            return modelState
                .Where(kv => kv.Value.ValidationState == ModelValidationState.Invalid)
                .ToDictionary(kv => kv.Key, kv => kv.Value.Errors.Select(e => e.ErrorMessage).ToList());
        }
    }
}
