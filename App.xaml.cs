namespace Pomoro;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
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