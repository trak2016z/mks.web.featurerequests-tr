using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.FeatureRequests.Model.Project
{
    /// <summary>
    /// Project view model
    /// </summary>
    public class ProjectView
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// CreatedAt - local time
        /// </summary>
        public DateTime CreatedAt { get; set; }

        public List<FeatureRequestView> FeatureRequests { get; set; } = new List<FeatureRequestView>();

        /// <summary>
        /// New request view model
        /// </summary>
        public FeatureRequestCreate NewRequest { get; set; } = new FeatureRequestCreate();

        /// <summary>
        /// New comment view model
        /// </summary>
        public CommentCreate NewComment { get; set; } = new CommentCreate();
    }
}
