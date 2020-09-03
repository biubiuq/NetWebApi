using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using NetCoreApi.Data;
using Newtonsoft.Json;

namespace NetCoreApi
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
      //添加cors 服务 配置跨域处理   
      services.AddCors(options =>
      {
        options.AddPolicy("any", builder =>
        {
          builder.WithMethods("GET", "POST", "HEAD", "PUT", "DELETE", "OPTIONS")
      //.AllowCredentials()//指定处理cookie
      .AllowAnyOrigin().AllowAnyHeader(); //允许任何来源的主机访问
        });
      });
      services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new  OpenApiInfo { Title = "My API", Version = "v1" });
            });
            services.AddControllers();
           
            services.AddControllers(setup =>
            {


            }).AddNewtonsoftJson(setup =>////返回的格式
            { ///设置首字母小写，如果不需要 则改为 DefaultContractResolver
                setup.SerializerSettings.ContractResolver =
                    new CamelCasePropertyNamesContractResolver();
              setup.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
             
              setup.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
             
           
            })
            .AddXmlDataContractSerializerFormatters().ConfigureApiBehaviorOptions(setup =>
            {
                setup.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Type = "http://www.baidu.com",
                        Title = "有错误！！！",
                        Status = StatusCodes.Status422UnprocessableEntity,
                        Detail = "请看详细信息",
                        Instance = context.HttpContext.Request.Path
                    };

                    problemDetails.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);

                    return new UnprocessableEntityObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                };
            });
    
      services.AddDbContext<NetCoreApiContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("NetCoreApiContext")));
      /////////////////////增加aop扩展测试
      ///
      ////addSingleton :正如名字所示它可以在你的进程中保持着一个实例，也就是说仅有一次实例化，不信的话代码演示一下
      ///HomeController演示类  
      services.AddSingleton<IOrderService, OrderService>();
      ////正从名字所述：Scope 就是一个作用域，那在 webapi 或者 mvc 中作用域是多大呢？ 对的，就是一个请求，当然请求会穿透 Presentation, Application, Repository 等等各层，
      ///在穿层的过程中肯定会有同一个类的多次注入，那这些多次注入在这个作用域下维持的就是单例，如下代码所示：
      services.AddScoped<IOrderServiceb, OrderServiceb>();
      services.AddTransient<IOrderService, OrderService>();

    }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            ///https配置
          //  app.UseHttpsRedirection();
            ////UseSwaggerUI 和
            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Api");
            });


      //    app.UseAuthorization();
      //配置Cors
      app.UseCors("any");
      app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
