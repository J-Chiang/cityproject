using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using DataTables.AspNet.AspNetCore;

namespace BackendCity
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
            services.AddDbContextPool<BackendDbContext>(
                options => options.UseMySql(BackendDbContext.ccnx,
                    mySqlOptions =>
                    {
                        mySqlOptions.ServerVersion(new Version(10, 2, 23), ServerType.MariaDb);
                    }
            ));

            services.AddMvc();

            services.RegisterDataTables();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP dTrequest pipeline.
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
