using Microsoft.AspNetCore.Mvc;
using MKS.Web.Data.FeatureRequests.Model.Interfaces;
using MKS.Web.Data.FeatureRequests.Query;
using MKS.Web.Data.FeatureRequests.Repository;
using MKS.Web.Data.FeatureRequests.Repository.Interfaces;
using MKS.Web.FeatureRequests.Model.Common;
using MKS.Web.FeatureRequests.Model.Components;
using MKS.Web.FeatureRequests.Model.Components.DataGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.FeatureRequests.Components
{
    /*
        Component.Invoke(nameof(DataGrid), new DataGridOptions()
        {
            Id = "demoGrid",
            PageIndex = 1,
            PageSize = 15,
            ItemType = typeof(Project),
            GridType = GridTypes.Table, //by default, possible options (flex / table / list)
            Columns = (new ColumnBuilder<Project>())
                .Property(p => p.Name)
                .Sortable()
                .CssClass("bold")
                .Link(p => Url.Action("View", new { id = p.Id }))
        })    
    */

    //DO THIS
    /*
        Component.Invoke(
            nameof(DataGrid), 
            (new DataGridBuilder<Project>())
                .Id("demoGrid")
                .PageIndex(1),
                .PageSize(15),
                .Columns(c =>
                    c.For(m => m.Name).Sortable().Format(name => string.Format(...))).CssClass("gówno")
                    c.Custom(m => string.Format(m.Name + "(" + m.Description...))...
                    c.Custom("TODO"))
                ...
        
        ); 
    */

    /*
        cshtml:
        <table><thead>
        @foreach(c in Model.Columns)
         {
         <th>
            if c.IsSorthable fancy arrows
            else normal c.Name
            </th>
        }
        </thead><tbody>
        @foreach(item in Model.Items)
        {
            foreach(c in Model.Columns)
            {
                <td>c.GetValue(item)</td>
            }
        }
        </tbody>
        <tfoot>
            @pager taking into account Model.PageIndex/PageSize/TotalCount
        </tfoot>

        <script>
            knockout scripts async downloading pages from repository
            - get req to Model.Source - action which takes IDataRequest<TItem> as a parameter
                and returns List<TItem> as json
            - we assume totalcount doesn't change, but what if it does?
                we simply return empty list and do model.totalcount - pagesize ish
        </script>
    */

    public class DataGrid : ViewComponent
    {
        public DataGrid()
        {
        }
        

        public IViewComponentResult Invoke(DataGridModel model)
        {
            return View(model);
        }
    }
}
