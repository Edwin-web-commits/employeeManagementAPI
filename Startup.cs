using EmployeeManagementAPI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;
using EmployeeManagementAPI.Configurations;
using EmployeeManagementAPI.IRepository;
using EmployeeManagementAPI.Repository;
using EmployeeManagementAPI.Services;

namespace EmployeeManagementAPI
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
            services.AddDbContext<DatabaseContext>(options =>

                    options.UseSqlServer(Configuration.GetConnectionString("sqlConnection"))
               );

            //configuring Identity core framework and Authentication
            services.AddAuthentication();
            services.ConfigureIdentity(); //This method ConfigureIdentity() is in the ServiceExtentions.cs ,thats where the Identity Framework is configured
            services.ConfigureJWT(Configuration); // configuring JWT. The method ConfigureJWT() is in the ServiceExtensions.cs class 

            services.AddCors(o => {
                o.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });

            services.AddAutoMapper(typeof(MapperInitializer)); //configurating AutoMapper 
            
            services.AddTransient<IUnitOfWork, UnitOfWork>(); //AddTransient means a new instance of IUnitOfWork will be created whenever it is needed

            services.AddScoped<IAuthManager, AuthManager>();

           


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EmployeeManagementAPI", Version = "v1" });
            });

            services.AddControllers().AddNewtonsoftJson(op => op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "EmployeeManagementAPI v1");
            });

            app.UseCors("AllowAll"); //configure Cors
            app.UseRouting();

            app.ConfigureExceptionHandler(); //configure global error handler

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
