using MKS.Web.Common;
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
        private string _propertyName;
        private string _value;

        private bool _isSortable;
        private Func<string, string> _formatFunc;
        private string _itemCssClass;
        private string _name;
        #endregion

        #region Constructors
        public DataGridColumnBuilder(Expression<Func<TItem, object>> propertySelector)
        {
            _propertyName = ExpressionHelper.GetMemberName<TItem, object>(propertySelector);
        }

        public DataGridColumnBuilder(Func<TItem, string> contentGetter)
        {
            //TODO: pass js function instead of lambda
            throw new NotImplementedException();
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
            //TODO: pass js format function
            throw new NotImplementedException();
        }

        public DataGridColumnBuilder<TItem> CssClass(string cssClass)
        {
            _itemCssClass = cssClass;
            return this;
        }

        public DataGridColumnBuilder<TItem> Name(string name)
        {
            _name = name;
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
                Name = _name,
                PropertyName = _propertyName,
                StaticValue = _value,
                IsSortable = _isSortable
            };
        }
        #endregion

        #region Private helper methods

        #endregion
    }
}
