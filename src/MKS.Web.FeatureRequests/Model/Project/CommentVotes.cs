using MKS.Web.Data.FeatureRequests.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.FeatureRequests.Model.Project
{
    public class CommentVotes
    {
        public int Votes { get; set; }
        public VoteType? CurrentUserVote { get; set; }
    }
}
