using System.Reflection.Metadata;
using Microsoft.VisualBasic;
using Pomoro.Domain.Constants;
using static Pomoro.Domain.Constants.Constants;
using Pomoro.ViewModels.Home;
namespace Pomoro.Views.Home.Components;

public class PomodoroControlsConfig : ContentView
{
    readonly Border ConteinerGlobal = new();
    readonly Grid Disposition = [];
    private readonly ImageButton Play = new();
    private readonly ImageButton Pause = new();
    private readonly ImageButton Reload = new();

    public PomodoroControlsConfig()
    {

        Disposition = new Grid
        {
            RowDefinitions = {
                new RowDefinition { Height = GridLength.Auto }
            },
            ColumnDefinitions = {
                new ColumnDefinition { Width = 44 },
                new ColumnDefinition { Width = 100 },
                new ColumnDefinition { Width = 44}
            },
            ColumnSpacing = 20,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };

        Play = new ImageButton
        {
            WidthRequest = 100,
            HeightRequest = 100,
            BackgroundColor = Microsoft.Maui.Graphics.Colors.Purple,
            CornerRadius = 50,
            Source = Icons.PlayWhite,
            BorderColor = Microsoft.Maui.Graphics.Colors.Transparent,
            // Aquí controlas el espacio interno
            // (100 - 32) / 2 = 34 px de margen
            Padding = new Thickness(36, 34, 32, 34), // más a la izquierda, menos a la derecha
            
        };
        Play.SetBinding(
            ImageButton.CommandProperty,
            nameof(HomePrincipalViewModel.IniciarPomodoroCommand));
        Grid.SetRow(Play, 0);
        Grid.SetColumn(Play, 1);
        Disposition.Children.Add(Play);

        Reload = new ImageButton
        {
            WidthRequest = 44,
            HeightRequest = 44,
            BackgroundColor = Microsoft.Maui.Graphics.Colors.Gray,
            Source = Icons.ReloadWhite,
            BorderColor = Microsoft.Maui.Graphics.Colors.Transparent,
            CornerRadius = 44/2,
            // Aquí controlas el espacio interno
            // (44 - 16) / 2 = 14 px de margen
            Padding = new Thickness(14), // más a la izquierda, menos a la derecha
        };
        Reload.SetBinding(
            ImageButton.CommandProperty,
            nameof(HomePrincipalViewModel.ReiniciarPomodoroCommand));
        Grid.SetRow(Reload, 0);
        Grid.SetColumn(Reload, 0);
        Disposition.Children.Add(Reload);

        Pause = new ImageButton
        {
            WidthRequest = 44,
            HeightRequest = 44,
            BackgroundColor = Microsoft.Maui.Graphics.Colors.Gray,
            Source = Icons.PauseWhire,
            BorderColor = Microsoft.Maui.Graphics.Colors.Transparent,
            CornerRadius = 44/2,
            // Aquí controlas el espacio interno
            // (44 - 16) / 2 = 14 px de margen
            Padding = new Thickness(14), // más a la izquierda, menos a la derecha
        };
        Pause.SetBinding(
            ImageButton.CommandProperty,
            nameof(HomePrincipalViewModel.PausaPomodoroCommand));
        Grid.SetRow(Pause, 0);
        Grid.SetColumn(Pause, 2);
        Disposition.Children.Add(Pause);

        
        
        ConteinerGlobal = new Border
        {
            Stroke = Microsoft.Maui.Graphics.Colors.Transparent, // quita el color del borde
            StrokeThickness = 0, // sin grosor
            BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent, // opcional si no quieres fondo
            Content = Disposition
        };
        
        Content = ConteinerGlobal;
    }
}