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

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Spacing = 30,
                Children = { _grafico, botonIniciar }
            };
        }
    }
}