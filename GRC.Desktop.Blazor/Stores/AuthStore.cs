using GRC.Desktop.Blazor.Models;

namespace GRC.Desktop.Blazor.Stores
{
    public class AuthStore
    {
        private UserSession? _currentSession;
        private readonly object _lockObject = new();

        public event Action? OnSessionChanged;

        public UserSession? CurrentSession
        {
            get
            {
                lock (_lockObject)
                {
                    return _currentSession;
                }
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                lock (_lockObject)
                {
                    return _currentSession != null && _currentSession.ExpiryTime > DateTime.UtcNow;
                }
            }
        }

        public void SetSession(UserSession session)
        {
            lock (_lockObject)
            {
                _currentSession = session;
                OnSessionChanged?.Invoke();
            }
        }

        public void ClearSession()
        {
            lock (_lockObject)
            {
                _currentSession = null;
                OnSessionChanged?.Invoke();
            }
        }

        public string? GetToken()
        {
            lock (_lockObject)
            {
                return _currentSession?.Token;
            }
        }

        public bool IsTokenExpired()
        {
            lock (_lockObject)
            {
                return _currentSession == null || _currentSession.ExpiryTime <= DateTime.UtcNow;
            }
        }
    }
}
