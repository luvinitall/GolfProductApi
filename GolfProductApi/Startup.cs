using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GolfProductApi.Entities;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OData.Edm;
using NLog.Extensions.Logging;

namespace GolfProductApi
{
    public class Startup
    {
        //if I want to same any config settings in json, uncomment
        //public static IConfiguration Configuration;

        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOData();
            services.AddMvc().AddMvcOptions(o => o.OutputFormatters.Add(
                    new XmlDataContractSerializerOutputFormatter()))
                .AddMvcOptions(o => o.InputFormatters.Add(
                    new XmlDataContractSerializerInputFormatter()));
            services.AddDbContext<GolfProductDbContext>(o =>
                o.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=GolfProductDb;Trusted_Connection=True"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //Adding NLog to the mix
            loggerFactory.AddNLog();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            app.UseStatusCodePages(); //Show a status code in browser instaead of exception in debugger
            app.UseMvc(routebuilder =>
            {
                routebuilder.MapODataServiceRoute("ODataRoute", "odata", GetEdmModel());
                routebuilder.Select().Expand().Filter().OrderBy().MaxTop(10).Count();
            });

            //app.UseODataBatching();

            app.Run(async (context) => { await context.Response.WriteAsync("Hello World!"); });
        }

        private static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.Namespace = "GolfProductApi";
            builder.ContainerName = "GolfProductApiContainer";
            builder.EntitySet<Catalog>("Catalogs");

            return builder.GetEdmModel();
        }
    }
}