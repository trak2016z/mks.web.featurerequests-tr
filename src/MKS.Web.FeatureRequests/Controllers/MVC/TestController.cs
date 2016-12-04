using Microsoft.AspNetCore.Mvc;
using MKS.Web.FeatureRequests.Model.MVC.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.FeatureRequests.Controllers.MVC
{
    /// <summary>
    /// Simple UI-test controller
    /// </summary>
    public class TestController : Controller
    {
        public IActionResult DataGrid()
        {
            var data = new List<TestListItem>()
            {
                new TestListItem() { Name = "name 1", Description = "desc 1", Date = DateTime.Now.AddDays(1) },
                new TestListItem() { Name = "name 2", Description = "desc 2", Date = DateTime.Now.AddDays(2) },
                new TestListItem() { Name = "name 3", Description = "desc 3", Date = DateTime.Now.AddDays(3) }
            };

            return View(data);
        }
    }
}
