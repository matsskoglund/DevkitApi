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
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

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
            //ConnectionString = Configuration.GetConnectionString("DefaultConnection");
            ConnectionString = Configuration.GetConnectionString("DBCONNECTION");
            ConnectionString = Environment.GetEnvironmentVariable("DBCONNECTION");
            _logger.LogInformation("DBCONNECTION: " + Environment.GetEnvironmentVariable("DBCONNECTION"));
            _logger.LogInformation("ConnectionString: " + ConnectionString);
        }

        public static string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<Startup>();

            IConfigurationRoot Configuration = builder.Build();
            //ConnectionString = Configuration.GetConnectionString("DefaultConnection");
            ConnectionString = Environment.GetEnvironmentVariable("DBCONNECTION");
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

            if (_env.IsDevelopment())
            {
                _logger.LogDebug("Environment is Development");

                //services.AddDbContext<DevkitContext>(options => options.UseInMemoryDatabase(databaseName: "database"));
                services.AddDbContext<DevkitContext>(options =>options.UseSqlite("Data Source=devkit.db"));
                // services.AddDbContext<DevkitContext>(options => options.UseSqlite("DataSource =:memory:"));

            }
            if (_env.IsStaging())
            {
                _logger.LogDebug("Environment is Staging");

                //services.AddDbContext<DevkitContext>(options => options.UseInMemoryDatabase(databaseName: "database"));
                //services.AddDbContext<DevkitContext>(options => options.UseSqlite("Data Source=devkit.db"));
                // services.AddDbContext<DevkitContext>(options => options.UseSqlite("DataSource =:memory:"));
                services.AddDbContext<DevkitContext>(options => options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));
            }
            else
            {
                _logger.LogDebug("Environment is Production");
                services.AddDbContext<DevkitContext>(options => options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));
            }            
            services.AddTransient<IDevkitService, DevkitService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                    {
                        Title = "DevkitAPI",
                        Version = "v1",
                        Description = "The API for Devkits",
                        Contact = new Contact { Name = "Mats Skoglund", Email = "mats.skoglund@scania.com", Url = "https://devtools.scania.com"}
                });
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "DevkitApi.xml");
                c.IncludeXmlComments(xmlPath);
            });

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DevkitApi V1"));
            app.UseCors("AllowAll");
            app.UseMvc();
            
        }
    }
}
