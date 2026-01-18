using GRC.Desktop.Blazor.Models;
using GRC.Desktop.Blazor.Configuration;
using System.Net.Http.Json;

namespace GRC.Desktop.Blazor.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public AuthService(HttpClient httpClient, ApiSettings apiSettings)
        {
            _httpClient = httpClient;
            _apiBaseUrl = apiSettings.BaseUrl;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/auth/register", request);
                return await response.Content.ReadFromJsonAsync<AuthResponse>() ?? new AuthResponse 
                { 
                    Success = false, 
                    Message = "Failed to register" 
                };
            }
            catch (Exception ex)
            {
                return new AuthResponse 
                { 
                    Success = false, 
                    Message = $"Registration error: {ex.Message}" 
                };
            }
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/auth/login", request);
                return await response.Content.ReadFromJsonAsync<AuthResponse>() ?? new AuthResponse 
                { 
                    Success = false, 
                    Message = "Failed to login" 
                };
            }
            catch (Exception ex)
            {
                return new AuthResponse 
                { 
                    Success = false, 
                    Message = $"Login error: {ex.Message}" 
                };
            }
        }

        public async Task<AuthResponse> VerifyOTPAsync(OTPVerificationRequest request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/auth/verify-otp", request);
                return await response.Content.ReadFromJsonAsync<AuthResponse>() ?? new AuthResponse 
                { 
                    Success = false, 
                    Message = "Failed to verify OTP" 
                };
            }
            catch (Exception ex)
            {
                return new AuthResponse 
                { 
                    Success = false, 
                    Message = $"OTP verification error: {ex.Message}" 
                };
            }
        }
    }
}
