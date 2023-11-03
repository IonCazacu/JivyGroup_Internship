using server.Services.SessionServices.Contracts;

namespace server.Services.SessionServices.Adapters
{
    public class SessionServiceFactory : ISessionServiceFactory
    {
        private readonly SessionService _sessionServiceInstance = SessionService.Instance;

        public ISessionService GetSessionService()
        {
            return _sessionServiceInstance;
        }
    }
}
