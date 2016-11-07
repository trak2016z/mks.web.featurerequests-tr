using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MKS.Web.Data.FeatureRequests;

namespace MKS.Web.FeatureRequests.Migrations
{
    [DbContext(typeof(FeatureRequestsDbContext))]
    [Migration("20161106194615_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MKS.Web.Data.FeatureRequests.Model.Comment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreatedAtUtc");

                    b.Property<string>("CreatedById")
                        .IsRequired();

                    b.Property<long>("FeatureRequestId");

                    b.Property<long?>("ParentId");

                    b.Property<int>("Votes");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("MKS.Web.Data.FeatureRequests.Model.CommentVote", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CommentId");

                    b.Property<DateTime>("CreatedAtUtc");

                    b.Property<string>("CreatedById")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("CommentVotes");
                });

            modelBuilder.Entity("MKS.Web.Data.FeatureRequests.Model.FeatureRequest", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAtUtc");

                    b.Property<string>("CreatedById")
                        .IsRequired();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<long>("ProjectId");

                    b.Property<int>("Votes");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("FeatureRequests");
                });

            modelBuilder.Entity("MKS.Web.Data.FeatureRequests.Model.Project", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAtUtc");

                    b.Property<string>("CreatedById")
                        .IsRequired();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("MKS.Web.Data.FeatureRequests.Model.User", b =>
                {
                    b.Property<string>("Id");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("MKS.Web.Data.FeatureRequests.Model.Vote", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAtUtc");

                    b.Property<string>("CreatedById")
                        .IsRequired();

                    b.Property<long>("FeatureRequestId");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("MKS.Web.Data.FeatureRequests.Model.Comment", b =>
                {
                    b.HasOne("MKS.Web.Data.FeatureRequests.Model.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MKS.Web.Data.FeatureRequests.Model.CommentVote", b =>
                {
                    b.HasOne("MKS.Web.Data.FeatureRequests.Model.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MKS.Web.Data.FeatureRequests.Model.FeatureRequest", b =>
                {
                    b.HasOne("MKS.Web.Data.FeatureRequests.Model.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MKS.Web.Data.FeatureRequests.Model.Project", b =>
                {
                    b.HasOne("MKS.Web.Data.FeatureRequests.Model.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MKS.Web.Data.FeatureRequests.Model.Vote", b =>
                {
                    b.HasOne("MKS.Web.Data.FeatureRequests.Model.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
