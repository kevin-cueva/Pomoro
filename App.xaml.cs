using Pomoro.Helpers;
using Pomoro.Domain.DTOS;
using Pomoro.Domain.Enums;
using Pomoro.Domain.Constants;
using Pomoro.Ui;
namespace Pomoro;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		MainPage = new TabBarUi();
		if (AppStorage.IsFirstRun())
		{
			AppStorage.SetFirstRunDone();
			// Guardar valores por defecto la primera vez
			var defaultPomodoro = new TimePomodoros();
			AppStorage.SavePomodoro(ModoPomodoro.PorDefecto, defaultPomodoro);
			AppStorage.SavePomodoro(ModoPomodoro.Flexible, defaultPomodoro);
			AppStorage.SavePomodoro(ModoPomodoro.Morning, defaultPomodoro);
			AppStorage.SavePomodoro(ModoPomodoro.Night, defaultPomodoro);
			AppStorage.SaveData(KeyStorage.CurrentMode, ModoPomodoro.PorDefecto.ToString());
		}
		else
		{
			// Cargar configuraciones guardadas (si es necesario)
			var config = AppStorage.GetTimePomodoros(ModoPomodoro.PorDefecto);
			Console.Write($"Datos:{config}");
		}
	}

	

}