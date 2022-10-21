using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;

namespace demoForVersion31.Extensions
{
    /// <summary>
    /// 路由和终结点扩展
    /// </summary>
    public static class RoutingEndpointExtensions
    {
        /// <summary>
        /// 添加和配置API版本相关服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApiVersions(this IServiceCollection services)
        {
            // 添加API版本控制
            services.AddApiVersioning(apiVersioningOptions =>
            {
                // 返回响应标头中支持的版本信息
                apiVersioningOptions.ReportApiVersions = true;
                // 此选项将用于不提供版本的请求，指向默认版本
                apiVersioningOptions.AssumeDefaultVersionWhenUnspecified = true;
                // 默认版本号，支持时间或数字版本号
                apiVersioningOptions.DefaultApiVersion = new ApiVersion(1, 0);
            });

            // 添加版本管理服务
            services.AddVersionedApiExplorer(apiExplorerOptions =>
            {
                // 设置API组名格式
                apiExplorerOptions.GroupNameFormat = "'v'VVV";
                // 在URL中替换版本
                apiExplorerOptions.SubstituteApiVersionInUrl = true;
                // 当未设置版本时指向默认版本
                apiExplorerOptions.AssumeDefaultVersionWhenUnspecified = true;
            });

            return services;
        }

        /// <summary>
        /// 添加和配置Swagger相关服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggers(this IServiceCollection services)
        {
            // AddApiVersions必须在AddSwaggers之前调用
            var apiVersionDescriptionProvider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
            
            // 添加SwaggerGen服务
            services.AddSwaggerGen(swaggerGenOptions =>
            {
                // 根据请求方式排序
                swaggerGenOptions.OrderActionsBy(o => o.HttpMethod);

                // 遍历已知的API版本
                apiVersionDescriptionProvider.ApiVersionDescriptions.ToList().ForEach(versionDescription =>
                {
                    var group = versionDescription.GroupName.ToString();
                    swaggerGenOptions.SwaggerDoc(group, new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Tesla Open API",
                        Version = group,
                        Description = $"Tesla Open API {group} Powered by ASP.NET Core"
                    });
                });

                // 重载方式
                swaggerGenOptions.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                // 遍历已存在的XML
                foreach (var name in Directory.GetFiles(AppContext.BaseDirectory, "*.*",
                    SearchOption.AllDirectories).Where(f => Path.GetExtension(f).ToLower() == ".xml"))
                {
                    swaggerGenOptions.IncludeXmlComments(name, includeControllerXmlComments: true);
                }
            });
                
            return services;
        }

        /// <summary>
        /// 使用和配置Swagger、ApiVersion相关中间件
        /// </summary>
        /// <param name="app"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwaggerAndApiVersions(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseApiVersioning();

            app.UseSwagger();

            app.UseSwaggerUI(swaggerUIOptions =>
            {
                provider.ApiVersionDescriptions.ToList().ForEach(versionDescription =>
                {
                    var group = versionDescription.GroupName.ToString();
                    swaggerUIOptions.SwaggerEndpoint($"/swagger/{group}/swagger.json", group);
                });
            });

            return app;
        }
    }
}
