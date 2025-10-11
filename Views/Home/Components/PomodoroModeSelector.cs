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
    readonly BotonMode buttonPorDefecto;
    readonly BotonMode buttonMorning;
    readonly BotonMode buttonNight;
    readonly BotonMode buttonFlexible;

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
        buttonPorDefecto = new BotonMode(new BotonModeConfig
        {
            IconSource = Constants.Icons.PomodoroVerde,
            Text = "Por defecto",
            Modo = ModoPomodoro.PorDefecto,
            Command = new Command(() =>
            {
                AppStorage.SavePomodoro(ModoPomodoro.PorDefecto, new TimePomodoros());
                SelectedButton(buttonPorDefecto!, ModoPomodoro.PorDefecto);
                SelectIcon(buttonPorDefecto!, Constants.Icons.PomodoroMarron);
            })
        });

        buttonMorning = new BotonMode(new BotonModeConfig
        {
            IconSource = Constants.Icons.SunriseVerde,
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
                AppStorage.SavePomodoro(ModoPomodoro.Morning, TimePomodoros);
                SelectedButton(buttonMorning!, ModoPomodoro.Morning);
                SelectIcon(buttonMorning!, Constants.Icons.SunriseMarron);
            })
        });
        buttonNight = new BotonMode(new BotonModeConfig
        {
            IconSource = Constants.Icons.MoonVerde,
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
                AppStorage.SavePomodoro(ModoPomodoro.Night, TimePomodoros);
                SelectedButton(buttonNight!, ModoPomodoro.Night);
                SelectIcon(buttonNight!, Constants.Icons.MoonMarron);

            })
        });
        buttonFlexible = new BotonMode(new BotonModeConfig
        {
            IconSource = Constants.Icons.SolutionVerde,
            Text = "Flexible",
            Modo = ModoPomodoro.Flexible,
            Command = new Command(() =>
            {
                TimePomodoros = new TimePomodoros
                {
                    WorkDuration = 60,
                    ShortBreakDuration = 10,
                    LongBreakDuration = 20,
                };
                AppStorage.SavePomodoro(ModoPomodoro.Flexible, TimePomodoros);
                SelectedButton(buttonFlexible!, ModoPomodoro.Flexible);
                SelectIcon(buttonFlexible!, Constants.Icons.SolutionMarron);
            })
        });

        Grid.SetRow(buttonPorDefecto, 0);
        Grid.SetColumn(buttonPorDefecto, 0);

        Grid.SetRow(buttonMorning, 0);
        Grid.SetColumn(buttonMorning, 1);

        Grid.SetRow(buttonNight, 1);
        Grid.SetColumn(buttonNight, 0);

        Grid.SetRow(buttonFlexible, 1);
        Grid.SetColumn(buttonFlexible, 1);

        grid.Children.Add(buttonNight);
        grid.Children.Add(buttonFlexible);
        grid.Children.Add(buttonMorning);
        grid.Children.Add(buttonPorDefecto);

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
        buttonPorDefecto.UpdateColor(Constants.Colors.ModeButton);
        buttonMorning.UpdateColor(Constants.Colors.ModeButton);
        buttonNight.UpdateColor(Constants.Colors.ModeButton);
        buttonFlexible.UpdateColor(Constants.Colors.ModeButton);

        // Cambiar el color del botón seleccionado
        selectedButton.UpdateColor(Constants.Colors.ModeButtonActive);
        AppStorage.SaveData(KeyStorage.CurrentMode, modoPomodoro.ToString());

    }
    private void SelectIcon(BotonMode selectedButton, string icon)
    {
        buttonPorDefecto.UpdateIconColor(Constants.Icons.PomodoroVerde);
        buttonMorning.UpdateIconColor(Constants.Icons.SunriseVerde);
        buttonNight.UpdateIconColor(Constants.Icons.MoonVerde);
        buttonFlexible.UpdateIconColor(Constants.Icons.SolutionVerde);

        selectedButton.UpdateIconColor(icon);  
    }

}

