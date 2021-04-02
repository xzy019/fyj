using Counselor.IRepository;
using Counselor.IService;
using Counselor.Repository;
using Counselor.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SqlSugar.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Counselor_.WebApi
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Counselor_.WebApi", Version = "v1" });
            });
            #region sqlsugarioc
            services.AddSqlSugar(new IocConfig()
            {
                ConnectionString = this.Configuration["SqlConn"],
                DbType = IocDbType.MySql,
                IsAutoCloseConnection = true//自动释放
            });
            #endregion
            #region IOC依赖注入
            services.AddCustomIOC();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Counselor_.WebApi v1"));
            }

            app.UseCors(builder =>
            {
                //WithOrigins方法：用于配置允许跨域访问的源
                builder.WithOrigins("http://localhost:8080").AllowAnyHeader().AllowAnyMethod();
            });

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();//鉴权

            app.UseAuthorization();//授权

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
    public static class IOCExtend
    {
        public static IServiceCollection AddCustomIOC(this IServiceCollection services)
        {
            services.AddScoped<IWorkInfoRepository, WorkInfoRepository>();
            services.AddScoped<IWorkInfoService, WorkInfoService>();
            services.AddScoped<IAdminInfoRepository, AdminInfoRepository>();
            services.AddScoped<IAdminInfoService, AdminInfoService>();
            services.AddScoped<ICounselorInfoRepository, CounselorInfoRepository>();
            services.AddScoped<ICounselorInfoService, CounselorInfoService>();
            services.AddScoped<IRoadInfoRepository, RoadInfoRepository>();
            services.AddScoped<IRoadInfoService, RoadInfoService>();
            services.AddScoped<INoticeInfoRepository, NoticeInfoRepository>();
            services.AddScoped<INoticeInfoService, NoticeInfoService>();
            return services;
        }

        public static IServiceCollection AddCustomJWT(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SDMC-CJAS1-SAD-DFSFA-SADHJVF-VF")),
                    ValidateIssuer = true,
                    ValidIssuer = "http://localhost:6060",
                    ValidateAudience = true,
                    ValidAudience = "http://localhost:5000",
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(60)
                };
            });
            return services;
        }
    }
}
