using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Banking.Operation.Transfer.Command.CrossCutting.Ioc;
using Newtonsoft.Json;

namespace Banking.Operation.Transfer.Command.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IHostEnvironment env)
        {
            var environmentName = env.EnvironmentName;
            var builder = new ConfigurationBuilder()
                 .SetBasePath(env.ContentRootPath)
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                 .AddJsonFile($"appsettings.{(string.IsNullOrEmpty(environmentName) ? "Development" : environmentName)}.json", optional: true, reloadOnChange: true)
                 .AddEnvironmentVariables();

            _configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                    .AddNewtonsoftJson(jsonOptions =>
             {
                 jsonOptions.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                 jsonOptions.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                 jsonOptions.UseCamelCasing(true);
             });
            services.ConfigureContainer(_configuration);
            services.AddApiVersioning(options => options.ReportApiVersions = true);
            services.AddVersionedApiExplorer(options =>
                     {
                         options.GroupNameFormat = "'v'VVV";
                         options.SubstituteApiVersionInUrl = true;
                     });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Banking.Operation.Transfer.Command.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Banking.Operation.Transfer.Command.Api v1"));
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
