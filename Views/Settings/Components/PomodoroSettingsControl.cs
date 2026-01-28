using Pomoro.Domain.DTOS;
using Pomoro.Domain.Enums;
using Pomoro.Helpers;

namespace Pomoro.Views.Settings.Components;

public class PomodoroSettingsControl : ContentView
{
    public static readonly BindableProperty PomodoroProperty = BindableProperty.Create(
        nameof(Pomodoro),
        typeof(PomodoroItemDto),
        typeof(PomodoroSettingsControl),
        default(PomodoroItemDto),
        propertyChanged: OnPomodoroChanged);

    public PomodoroItemDto Pomodoro
    {
        get => (PomodoroItemDto)GetValue(PomodoroProperty);
        set => SetValue(PomodoroProperty, value);
    }

    public  TimeSliderCard _timeSliderCard;

    public PomodoroSettingsControl()
    {
        _timeSliderCard = new TimeSliderCard();

        Content = new VerticalStackLayout
        {
            Children = { _timeSliderCard }
        };

        InitializeSlidersData();
    }

    private void InitializeSlidersData()
    {
        var sliderDataJob = new SliderDto
        {
            TipoAjuste = "Trabajo",
            MinValue = Domain.Constants.Constants.Tiempos.MinutosMinTrabajo,
            MaxValue = Domain.Constants.Constants.Tiempos.MinutosMaxTrabajo,
            SufijoDescripcion = string.Empty,
            Icono = Domain.Constants.Constants.Icons.BookTab,
            ValorActual = Pomodoro?.Tiempos[0].WorkDuration ?? 25
        };

        var sliderDataBreak = new SliderDto
        {
            TipoAjuste = "Descanso Corto",
            MinValue = Domain.Constants.Constants.Tiempos.MinutosMinDescanso,
            MaxValue = Domain.Constants.Constants.Tiempos.MinutosMaxDescanso,
            SufijoDescripcion = string.Empty,
            Icono = Domain.Constants.Constants.Icons.ManualWhite,
            ValorActual = Pomodoro?.Tiempos[0].ShortBreakDuration ?? 5
        };

        var sliderDataLongBreak = new SliderDto
        {
            TipoAjuste = "Descanso Largo",
            MinValue = Domain.Constants.Constants.Tiempos.MinutosMinDescansoLargo,
            MaxValue = Domain.Constants.Constants.Tiempos.MinutosMaxDescansoLargo,
            SufijoDescripcion = string.Empty,
            Icono = Domain.Constants.Constants.Icons.PauseWhire,
            ValorActual = Pomodoro?.Tiempos[0].LongBreakDuration ?? 15
        };

        var sliderDataRepetitions = new SliderDto
        {
            TipoAjuste = "Repeticiones",
            MinValue = Domain.Constants.Constants.Tiempos.VecesMinimasParaDescansoLargo,
            MaxValue = Domain.Constants.Constants.Tiempos.VecesMaximasParaDescansoLargo,
            SufijoDescripcion = "Pomodoros antes del descanso largo",
            Icono = Domain.Constants.Constants.Icons.SolutionMarron,
            ValorActual = Pomodoro?.Tiempos[0].Repetitions ?? 4
        };

        _timeSliderCard.UpdateSlidersData(new List<SliderDto>
        {
            sliderDataJob,
            sliderDataBreak,
            sliderDataLongBreak,
            sliderDataRepetitions
        }, ModoPomodoro.PorDefecto);
    }

    private static void OnPomodoroChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (PomodoroSettingsControl)bindable;
        var pomodoro = newValue as PomodoroItemDto;

        if (pomodoro != null && control._timeSliderCard.ViewModel.SlidersData != null && control._timeSliderCard.ViewModel.SlidersData.Count >= 4)
        {
            control._timeSliderCard.ViewModel.SlidersData[0].ValorActual = pomodoro.Tiempos[0].WorkDuration;
            control._timeSliderCard.ViewModel.SlidersData[1].ValorActual = pomodoro.Tiempos[0].ShortBreakDuration;
            control._timeSliderCard.ViewModel.SlidersData[2].ValorActual = pomodoro.Tiempos[0].LongBreakDuration;
            control._timeSliderCard.ViewModel.SlidersData[3].ValorActual = pomodoro.Tiempos[0].Repetitions;
            
            // Reconstruir las tarjetas para reflejar los nuevos valores
            control._timeSliderCard.UpdateSlidersData(
                control._timeSliderCard.GetCurrentSlidersData(), 
                Utils.ModoPomodoroPorNombre(pomodoro.NombreModo));
        }
    }
}
