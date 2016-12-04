using MKS.Web.Data.FeatureRequests.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.Data.FeatureRequests.Model
{
    public class Comment : ICreationTracked, IEntity
    {
        public long Id { get; set; }

        /// <summary>
        /// If this comment is a reply to other comment
        /// </summary>
        public long? ParentId { get; set; }
        public long FeatureRequestId { get; set; }
        public string Content { get; set; }
        public int Votes { get; set; }

        public User CreatedBy { get; set; }
        [Required]
        public string CreatedById { get; set; }
        public DateTime CreatedAtUtc { get; set; }

    }
}
