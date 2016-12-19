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
using MKS.Web.Common.ResourceManager;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Formatters;
using MKS.Web.Data.FeatureRequests.Model;

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

            services.AddMvc();

            // bootstrap html helpers
            // https://github.com/justdmitry/BootstrapMvc
            services.AddTransient(typeof(BootstrapMvc.Mvc6.BootstrapHelper<>));
            ConfigureRepositories(services);
            services.AddScoped<IResourceManager, ResourceManager>();
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
                },
            };

            openIdOpts.Events = new OpenIdConnectEvents()
            {
                OnTokenValidated = async context => await Startup.OnTokenValidated(app.ApplicationServices, context)
            };

            app.UseOpenIdConnectAuthentication(openIdOpts);

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "default",
                  template: "{controller=Projects}/{action=List}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUi();
        }

        /// <summary>
        /// Register all repositories.
        /// </summary>
        private void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<ProjectsRepository>();
            services.AddScoped<UserRepository>();
            services.AddScoped<FeatureRequestsRepository>();
            services.AddScoped<CommentsRepository>();
        }

        private static async Task OnTokenValidated(IServiceProvider serviceProvider, TokenValidatedContext context)
        {
            //TODO: google & givenname
            using (var serviceScope = serviceProvider.GetService<IServiceScopeFactory>().CreateScope())
            {
                var userRepository = serviceScope.ServiceProvider.GetRequiredService<UserRepository>();
                var userId = context.Ticket.Principal.Claims.First(c => c.Type == JwtClaimTypes.Subject).Value;
                var userName = context.Ticket.Principal.Claims.First(c => c.Type == JwtClaimTypes.Name).Value;
                await userRepository.AddOrUpdateAsync(new User()
                {
                    Id = userId,
                    GivenName = userName
                });
            }
        }
    }
}
