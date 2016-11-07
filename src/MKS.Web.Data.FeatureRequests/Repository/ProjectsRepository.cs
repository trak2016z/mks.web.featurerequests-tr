using MKS.Web.Data.FeatureRequests.Model;
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
    }
}
