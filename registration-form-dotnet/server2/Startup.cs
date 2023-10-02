using Microsoft.EntityFrameworkCore;
using server2.Services.UserServices.Data;
using server2.Services.UserServices.UserModule;

namespace server2.Startup
{
    public class StartupClass
    {
        public IConfiguration Configuration { get; }
        public StartupClass(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext < UserContext > (opt => {
                opt.UseSqlite(Configuration.GetConnectionString
                    ("DefaultConnection") ?? "Data Source=./Database/Database.db");
            });
            services.AddCors(opt => {
                opt.AddPolicy("AllowLocalhost3000",
                    builder => builder
                    .WithOrigins("http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            UserModuleInitializer.Initialize(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors("AllowLocalhost3000");
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
