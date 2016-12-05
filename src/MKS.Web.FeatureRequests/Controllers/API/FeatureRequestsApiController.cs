using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MKS.Web.Common;
using MKS.Web.Data.FeatureRequests.Exception;
using MKS.Web.Data.FeatureRequests.Model;
using MKS.Web.Data.FeatureRequests.Repository;
using MKS.Web.FeatureRequests.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.FeatureRequests.Controllers.API
{
    [Authorize]
    [Route("api/featurerequests")]
    public class FeatureRequestsApiController : Controller
    {
        private readonly FeatureRequestsRepository _featureRequests;

        public FeatureRequestsApiController(FeatureRequestsRepository featureRequests)
        {
            _featureRequests = featureRequests;
        }

        /// <summary>
        /// GET: /api/featurerequests/10
        /// </summary>
        [Route("{projectId}")]
        public IActionResult GetByProject(long projectId)
        {
            var entities = _featureRequests.GetByProjectId(projectId);
            return Ok(Mapper.Map<List<FeatureRequestDTO>>(entities));
        }


        /// <summary>
        /// POST: /api/featurerequests/10
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{projectId}")]
        public IActionResult AddRequest(long projectId, [FromBody]FeatureRequestDTO model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var entity = Mapper.Map<FeatureRequest>(model);
                    entity.CreatedById = IdentityHelper.GetUserId(User);
                    _featureRequests.Add(projectId, entity);
                    return Ok();
                }
                catch (BusinessException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                    return Ok(ModelState);
                }
            }
            else
            {
                return Ok(ModelState);
            }
        }

        [HttpPost]
        [Route("isnameavailable")]
        public IActionResult IsNameAvailable([FromBody]string name)
        {
            return Ok(_featureRequests.IsNameAvailable(name));
        }
    }
}
