namespace GRC.Desktop.Blazor.Configuration
{
    /// <summary>
    /// Configuration settings for API connections.
    /// </summary>
    public class ApiSettings
    {
        /// <summary>
        /// Gets or sets the base URL for the API.
        /// </summary>
        public string BaseUrl { get; set; } = "https://localhost:7001/api";

        /// <summary>
        /// Gets or sets the HTTP request timeout in seconds.
        /// </summary>
        public int Timeout { get; set; } = 30;
    }
}
