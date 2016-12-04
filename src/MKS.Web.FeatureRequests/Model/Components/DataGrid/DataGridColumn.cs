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
        public Func<object, object> PropertyGetter { get; set; } //we don't need type checking here
        public Func<object, string> CustomGetter { get; set; }
        public string StaticValue { get; set; }

        public bool IsSortable { get; set; }
        public Func<string, string> Format { get; set; }

        /// <summary>
        /// Get cell value for a single row.
        /// </summary>
        /// <param name="model">Item for the row.</param>
        /// <returns>Html to embed raw</returns>
        public HtmlString GetValue<TItem>(TItem model)
        {
            if (PropertyGetter != null)
                return new HtmlString(PropertyGetter(model).ToString());
            else if (CustomGetter != null)
                return new HtmlString(CustomGetter(model));
            else if (StaticValue != null)
                return new HtmlString(StaticValue);

            return null;
        }
    }
}
