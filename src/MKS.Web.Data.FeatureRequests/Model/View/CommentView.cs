using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.Data.FeatureRequests.Model.View
{
    public class CommentView
    {
        public long Id { get; set; }

        /// <summary>
        /// If this comment is a reply to other comment
        /// </summary>
        public long? ParentId { get; set; }
        public long FeatureRequestId { get; set; }
        public string Content { get; set; }
        public int Votes { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedAt { get; set; } //local time
    }
}
