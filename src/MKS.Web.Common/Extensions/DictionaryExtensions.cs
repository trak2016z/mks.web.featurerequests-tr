using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.Common.Extensions
{
    public static class DictionaryExtensions
    {
        private static CamelCasePropertyNamesContractResolver _newtonsoftCamelCaseResolver = new CamelCasePropertyNamesContractResolver();

        /// <summary>
        /// Transforms the dictionary so it has camel cased keys.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static Dictionary<string, TValue> ToCamelCasedKeys<TValue>(this Dictionary<string, TValue> dict)
        {
            return dict.ToDictionary(kv => _newtonsoftCamelCaseResolver.GetResolvedPropertyName(kv.Key), kv => kv.Value);
        }
    }
}
