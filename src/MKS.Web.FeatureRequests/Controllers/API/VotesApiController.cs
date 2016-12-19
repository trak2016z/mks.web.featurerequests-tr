using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MKS.Web.Data.FeatureRequests.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MKS.Web.Common;
using MKS.Web.Data.FeatureRequests.Exception;
using MKS.Web.FeatureRequests.Model.Project;

namespace MKS.Web.FeatureRequests.Controllers.API
{
    [Authorize]
    [Route("api/votes")]
    public class VotesApiController : BaseController
    {
        private readonly CommentsRepository _comments;
        private readonly FeatureRequestsRepository _featureRequests;

        public VotesApiController(CommentsRepository comments, FeatureRequestsRepository featureRequests)
        {
            _comments = comments;
            _featureRequests = featureRequests;
        }

        #region Comments
        [HttpPost]
        [Route("comment/{commentId}/{direction}")]
        public IActionResult VoteComment(long commentId, string direction)
        {
            direction = direction.ToLower();
            try
            {
                switch (direction)
                {
                    case "up":
                        _comments.Upvote(User.GetUserId(), commentId);
                        break;
                    case "down":
                        _comments.Downvote(User.GetUserId(), commentId);
                        break;
                    default:
                        return BadRequest();
                }
            }
            catch (BusinessException ex)
            {
                //vote duplicate
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet]
        [Route("comment/{commentId}")]
        public IActionResult GetCommentVotes(long commentId)
        {
            try
            {
                var voteData = _comments.GetVotesAndCurrentUserVote(commentId, User.GetUserId());

                return Ok(voteData != null ? new CommonVotes()
                {
                    Votes = voteData.Item1,
                    CurrentUserVote = voteData.Item2
                } : null);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        #endregion

        #region FeatureRequests
        [HttpPost]
        [Route("featurerequest/{requestId}/{direction}")]
        public IActionResult VoteFeatureRequest(long requestId, string direction)
        {
            direction = direction.ToLower();
            try
            {
                switch (direction)
                {
                    case "up":
                        _featureRequests.Upvote(User.GetUserId(), requestId);
                        break;
                    case "down":
                        _featureRequests.Downvote(User.GetUserId(), requestId);
                        break;
                    default:
                        return BadRequest();
                }
            }
            catch (BusinessException ex)
            {
                //vote duplicate
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet]
        [Route("featurerequest/{requestId}")]
        public IActionResult GetFeatureRequestVotes(long requestId)
        {
            try
            {
                var voteData = _featureRequests.GetVotesAndCurrentUserVote(requestId, User.GetUserId());

                return Ok(voteData != null ? new CommonVotes()
                {
                    Votes = voteData.Item1,
                    CurrentUserVote = voteData.Item2,
                    CanUpvote = voteData.Item2 == null || voteData.Item2 == Data.FeatureRequests.Model.Entity.VoteType.Down,
                    CanDownvote = voteData.Item2 == null || voteData.Item2 == Data.FeatureRequests.Model.Entity.VoteType.Up
                } : null);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        #endregion
    }
}
