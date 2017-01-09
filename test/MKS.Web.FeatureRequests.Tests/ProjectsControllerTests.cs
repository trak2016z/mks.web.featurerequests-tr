using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKS.Web.Data.FeatureRequests;
using MKS.Web.Data.FeatureRequests.Model;
using MKS.Web.Data.FeatureRequests.Repository;
using MKS.Web.FeatureRequests.Controllers.MVC;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace MKS.Web.FeatureRequests.Tests
{
    public class ProjectsControllerTests
    {
        [Fact]
        public void TestCreate()
        {
            var dbContext = Substitute.For<FeatureRequestsDbContext>();
            var projectsRepository = Substitute.For<ProjectsRepository>(dbContext);
            var featureRequests = Substitute.For<FeatureRequestsRepository>(dbContext);

            var controller = new ProjectsController(projectsRepository, featureRequests);

            var fakeUser = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                 new Claim("sub", "123"),
            }));
            controller.ControllerContext.HttpContext = new DefaultHttpContext()
            {
                User = fakeUser
            };

            var createGetResult = controller.Create() as ViewResult;
            Assert.True(createGetResult != null); //returns a mvc view
            Assert.Equal("Edit", createGetResult.ViewName); //returns the same view as edit

            controller.Create(new Model.Project.ProjectEdit()
            {
                Id = 0,
                Description = "Test",
                Name = "Also test"
            });

            projectsRepository.Received().Add(Arg.Any<Project>());
            var addArg = (Project)projectsRepository.ReceivedCalls().First().GetArguments().First();
            Assert.Equal("123", addArg.CreatedById); //used faked user id
            projectsRepository.DidNotReceive().Update(Arg.Any<Project>());
        }
    }
}
