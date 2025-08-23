using System.ComponentModel;

namespace ViewModels.Home;

public class HomePrincipalViewModel : INotifyPropertyChanged
{
    private readonly IDispatcher _dispatcher;
    private readonly TimeSpan _duracion = TimeSpan.FromMinutes(5);
    private DateTime _inicio;
    private IDispatcherTimer _timer;

    private float _progreso;
    public float Progreso
    {
        get => _progreso;
        set
        {
            if (_progreso != value)
            {
                _progreso = value;
                OnPropertyChanged(nameof(Progreso));
            }
        }
    }

    private string _tiempoRestante;
    public string TiempoRestante
    {
        get => _tiempoRestante;
        set
        {
            if (_tiempoRestante != value)
            {
                _tiempoRestante = value;
                OnPropertyChanged(nameof(TiempoRestante));
            }
        }
    }

    public string Title { get; } = "Bienvenido a ClarityPom";
    public Command IniciarPomodoroCommand { get; }

    public HomePrincipalViewModel(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
        Progreso = 0f;
        TiempoRestante = _duracion.ToString(@"mm\:ss");

        IniciarPomodoroCommand = new Command(IniciarPomodoro);
    }

    private void IniciarPomodoro()
    {
        _inicio = DateTime.Now;
        Progreso = 0f;

        _timer = _dispatcher.CreateTimer();
        _timer.Interval = TimeSpan.FromMilliseconds(100);
        _timer.Tick += (s, e) => ActualizarProgreso();
        _timer.Start();
    }

    private void ActualizarProgreso()
    {
        var transcurrido = DateTime.Now - _inicio;
        var tiempoRestante = _duracion - transcurrido;

        if (tiempoRestante.TotalSeconds < 0)
            tiempoRestante = TimeSpan.Zero;

        Progreso = (float)Math.Min(transcurrido.TotalSeconds / _duracion.TotalSeconds, 1.0f);
        TiempoRestante = tiempoRestante.ToString(@"mm\:ss");

        if (Progreso >= 1.0)
        {
            _timer.Stop();
            Console.WriteLine("✅ ¡Tiempo completado!");
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    private void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}