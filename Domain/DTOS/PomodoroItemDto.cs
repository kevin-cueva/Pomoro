using System.ComponentModel;
using Pomoro.Domain.DTOS;

public class PomodoroItemDto : INotifyPropertyChanged
{
    private string _nombreModo;
    public required string NombreModo
    {
        get => _nombreModo;
        set
        {
            if (_nombreModo != value)
            {
                _nombreModo = value;
                OnPropertyChanged(nameof(NombreModo));
            }
        }
    }

    private List<TimePomodoros> _tiempos;
    public required List<TimePomodoros> Tiempos
    {
        get => _tiempos;
        set
        {
            if (_tiempos != value)
            {
                _tiempos = value;
                OnPropertyChanged(nameof(Tiempos));
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}