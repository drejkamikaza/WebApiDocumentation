using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace WebApiDocumentation.CoreSwagger
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            //Add API versioning
            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });

            //Generate Swagger JSON document
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.IncludeXmlComments($"{ApplicationEnvironment.ApplicationBasePath}\\WebApiDocumentation.CoreSwagger.xml");
                options.MultipleApiVersions(new Swashbuckle.Swagger.Model.Info[]
                {
                   new Swashbuckle.Swagger.Model.Info
                   {
                       Version = "1.0",
                       Title = "API",
                       Description = "A RESTful API to show Swagger and Swashbuckle V1"
                   },
                   new Swashbuckle.Swagger.Model.Info
                   {
                       Version = "2.0",
                       Title = "API (version v2)",
                       Description = "A RESTful API to show Swagger and Swashbuckle V2"
                   }
               }, (description, version) =>
               {
                   var controllerDesc = description.ActionDescriptor as ControllerActionDescriptor;
                   if (controllerDesc == null)
                       return false;

                   var apiVersionAttrs = controllerDesc.ControllerTypeInfo?.CustomAttributes?.Where(ca => ca.AttributeType == typeof(ApiVersionAttribute) && ca.ConstructorArguments != null);
                   if (apiVersionAttrs == null || !apiVersionAttrs.Any())
                       return false;

                   bool enabled = apiVersionAttrs.Any(ca => ca.ConstructorArguments.Any(arg => arg.Value is string && ((string)arg.Value).Equals(version, StringComparison.CurrentCultureIgnoreCase)));
                   return enabled;
               });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(routeTemplate: "swagger/{apiVersion}/swagger.json");

            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/1.0/swagger.json", "Meetup Doc API v1");
                options.SwaggerEndpoint("/swagger/2.0/swagger.json", "Meetup Doc API v2");
            });
        }
    }
}
