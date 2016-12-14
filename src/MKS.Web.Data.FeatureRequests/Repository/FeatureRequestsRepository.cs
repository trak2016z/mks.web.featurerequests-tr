using MKS.Web.Data.FeatureRequests.Exception;
using MKS.Web.Data.FeatureRequests.Model;
using MKS.Web.Data.FeatureRequests.Model.Entity;
using MKS.Web.Data.FeatureRequests.Model.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.Data.FeatureRequests.Repository
{
    public class FeatureRequestsRepository : BaseRepository<FeatureRequest>
    {
        public FeatureRequestsRepository(FeatureRequestsDbContext db)
            : base(db)
        {
        }

        public List<FeatureRequest> GetByProjectId(long projectId)
        {
            return _db.FeatureRequests.Where(f => f.ProjectId == projectId).ToList();
        }

        public List<FeatureRequestView> GetViewByProjectId(string currentUserId, long projectId)
        {
            var q = (from request in _db.FeatureRequests.Where(c => c.ProjectId == projectId)
                     from user in _db.Users.Where(u => u.Id == request.CreatedById)
                     from vote in _db.Votes.Where(v => v.FeatureRequestId == request.Id && v.CreatedById == currentUserId).DefaultIfEmpty()
                     select new Model.View.FeatureRequestView()
                     {
                         Id = request.Id,
                         ProjectId = request.ProjectId,
                         Name = request.Name,
                         Description = request.Description,
                         Votes = request.Votes,
                         CreatedBy = request.CreatedBy,
                         CreatedById = request.CreatedById,
                         CreatedAt = request.CreatedAtUtc,
                         CurrentUserVote = vote != null ? vote.Type : (VoteType?)null,
                         CanUpvote = false,     //check outside db
                         CanDownvote = false
                     });

            //to local time has to be done outside db
            var list = q.ToList();
            foreach (var c in list)
            {
                c.CreatedAt = c.CreatedAt.ToLocalTime();

                if (c.CreatedById == currentUserId)
                    c.CanUpvote = c.CanDownvote = false;
                else
                {
                    c.CanUpvote = c.CurrentUserVote == null || c.CurrentUserVote.Value == VoteType.Down;
                    c.CanDownvote = c.CurrentUserVote == null || c.CurrentUserVote.Value == VoteType.Up;
                }
            }

            return list;
        }

        public void Add(long projectId, FeatureRequest entity)
        {
            entity.ProjectId = projectId;
            entity.Votes = 1;
            entity.CreatedAtUtc = DateTime.Now;

            using (var transaction = _db.Database.BeginTransaction())
            {
                if(!_db.FeatureRequests.Any(e => e.Name.ToLower() == entity.Name.ToLower()))
                {
                    _db.FeatureRequests.Add(entity);
                    _db.SaveChanges();
                    transaction.Commit();
                }
                else
                {
                    throw new BusinessException("Request with the same name already exists.", nameof(FeatureRequest.Name));
                }
            }
        }

        public bool IsNameAvailable(string name)
        {
            return !_db.FeatureRequests.Any(e => e.Name.ToLower() == name.ToLower());
        }

        private void Vote(string userId, long featureRequestId, VoteType type)
        {
            using (var transaction = _db.Database.BeginTransaction())
            {
                var request = _db.FeatureRequests.FirstOrDefault(f => f.Id == featureRequestId);
                if (request == null)
                {
                    throw new BusinessException("No such feature request exists.", nameof(featureRequestId));
                }

                var existingUserVote = _db.Votes.FirstOrDefault(v => v.CreatedById == userId && v.FeatureRequestId == featureRequestId);

                if (existingUserVote == null)
                {
                    request.Votes += (type == VoteType.Up ? 1 : -1);
                    _db.Votes.Add(new Model.Vote()
                    {
                        FeatureRequestId = featureRequestId,
                        CreatedAtUtc = DateTime.UtcNow,
                        CreatedById = userId,
                        Type = type
                    });
                }
                else
                {
                    if (existingUserVote.Type == type)
                    {
                        throw new BusinessException("Already voted that way.", nameof(featureRequestId));
                    }

                    if (type == VoteType.Up && existingUserVote.Type == VoteType.Down)
                    {
                        //voted -1 earlier, changed to +1
                        request.Votes += 2;
                    }
                    else
                    {
                        //voted +1 earlier, changed to -1
                        request.Votes -= 2;
                    }

                    //update type
                    existingUserVote.Type = type;
                }

                _db.SaveChanges();
                transaction.Commit();
            }
        }

        /// <summary>
        /// +1 a feature request
        /// </summary>
        public void Upvote(string userId, long requestId)
        {
            Vote(userId, requestId, VoteType.Up);
        }

        /// <summary>
        /// -1 a feature request
        /// </summary>
        public void Downvote(string userId, long requestId)
        {
            Vote(userId, requestId, VoteType.Down);
        }

        /// <summary>
        /// Get feature request votes and current vote
        /// </summary>
        /// <param name="featureRequestId"></param>
        /// <returns></returns>
        public Tuple<int, VoteType?> GetVotesAndCurrentUserVote(long featureRequestId, string currentUserId)
        {
            var q = from request in _db.FeatureRequests.Where(c => c.Id == featureRequestId)
                    from vote in _db.Votes.Where(v => v.FeatureRequestId == request.Id && v.CreatedById == currentUserId).DefaultIfEmpty()
                    select new
                    {
                        request.Votes,
                        CurrentUserVote = vote != null ? vote.Type : (VoteType?)null
                    };

            var data = q.FirstOrDefault();
            return data != null ? Tuple.Create(data.Votes, data.CurrentUserVote) : null;
        }
    }
}
