using MKS.Web.Data.FeatureRequests.Model;
using MKS.Web.Data.FeatureRequests.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MKS.Web.Data.FeatureRequests.Extensions;
using MKS.Web.Data.FeatureRequests.Exception;
using MKS.Web.Data.FeatureRequests.Model.Entity;

namespace MKS.Web.Data.FeatureRequests.Repository
{
    public class CommentsRepository : BaseRepository<Comment>
    {
        public CommentsRepository(FeatureRequestsDbContext db)
            : base(db)
        {

        }

        public override void Add(Comment entity)
        {
            using (var transaction = _db.Database.BeginTransaction())
            {
                if(_db.FeatureRequests.Any(r => r.Id == entity.FeatureRequestId))
                {
                    //if parent id specified, check if it exists
                    if(entity.ParentId > 0 && !_db.Comments.Any(c => c.Id == entity.ParentId))
                    {
                        throw new BusinessException("Invalid parent id.", nameof(entity.ParentId));
                    }

                    _db.Comments.Add(entity);
                    _db.SaveChanges();
                    transaction.Commit();
                }
                else
                {
                    throw new BusinessException("Invalid feature request or parent comment id.", nameof(entity.FeatureRequestId));
                }
            }
        }

        /// <summary>
        /// Get by request id and pager info.
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<Model.View.CommentView> GetByFeatureRequest(long requestId, string currentUserId, IDataRequest<Model.View.CommentView> query)
        {
            var q = (from comment in _db.Comments.Where(c => c.FeatureRequestId == requestId)
                    from user in _db.Users.Where(u => u.Id == comment.CreatedById)
                    from vote in _db.CommentVotes.Where(v => v.CommentId == comment.Id && v.CreatedById == currentUserId).DefaultIfEmpty()
                    select new Model.View.CommentView()
                    {
                        Id = comment.Id,
                        ParentId = comment.ParentId,
                        FeatureRequestId = comment.FeatureRequestId,
                        Content = comment.Content,
                        Votes = comment.Votes,
                        CreatedByName = user.GivenName,
                        CreatedById = comment.CreatedById,
                        CreatedAt = comment.CreatedAtUtc,
                        CurrentUserVote = vote != null ? vote.Type : (VoteType?)null
                    }).FilterByDataRequest(query);

            //to local time has to be done outside db
            var list = q.ToList();
            foreach (var c in list)
                c.CreatedAt = c.CreatedAt.ToLocalTime();

            return list;
        }

        private void Vote(string userId, long commentId, VoteType type)
        {
            using (var transaction = _db.Database.BeginTransaction())
            {
                var comment = _db.Comments.FirstOrDefault(c => c.Id == commentId);
                if (comment == null)
                {
                    throw new BusinessException("No such comment exists.", nameof(commentId));
                }

                var existingUserVote = _db.CommentVotes.FirstOrDefault(c => c.CreatedById == userId && c.CommentId == commentId);

                if (existingUserVote == null)
                {
                    comment.Votes += (type == VoteType.Up ? 1 : -1);
                    _db.CommentVotes.Add(new CommentVote()
                    {
                        CommentId = commentId,
                        CreatedAtUtc = DateTime.UtcNow,
                        CreatedById = userId,
                        Type = type
                    });
                }
                else
                {
                    if (existingUserVote.Type == type)
                    {
                        throw new BusinessException("Already voted that way.", nameof(commentId));
                    }

                    if (type == VoteType.Up && existingUserVote.Type == VoteType.Down)
                    {
                        //voted -1 earlier, changed to +1
                        comment.Votes += 2;
                    }
                    else
                    {
                        //voted +1 earlier, changed to -1
                        comment.Votes -= 2;
                    }

                    //update type
                    existingUserVote.Type = type;
                }

                _db.SaveChanges();
                transaction.Commit();
            }
        }

        /// <summary>
        /// +1 a comment
        /// </summary>
        public void Upvote(string userId, long commentId)
        {
            Vote(userId, commentId, VoteType.Up);
        }

        /// <summary>
        /// -1 a comment
        /// </summary>
        public void Downvote(string userId, long commentId)
        {
            Vote(userId, commentId, VoteType.Down);
        }

        /// <summary>
        /// Get comment votes and current vote
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public Tuple<int, VoteType?> GetVotesAndCurrentUserVote(long commentId, string currentUserId)
        {
            var q = from comment in _db.Comments.Where(c => c.Id == commentId)
                    from vote in _db.CommentVotes.Where(c => c.CommentId == comment.Id && c.CreatedById == currentUserId).DefaultIfEmpty()
                    select new
                    {
                        comment.Votes,
                        CurrentUserVote = vote != null ? vote.Type : (VoteType?)null
                    };

            var data = q.FirstOrDefault();
            return data != null ? Tuple.Create(data.Votes, data.CurrentUserVote) : null;
        }
    }
}
