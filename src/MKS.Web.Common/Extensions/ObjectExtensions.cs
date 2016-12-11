using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MKS.Web.Common.Extensions
{
    public static class ObjectExtensions
    {
        //default json settings matching asp net core 1.1
        //default casing = camel case, not pascal case
        private static JsonSerializerSettings jsonSettings = new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public static HtmlString ToJson<TAny>(this TAny obj)
        {
            return new HtmlString(JsonConvert.SerializeObject(obj, jsonSettings));
        }
    }
}
