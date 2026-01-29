using System.ComponentModel;

public class SliderDto : INotifyPropertyChanged
{
    private int _valorActual;

    public int ValorActual
    {
        get => _valorActual;
        set
        {
            if (_valorActual != value)
            {
                _valorActual = value;
                OnPropertyChanged(nameof(ValorActual));
            }
        }
    }

    public int MinValue { get; set; }
    public int MaxValue { get; set; }
    public string Icono { get; set; } = string.Empty;
    public string TipoAjuste { get; set; } = string.Empty;
    public string? SufijoDescripcion { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string name)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
