using server2.Services.UserServices.Adapters;
using server2.Services.UserServices.Ports;

namespace server2.Services.UserServices.UserModule
{
    public static class UserModuleInitializer
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}
