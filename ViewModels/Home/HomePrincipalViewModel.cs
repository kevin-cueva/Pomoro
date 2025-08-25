using System.ComponentModel;

namespace ViewModels.Home;

public class HomePrincipalViewModel : INotifyPropertyChanged
{
    private readonly IDispatcher _dispatcher; // es una interfaz que permite ejecutar código en el hilo principal de la aplicación (UI thread). se está usando para crear un temporizador (IDispatcherTimer) que dispara eventos en el hilo de la interfaz.
    private readonly TimeSpan[] _duracion = [
        TimeSpan.FromMinutes(1), // Pomodoro
        TimeSpan.FromMinutes(2),  // Descanso corto
        TimeSpan.FromMinutes(1), // Pomodoro
        TimeSpan.FromMinutes(2),  // Descanso corto
        TimeSpan.FromMinutes(3)  // Descanso largo
    ];
    private DateTime _inicio;
    private IDispatcherTimer _timer;
    private int contTimePomodoro = 0;
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
    public string Title { get; } = "Bienvenido a pomoro";
    public Command IniciarPomodoroCommand { get; }
    public ModoPomodoro ModoPomodoro { get; set; }

    public HomePrincipalViewModel(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
        Progreso = 0f;
        TiempoRestante = _duracion[contTimePomodoro].ToString(@"mm\:ss");
        IniciarPomodoroCommand = new Command(IniciarPomodoro);
    }

    private void IniciarPomodoro()
    {
        _inicio = DateTime.Now;
        Progreso = 0f;
        contTimePomodoro = 0;
        _timer = _dispatcher.CreateTimer();
        _timer.Interval = TimeSpan.FromMilliseconds(100);
        _timer.Tick += (s, e) => ActualizarProgreso();
        _timer.Start();
    }
    private void SiguienteEstadoPomodoro()
    {
        _timer.Stop();
        _inicio = DateTime.Now;
        Progreso = 0f;
        _timer.Start();
    }
    private void ActualizarProgreso()
    {
        var transcurrido = DateTime.Now - _inicio;
        var tiempoRestante = _duracion[contTimePomodoro] - transcurrido;

        if (tiempoRestante.TotalSeconds < 0)
            tiempoRestante = TimeSpan.Zero;

        Progreso = (float)Math.Min(transcurrido.TotalSeconds / _duracion[contTimePomodoro].TotalSeconds, 1.0f);
        TiempoRestante = tiempoRestante.ToString(@"mm\:ss");

        if (Progreso >= 1.0 && contTimePomodoro < _duracion.Length - 1)
        {
            contTimePomodoro++;
            SiguienteEstadoPomodoro();
            Console.WriteLine("✅ ¡Tiempo completado!");
        }
        else if (Progreso >= 1.0 && contTimePomodoro >= _duracion.Length )
        {
            contTimePomodoro = 0;
            _timer.Stop();
            Console.WriteLine("✅ ¡Tiempo completado final!");
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    private void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}