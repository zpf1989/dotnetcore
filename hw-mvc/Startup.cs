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
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Net.Http.Headers;
using RewriteRules;

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

            services.AddRouting();//添加路由中间件（Microsoft.AspNetCore.Routing）

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // 必须
        //启动顺序——2
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //Each Use extension method adds a middleware component to the request pipeline
            //异常处理中间件首先注册，这样就可以获取后续调用引发的所有异常
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //启用默认文件
            //必须在UseStaticFiles之前，不具有UseStaticFiles的功能
            app.UseDefaultFiles(new DefaultFilesOptions
            {
                //自定义默认文件名称
                DefaultFileNames = new List<string> { "login.html", "idx.html" }
            });

            //启用静态文件路由
            //该管道注册顺序比较靠前，可以及早处理静态文件从而切断后续处理管道
            app.UseStaticFiles(); //makes the files in web root (wwwroot by default) servable
                                  //it is required to serve the CSS, images and JavaScript in the wwwroot folder
            app.UseStaticFiles(new StaticFileOptions //显式指定静态文件物理路径和虚拟路径（相对）
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"MyStaticFiles")),
                //路径大小写问题：windows不区分，mac、linux区分大小写
                RequestPath = new PathString("/staticfiles"),
                OnPrepareResponse = ctx =>
                {
                    //设置缓存1分钟
                    ctx.Context.Response.Headers.Add("Cache-Control", "public,max-age=60");
                }
            });
            //启用文件浏览
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"MyStaticFiles")),
                RequestPath = new PathString("/staticfiles")
            });

            //Identity does not short-circuit unauthenticated requests. 
            //Although Identity authenticates requests, authorization (and rejection) occurs only 
            //after MVC selects a specific controller and action.
            // app.UseIdentity();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            //自定义路由
            UseMyRoute(app);
            //url重写
            RewriteTest(app, env);
        }

        //使用自定义路由
        private void UseMyRoute(IApplicationBuilder app)
        {
            var trackPkgRouteHandler = new RouteHandler(context =>
            {
                var routeValues = context.GetRouteData().Values;
                return context.Response.WriteAsync($"Hello! Route values:{string.Join(", ", routeValues)}");
            });

            var routeBuilder = new RouteBuilder(app, trackPkgRouteHandler);
            //
            routeBuilder.MapRoute(
                    "Track Package Route",
                    "package/{operation:regex(^(track|create|detonate)$)}/{id:int}"
                );
            //
            routeBuilder.MapGet("hello/{name}", context =>
            {
                var values = context.GetRouteData().Values;
                return context.Response.WriteAsync($"Hi, {values["name"]}!");
            });

            //build
            var routes = routeBuilder.Build();
            app.UseRouter(routes);

            //
            // URLGenerationTest(app,routes);
        }

        // public void Configure(IApplicationBuilder app)
        // {
        //     app.UseRequestCulture();//自定义中间件
        //     //Map* extensions are used as a convention for branching the pipeline
        //     app.Map("/map1", HandleMapTest1);
        //     app.Map("/map2", HandleMapTest2);
        //     //条件映射
        //     app.MapWhen(context => context.Request.Query.ContainsKey("branch"), HandleBranch);
        //     app.MapWhen(context=>context.Request.Query.ContainsKey("culture"),HandleBranchCulture);
        //     //嵌套映射
        //     // app.Map("/level1", level1App => {
        //     //         level1App.Map("/level2a", level2AApp => {
        //     //             // "/level1/level2a"
        //     //             //...
        //     //         });
        //     //         level1App.Map("/level2b", level2BApp => {
        //     //             // "/level1/level2b"
        //     //             //...
        //     //         });
        //     //     });

        //     app.Run(async context =>
        //     {
        //         await context.Response.WriteAsync("Hello from non-Map delegate. <p>");
        //     });
        // }

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

        private static void URLGenerationTest(IApplicationBuilder app, IRouter routes)
        {
            app.Run(async context =>
            {
                var dic = new RouteValueDictionary
                {
                    { "operation", "create" },
                    { "id", 123}
                };
                var vpc = new VirtualPathContext(context, null, dic, "Track Package Route");
                var path = routes.GetVirtualPath(vpc).VirtualPath;

                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync("Menu<hr/>");
                await context.Response.WriteAsync($"<a href='{path}'>Create Package 123</a><br/>");
            });
        }

        private static void RewriteTest(IApplicationBuilder app, IHostingEnvironment env)
        {
            var options = new RewriteOptions()
                .AddRedirect("redirect-rule/(.*)", "redirected/$1")//重定向，statuscode：defualt 302
                .AddRewrite(@"^rewrite-rule/(\d+)/(\d+)", "rewritten?var1=$1&var2=$2", skipRemainingRules: true)//重写
                .AddApacheModRewrite(env.ContentRootFileProvider, "/Rules/ApacheModRewrite.txt")
                .AddIISUrlRewrite(env.ContentRootFileProvider, "/Rules/IISUrlRewrite.xml")
                .Add(RedirectXMLRequests)
                .Add(new RedirectImageRules(".png", "/png-images"))
                .Add(new RedirectImageRules(".jpg", "/jpg-images"));

            app.UseRewriter(options);

            app.Run(context =>
                        context.Response.WriteAsync($"Rewritten or Redirected Url: {context.Request.Path + context.Request.QueryString}"));
        }

        static void RedirectXMLRequests(RewriteContext context)
        {
            var request = context.HttpContext.Request;

            // Because we're redirecting back to the same app, stop processing if the request has already been redirected
            if (request.Path.StartsWithSegments(new PathString("/xmlfiles")))
            {
                return;
            }

            if (request.Path.Value.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
            {
                var response = context.HttpContext.Response;
                response.StatusCode = StatusCodes.Status301MovedPermanently;
                context.Result = RuleResult.EndResponse;
                response.Headers[HeaderNames.Location] = "/xmlfiles" + request.Path + request.QueryString;
            }
        }
    }
}
