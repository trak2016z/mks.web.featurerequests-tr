using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.FeatureRequests.Model.DTO
{
    public class CommentDTO
    {
        public string CreatedByName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Content { get; set; }
        public int Votes { get; set; }
    }
}
