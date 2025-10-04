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
    private static Color SelectedButtonModeColorInit(ModoPomodoro modoPomodoro)
    {
        var currentMode = AppStorage.GetData(KeyStorage.CurrentMode);
        ModoPomodoro modoPomodoroLast = Utils.ParseEnum(currentMode, ModoPomodoro.PorDefecto);

        if (modoPomodoro == modoPomodoroLast)
            return (Color)Application.Current!.Resources[Constants.Colors.ModeButtonActive];

        return (Color)Application.Current!.Resources[Constants.Colors.ModeButton];

    }
}