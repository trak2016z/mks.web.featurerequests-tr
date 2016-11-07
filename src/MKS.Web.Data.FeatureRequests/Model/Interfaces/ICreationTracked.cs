using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.Data.FeatureRequests.Model.Interfaces
{
    public interface ICreationTracked
    {
        User CreatedBy { get; set; }
        string CreatedById { get; set; }
        DateTime CreatedAtUtc { get; set; }
    }
}
