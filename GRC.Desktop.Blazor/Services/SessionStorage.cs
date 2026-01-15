namespace GRC.Desktop.Blazor.Services
{
    /// <summary>
    /// In-memory session storage for OTP and temporary data during authentication flow
    /// </summary>
    public static class SessionStorage
    {
        private static readonly Dictionary<string, object> _storage = new();
        private static readonly object _lockObject = new();

        public static string SessionId
        {
            get => Get<string>("sessionId") ?? string.Empty;
            set => Set("sessionId", value);
        }

        public static void Set<T>(string key, T value) where T : class
        {
            lock (_lockObject)
            {
                if (value == null)
                {
                    _storage.Remove(key);
                }
                else
                {
                    _storage[key] = value;
                }
            }
        }

        public static T? Get<T>(string key) where T : class
        {
            lock (_lockObject)
            {
                if (_storage.TryGetValue(key, out var value))
                {
                    return value as T;
                }
                return null;
            }
        }

        public static void Clear()
        {
            lock (_lockObject)
            {
                _storage.Clear();
            }
        }

        public static void Remove(string key)
        {
            lock (_lockObject)
            {
                _storage.Remove(key);
            }
        }
    }
}
