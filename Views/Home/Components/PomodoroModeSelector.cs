using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using Pomoro.Domain.DTOS;

namespace Views.Home.Components;

class PomodoroModeSelector : ContentView
{

    TimePomodoros TimePomodoros = new();
    readonly BotonMode buttonAutomatic = new("isettings_icon_white", "Automático", "#111111", null);
    readonly BotonMode buttonMorning = new("isunrise_white", "Manana", "#888888", null);
    readonly BotonMode buttonNight = new("imoon_white", "Noche", "#888888", null);
    readonly BotonMode buttonManual = new("isettings_maul_white", "Manual", "#888888", null);

    public PomodoroModeSelector()
    {
        var grid = new Grid
        {
            RowDefinitions = {
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = GridLength.Auto },
            },
            ColumnDefinitions = {
                new ColumnDefinition { Width = 90 },
                new ColumnDefinition { Width = 90 }
            },

            RowSpacing = 4,
            ColumnSpacing = 4,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center

        };

        #region BOTON
        buttonAutomatic = new BotonMode("isettings_icon_white", "Automático", "#111111", new Command(() =>
        {
            TimePomodoros = new TimePomodoros
            {
                WorkDuration = 4,
                ShortBreakDuration = 2,
                LongBreakDuration = 5,
                Repetitions = 3
            };
            SelectedButton(buttonAutomatic);
            Console.WriteLine("Modo Pomodoro: " + ModoPomodoro.Automatic.ToString());
        }));

        buttonMorning = new BotonMode("isunrise_white", "Manana", "#888888", new Command(() =>
        {
            TimePomodoros = new TimePomodoros
            {

            };
            Console.WriteLine("Modo Pomodoro: " + ModoPomodoro.Automatic.ToString());
            SelectedButton(buttonMorning);
        }));
        buttonNight = new BotonMode("imoon_white", "Noche", "#888888", new Command(() =>
        {
            TimePomodoros = new TimePomodoros
            {

            };
            Console.WriteLine("Modo Pomodoro: " + ModoPomodoro.Manual.ToString());
            SelectedButton(buttonNight);
        }));
        buttonManual = new BotonMode("isettings_maul_white", "Manual", "#888888", new Command(() =>
        {
            TimePomodoros = new TimePomodoros
            {

            };
            Console.WriteLine("Modo Pomodoro: " + ModoPomodoro.Automatic.ToString());
            SelectedButton(buttonManual);
        }));

        Grid.SetRow(buttonAutomatic, 0);
        Grid.SetColumn(buttonAutomatic, 0);

        Grid.SetRow(buttonMorning, 0);
        Grid.SetColumn(buttonMorning, 1);

        Grid.SetRow(buttonNight, 1);
        Grid.SetColumn(buttonNight, 0);

        Grid.SetRow(buttonManual, 1);
        Grid.SetColumn(buttonManual, 1);

        grid.Children.Add(buttonNight);
        grid.Children.Add(buttonManual);
        grid.Children.Add(buttonMorning);
        grid.Children.Add(buttonAutomatic);

        Content = new StackLayout
        {
            Orientation = StackOrientation.Vertical,
            Spacing = 10,
            Children =
            { grid }
        };
        #endregion


    }

    private void SelectedButton(BotonMode selectedButton)
    {
        // Cambiar el color de todos los botones a gris
        buttonAutomatic.UpdateColor("#888888");
        buttonMorning.UpdateColor("#888888");
        buttonNight.UpdateColor("#888888");
        buttonManual.UpdateColor("#888888");

        // Cambiar el color del botón seleccionado
        selectedButton.UpdateColor("#111111");
    }

}

public class BotonMode : ContentView
{
    public BotonMode(string? iconSource, string? text, string? color, Command? command)
    {
        // Icono (puede ser FontImageSource si usas Material Icons / FontAwesome)
        var iconLabel = new Image
        {
            Source = iconSource, // MAUI buscará automatico.svg o automatico.png en Resources/Images
            WidthRequest = 32,
            HeightRequest = 32,
            HorizontalOptions = LayoutOptions.Center
        };
        // Texto debajo del ícono
        var textLabel = new Label
        {
            Text = text,
            FontSize = 12,
            HorizontalOptions = LayoutOptions.Center,
            TextColor = Colors.White,
            FontAttributes = FontAttributes.Bold,
            Padding = new Thickness(0, 0, 0, 12)
        };


        var stack = new VerticalStackLayout
        {
            Spacing = 4,
            Children = { iconLabel, textLabel }
        };

        var buttonFrame = new Border
        {
            Content = stack,
            BackgroundColor = Color.FromArgb(color),
            StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(8) },
            Padding = new Thickness(8, 8, 8, 8),     // Espacio interno (izq, arriba, der, abajo)
            HorizontalOptions = LayoutOptions.Fill,
            WidthRequest = 90,
            HeightRequest = 68
        };

        buttonFrame.GestureRecognizers.Add(new TapGestureRecognizer
        {
            Command = command
        });

        Content = buttonFrame;
    }
    public void UpdateColor(string color)
    {
        if (Content is Border border)
        {
            border.BackgroundColor = Color.FromArgb(color);
        }
    }
}