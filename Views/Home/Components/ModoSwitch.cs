namespace Views.Home.Components;
using Microsoft.Maui.Controls.Shapes;

public class ModoSwitch : ContentView
{
    public ModoPomodoro ModoPomodoro { get; private set; } = ModoPomodoro.Automatic;
    public ModoSwitch()
    {
        var grid = new Grid
        {
            RowDefinitions = { new RowDefinition { Height = 30 } },
            ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                },
            HeightRequest = 30,
            WidthRequest = 200
        };
        // Variables de estado
        bool esModoAutomatico = true;
        var indicator = new Border
        {
            BackgroundColor = Colors.Gray,
            StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(8, 8, 8, 8)
            },
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            WidthRequest = 100,
            HeightRequest = 30
        };

        // Añadir texto "Automático" (izquierda)
        var labelAuto = new Label
        {
            Text = "Automático",
            HorizontalTextAlignment = TextAlignment.Start,
            VerticalTextAlignment = TextAlignment.Center,
            Margin = new Thickness(10, 0, 0, 0),
            TextColor = Colors.White
        };

        Grid.SetRow(labelAuto, 0);
        Grid.SetColumn(labelAuto, 0);

        // Añadir texto "Manual" (derecha)
        var labelManual = new Label
        {
            Text = "Manual",
            HorizontalTextAlignment = TextAlignment.End,
            VerticalTextAlignment = TextAlignment.Center,
            Margin = new Thickness(0, 0, 10, 0),
            TextColor = Colors.White
        };



        Grid.SetRow(labelManual, 0);
        Grid.SetColumn(labelManual, 1);

        // Posicionar el indicador (empieza en la izquierda)
        Grid.SetRow(indicator, 0);
        Grid.SetColumn(indicator, 0);

        // Añadir todos los elementos al Grid
        grid.Children.Add(labelAuto);
        grid.Children.Add(labelManual);
        grid.Children.Add(indicator);

        // Hacer que el Grid sea clickeable
        var tapGesture = new TapGestureRecognizer();
        tapGesture.Tapped += async (s, e) =>
        {
            esModoAutomatico = !esModoAutomatico;

            // Animar el movimiento del indicator
            if (esModoAutomatico)
            {
                ModoPomodoro = ModoPomodoro.Automatic;
                indicator.BackgroundColor = Colors.Gray;
                await indicator.TranslateTo(0, 0, 250, Easing.SinOut);
            }
            else
            {
                ModoPomodoro = ModoPomodoro.Manual;
                indicator.BackgroundColor = Colors.Gray;
                await indicator.TranslateTo(100, 0, 250, Easing.SinOut); // (x, y, duración, tipoDeAnimación);
            }
        };
        grid.GestureRecognizers.Add(tapGesture);
        var frameGrid = new Border
        {
            Content = grid,
            BackgroundColor = Colors.Black,     // Fondo negro
            StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(8) },
            Padding = new Thickness(0, 0),     // Espacio interno (izq, arriba, der, abajo)
            HorizontalOptions = LayoutOptions.Center,

        };


        // Panel de título + slider
        var modoContainer = new StackLayout
        {
            Orientation = StackOrientation.Vertical,
            Spacing = 10,
            Children =
            {
                new Label
                {
                    Text = "Modo",
                    FontAttributes = FontAttributes.Bold,
                    HorizontalTextAlignment = TextAlignment.Center
                },
                frameGrid,         // Muestra el switch personalizado
                
            }
        };
        Content = new StackLayout
        {
            Orientation = StackOrientation.Vertical,
            Spacing = 10,
            Children =
            { modoContainer }
        };

    }
}
