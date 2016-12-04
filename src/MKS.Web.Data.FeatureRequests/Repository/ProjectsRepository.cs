using MKS.Web.Data.FeatureRequests.Model;
using MKS.Web.Data.FeatureRequests.Model.Query;
using MKS.Web.Data.FeatureRequests.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.Data.FeatureRequests.Repository
{
    public class ProjectsRepository : BaseRepository<Project>
    {
        public ProjectsRepository(FeatureRequestsDbContext db)
            : base(db)
        {
        }

        public List<ProjectListItem> GetSimpleList(IDataRequest<Project> request)
        {
            return GetListQueryable(request)
                .Select(p => new ProjectListItem()
                {
                    Id = p.Id,
                    Name = p.Name,
                    CreatedAtUtc = p.CreatedAtUtc
                })
                .ToList();
        }
    }
}
