using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using Pomoro.Domain.DTOS;
using Pomoro.Domain.Enums;

namespace Views.Home.Components;

class PomodoroModeSelector : ContentView
{

    TimePomodoros TimePomodoros = new();
    readonly BotonMode buttonAutomatic;
    readonly BotonMode buttonMorning;
    readonly BotonMode buttonNight;
    readonly BotonMode buttonManual;

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
        buttonAutomatic = new BotonMode(new BotonModeConfig
        {
            IconSource = "isettings_icon_white",
            Text = "Automático",
            Color = "#111111",
            Command = new Command(() =>
            {
                TimePomodoros = new TimePomodoros
                {

                };
                Console.WriteLine("Modo Pomodoro: " + ModoPomodoro.Automatic.ToString());
                SelectedButton(buttonAutomatic);
            })
        });

        buttonMorning = new BotonMode(new BotonModeConfig
        {
            IconSource = "isunrise_white",
            Text = "Mañana",
            Color = "#888888",
            Command = new Command(() =>
            {
                TimePomodoros = new TimePomodoros
                {

                };
                SelectedButton(buttonMorning);
            })
        });
        buttonNight = new BotonMode(new BotonModeConfig
        {
            IconSource = "imoon_white",
            Text = "Noche",
            Color = "#888888",
            Command = new Command(() =>
            {
                TimePomodoros = new TimePomodoros
                {

                };
                SelectedButton(buttonNight);
            })
        });
        buttonManual = new BotonMode(new BotonModeConfig
        {
            IconSource = "isettings_maul_white",
            Text = "Manual",
            Color = "#888888",
            Command = new Command(() =>
            {
                TimePomodoros = new TimePomodoros
                {

                };
                SelectedButton(buttonManual);
            })
        });

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
    public BotonMode(BotonModeConfig? botonModeConfig)
    {
        // Icono (puede ser FontImageSource si usas Material Icons / FontAwesome)
        var iconLabel = new Image
        {
            Source = botonModeConfig!.IconSource,
            WidthRequest = 32,
            HeightRequest = 32,
            HorizontalOptions = LayoutOptions.Center
        };
        // Texto debajo del ícono
        var textLabel = new Label
        {
            Text = botonModeConfig.Text,
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
            BackgroundColor = Color.FromArgb(botonModeConfig.Color),
            StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(8) },
            Padding = new Thickness(8, 8, 8, 8),     // Espacio interno (izq, arriba, der, abajo)
            HorizontalOptions = LayoutOptions.Fill,
            WidthRequest = 90,
            HeightRequest = 68
        };

        buttonFrame.GestureRecognizers.Add(new TapGestureRecognizer
        {
            Command = botonModeConfig.Command
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