using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.FeatureRequests.Model.Project
{
    public class CommentCreate
    {
        public long RequestId { get; set; }
        public string Content { get; set; }
    }
}
