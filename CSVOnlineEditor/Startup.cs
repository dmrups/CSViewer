using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.EntityFrameworkCore;
using CSVOnlineEditor.Models;
using CSVOnlineEditor.Interfaces;
using CSVOnlineEditor.Data.DBContexts;
using CSVOnlineEditor.Data.Repositories;
using CSVOnlineEditor.Data.DBConnectionProviders;
using CSVOnlineEditor.Serializers;
using CSVOnlineEditor.Builders;

namespace CSVOnlineEditor
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ICSVSerializer, CSVSerializer>();
            services.AddScoped<IBuilder<Applicant>, ApplicantBuilder>();
            services.AddScoped<IApplicantRepository, ApplicantRepository>();

            services.AddDbContext<Storage>(options => options.UseSqlServer(((new DefaultConnectionProvider()).GetConnection())));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context, next) =>
            {
                await next();

                if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
                {
                    context.Request.Path = "/index.html";
                    await next();
                }
            });

            app.UseStaticFiles();
            app.UseMvc(
                routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller}/{action}/{id?}");
                }
            );
        }
    }
}
