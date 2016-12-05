using MKS.Web.Data.FeatureRequests.Exception;
using MKS.Web.Data.FeatureRequests.Model;
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
    }
}
