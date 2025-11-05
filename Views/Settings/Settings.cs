using System.Collections.ObjectModel;

namespace Pomoro.Views.Settings;

public class Settings : ContentPage
{
	private CollectionView collectionView;
	private readonly ObservableCollection<Person> itemsSource;
	public Settings()
	{
		// Inicializar la fuente de datos
		itemsSource =
		[
			new Person("Ana", 25),
			new Person("Luis", 30),
			new Person("Carla", 22)
		];
		CollectionViewComponent();
		// Envolver en un layout contenedor
		Content = new VerticalStackLayout
		{
			Children = { collectionView },
			// El padding seguro se aplica a la página, y el layout lo respeta
		};
	}
	private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (e.CurrentSelection.FirstOrDefault() is Person selectedPerson)
		{
			DisplayAlert("Seleccionado", $"Has seleccionado a {selectedPerson.Name}", "OK");
		}
	}

	private void CollectionViewComponent()
	{// Inicializar la fuente de datos
		collectionView = new CollectionView
		{
			// Datos de ejemplo
			ItemsSource = itemsSource,
			ItemTemplate = new DataTemplate(() =>
			{
				// Cada ítem será un Label con el nombre y la edad
				var nameLabel = new Label { FontAttributes = FontAttributes.Bold };
				var ageLabel = new Label();

				// Crear un layout vertical para cada ítem
				var stackLayout = new VerticalStackLayout
				{
					Children = { nameLabel, ageLabel },
					Padding = new Thickness(10)
				};

				// Enlazar las propiedades del modelo a los controles
				nameLabel.SetBinding(Label.TextProperty, nameof(Person.Name));
				ageLabel.SetBinding(Label.TextProperty, nameof(Person.Age));

				return stackLayout;
			})
		};

		// Opcional: manejar selección
		collectionView.SelectionMode = SelectionMode.Single;
		collectionView.SelectionChanged += OnSelectionChanged;
	}
}

public class Person(string name, int age)
{
    public string Name { get; set; } = name;
    public int Age { get; set; } = age;
}