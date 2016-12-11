using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.FeatureRequests.Model.Project
{
    public class CommentView
    {
        public long Id { get; set; }
        public long? ParentId { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Content { get; set; }
        public int Votes { get; set; }
    }
}
