using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MKS.Web.FeatureRequests.Model.Components.DataGrid
{
    public class DataGridColumn
    {
        public string Name { get; set; }
        public string PropertyName { get; set; }
        public string StaticValue { get; set; }
        public bool IsSortable { get; set; }
    }
}
