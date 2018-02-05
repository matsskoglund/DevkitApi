using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DevkitApi.Model;
using Microsoft.EntityFrameworkCore;
using DevkitApi.Services;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace DevkitApi
{
    public class Startup
    {
        public static string ConnectionString { get; private set; }
        private readonly ILogger<Startup> _logger;
        IHostingEnvironment _env;
        public Startup(IHostingEnvironment env, ILogger<Startup> logger, IConfiguration configuration)
        {
            _logger = logger;
            _env = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
            ConnectionString = Configuration.GetConnectionString("DefaultConnection");
            _logger.LogInformation("ConnectionString: " + ConnectionString);
        }

        public static string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<Startup>();

            IConfigurationRoot Configuration = builder.Build();
            ConnectionString = Configuration.GetConnectionString("DefaultConnection");
            System.Console.WriteLine("GetConnectionString: " + ConnectionString);
            return ConnectionString;
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(
         options => options.AddPolicy(
             "AllowAll", p => p.AllowAnyOrigin()
                 .AllowAnyHeader()
                 .AllowAnyMethod()
                 .AllowCredentials()
             )
         );


            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            if (_env.IsDevelopment() || _env.IsStaging())
            {
                
                //services.AddDbContext<DevkitContext>(options => options.UseInMemoryDatabase(databaseName: "database"));
                services.AddDbContext<DevkitContext>(options =>options.UseSqlite("Data Source=devkit.db"));
            }
            else
            {
                services.AddDbContext<DevkitContext>(options => options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));
            }            
            services.AddTransient<IDevkitService, DevkitService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseCors("AllowAll");
            app.UseMvc();
        }
    }
}
