using Microsoft.Extensions.Logging;

namespace Pomoro;
using Plugin.Maui.Audio;
using Pomoro.Services.Interfaces;
using Pomoro.Services;


public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		// Inyectar el servicio AudioManager
		builder.Services.AddSingleton(AudioManager.Current);


		builder.Services.AddSingleton<IPlaySoundEndPomodoro, PlaySoundEndPomodoro>();
		builder.Services.AddSingleton<ITimerService, TimerService>();
		builder.Services.AddSingleton<IModoOperationService, ModoOperationService>();
        return builder.Build();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
