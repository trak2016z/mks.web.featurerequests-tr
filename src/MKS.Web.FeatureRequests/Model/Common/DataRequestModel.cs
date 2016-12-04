using MKS.Web.Data.FeatureRequests.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using MKS.Web.Data.FeatureRequests.Model.Interfaces;
using MKS.Web.FeatureRequests.Model.DataRequest;
using MKS.Web.Common;

namespace MKS.Web.Data.FeatureRequests.Query
{
    /// <summary>
    /// Base data request model for lists.
    /// </summary>
    public class DataRequestModel
    {
        public SortDirection Direction { get; set; } = SortDirection.ASC;

        public string OrderBy { get; set; } = nameof(IEntity.Id);

        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 10;


        public Data.FeatureRequests.Query.IDataRequest<T> ToDataRequest<T>() where T: IEntity
        {
            var request = new DataRequest<T>()
            {
                OrderBy = ExpressionHelper.CreateMemberGetter<T>(OrderBy),
                PageIndex = PageIndex,
                PageSize = PageSize,
                Where = null
            };

            return request;
        }
    }
}
