namespace server.Services.UserServices.Session
{
    public class Singleton
    {
        private Singleton() { }
        private static readonly object _lock = new();
        private static Singleton? _instance;
        private string? SessionUuid;
        private Dictionary<string, object>? Session;

        public static Singleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new Singleton
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

        // Get Session Uuid
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
            if (_instance != null && _instance.Session != null && _instance.Session.ContainsKey(key))
            {
                return _instance.Session[key];
            }
            return null;
        }

        public void EndSession(string key)
        {
            if (_instance != null && _instance.Session != null && _instance.Session.ContainsKey(key))
            {
                _instance.Session.Remove(key);
            }
        }
    }
}
