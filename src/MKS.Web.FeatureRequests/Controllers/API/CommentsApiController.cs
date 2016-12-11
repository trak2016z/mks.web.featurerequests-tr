using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MKS.Web.Data.FeatureRequests.Model;
using MKS.Web.Data.FeatureRequests.Model.View;
using MKS.Web.Data.FeatureRequests.Query;
using MKS.Web.Data.FeatureRequests.Repository;
using MKS.Web.FeatureRequests.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.FeatureRequests.Controllers.API
{
    [Authorize]
    [Route("api/comments")]
    public class CommentsApiController : Controller
    {
        private readonly CommentsRepository _comments;

        public CommentsApiController(CommentsRepository comments)
        {
            _comments = comments;
        }

        //POST: api/comments/request/4
        [HttpPost]
        [Route("request/{requestId}")]
        public IActionResult GetByRequest([FromRoute]long requestId, [FromBody]DataRequestModel query)
        {
            var entities = _comments.GetByFeatureRequest(requestId, query.ToDataRequest<CommentView>());
            var viewModels = Mapper.Map<List<CommentDTO>>(entities);
            return Ok(viewModels);
        }
    }
}
