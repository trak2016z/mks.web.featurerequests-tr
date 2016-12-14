using MKS.Web.Data.FeatureRequests.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.FeatureRequests.Model.Project
{
    /// <summary>
    /// Common votes info
    /// </summary>
    public class CommonVotes
    {
        public int Votes { get; set; }

        /// <summary>
        /// How did current user vote
        /// </summary>
        public VoteType? CurrentUserVote { get; set; }
    }
}
