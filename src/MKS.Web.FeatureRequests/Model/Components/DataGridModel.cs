using MKS.Web.Data.FeatureRequests.Query;
using MKS.Web.FeatureRequests.Model.Components.DataGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.FeatureRequests.Model.Components
{
    public class DataGridModel
    {
        public string Id { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string OrderBy { get; set; }
        public SortDirection OrderDirection { get; set; }
        public List<object> Items { get; set; } //we don't really care about the type as long as it can be serialized to json
        public int TotalCount { get; set; }
        public string SourceUrl { get; set; }
        public List<DataGridColumn> Columns { get; set; }
    }
}
