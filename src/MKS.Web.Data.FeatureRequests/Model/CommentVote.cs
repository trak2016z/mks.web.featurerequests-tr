using MKS.Web.Data.FeatureRequests.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.Data.FeatureRequests.Model
{
    public class CommentVote : ICreationTracked, IEntity
    {
        public long Id { get; set; }
        public long CommentId { get; set; }
        public User CreatedBy { get; set; }
        [Required]
        public string CreatedById { get; set; }
        public DateTime CreatedAtUtc { get; set; }
    }
}
