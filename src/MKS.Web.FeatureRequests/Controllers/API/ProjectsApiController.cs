using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MKS.Web.Data.FeatureRequests.Repository;
using MKS.Web.Data.FeatureRequests.Model;
using Microsoft.AspNetCore.Authorization;
using MKS.Web.FeatureRequests.Model.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MKS.Web.FeatureRequests.Controllers
{
    /// <summary>
    /// Manages projects.
    /// </summary>
    [Authorize]
    [Route("api/projects")]
    public class ProjectsApiController : Controller
    {
        private readonly ProjectsRepository _projects;
        private readonly IMapper _mapper;

        public ProjectsApiController(ProjectsRepository projects, IMapper mapper)
        {
            _projects = projects;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all projects.
        /// </summary>
        [HttpGet]
        public IEnumerable<ProjectDTO> Get()
        {
            return _mapper.Map<List<ProjectDTO>>(_projects.GetList());
        }

        /// <summary>
        /// Get a single project by id.
        /// </summary>
        [HttpGet("{id}")]
        [Produces(typeof(ProjectDTO))]
        public IActionResult Get(long id)
        {
            var p = _projects.GetById(id);

            if (p != null)
                return Ok(_mapper.Map<ProjectDTO>(p));
            else
                return NotFound();
        }

        /// <summary>
        /// Add a new project.
        /// </summary>
        [HttpPost]
        [Produces(typeof(ModelStateDictionary))]
        public IActionResult Post([FromBody]ProjectDTO model)
        {
            if(ModelState.IsValid)
            {
                _projects.Add(_mapper.Map<Project>(model));
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Update a project.
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody]ProjectDTO model)
        {
            if (ModelState.IsValid)
            {
                model.Id = id;
                _projects.Update(_mapper.Map<Project>(model));
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Delete a project.
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _projects.Delete(id);
            return Ok();
        }
    }
}
