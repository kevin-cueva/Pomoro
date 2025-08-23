using ViewModels.Home;

namespace Views.Home
{
    public partial class HomePrincipal : ContentPage
    {
        private readonly ProgresoCircularDrawable _drawable;
        private readonly GraphicsView _grafico;
        private readonly HomePrincipalViewModel _viewModel;

        public HomePrincipal()
        {
            _viewModel = new HomePrincipalViewModel(Dispatcher);
            BindingContext = _viewModel;

            _drawable = new ProgresoCircularDrawable();
            _grafico = new GraphicsView
            {
                Drawable = _drawable,
                HeightRequest = 300,
                WidthRequest = 300
            };

            // Redibujar cuando el ViewModel cambie
            _viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(HomePrincipalViewModel.Progreso))
                    _drawable.Progreso = _viewModel.Progreso;

                if (e.PropertyName == nameof(HomePrincipalViewModel.TiempoRestante))
                    _drawable.TiempoRestante = _viewModel.TiempoRestante;

                _grafico.Invalidate();
            };

            var botonIniciar = new Button
            {
                Text = "Iniciar Pomodoro",
                HorizontalOptions = LayoutOptions.Center
            };
            botonIniciar.SetBinding(
                Button.CommandProperty,
                nameof(HomePrincipalViewModel.IniciarPomodoroCommand));

            #region toggle switch personalizado
            // Grid principal: 1 fila, 2 columnas
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
            var indicator = new Frame
            {
                BackgroundColor = Colors.Blue,
                CornerRadius = 8,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 100,
                HeightRequest = 30
            };

            var labelModo = new Label
            {
                Text = "Automático",
                FontSize = 14,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
                TextColor = Colors.White
            };



            // Añadir texto "Automático" (izquierda)
            var labelAuto = new Label
            {
                Text = "Automático",
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
                //Margin = new Thickness(0, 0, 10, 0),
                TextColor = Colors.White
            };

            var frameAuto = new Frame
            {
                Content = labelAuto,
                BackgroundColor = Colors.Black,     // Fondo negro
                CornerRadius = 8,                   // Bordes redondeados
                BorderColor = Colors.Black,         // Para que el borde no sea gris
                Padding = new Thickness(10, 5),     // Espacio interno (izq, arriba, der, abajo)
                HorizontalOptions = LayoutOptions.Start, // Para que no se expanda
                HasShadow = false                   // Desactiva sombra
            };


            Grid.SetRow(frameAuto, 0);
            Grid.SetColumn(frameAuto, 0);

            // Añadir texto "Manual" (derecha)
            var labelManual = new Label
            {
                Text = "Manual",
                HorizontalTextAlignment = TextAlignment.End,
                VerticalTextAlignment = TextAlignment.Center,
                //Margin = new Thickness(10, 0, 0, 0),
                TextColor = Colors.White
            };
            var frameManual = new Frame
            {
                Content = labelManual,
                BackgroundColor = Colors.Black,     // Fondo negro
                CornerRadius = 8,                   // Bordes redondeados
                BorderColor = Colors.Black,         // Para que el borde no sea gris
                Padding = new Thickness(0, 0, 5, 0),     // Espacio interno (izq, arriba, der, abajo)
                HorizontalOptions = LayoutOptions.End, // Para que no se expanda
                HasShadow = false                   // Desactiva sombra
            };


            Grid.SetRow(frameManual, 0);
            Grid.SetColumn(frameManual, 1);

            // Posicionar el indicador (empieza en la izquierda)
            Grid.SetRow(indicator, 0);
            Grid.SetColumn(indicator, 0);

            // Añadir todos los elementos al Grid
            grid.Children.Add(frameAuto);
            grid.Children.Add(frameManual);
            grid.Children.Add(indicator);

            // Hacer que el Grid sea clickeable
            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += async (s, e) =>
            {
                esModoAutomatico = !esModoAutomatico;

                // Animar el movimiento del indicator
                if (esModoAutomatico)
                {
                    labelModo.Text = "Automático";
                    indicator.BackgroundColor = Colors.Gray;
                    await indicator.TranslateTo(0, 0, 250, Easing.SinOut);
                }
                else
                {
                    labelModo.Text = "Manual";
                    indicator.BackgroundColor = Colors.Gray;
                    await indicator.TranslateTo(100, 0, 250, Easing.SinOut); // (x, y, duración, tipoDeAnimación);
                }
            };
            grid.GestureRecognizers.Add(tapGesture);
            var frameGrid = new Frame
            {
                Content = grid,
                BackgroundColor = Colors.Black,     // Fondo negro
                CornerRadius = 8,                   // Bordes redondeados
                BorderColor = Colors.Black,         // Para que el borde no sea gris
                Padding = new Thickness(0, 0),     // Espacio interno (izq, arriba, der, abajo)
                HorizontalOptions = LayoutOptions.Center,
                HasShadow = false                   // Desactiva sombra
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
                labelModo // Muestra el modo actual
            }
            };




            #endregion
            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Spacing = 30,
                Children = { _grafico, botonIniciar, modoContainer }
            };
        }
    }
}