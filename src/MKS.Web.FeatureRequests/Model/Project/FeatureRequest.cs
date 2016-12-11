using Microsoft.AspNetCore.Mvc;
using MKS.Web.FeatureRequests.Controllers.API;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.FeatureRequests.Model.Project
{
    public class FeatureRequestView
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public int Votes { get; set; }

        public List<CommentView> Comments { get; set; } = new List<CommentView>();
    }
}
