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

