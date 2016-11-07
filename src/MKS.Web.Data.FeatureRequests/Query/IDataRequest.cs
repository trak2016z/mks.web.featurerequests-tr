using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MKS.Web.Data.FeatureRequests.Query
{
    public interface IDataRequest<T>
    {
        int PageSize { get; set; }
        int PageIndex { get; set; }
        Expression<Func<T, object>> OrderBy { get; set; }
        SortDirection Direction { get; set; }
        Expression<Func<T, bool>> Where { get; set; }
    }
}
