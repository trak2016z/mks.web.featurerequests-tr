using AutoMapper;
using MKS.Web.Data.FeatureRequests.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.FeatureRequests.Model.DTO
{
    /// <summary>
    /// Configures mappings between entities and transfer objects.
    /// </summary>
    public class DTOProfile : Profile
    {
        public DTOProfile()
        {
            CreateMap<Project, ProjectDTO>();
            CreateMap<ProjectDTO, Project>();

            CreateMap<FeatureRequest, FeatureRequestDTO>();
            CreateMap<FeatureRequestDTO, FeatureRequest>();
        }
    }
}
