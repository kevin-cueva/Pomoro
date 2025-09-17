using System.ComponentModel;
using Pomoro.Domain.DTOS;
using Pomoro.Services.Interfaces;
using Pomoro.Domain.Enums;

namespace Pomoro.ViewModels.Home;

public class HomePrincipalViewModel : INotifyPropertyChanged
{

    public Command IniciarPomodoroCommand { get; }
    public Command PausaPomodoroCommand { get; }
    public Command ReiniciarPomodoroCommand { get; }
    public ModoPomodoro ModoPomodoro { get; set; }

    private readonly IPlaySoundEndPomodoro _playSoundEndPomodoro;
    private readonly ITimerService _timerService;
    private readonly IModoOperationService _modoOperationService;

    private ModoOperation _estado;
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
         ITimerService timerService,
         IModoOperationService modoOperationService
     )
    {
        _modoOperationService = modoOperationService;
        _playSoundEndPomodoro = playSoundEndPomodoro;
        _timerService = timerService;

        _estado = ModoOperation.Work;
        Progreso = 0f;
        TiempoRestante = TimeSpan.FromMinutes(_modoOperationService
            .GetOperationDuration(_estado)).ToString(@"mm\:ss");

        IniciarPomodoroCommand = new Command(IniciarPomodoro);
        PausaPomodoroCommand = new Command(PausaPomodoro);
        ReiniciarPomodoroCommand = new Command(() => ReiciarPomodoro());
        // Suscribirse a eventos del servicio
        _timerService.OnTimerTick += ActualizarUI;
        _timerService.OnTimerCompleted += OnTiempoCompletado;
    }

    private void IniciarPomodoro()
    {
        var duracion = TimeSpan.FromMinutes(_modoOperationService
            .GetOperationDuration(_estado));
        _timerService.Start(duracion);
    }

    private void PausaPomodoro()
    {
        if (_timerService.TiempoRestante != TimeSpan.Zero)
        {
            _timerService.Pause();
        }
    }
    private void ReiciarPomodoro()
    {
        _timerService.Reload();
        Progreso = _timerService.Progreso;
        TiempoRestante = _timerService.TiempoRestante.ToString(@"mm\:ss");
        ActualizarUI();

    }
    private void ActualizarUI()
    {
        Progreso = _timerService.Progreso;
        TiempoRestante = _timerService.TiempoRestante.ToString(@"mm\:ss");
    }

    private void OnTiempoCompletado()
    {
        _playSoundEndPomodoro.ReproducirSonidoFinPomodoro();
        _estado = _modoOperationService.ChangeState(_estado);
        var nuevaDuracion = TimeSpan.FromMinutes(_modoOperationService
            .GetOperationDuration(_estado));
        _timerService.Start(nuevaDuracion); // Siguiente estado
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