using Pomoro.Helpers;
using Pomoro.Domain.DTOS;
using Pomoro.Domain.Enums;
using Pomoro.Domain.Constants;
namespace Pomoro;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		if (AppStorage.IsFirstRun())
		{
			AppStorage.SetFirstRunDone();
			// Guardar valores por defecto la primera vez
			var defaultPomodoro = new TimePomodoros();
			AppStorage.SavePomodoro(ModoPomodoro.Automatic, defaultPomodoro);
			AppStorage.SavePomodoro(ModoPomodoro.Manual, defaultPomodoro);
			AppStorage.SavePomodoro(ModoPomodoro.Morning, defaultPomodoro);
			AppStorage.SavePomodoro(ModoPomodoro.Night, defaultPomodoro);
			AppStorage.SaveData(KeyStorage.CurrentMode, ModoPomodoro.Automatic.ToString());
		}
		else
		{
			// Cargar configuraciones guardadas (si es necesario)
			var config = AppStorage.GetTimePomodoros(ModoPomodoro.Automatic);
			Console.Write($"Datos:{config}");
		}
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new Views.Home.HomePrincipal())
		{
			// Puedes establecer propiedades adicionales de la ventana aquí si es necesario.
			// Por ejemplo, puedes establecer el título de la ventana:
			Title = "Pomoro"
		};
	}

}