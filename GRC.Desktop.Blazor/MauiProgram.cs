using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using GRC.Desktop.Blazor.Services;
using GRC.Desktop.Blazor.Stores;
using GRC.Desktop.Blazor.Configuration;

namespace GRC.Desktop.Blazor;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		
		// Load configuration from appsettings.json
		var config = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			.Build();

		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();

		// Get API settings from configuration
		var apiSettings = new ApiSettings();
		config.GetSection("ApiSettings").Bind(apiSettings);

		// Register API settings as singleton
		builder.Services.AddSingleton(apiSettings);

		// Register services
		builder.Services.AddScoped<AuthService>();
		builder.Services.AddSingleton<AuthStore>();
		builder.Services.AddScoped(sp =>
		{
			var settings = sp.GetRequiredService<ApiSettings>();
			return new HttpClient
			{
				BaseAddress = new Uri(settings.BaseUrl),
				Timeout = TimeSpan.FromSeconds(settings.Timeout)
			};
		});

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
