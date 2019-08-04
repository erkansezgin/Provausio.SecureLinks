﻿using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Provausio.SecureLink.Api.DependencyInjection;
using Provausio.SecureLink.Api.SecuredLinks.Models;
using Provausio.SecureLink.Application.Properties;
using RiskFirst.Hateoas;

namespace Provausio.SecureLink.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddServices();
            services.AddDatabases(Configuration);
            services.AddMediatR(
                typeof(ApplicationAnchor).Assembly, 
                typeof(Startup).Assembly);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.InstallSwagger();
            services.AddHateoas();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Swagger();
            app.UseMvc();
        }
    }
}
