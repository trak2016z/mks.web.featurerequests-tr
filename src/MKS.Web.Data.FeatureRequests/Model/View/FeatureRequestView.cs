using MKS.Web.Data.FeatureRequests.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.Data.FeatureRequests.Model.View
{
    public class FeatureRequestView
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Votes { get; set; }
        public VoteType? CurrentUserVote { get; set; }
        public bool CanUpvote { get; set; }
        public bool CanDownvote { get; set; }
        public User CreatedBy { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
