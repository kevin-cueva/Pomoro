using System;
using Pomoro.Services.Interfaces;

namespace Pomoro.Services
{
    public class TimerService : ITimerService, IDisposable
    {
        private IDispatcherTimer _timer;
        private DateTime _inicio;
        private TimeSpan _duracion;
        private bool _isRunning;

        public float Progreso { get; private set; }
        public TimeSpan TiempoRestante { get; private set; }
        public bool IsRunning => _isRunning;

        public event Action OnTimerTick;
        public event Action OnTimerCompleted;

        private readonly IDispatcher _dispatcher;

        public TimerService(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public void Start(TimeSpan duracion)
        {
            if (_isRunning) Stop();

            _duracion = duracion;
            _inicio = DateTime.Now;
            _isRunning = true;

            _timer = _dispatcher.CreateTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(100);
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var transcurrido = DateTime.Now - _inicio;
            TiempoRestante = _duracion - transcurrido;

            if (TiempoRestante <= TimeSpan.Zero)
            {
                TiempoRestante = TimeSpan.Zero;
                Progreso = 1.0f;
                Stop();
                OnTimerCompleted?.Invoke();
            }
            else
            {
                Progreso = (float)(transcurrido.TotalSeconds / _duracion.TotalSeconds);
            }

            OnTimerTick?.Invoke();
        }

        public void Stop()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Tick -= Timer_Tick; // Evita memory leaks
                _timer = null;
            }
            _isRunning = false;
        }

        public void Pause()
        {
            if (_isRunning)
            {
                _timer?.Stop();
                _isRunning = false;
            }
        }

        public void Dispose()
        {
            Stop();
            _timer = null;
        }
    }
}