using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using Middleware;
using System.Text;

namespace hw_mvc
{
    /*
        定义请求处理管道；
        配置服务；
        You can define separate Startup classes for different environments, 
            and the appropriate one will be selected at runtime
        You can request these services by including the appropriate interface as a parameter on your Startup class's 
            constructor or one of its Configure or ConfigureServices methods
     */
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

            // throw new Exception("---errors during startup test---");
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // 非必须
        //启动顺序——1
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // 必须
        //启动顺序——2
        // public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        // {
        //     loggerFactory.AddConsole(Configuration.GetSection("Logging"));
        //     loggerFactory.AddDebug();

        //     //Each Use extension method adds a middleware component to the request pipeline
        //     //异常处理中间件首先注册，这样就可以获取后续调用引发的所有异常
        //     if (env.IsDevelopment())
        //     {
        //         app.UseDeveloperExceptionPage();
        //         app.UseBrowserLink();
        //     }
        //     else
        //     {
        //         app.UseExceptionHandler("/Home/Error");
        //     }

        //     //启用静态文件路由
        //     //该管道注册顺序比较靠前，可以及早处理静态文件从而切断后续处理管道
        //     app.UseStaticFiles();

        //     //Identity does not short-circuit unauthenticated requests. 
        //     //Although Identity authenticates requests, authorization (and rejection) occurs only 
        //     //after MVC selects a specific controller and action.
        //     app.UseIdentity();

        //     app.UseMvc(routes =>
        //     {
        //         routes.MapRoute(
        //             name: "default",
        //             template: "{controller=Home}/{action=Index}/{id?}");
        //     });
        // }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRequestCulture();//自定义中间件
            //Map* extensions are used as a convention for branching the pipeline
            app.Map("/map1", HandleMapTest1);
            app.Map("/map2", HandleMapTest2);
            //条件映射
            app.MapWhen(context => context.Request.Query.ContainsKey("branch"), HandleBranch);
            app.MapWhen(context=>context.Request.Query.ContainsKey("culture"),HandleBranchCulture);
            //嵌套映射
            // app.Map("/level1", level1App => {
            //         level1App.Map("/level2a", level2AApp => {
            //             // "/level1/level2a"
            //             //...
            //         });
            //         level1App.Map("/level2b", level2BApp => {
            //             // "/level1/level2b"
            //             //...
            //         });
            //     });

            app.Run(async context =>
            {
                await context.Response.WriteAsync("Hello from non-Map delegate. <p>");
            });
        }

        private static void HandleMapTest1(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Map Test 1");
            });
        }

        private static void HandleMapTest2(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Map Test 2");
            });
        }

        private static void HandleBranch(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var branchVer = context.Request.Query["branch"];
                await context.Response.WriteAsync($"Branch used = {branchVer}");
            });
        }

        private static void HandleBranchCulture(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync($"Hello {CultureInfo.CurrentCulture.DisplayName}");
            });
        }
    }
}
