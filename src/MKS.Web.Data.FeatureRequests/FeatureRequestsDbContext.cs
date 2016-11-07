using Microsoft.EntityFrameworkCore;
using MKS.Web.Data.FeatureRequests.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.Data.FeatureRequests
{
    public class FeatureRequestsDbContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentVote> CommentVotes { get; set; }
        public DbSet<FeatureRequest> FeatureRequests { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Vote> Votes { get; set; }

        /// <summary>
        /// Allows specifying provider at the application level.
        /// </summary>
        public FeatureRequestsDbContext(DbContextOptions<FeatureRequestsDbContext> options)
            : base(options)
        {

        }
    }
}
