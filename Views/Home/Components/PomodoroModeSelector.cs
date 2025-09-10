using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using Pomoro.Domain.DTOS;

namespace Views.Home.Components;

class PomodoroModeSelector : ContentView
{

    TimePomodoros TimePomodoros = new();

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
        var buttonAutomatic = new BotonMode("isettings_icon_white", "Automático", new Command(() =>
        {
            TimePomodoros = new TimePomodoros
            {
                WorkDuration = 4,
                ShortBreakDuration = 2,
                LongBreakDuration = 5,
                Repetitions = 3
            };
            Console.WriteLine("Modo Pomodoro: " + ModoPomodoro.Automatic.ToString());
        }));
        var buttonMorning = new BotonMode("isunrise_white", "Manana", new Command(() =>
        {
            TimePomodoros = new TimePomodoros
            {

            };
            Console.WriteLine("Modo Pomodoro: " + ModoPomodoro.Automatic.ToString());
        }));
        var buttonNight = new BotonMode("imoon_white", "Noche", new Command(() =>
        {
            TimePomodoros = new TimePomodoros
            {

            };
            Console.WriteLine("Modo Pomodoro: " + ModoPomodoro.Manual.ToString());
        }));
        var manual = new BotonMode("isettings_maul_white", "Manual", new Command(() =>
        {
            TimePomodoros = new TimePomodoros
            {

            };
            Console.WriteLine("Modo Pomodoro: " + ModoPomodoro.Automatic.ToString());
        }));

        Grid.SetRow(buttonAutomatic, 0);
        Grid.SetColumn(buttonAutomatic, 0);

        Grid.SetRow(buttonMorning, 0);
        Grid.SetColumn(buttonMorning, 1);

        Grid.SetRow(buttonNight, 1);
        Grid.SetColumn(buttonNight, 0);

        Grid.SetRow(manual, 1);
        Grid.SetColumn(manual, 1);

        grid.Children.Add(buttonNight);
        grid.Children.Add(manual);
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

}

public class BotonMode : ContentView
{
    public BotonMode(string iconSource, string text, Command command)
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
            BackgroundColor = Colors.Black,
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
}