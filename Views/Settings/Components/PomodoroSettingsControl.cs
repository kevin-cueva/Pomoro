using System.Runtime.CompilerServices;
using Pomoro.Domain.DTOS;

namespace Pomoro.Views.Settings.Components;

public class PomodoroSettingsControl : ContentView
{
    public static readonly BindableProperty PomodoroProperty =
        BindableProperty.Create(
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

    private readonly TimeSliderCard _timeSliderCard;
    public PomodoroSettingsControl()
    {
        
        _timeSliderCard = new TimeSliderCard();
        Content = new VerticalStackLayout
        {
            Children = { _timeSliderCard }
        };
    }

    private static void OnPomodoroChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        var control = (PomodoroSettingsControl)bindable;
        var pomodoro = newValue as PomodoroItemDto;

        // control._label.Text = pomodoro is null
        //     ? string.Empty
        //     : $"Este es un control de configuración de Pomodoro. {pomodoro.NombreModo}";
    }
}
