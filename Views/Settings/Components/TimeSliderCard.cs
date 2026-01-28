using System.Drawing;
using System.Reflection.Metadata;
using Microsoft.Maui.Controls.Shapes;
using Pomoro.Domain.DTOS;
using Pomoro.Helpers;
using Pomoro.ViewModels.Setting;

namespace Pomoro.Views.Settings.Components;

public class TimeSliderCard : ContentView
{
	private readonly List<Border> contenedorPrincipal;
	public TimeSliderCardViewModel ViewModel { get; set; }

	public TimeSliderCard()
	{
		contenedorPrincipal = [];
		ViewModel = new TimeSliderCardViewModel();
		BuildCards();
	}

	private void BuildCards()
	{
		contenedorPrincipal.Clear();
		
		if (ViewModel.SlidersData == null || ViewModel.SlidersData.Count == 0)
		{
			Content = new VerticalStackLayout
			{
				Children = {
					new Label
					{
						Text = "No hay datos para mostrar.",
						FontSize = 16,
						HorizontalOptions = LayoutOptions.Center,
						VerticalOptions = LayoutOptions.Center
					}
				}
			};
			return;
		}

		foreach (var sliderData in ViewModel.SlidersData)
		{
			var card = CreateSliderCard(sliderData);
			contenedorPrincipal.Add(card);
		}

		var mainLayout = new VerticalStackLayout();
		foreach (var child in contenedorPrincipal)
		{
			mainLayout.Children.Add(child);
		}

		Content = mainLayout;
	}

	private Border CreateSliderCard(SliderDto sliderData)
	{
		#region Primer Componente: Icono, Titulo y Tiempo Seleccionado
		var iconoTipoSlider = new Image
		{
			WidthRequest = 24,
			HeightRequest = 24,
			Source = sliderData?.Icono ?? Domain.Constants.Constants.Icons.BookTab,
		};
		
		var tituloSlider = new Label
		{
			Text = sliderData?.TipoAjuste ?? "Trabajo",
			FontSize = 18,
			VerticalOptions = LayoutOptions.Center
		};

		var tiempoSeleccionado = new Label
		{
			Text = $"{(int)(sliderData?.ValorActual ?? 25)} min",
			FontSize = 16,
			VerticalOptions = LayoutOptions.Center,
			HorizontalOptions = LayoutOptions.End
		};

		var selectorTiempo = new Slider
		{
			Minimum = sliderData?.MinValue ?? 1,
			Maximum = sliderData?.MaxValue ?? 60,
			Value = sliderData?.ValorActual ?? 25,
			HorizontalOptions = LayoutOptions.Fill
		};

		// Actualizar el label cuando cambia el slider
		selectorTiempo.ValueChanged += (s, e) =>
		{
			tiempoSeleccionado.Text = $"{(int)e.NewValue} min";
		};

		var primerComponente = new HorizontalStackLayout
		{
			Spacing = 10,
			Children = { iconoTipoSlider, tituloSlider, tiempoSeleccionado }
		};
		#endregion

		#region Tercer Componente: Labels Minimo, Maximo
		var minimoTiempoLabel = new Label
		{
			Text = $"{sliderData?.MinValue ?? 1} min",
			FontSize = 14,
			HorizontalOptions = LayoutOptions.Start
		};
		
		var maximoTiempoLabel = new Label
		{
			Text = $"{sliderData?.MaxValue ?? 60} min",
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

		tercerComponente.Add(minimoTiempoLabel, 0, 0);
		tercerComponente.Add(maximoTiempoLabel, 1, 0);
		#endregion

		#region Cuarto Componente: Sufijo Descripcion
		var sufijoDescripcion = new Label
		{
			Text = sliderData?.SufijoDescripcion ?? "min",
			FontSize = 12,
			HorizontalOptions = LayoutOptions.Start
		};
		
		if (string.IsNullOrEmpty(sufijoDescripcion.Text))
		{
			sufijoDescripcion.IsVisible = false;
		}
		#endregion

		var contenedorInterno = new VerticalStackLayout
		{
			Spacing = 10,
			Children = 
			{
				primerComponente,
				selectorTiempo,
				tercerComponente,
				sufijoDescripcion,
			}
		};

		return new Border
		{
			Stroke = Colors.LightGray,
			StrokeThickness = 1,
			Background = new SolidColorBrush(Colors.White),
			Padding = new Thickness(15),
			StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(12) },
			Margin = new Thickness(0, 10),
			Content = contenedorInterno
		};
	}

	public void UpdateSlidersData(List<SliderDto> newData, Pomoro.Domain.Enums.ModoPomodoro modoPomodoro)
	{
		ViewModel.UpdateSlidersData(newData, modoPomodoro);
		BuildCards();
	}

	public List<SliderDto> GetCurrentSlidersData()
	{
		return ViewModel.GetCurrentSlidersData();
	}	
}