# GRC Desktop Application

A Windows desktop application for Governance, Risk, and Compliance (GRC) management built with .NET MAUI Blazor Hybrid.

## Features

- **Authentication System**: Secure login with OTP verification
- **Role-Based Access**: Multiple user roles (Super Admin, Admin, Approver, etc.)
- **Desktop UI**: Native Windows application with web-based UI
- **API Integration**: Communicates with ASP.NET Core API backend
- **Secure Storage**: Persistent session management using platform secure storage

## Architecture

- **Frontend**: .NET MAUI Blazor Hybrid (Windows EXE)
- **Backend**: ASP.NET Core API (separate service)
- **Communication**: HTTP REST API calls
- **Storage**: Secure platform storage for sessions
- **UI Pattern**: MVVM-friendly design

## Project Structure

```
GRC.Desktop.Blazor/
├── Components/
│   ├── Layout/
│   │   ├── MainLayout.razor
│   │   └── NavMenu.razor
│   └── Pages/
│       ├── Dashboard.razor
│       ├── Login.razor
│       ├── Register.razor
│       └── VerifyOTP.razor
├── Models/
│   ├── AuthResponse.cs
│   ├── LoginRequest.cs
│   ├── OrganizationType.cs
│   ├── OTPVerificationRequest.cs
│   ├── RegisterRequest.cs
│   ├── RoleType.cs
│   └── UserSession.cs
├── Services/
│   ├── AuthService.cs
│   ├── SecureStorageService.cs
│   └── SessionStorage.cs
├── Stores/
│   └── AuthStore.cs
├── wwwroot/css/
│   └── app.css
├── appsettings.json
├── MauiProgram.cs
└── GRC.Desktop.Blazor.csproj
```

## API Backend

**Note**: The ASP.NET Core API backend (`Agrc.Api`) has been temporarily removed from the solution. It will be added later when the authentication endpoints are implemented.

The application expects the following API endpoints:
- `POST /api/auth/register` - User registration
- `POST /api/auth/login` - User login
- `POST /api/auth/verify-otp` - OTP verification

## User Roles

- Management – Approver (2 approvers required)
- Super Admin
- Admin
- Approver
- Checker / Reviewer
- Maker
- Process Owner
- Control Owner
- Risk Owner

## Building and Running

### Prerequisites

- .NET 8.0.13 SDK or later
- .NET MAUI workload installed
- Windows 10 version 1903 (19H1) or later (build 18362) or Windows 11

### Build Commands

```bash
# Build the solution
dotnet build GRC.sln

# Run the desktop application
dotnet run --project GRC.Desktop.Blazor/GRC.Desktop.Blazor.csproj --framework net8.0-windows10.0.19041.0

# Publish as single EXE
dotnet publish GRC.Desktop.Blazor/GRC.Desktop.Blazor.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -f net8.0-windows10.0.19041.0
```

### API Backend

The application requires a running ASP.NET Core API backend. Update the `appsettings.json` file with the correct API base URL.

## Configuration

Update `appsettings.json` to configure the API endpoint:

```json
{
  "ApiSettings": {
    "BaseUrl": "https://your-api-url"
  }
}
```

## Security Features

- Secure token storage using platform secure storage
- Session expiration handling
- OTP verification for enhanced security
- No direct database access from client

## Packaging

The application can be packaged as a single Windows EXE file using the publish command above, making it suitable for on-premise deployment without IIS or browser dependencies.