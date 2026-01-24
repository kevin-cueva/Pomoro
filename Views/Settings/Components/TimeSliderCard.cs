using System.Drawing;
using System.Reflection.Metadata;
using Microsoft.Maui.Controls.Shapes;

namespace Pomoro.Views.Settings.Components;

public class TimeSliderCard : ContentView
{
	private readonly Border ContenedorPrincipal;
	private readonly Image IconoTipoSlider;
	private readonly Label TituloSlider;
	private readonly Label TiempoSeleccionado;
	private readonly Label MinimoTiempoLabel;
	private readonly Label MaximoTiempoLabel;
	private readonly Label SufijoDescripcion;
	private readonly Slider SelectorTiempo;

	public TimeSliderCard()
	{

		#region Primer Componente: Icono, Titulo y Tiempo Seleccionado
		IconoTipoSlider = new Image
		{
			WidthRequest = 24,
			HeightRequest = 24,
			Source = Domain.Constants.Constants.Icons.BookTab, // Asegúrate de tener este recurso en tu proyecto
		};
		TituloSlider = new Label
		{
			Text = "Duración del Pomodoro",
			FontSize = 18,
			VerticalOptions = LayoutOptions.Center
		};

		TiempoSeleccionado = new Label
		{
			Text = "25 min",
			FontSize = 16,
			VerticalOptions = LayoutOptions.Center,
			HorizontalOptions = LayoutOptions.End
		};
		HorizontalStackLayout primerComponente = new()
		{
			Spacing = 10,
			Children =
			{
				IconoTipoSlider,
				TituloSlider,
				TiempoSeleccionado
			}
		};
		#endregion

		#region Segundo COmponente: Slider
		SelectorTiempo = new Slider
		{
			Minimum = 1,
			Maximum = 60,
			Value = 25,
			HorizontalOptions = LayoutOptions.Center
		};
		#endregion

		#region Tercer Componente: Labels Minimo, Maximo
		MinimoTiempoLabel = new Label
		{
			Text = "1 min",
			FontSize = 14,
			HorizontalOptions = LayoutOptions.Start
		};
		MaximoTiempoLabel = new Label
		{
			Text = "60 min",
			FontSize = 14,
			HorizontalOptions = LayoutOptions.End
		};

		var tercerComponente = new Grid
		{
			ColumnDefinitions =
			{
				new ColumnDefinition { Width = GridLength.Star },
				new ColumnDefinition { Width = GridLength.Auto }
			}
		};

		tercerComponente.Add(MinimoTiempoLabel, 0, 0);
		tercerComponente.Add(MaximoTiempoLabel, 1, 0);
		#endregion

		#region Cuarto Componente: Sufijo Descripcion
		SufijoDescripcion = new Label
		{
			Text = "Ajusta la duración de tu sesión de trabajo.",
			FontSize = 12,
			HorizontalOptions = LayoutOptions.Start
		};
		#endregion
		VerticalStackLayout ContenedorInterno = new()
		{
			Spacing = 10
		};
		ContenedorInterno.Children.Add(primerComponente);
		ContenedorInterno.Children.Add(SelectorTiempo);
		ContenedorInterno.Children.Add(tercerComponente);
		ContenedorInterno.Children.Add(SufijoDescripcion);

		ContenedorPrincipal = new Border
		{
			Stroke = Colors.LightGray,
			StrokeThickness = 1,
			Background = new SolidColorBrush(Colors.White),
			Padding = new Thickness(15),
			StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(12) },
			Margin = new Thickness(0, 10),
			Content = ContenedorInterno
		};


		Content = new VerticalStackLayout
		{
			Children = {
				ContenedorPrincipal

				}
		};
	}
}
