using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MKS.Web.FeatureRequests.Model.MVC.Project;
using MKS.Web.Data.FeatureRequests.Repository;
using MKS.Web.Data.FeatureRequests.Query;
using MKS.Web.FeatureRequests.Model.DataRequest;
using MKS.Web.FeatureRequests.Model.Common;
using System.Security.Claims;
using MKS.Web.Common;
using MKS.Web.FeatureRequests.Model;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MKS.Web.FeatureRequests.Controllers.MVC
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly ProjectsRepository _projects;

        public ProjectsController(ProjectsRepository projects)
        {
            _projects = projects;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(List));
        }

        public IActionResult List(DataRequestModel query)
        {
            if (query == null) query = new DataRequestModel();

            var data = _projects.GetList(query.ToDataRequest<Data.FeatureRequests.Model.Project>())
                .Select(p => new Project(p))
                .ToList();
            return View(data);
        }

        #region Create()
        [Authorize(Roles = Constants.RoleNames.Superadministrator)]
        public IActionResult Create()
        {
            return View("Edit", new Project());
        }

        [HttpPost]
        [Authorize(Roles = Constants.RoleNames.Superadministrator)]
        public IActionResult Create(Project model)
        {
            if (ModelState.IsValid)
            {
                _projects.Add(new Data.FeatureRequests.Model.Project()
                {
                    Name = model.Name,
                    Description = model.Description,
                    CreatedById = IdentityHelper.GetUserId(User),
                    CreatedAtUtc = DateTime.Now.ToUniversalTime()
                });

                return RedirectToAction(nameof(List));
            }

            return View("Edit", model);
        }
        #endregion

        #region Edit()
        [Authorize(Roles = Constants.RoleNames.Superadministrator)]
        public IActionResult Edit(int id)
        {
            var model = _projects.GetById(id);
            if (model == null) return NotFound();

            return View(new Project(model));
        }

        [HttpPost]
        [Authorize(Roles = Constants.RoleNames.Superadministrator)]
        public IActionResult Edit(long id, Project model)
        {
            if (ModelState.IsValid)
            {
                _projects.Update(id, p =>
                {
                    p.Name = model.Name;
                    p.Description = model.Description;
                });
            }

            return View(model);
        }
        #endregion

        #region View()
        public IActionResult View(long id)
        {
            var project = _projects.GetById(id);
            if (project == null)
                return NotFound();

            return View(new Project(project));
        }
        #endregion
    }
}
