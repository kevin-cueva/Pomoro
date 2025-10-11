using System.Reflection.Metadata;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using Pomoro.Domain.DTOS;
using Pomoro.Domain.Enums;
using Pomoro.Domain.Constants;
using Pomoro.Helpers;
namespace Pomoro.Views.Home.Components;

public class BotonMode : ContentView
{
    public BotonMode(BotonModeConfig? botonModeConfig)
    {
        // Icono (puede ser FontImageSource si usas Material Icons / FontAwesome)
        var iconLabel = new Image
        {
            Source = SelectedButtonModeIconInit(botonModeConfig!.Modo, botonModeConfig!.IconSource),
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
            TextColor = (Color)Application.Current!.Resources[Constants.Colors.Text],
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
            BackgroundColor = BotonMode.SelectedButtonModeColorInit(botonModeConfig.Modo),
            StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(8) },
            Padding = new Thickness(8, 8, 8, 8),     // Espacio interno (izq, arriba, der, abajo)
            Stroke = Colors.Transparent, // Borde transparente
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
    public void UpdateIconColor(string iconColor)
    {
        if (Content is Border border && border.Content is VerticalStackLayout stack)
        {
            foreach (var child in stack.Children)
            {
                if (child is Image icon)
                {
                    icon.Source = iconColor;
                }
            }
        }
    }
    
    /// <summary>
    /// Return the Color for select Mode 
    /// </summary>
    /// <param name="modoPomodoro"></param>
    /// <returns></returns>
    private static Color SelectedButtonModeColorInit(ModoPomodoro modoPomodoro)
    {
        if (GetLastModeButton(modoPomodoro))
            return (Color)Application.Current!.Resources[Constants.Colors.ModeButtonActive];

        return (Color)Application.Current!.Resources[Constants.Colors.ModeButton];

    }

     /// <summary>
    /// Devuelve el ícono correspondiente al modo seleccionado de Pomodoro.
    /// Si el modo pasado es el último modo seleccionado, convierte el ícono "verde" al ícono "marrón" correspondiente.
    /// Si no es el último modo seleccionado, retorna el ícono original.
    /// </summary>
    /// <param name="modoPomodoro">ModoPomodoro actual que se está evaluando.</param>
    /// <param name="icons">Nombre del ícono actual asociado al modo.</param>
    /// <returns>
    /// El nombre del ícono modificado si es el último modo seleccionado, o el ícono original en caso contrario.
    /// </returns>
    private static string SelectedButtonModeIconInit(ModoPomodoro modoPomodoro, string icons)
    {
        if (GetLastModeButton(modoPomodoro))
            return icons switch
            {
                Constants.Icons.PomodoroVerde => Constants.Icons.PomodoroMarron,
                Constants.Icons.SunriseVerde => Constants.Icons.SunriseMarron,
                Constants.Icons.MoonVerde => Constants.Icons.MoonMarron,
                Constants.Icons.SolutionVerde => Constants.Icons.SolutionMarron,
                _ => string.Empty,
            };
        ;

        return icons;
    }


    /// <summary>
    /// Determines whether the specified <paramref name="modoPomodoro"/> is the last selected Pomodoro mode.
    /// Retrieves the current mode from persistent storage and compares it to the provided mode.
    /// </summary>
    /// <param name="modoPomodoro">The Pomodoro mode to check against the last selected mode.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="modoPomodoro"/> matches the last selected mode; otherwise, <c>false</c>.
    /// </returns>
    private static bool GetLastModeButton(ModoPomodoro modoPomodoro)
    {
        var currentMode = AppStorage.GetData(KeyStorage.CurrentMode);
        var lastMode = Utils.ParseEnum(currentMode, ModoPomodoro.PorDefecto);
        return modoPomodoro == lastMode;
    }
    
}