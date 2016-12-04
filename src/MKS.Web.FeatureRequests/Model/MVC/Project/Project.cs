using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.FeatureRequests.Model.MVC.Project
{
    /// <summary>
    /// Project view model
    /// </summary>
    public class Project
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// CreatedAt - local time
        /// </summary>
        public DateTime CreatedAt { get; set; }


        /// <summary>
        /// Defaults
        /// </summary>
        public Project()
        {

        }

        /// <summary>
        /// Create from domain/entity model.
        /// </summary>
        public Project(MKS.Web.Data.FeatureRequests.Model.Project data)
        {
            Id = data.Id;
            Name = data.Name;
            Description = data.Description;
        }
    }
}
