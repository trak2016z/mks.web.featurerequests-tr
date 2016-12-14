using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//business models & view models namespace aliases
using BM = MKS.Web.Data.FeatureRequests.Model;
using VM = MKS.Web.FeatureRequests.Model;

namespace MKS.Web.FeatureRequests.Model
{
    /// <summary>
    /// Configures mappings between entities and transfer objects.
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BM.FeatureRequest, VM.Project.FeatureRequestView>()
                .ForMember(vm => vm.Comments, o => o.Ignore());

            CreateMap<BM.View.FeatureRequestView, VM.Project.FeatureRequestView>()
                .ForMember(vm => vm.Comments, o => o.Ignore());

            CreateMap<BM.Project, VM.Project.ProjectListItem>()
                .ForMember(vm => vm.CreatedAt, o => o.MapFrom(m => m.CreatedAtUtc.ToLocalTime()));

            CreateMap<BM.Project, VM.Project.ProjectView>()
                .ForMember(vm => vm.FeatureRequests, o => o.Ignore())
                .ForMember(vm => vm.NewComment, o => o.Ignore())
                .ForMember(vm => vm.NewRequest, o => o.Ignore())
                .ForMember(vm => vm.CreatedAt, o => o.MapFrom(m => m.CreatedAtUtc.ToLocalTime()));

            CreateMap<BM.Project, VM.Project.ProjectEdit>();
            CreateMap<BM.Comment, VM.Project.CommentView>();
            CreateMap<BM.View.CommentView, VM.Project.CommentView>();

            CreateMap<VM.Project.CommentCreate, BM.Comment>();
            CreateMap<VM.Project.FeatureRequestCreate, BM.FeatureRequest>();
            CreateMap<VM.Project.ProjectEdit, BM.Project>();
        }
    }
}
