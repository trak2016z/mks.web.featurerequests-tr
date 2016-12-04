using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MKS.Web.FeatureRequests.Model.Components.DataGrid
{
    public class DataGridColumnsBuilder<TItem> where TItem : class, new()
    {
        private List<DataGridColumnBuilder<TItem>> columnBuilders = new List<DataGridColumnBuilder<TItem>>();

        /// <summary>
        /// Configure a column for model property.
        /// </summary>
        /// <param name="propertySelector"></param>
        /// <returns></returns>
        public DataGridColumnBuilder<TItem> For(Expression<Func<TItem, object>> propertySelector)
        {
            var builder = new DataGridColumnBuilder<TItem>(propertySelector);
            columnBuilders.Add(builder);
            return builder;
        }

        /// <summary>
        /// Configure a column which uses a custom function to produce output.
        /// </summary>
        /// <param name="contentGetter"></param>
        /// <returns></returns>
        public DataGridColumnBuilder<TItem> Custom(Func<TItem, string> contentGetter)
        {
            var builder = new DataGridColumnBuilder<TItem>(contentGetter);
            columnBuilders.Add(builder);
            return builder;
        }

        /// <summary>
        /// Configure a column with static content.
        /// </summary>
        /// <param name="staticValue"></param>
        /// <returns></returns>
        public DataGridColumnBuilder<TItem> Custom(string staticValue)
        {
            var builder = new DataGridColumnBuilder<TItem>(staticValue);
            columnBuilders.Add(builder);
            return builder;
        }

        /// <summary>
        /// Usually useful only for another builder.
        /// </summary>
        /// <returns>column models in order which should apper on the UI</returns>
        public List<DataGridColumn> Build()
        {
            return columnBuilders.Select(b => b.Build()).ToList();
        }
    }
}
