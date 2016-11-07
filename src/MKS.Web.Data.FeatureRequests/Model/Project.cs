using MKS.Web.Data.FeatureRequests.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.Data.FeatureRequests.Model
{
    public class Project : ICreationTracked, IEntity
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public User CreatedBy { get; set; }
        [Required]
        public string CreatedById { get; set; }
        public DateTime CreatedAtUtc { get; set; }
    }
}
