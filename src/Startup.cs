using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreCodeCamp.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Versioning;
using CoreCodeCamp.Controllers;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;

namespace CoreCodeCamp
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
          services.AddDbContext<CampContext>();
          services.AddScoped<ICampRepository, CampRepository>();
            services.AddApiVersioning(opt =>
            {
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 1);
                opt.ReportApiVersions = true;
                //opt.ApiVersionReader = new QueryStringApiVersionReader(new string[] { "ver" });
                opt.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader("X-Version"),
                    new QueryStringApiVersionReader(new string[] { "ver" }));

                opt.Conventions.Controller<TalksController>()
                .HasApiVersion(new ApiVersion(1, 0))
                .HasApiVersion(new ApiVersion(1, 1))
                .Action(c => c.Delete(default(string), default(int)))
                .MapToApiVersion(1, 1);
            });




         services.AddAutoMapper(typeof(Startup));
          services.AddMvc(opt => opt.EnableEndpointRouting = false)
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
          if (env.IsDevelopment())
          {
            app.UseDeveloperExceptionPage();
          }
      
          app.UseMvc();
    }
  }
}
