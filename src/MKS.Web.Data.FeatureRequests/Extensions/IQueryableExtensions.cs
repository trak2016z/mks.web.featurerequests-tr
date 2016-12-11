using MKS.Web.Data.FeatureRequests.Model.Interfaces;
using MKS.Web.Data.FeatureRequests.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.Data.FeatureRequests.Extensions
{
    internal static class IQueryableExtensions
    {
        /// <summary>
        /// Further filter query by client specified request.
        /// </summary>
        /// <param name="q"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static IQueryable<T> FilterByDataRequest<T>(this IQueryable<T> q, IDataRequest<T> request) 
        {
            if (request.Where != null)
                q = q.Where(request.Where);

            if (request.OrderBy != null)
            {
                if (request.Direction == SortDirection.ASC)
                    q = q.OrderBy(request.OrderBy);
                else
                    q = q.OrderByDescending(request.OrderBy);
            }

            return q.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize);
        }
    }
}
