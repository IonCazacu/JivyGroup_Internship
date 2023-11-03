namespace server.Services.SessionServices.Contracts
{
    public interface ISessionService
    {
        public string? GetSessionUuid { get; }
        public void SetSession(string key, object value);
        public object? GetSession(string key);
        public void EndSession(string key);
    }
}
