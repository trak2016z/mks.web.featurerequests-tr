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
using MKS.Web.Common.Extensions;

namespace MKS.Web.FeatureRequests.Controllers.API
{
    [Authorize]
    [Route("api/featurerequests")]
    public class FeatureRequestsApiController : BaseController
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
            return base.Ok(Mapper.Map<List<Model.FeatureRequest>>(entities));
        }


        /// <summary>
        /// POST: /api/featurerequests/10
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{projectId}")]
        public IActionResult AddRequest(long projectId, [FromBody] Model.FeatureRequest model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var entity = Mapper.Map<Data.FeatureRequests.Model.FeatureRequest>(model);
                    entity.CreatedById = IdentityHelper.GetUserId(User);
                    _featureRequests.Add(projectId, entity);
                    return Ok();
                }
                catch (BusinessException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                    return BadRequestValidationErrors();
                }
            }
            else
            {
                return BadRequestValidationErrors();
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
