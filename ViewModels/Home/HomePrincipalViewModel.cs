using System.ComponentModel;
using Pomoro.Domain.DTOS;
using Pomoro.Services.Interfaces;

namespace ViewModels.Home;

public class HomePrincipalViewModel : INotifyPropertyChanged
{

    public Command IniciarPomodoroCommand { get; }
    public ModoPomodoro ModoPomodoro { get; set; }

    private readonly IPlaySoundEndPomodoro _playSoundEndPomodoro;
    private readonly ITimerService _timerService;
    
    private TimePomodoros _timePomodoros = new();
    private int _estado;
    private int _contTimePomodoro;

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

    public string Title { get; } = "Bienvenido a pomoro";

   public HomePrincipalViewModel(
        IPlaySoundEndPomodoro playSoundEndPomodoro,
        ITimerService timerService
    )
    {
        _playSoundEndPomodoro = playSoundEndPomodoro;
        _timerService = timerService;

        _estado = _timePomodoros.WorkDuration;
        Progreso = 0f;
        TiempoRestante = TimeSpan.FromMinutes(_estado).ToString(@"mm\:ss");

        IniciarPomodoroCommand = new Command(IniciarPomodoro);

        // Suscribirse a eventos del servicio
        _timerService.OnTimerTick += ActualizarUI;
        _timerService.OnTimerCompleted += OnTiempoCompletado;
    }

    private void IniciarPomodoro()
    {
        var duracion = TimeSpan.FromMinutes(_estado);
        _timerService.Start(duracion);
    }

    private void ActualizarUI()
    {
        Progreso = _timerService.Progreso;
        TiempoRestante = _timerService.TiempoRestante.ToString(@"mm\:ss");
    }

    private void OnTiempoCompletado()
    {
        _playSoundEndPomodoro.ReproducirSonidoFinPomodoro();
        
        _estado = EstadoPomodoro(_estado);
        var nuevaDuracion = TimeSpan.FromMinutes(_estado);
        _timerService.Start(nuevaDuracion); // Siguiente estado
    }

    private int EstadoPomodoro(int estadoActual)
    {
        if (estadoActual == _timePomodoros.WorkDuration)
        {
            _contTimePomodoro++;
            if (_contTimePomodoro >= _timePomodoros.Repetitions)
            {
                _contTimePomodoro = 0;
                return _timePomodoros.LongBreakDuration;
            }
            else
            {
                return _timePomodoros.ShortBreakDuration;
            }
        }
        else
        {
            return _timePomodoros.WorkDuration;
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    // Si usas DI, considera implementar IDisposable para desuscribir eventos
    public void Dispose()
    {
        _timerService.OnTimerTick -= ActualizarUI;
        _timerService.OnTimerCompleted -= OnTiempoCompletado;
    }
}