using Microsoft.EntityFrameworkCore;
using MKS.Web.Data.FeatureRequests;
using MKS.Web.Data.FeatureRequests.Model;
using MKS.Web.Data.FeatureRequests.Repository;
using NSubstitute;
using System;
using Xunit;

namespace Tests
{
    public class ProjectRepositoryTests
    {
        [Fact]
        public void TestSaveChanges() 
        {
            var fakeDbContext = Substitute.For<FeatureRequestsDbContext>();

            var repository = new ProjectsRepository(fakeDbContext);

            var entity = new MKS.Web.Data.FeatureRequests.Model.Project()
            {
                Id = 1,
                CreatedAtUtc = DateTime.UtcNow,
                CreatedById = "1",
                Description = "Test",
                Name = "Test"
            };

            repository.Add(entity);
            repository.Update(entity);

            fakeDbContext.Received(2).SaveChanges();
        }

        [Fact]
        public void TestCRUD()
        {
            var fakeDbContext = Substitute.For<FeatureRequestsDbContext>();
            fakeDbContext.Projects = Substitute.For<DbSet<Project>>();
            fakeDbContext.Set<Project>().Returns(fakeDbContext.Projects);

            var repository = new ProjectsRepository(fakeDbContext);

            var entity = new MKS.Web.Data.FeatureRequests.Model.Project()
            {
                Id = 1,
                CreatedAtUtc = DateTime.UtcNow,
                CreatedById = "1",
                Description = "Test",
                Name = "Test"
            };

            repository.Add(entity);
            fakeDbContext.Received().Add(Arg.Any<Project>());

            repository.Update(entity);
            fakeDbContext.Received().Update(Arg.Any<Project>());
        }
    }
}
