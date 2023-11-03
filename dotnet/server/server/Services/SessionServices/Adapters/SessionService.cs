using server.Services.SessionServices.Contracts;

namespace server.Services.SessionServices.Adapters
{
    public class SessionService : ISessionService
    {
        private SessionService() { }

        private static readonly object _lock = new();
        private static SessionService? _instance;
        private string? SessionUuid;
        private Dictionary<string, object>? Session;

        public static SessionService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new SessionService
                            {
                                SessionUuid = Guid.NewGuid().ToString(),
                                Session = new Dictionary<string, object>()
                            };
                        }
                    }
                }
                return _instance;
            }
        }

        public string? GetSessionUuid
        {
            get
            {
                if (_instance != null)
                {
                    return _instance.SessionUuid;
                }
                return null;
            }
        }

        public void SetSession(string key, object value)
        {            
            lock (_lock)
            {
                if (_instance != null && _instance.Session != null)
                {
                    _instance.Session[key] = value;
                }
            }
        }

        public object? GetSession(string key)
        {
            if (
                _instance != null &&
                _instance.Session != null &&
                _instance.Session.ContainsKey(key)
            ) {
                return _instance.Session[key];
            }
            return null;
        }

        public void EndSession(string key)
        {
            if (
                _instance != null &&
                _instance.Session != null &&
                _instance.Session.ContainsKey(key)
            ) {
                _instance.Session.Remove(key);
            }
        }
    }
}
