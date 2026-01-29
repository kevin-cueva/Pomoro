using System.Collections.ObjectModel;
using Microsoft.Maui.Controls.Shapes;
using Pomoro.Domain.Constants;
using Pomoro.Domain.DTOS;
using Pomoro.Domain.Enums;
using Pomoro.Helpers;
using Pomoro.Views.Settings.Components;
namespace Pomoro.Views.Settings;

public class Settings : ContentPage
{
	private CollectionView collectionView;
	private readonly ObservableCollection<PomodoroItemDto> itemsSource;
	public Settings()
	{
		var titulo = new Label
		{
			Text = "Configuración Pomodoro",
			FontSize = 22,
			FontAttributes = FontAttributes.Bold,
			HorizontalOptions = LayoutOptions.Start,
			Margin = new Thickness(10, 20, 0, 10) // (izquierda, arriba, derecha, abajo)
		};
		itemsSource = ObservableListaPomodoros();
		CollectionViewComponent();

		
		// Envolver en un ScrollView con layout contenedor
		Content = new ScrollView
		{
			Content = new VerticalStackLayout
			{
				Children = { titulo, collectionView },
				// El padding seguro se aplica a la página, y el layout lo respeta
			}
		};
	}

	private void CollectionViewComponent()
	{
		PomodoroSettingsControl pomodoroSettingsControl = new();
		
		// Inicializar la fuente de datos
		collectionView = new CollectionView
		{
			// Datos de ejemplo
			ItemsSource = itemsSource,
			SelectionMode = SelectionMode.None, // Deshabilitar selección
			ItemTemplate = new DataTemplate(static () =>
			{
				// Crear el contenedor principal de cada fila con bordes redondeados
				var rowContainer = new Border
				{
					Stroke = Color.FromArgb("#E5E5E5"),
					StrokeThickness = 1,
					Background = new SolidColorBrush(Colors.White), // ✅ Esto es como se define el fondo en Border
					Padding = new Thickness(15),
					StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(12) }, // ✅ Define bordes redondeados
					Margin = new Thickness(10, 5)
				};
				var nameLabel = new Label { FontAttributes = FontAttributes.Bold };
				var itemPomodoroSettingsControl = new PomodoroSettingsControl()
				{
					Pomodoro = PomodoroItemDtoNamed(nameLabel.Text ?? string.Empty)!,
					IsVisible = false
				};
				
				// Agregar evento de clic al contenedor
				var tapGesture = new TapGestureRecognizer();
				tapGesture.Tapped += (s, e) =>
				{
					// Manejar el clic del contenedor
					itemPomodoroSettingsControl.Pomodoro = PomodoroItemDtoNamed(nameLabel.Text ?? string.Empty)!;
					itemPomodoroSettingsControl.IsVisible = !itemPomodoroSettingsControl.IsVisible;
					
				};
				rowContainer.GestureRecognizers.Add(tapGesture);

				

				var stackLayout = new VerticalStackLayout
				   {
					   Children = { nameLabel, itemPomodoroSettingsControl },
					   Padding = new Thickness(10)
				   };

				// Enlazar las propiedades del modelo a los controles
				nameLabel.SetBinding(Label.TextProperty, nameof(PomodoroItemDto.NombreModo));
				rowContainer.Content = stackLayout;

				return rowContainer;
			})
			
		};
		

		// Opcional: manejar selección
		//collectionView.SelectionMode = SelectionMode.Single;
		//collectionView.SelectionChanged += OnSelectionChanged;
	}

	#region EVENTOS
	private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (e.CurrentSelection.FirstOrDefault() is PomodoroItemDto selectedPerson)
		{
			
			DisplayAlert("Seleccionado", $"Has seleccionado a {selectedPerson.NombreModo}", "OK");
		}
	}
	#endregion

	/// <summary>
	/// Obtiene la lista de Pomodoros desde el almacenamiento.
	/// </summary>
	/// <returns></returns>
	private ObservableCollection<PomodoroItemDto> ObservableListaPomodoros()
	{
		var listaPomodoros = ObtenerListaTimePomodoros();

		return new ObservableCollection<PomodoroItemDto>(listaPomodoros);
	}
	private static List<PomodoroItemDto> ObtenerListaTimePomodoros()
	{
		var timePomodoros = AppStorage.GetAllTimePomodoros();

		var listaPomodoros = timePomodoros
		.Select(kvp => new PomodoroItemDto
			{
				NombreModo = Utils.NombreModoPomodoro(kvp.Key),
				Tiempos = kvp.Value
			})
			.ToList();
			

		return listaPomodoros;
	}
	public static PomodoroItemDto? PomodoroItemDtoNamed(string nombreModo)
	{
		var listaPomodoros = ObtenerListaTimePomodoros();
		return listaPomodoros.FirstOrDefault(p => p.NombreModo == nombreModo);
	}
}

