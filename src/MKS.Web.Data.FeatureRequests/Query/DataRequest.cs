using MKS.Web.Data.FeatureRequests.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using MKS.Web.Data.FeatureRequests.Model.Interfaces;

namespace MKS.Web.FeatureRequests.Model.DataRequest
{
    /// <summary>
    /// Default data request for lists.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataRequest<T> : IDataRequest<T> 
    {
        public SortDirection Direction { get; set; } = SortDirection.ASC;

        public Expression<Func<T, object>> OrderBy { get; set; } = null;

        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public Expression<Func<T, bool>> Where { get; set; } = null;
    }
}
