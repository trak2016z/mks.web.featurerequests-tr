using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MKS.Web.Data.FeatureRequests.Query;
using MKS.Web.Data.FeatureRequests.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MKS.Web.Common;
using MKS.Web.Data.FeatureRequests.Exception;
using MKS.Web.FeatureRequests.Model.DataRequest;

using VM = MKS.Web.FeatureRequests.Model.Project;
using BM = MKS.Web.Data.FeatureRequests.Model;

namespace MKS.Web.FeatureRequests.Controllers.API
{
    [Authorize]
    [Route("api/comments")]
    public class CommentsApiController : BaseController
    {
        private readonly CommentsRepository _comments;

        public CommentsApiController(CommentsRepository comments)
        {
            _comments = comments;
        }

        //GET: api/comments/request/4
        [HttpGet]
        [Route("request/{requestId}")]
        public IActionResult GetByRequest([FromRoute]long requestId)
        {
            var entities = _comments.GetByFeatureRequest(requestId, User.GetUserId(), new DataRequest<Data.FeatureRequests.Model.View.CommentView>()
            {
                PageSize = int.MaxValue,
                OrderBy = c => c.CreatedAt,
                Direction = SortDirection.DESC
            });

            var viewModels = Mapper.Map<List<VM.CommentView>>(entities);
            return Ok(viewModels);
        }

        //POST: api/comments/request/4
        [HttpPost]
        [Route("request/{requestId}")]
        public IActionResult GetByRequest([FromRoute]long requestId, [FromBody]DataRequestModel query)
        {
            var entities = _comments.GetByFeatureRequest(requestId, User.GetUserId(), query.ToDataRequest<BM.View.CommentView>());
            var viewModels = Mapper.Map<List<VM.CommentView>>(entities);
            return Ok(viewModels);
        }

        //POST: api/comments/request
        [HttpPost]
        [Route("request")]
        public IActionResult AddRequestComment([FromBody]VM.CommentCreate model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    _comments.Add(new Data.FeatureRequests.Model.Comment()
                    {
                        Content = model.Content,
                        CreatedById = User.GetUserId(),
                        FeatureRequestId = model.RequestId,
                        ParentId = null,
                        CreatedAtUtc = DateTime.Now.ToUniversalTime()
                    });

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
    }
}
