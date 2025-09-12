using ViewModels.Home;
using Pomoro.Services;
using Plugin.Maui.Audio;
using Pomoro.Views.Home.Components;
using Pomoro.ViewModels.Home;
namespace Pomoro.Views.Home
{
    public partial class HomePrincipal : ContentPage
    {
        private readonly ProgresoCircularDrawable _drawable;
        private readonly GraphicsView _grafico;
        private readonly HomePrincipalViewModel _viewModel;

        public HomePrincipal()
        {
            //Boton switch de modo (automatico/manual)
            //var switchModo = new ModoSwitch();

            //Boton de modo
            var botonGrid = new PomodoroModeSelector();

            var soundService = new PlaySoundEndPomodoro(AudioManager.Current); 
            var timerService = new TimerService(Dispatcher);
            var modoOperationService = new ModoOperationService();

            _viewModel = new HomePrincipalViewModel(soundService, timerService, modoOperationService);
            

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
                //_viewModel.ModoPomodoro = switchModo.ModoPomodoro;
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

            var botonPausa = new Button
            {
                Text = "Iniciar Pausa",
                HorizontalOptions = LayoutOptions.Center
            };
            botonPausa.SetBinding(
                Button.CommandProperty,
                nameof(HomePrincipalViewModel.PausaPomodoroCommand));

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Spacing = 30,
                Children = { _grafico, botonGrid, botonIniciar, botonPausa }
            };
        }
    }
}