using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MKS.Web.Data.FeatureRequests;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Swashbuckle.Swagger.Model;
using MKS.Web.Data.FeatureRequests.Repository;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using IdentityModel;

namespace MKS.Web.FeatureRequests
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            string migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            // Add framework services.
            services.AddDbContext<FeatureRequestsDbContext>(options =>
                options.UseNpgsql(connectionString, o => o.MigrationsAssembly(migrationsAssembly)));

            services.AddMvc();
            services.AddAutoMapper();

            string pathToDoc = Configuration["DocPath"];

            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "Feature reuqests app API",
                    Description = "",
                    TermsOfService = "None"
                });
                options.IncludeXmlComments(pathToDoc);
                options.DescribeAllEnumsAsStrings();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //run migrations on app start in debug mode
            //in release you should deploy the whole db
            //or apply migrations manually through CLI or SQL scripts
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<FeatureRequestsDbContext>().Database.Migrate();
            }

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Cookies"
            });

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            var openIdOpts = new OpenIdConnectOptions
            {
                AuthenticationScheme = "oidc",
                SignInScheme = "Cookies",

                Authority = Configuration["AuthUrl"],
                RequireHttpsMetadata = false,                                   //TODO: enable in prod

                ClientId = "MKS.Web.MVC.FeatureRequests",
                SaveTokens = true,
                GetClaimsFromUserInfoEndpoint = true,

                Scope = { "openid", "profile", "roles" },

                TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    RoleClaimType = JwtClaimTypes.Role,
                    NameClaimType = JwtClaimTypes.Name
                }
            };

            app.UseOpenIdConnectAuthentication(openIdOpts);

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();

            app.UseSwagger();
            app.UseSwaggerUi();
        }

        /// <summary>
        /// Register all repositories.
        /// </summary>
        private void ConfigureRepositories(IServiceCollection services)
        {
            services.AddTransient<ProjectsRepository>();
        }
    }
}
