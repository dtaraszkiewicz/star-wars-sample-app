﻿using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StarWarsSampleApp.Application.Episodes.Commands.CreateEpisode;
using StarWarsSampleApp.Application.Episodes.Queries.GetEpisodes;
using StarWarsSampleApp.Application.Infrastructure;
using StarWarsSampleApp.Application.Infrastructure.Automapper;
using StarWarsSampleApp.Persistence;
using Swashbuckle.AspNetCore.Swagger;

namespace StarWarsSampleApp.Api
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
            // Add Swagger
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Star Wars Sample API"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);
            });

            // Add AutoMapper
            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });

            //Add DbContext using SQL Server Provider
            services.AddDbContext<StarWarsSampleAppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("StarWarsSampleAppDatabase")));

            //Add MediatR registering all handlers in given assembly 
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddMediatR(typeof(GetEpisodesQueryHandler).Assembly.GetTypes());

            services.AddMvc(
                    config =>
                    {
                        config.Filters.Add(typeof(ExceptionHandler));
                    })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CreateEpisodeCommandValidator>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(x => { x.SwaggerEndpoint("/swagger/v1/swagger.json", "Star Wars Sample API"); });
        }
    }
}
