using System.Reflection.Metadata;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using Pomoro.Domain.DTOS;
using Pomoro.Domain.Enums;
using Pomoro.Domain.Constants;
using Pomoro.Helpers;
namespace Pomoro.Views.Home.Components;

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
            IconSource = Constants.Icons.SettingsWhite,
            Text = "Automático",
            Modo = ModoPomodoro.Automatic,
            Command = new Command(() =>
            {
                AppStorage.SavePomodoro(ModoPomodoro.Automatic,new TimePomodoros());
                SelectedButton(buttonAutomatic!, ModoPomodoro.Automatic);
            })
        });

        buttonMorning = new BotonMode(new BotonModeConfig
        {
            IconSource = Constants.Icons.SunriseWhite,
            Text = "Mañana",
            Modo = ModoPomodoro.Morning,
            Command = new Command(() =>
            {
                TimePomodoros = new TimePomodoros
                {
                    WorkDuration = 40,
                    ShortBreakDuration = 7,
                    LongBreakDuration = 20,
                };
                AppStorage.SavePomodoro(ModoPomodoro.Morning,TimePomodoros);

                SelectedButton(buttonMorning!, ModoPomodoro.Morning);
            })
        });
        buttonNight = new BotonMode(new BotonModeConfig
        {
            IconSource = Constants.Icons.MoonWhite,
            Text = "Noche",
            Modo = ModoPomodoro.Night,
            Command = new Command(() =>
            {
                TimePomodoros = new TimePomodoros
                {
                    WorkDuration = 20,
                    ShortBreakDuration = 5,
                    LongBreakDuration = 10,
                };
                AppStorage.SavePomodoro(ModoPomodoro.Night,TimePomodoros);
                SelectedButton(buttonNight!, ModoPomodoro.Night);
            })
        });
        buttonManual = new BotonMode(new BotonModeConfig
        {
            IconSource = Constants.Icons.ManualWhite,
            Text = "Manual",
            Modo = ModoPomodoro.Manual,
            Command = new Command(() =>
            {
                TimePomodoros = new TimePomodoros
                {
                    WorkDuration = 60,
                    ShortBreakDuration = 10,
                    LongBreakDuration = 20,
                };
                AppStorage.SavePomodoro(ModoPomodoro.Manual,TimePomodoros);
                SelectedButton(buttonManual!, ModoPomodoro.Manual);
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

    private void SelectedButton(BotonMode selectedButton, ModoPomodoro modoPomodoro)
    {
        // Cambiar el color de todos los botones a gris
        buttonAutomatic.UpdateColor(Constants.Colors.BotonNoSelect);
        buttonMorning.UpdateColor(Constants.Colors.BotonNoSelect);
        buttonNight.UpdateColor(Constants.Colors.BotonNoSelect);
        buttonManual.UpdateColor(Constants.Colors.BotonNoSelect);

        // Cambiar el color del botón seleccionado
        selectedButton.UpdateColor(Constants.Colors.BotonSelect);
        
        AppStorage.SaveData(KeyStorage.CurrentMode, modoPomodoro.ToString());
        
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
            BackgroundColor = SelectedButtonMode(botonModeConfig.Modo),
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
            border.BackgroundColor = (Color)Application.Current!.Resources[color];
        }
    }
    private Color SelectedButtonMode(ModoPomodoro modoPomodoro)
    {
        var currentMode = AppStorage.GetData(KeyStorage.CurrentMode);
        ModoPomodoro modoPomodoroLast = Utils.ParseEnum(currentMode, ModoPomodoro.Automatic);
        if (modoPomodoro == modoPomodoroLast)
            return (Color)Application.Current!.Resources[Constants.Colors.BotonSelect];
        return (Color)Application.Current!.Resources[Constants.Colors.BotonNoSelect];

    }
}