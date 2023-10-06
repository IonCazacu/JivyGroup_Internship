using server.Services.UserServices.Adapters;
using server.Services.UserServices.Ports;

namespace server.Services.UserServices.UserModule
{
    public static class UserModuleInitializer
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}
