using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Services.SessionServices.Adapters;
using server.Services.SessionServices.Contracts;
using server.Services.UserServices.Authorization.Basic;
using server.Services.UserServices.Context;
using server.Services.UserServices.UserModule;

namespace server.Startup
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

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();

            services.AddMvc().ConfigureApiBehaviorOptions(options =>
            {
                // Suppress the default ModelStateInvalidFilter
                options.SuppressModelStateInvalidFilter = true;

                // Define a custom response for invalid model state
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var modelState = actionContext.ModelState.Values;
                    return new BadRequestObjectResult(modelState);
                };
            });

            services.AddDbContext<UserContext>(opt =>
            {
                opt.UseSqlite(
                    Configuration.GetConnectionString("DefaultConnection")
                    ??
                    "Data Source=./Database/Database.db");
            });
            
            services.AddCors(opt =>
            {
                opt.AddPolicy("AllowLocalhost3001",
                    builder => builder.WithOrigins("http://localhost:3001").AllowAnyMethod().AllowAnyHeader());
            });

            services.AddSingleton<ISessionServiceFactory, SessionServiceFactory>();

            services
                .AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, AuthorizationHandler>("BasicAuthentication", null);

            UserModuleInitializer.Initialize(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();

                app.UseSwagger();

                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("AllowLocalhost3001");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
