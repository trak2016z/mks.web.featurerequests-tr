using MKS.Web.Data.FeatureRequests.Model;
using MKS.Web.Data.FeatureRequests.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MKS.Web.Data.FeatureRequests.Extensions;

namespace MKS.Web.Data.FeatureRequests.Repository
{
    public class CommentsRepository : BaseRepository<Comment>
    {
        public CommentsRepository(FeatureRequestsDbContext db)
            : base(db)
        {

        }

        /// <summary>
        /// Get by request id and pager info.
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<Model.View.CommentView> GetByFeatureRequest(long requestId, IDataRequest<Model.View.CommentView> query)
        {
            var q = from comment in _db.Comments.Where(c => c.FeatureRequestId == requestId)
                    from user in _db.Users.Where(u => u.Id == comment.CreatedById)
                    select new Model.View.CommentView()
                    {
                        Id = comment.Id,
                        ParentId = comment.ParentId,
                        FeatureRequestId = comment.FeatureRequestId,
                        Content = comment.Content,
                        Votes = comment.Votes,
                        CreatedByName = user.FirstName,
                        CreatedById = comment.CreatedById,
                        CreatedAt = comment.CreatedAtUtc,
                    };

            //to local time has to be done outside db
            var list = q.ToList();
            foreach (var c in list)
                c.CreatedAt = c.CreatedAt.ToLocalTime();

            return list;
        }
    }
}
