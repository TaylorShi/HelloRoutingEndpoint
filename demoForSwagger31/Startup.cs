using demoForSwagger31.Consraints;
using demoForSwagger31.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace demoForSwagger31
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(swaggerGenOptions =>
            {
                typeof(ApiVersion).GetEnumNames().ToList().ForEach(version =>
                {
                    swaggerGenOptions.SwaggerDoc(version, new Microsoft.OpenApi.Models.OpenApiInfo
                    { 
                        Title = "Tesla Open API", 
                        Version = version,
                        Description = $"Tesla Open API {version} Powered by ASP.NET Core"
                    });
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                swaggerGenOptions.IncludeXmlComments(xmlPath);
            });

            services.AddRouting(routeOptions =>
            {
                routeOptions.ConstraintMap.Add("IsLong", typeof(TeslaRouteConsraint));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(swaggerUIOptions =>
            {
                typeof(ApiVersion).GetEnumNames().ToList().ForEach(version =>
                {
                    swaggerUIOptions.SwaggerEndpoint($"/swagger/{version}/swagger.json", version);
                });
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
