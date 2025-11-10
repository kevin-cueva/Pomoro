using System.Collections.ObjectModel;
using Pomoro.Domain.Constants;
using Pomoro.Domain.DTOS;
using Pomoro.Domain.Enums;
using Pomoro.Helpers;
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
		itemsSource = ObtenerListaPomodoros();
		CollectionViewComponent();
		// Envolver en un layout contenedor
		Content = new VerticalStackLayout
		{
			Children = { titulo, collectionView },
			// El padding seguro se aplica a la página, y el layout lo respeta
		};
	}

	private void CollectionViewComponent()
	{// Inicializar la fuente de datos
		collectionView = new CollectionView
		{
			// Datos de ejemplo
			ItemsSource = itemsSource,
			ItemTemplate = new DataTemplate(static () =>
			{
				// Cada ítem será un Label con el nombre y la edad
				var nameLabel = new Label { FontAttributes = FontAttributes.Bold };

				ImageButton imageButton = new ImageButton
				{
					Source = Constants.Icons.PomodoroVerde,
					WidthRequest = 40,
					HeightRequest = 40,
					BackgroundColor = Colors.Transparent
				};
				// Crear un layout vertical para cada ítem
				var stackLayout = new HorizontalStackLayout
				{
					Children = { nameLabel, imageButton },
					Padding = new Thickness(10)
				};

				// Enlazar las propiedades del modelo a los controles
				nameLabel.SetBinding(Label.TextProperty, nameof(PomodoroItemDto.NombreModo));

				return stackLayout;
			})
		};

		// Opcional: manejar selección
		collectionView.SelectionMode = SelectionMode.Single;
		collectionView.SelectionChanged += OnSelectionChanged;
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
	private ObservableCollection<PomodoroItemDto> ObtenerListaPomodoros()
    {
        var timePomodoros = AppStorage.GetAllTimePomodoros();

        var listaPomodoros = timePomodoros
            .Select(kvp => new PomodoroItemDto
            {
                NombreModo = Utils.NombreModoPomodoro(kvp.Key),
                Tiempos = kvp.Value
            })
            .ToList();

        return new ObservableCollection<PomodoroItemDto>(listaPomodoros);
    }
}
