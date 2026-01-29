using Pomoro.Domain.Constants;
using Pomoro.Domain.DTOS;
using Pomoro.Domain.Enums;
using Pomoro.Helpers;

namespace Pomoro.Views.Settings.Components;

public class PomodoroSettingsControl : ContentView
{
    public static readonly BindableProperty PomodoroProperty =
        BindableProperty.Create(
            nameof(Pomodoro),
            typeof(PomodoroItemDto),
            typeof(PomodoroSettingsControl),
            propertyChanged: OnPomodoroChanged);

    public PomodoroItemDto Pomodoro
    {
        get => (PomodoroItemDto)GetValue(PomodoroProperty);
        set => SetValue(PomodoroProperty, value);
    }

    private readonly TimeSliderCard _timeSliderCard;
    private readonly Button _guardarButton;

    public PomodoroSettingsControl()
    {
        _timeSliderCard = new TimeSliderCard();
        _guardarButton = new Button
        {
            Text = "Guardar Cambios",
            BackgroundColor = Colors.Green,
            TextColor = Colors.White,
            CornerRadius = 8,
            Margin = new Thickness(0, 10)
        };

        _guardarButton.Clicked += GuardarCambios;

        Content = new VerticalStackLayout
        {
            Children = { _timeSliderCard, _guardarButton }
        };
    }

    private static void OnPomodoroChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (PomodoroSettingsControl)bindable;
        var pomodoro = (PomodoroItemDto)newValue;

        control._timeSliderCard.ViewModel.SlidersData.Clear();

        control._timeSliderCard.ViewModel.SlidersData.Add(new SliderDto
        {
            TipoAjuste = "Trabajo",
            MinValue = Constants.Tiempos.MinutosMinTrabajo,
            MaxValue = Constants.Tiempos.MinutosMaxTrabajo,
            ValorActual = pomodoro.Tiempos[0].WorkDuration
        });

        control._timeSliderCard.ViewModel.SlidersData.Add(new SliderDto
        {
            TipoAjuste = "Descanso Corto",
            MinValue = Constants.Tiempos.MinutosMinDescanso,
            MaxValue = Constants.Tiempos.MinutosMaxDescanso,
            ValorActual = pomodoro.Tiempos[0].ShortBreakDuration
        });

        control._timeSliderCard.ViewModel.SlidersData.Add(new SliderDto
        {
            TipoAjuste = "Descanso Largo",
            MinValue = Constants.Tiempos.MinutosMinDescansoLargo,
            MaxValue = Constants.Tiempos.MinutosMaxDescansoLargo,
            ValorActual = pomodoro.Tiempos[0].LongBreakDuration
        });

        control._timeSliderCard.ViewModel.SlidersData.Add(new SliderDto
        {
            TipoAjuste = "Repeticiones",
            MinValue = Constants.Tiempos.VecesMinimasParaDescansoLargo,
            MaxValue = Constants.Tiempos.VecesMaximasParaDescansoLargo,
            ValorActual = pomodoro.Tiempos[0].Repetitions,
            SufijoDescripcion = "Pomodoros antes del descanso largo"
        });

        control._timeSliderCard.Initialize();
    }

    private void GuardarCambios(object? sender, EventArgs e)
    {
        var sliders = _timeSliderCard.ViewModel.SlidersData;

        Pomodoro.Tiempos[0].WorkDuration =
            sliders.First(x => x.TipoAjuste == "Trabajo").ValorActual;

        Pomodoro.Tiempos[0].ShortBreakDuration =
            sliders.First(x => x.TipoAjuste == "Descanso Corto").ValorActual;

        Pomodoro.Tiempos[0].LongBreakDuration =
            sliders.First(x => x.TipoAjuste == "Descanso Largo").ValorActual;

        Pomodoro.Tiempos[0].Repetitions =
            sliders.First(x => x.TipoAjuste == "Repeticiones").ValorActual;

        AppStorage.SavePomodoro(
            Utils.ModoPomodoroPorNombre(Pomodoro.NombreModo),
            Pomodoro.Tiempos[0]
        );

        Application.Current!.MainPage!
            .DisplayAlert("Éxito", "Cambios guardados correctamente", "OK");
    }
}
