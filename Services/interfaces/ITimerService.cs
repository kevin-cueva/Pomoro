namespace Pomoro.Services.Interfaces
{
    public interface ITimerService
    {
        float Progreso { get; }
        TimeSpan TiempoRestante { get; }
        bool IsRunning { get; }

        // Evento para notificar cambios (progreso, tiempo restante)
        event Action OnTimerTick;
        event Action OnTimerCompleted;

        void Start(TimeSpan duracion);
        void Stop();
        void Pause();
    }
}