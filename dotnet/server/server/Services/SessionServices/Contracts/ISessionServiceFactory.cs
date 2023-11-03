namespace server.Services.SessionServices.Contracts
{
    public interface ISessionServiceFactory
    {
        ISessionService GetSessionService();
    }
}
