using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.FeatureRequests.Model
{
    /// <summary>
    /// Configures mappings between entities and transfer objects.
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Data.FeatureRequests.Model.FeatureRequest, FeatureRequest>();
            CreateMap<FeatureRequest, Data.FeatureRequests.Model.FeatureRequest>();
        }
    }
}
