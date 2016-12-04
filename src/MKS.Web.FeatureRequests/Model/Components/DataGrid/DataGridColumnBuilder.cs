using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MKS.Web.FeatureRequests.Model.Components.DataGrid
{
    public class DataGridColumnBuilder<TItem> where TItem : class, new()
    {
        #region Private fields
        //column value variants
        private Func<object, object> _property;
        private Func<object, string> _custom;
        private string _value;

        private bool _isSortable;
        private Func<string, string> _formatFunc;
        private string _itemCssClass;
        #endregion

        #region Constructors
        public DataGridColumnBuilder(Func<TItem, object> propertySelector)
        {
            _property = i => propertySelector((TItem)i);
        }

        public DataGridColumnBuilder(Func<TItem, string> contentGetter)
        {
            _custom = i => contentGetter((TItem)i);
        }

        public DataGridColumnBuilder(string staticValue)
        {
            _value = staticValue;
        }
        #endregion

        #region Public methods
        public DataGridColumnBuilder<TItem> Sortable()
        {
            _isSortable = true;
            return this;
        }

        public DataGridColumnBuilder<TItem> Format(Func<string, string> format)
        {
            _formatFunc = format;
            return this;
        }

        public DataGridColumnBuilder<TItem> CssClass(string cssClass)
        {
            _itemCssClass = cssClass;
            return this;
        }

        /// <summary>
        /// Usually useful only for another builder.
        /// </summary>
        /// <returns>column model</returns>
        public DataGridColumn Build()
        {
            return new DataGridColumn()
            {
                PropertyGetter = _property,
                CustomGetter = _custom,
                StaticValue = _value,
                Format = _formatFunc,
                IsSortable = _isSortable
            };
        }
        #endregion

        #region Private helper methods

        #endregion
    }
}
