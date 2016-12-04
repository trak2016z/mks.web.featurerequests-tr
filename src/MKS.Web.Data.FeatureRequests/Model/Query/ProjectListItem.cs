using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.Data.FeatureRequests.Model.Query
{
    public class ProjectListItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAtUtc { get; set; }
    }
}
