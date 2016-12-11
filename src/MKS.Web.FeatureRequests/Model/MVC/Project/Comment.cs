using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.FeatureRequests.Model.MVC.Project
{
    public class Comment
    {
        public string Author { get; set; }
        public string Content { get; set; }
        public DateTime PostedAt { get; set; }
        public int Votes { get; set; }
    }
}
