using server.Services.UserServices.Adapters;
using server.Services.UserServices.Contracts;

namespace server.Services.UserServices.UserModule
{
    public static class UserModuleInitializer
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserFacade, UserFacade>();
        }
    }
}
