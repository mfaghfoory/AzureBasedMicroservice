using AzureBasedMicroservice.EntityFramework.DBContext;
using AzureBasedMicroservice.Shared.CQRS;
using CustomersService.Handlers;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace CustomersService
{
    public class Startup
    {
        private string projectName = "CustomersService";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, AzureBasedMicroserviceContext>();
            services.AddMvc().AddFluentValidation()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = projectName, Version = "v1" });
            });
            var massTransitConfig = Configuration.GetSection("MassTransitConfig").Get<MassTransitConfig>();
            services.AddSingleton(massTransitConfig);

            services.MassTransist(projectName, massTransitConfig,
                new List<Type> 
                {
                     typeof(AlterationFinishedHandler)
                });
            services.AddApplicationInsightsTelemetry();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", projectName);
            });
            app.UseMvc();
        }
    }
}
