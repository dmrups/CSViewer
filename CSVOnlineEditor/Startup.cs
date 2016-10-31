using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CSVOnlineEditor.Models;
using CSVOnlineEditor.Interfaces;
using CSVOnlineEditor.Data.DBContexts;
using CSVOnlineEditor.Data.Repositories;
using CSVOnlineEditor.Serializers;
using CSVOnlineEditor.Builders;
using CSVOnlineEditor.Parsers;
using CSVOnlineEditor.Helpers;
using CSVOnlineEditor.ObjectAccessors;

namespace CSVOnlineEditor
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IValueSerializer, ValueSerializer>();
            services.AddScoped<IValueParser, ValueParser>();
            services.AddScoped<ICSVSerializer, CSVSerializer>();
            services.AddScoped<IBuilder<Applicant>, ApplicantBuilder>();
            services.AddScoped<IAccessor<Applicant>, ApplicantAccessor>();
            services.AddScoped<IApplicantRepository, ApplicantRepository>();

            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            services.AddSingleton(configurationBuilder);

            var connectionString = configurationBuilder.Build()["ConnectionStrings:DefaultConnection"];

            services.AddDbContext<Storage>(options => options.UseSqlServer((connectionString)));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
            ILoggerFactory loggerFactory, Storage storage, 
            IValueParser valueParser, IValueSerializer valueSerializer)
        {
            storage.Database.Migrate();

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

            // Fill ServicesHelper
            ServicesHelper.Parser = valueParser;
            ServicesHelper.Serializer = valueSerializer;
        }
    }
}
