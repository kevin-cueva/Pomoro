namespace Pomoro.Domain.DTOS;

/// <summary>
/// Represents the configuration for Pomodoro time intervals and breaks.
/// </summary>
public class TimePomodoros
{
    /// <summary>
    /// Gets or sets the duration of a work interval in minutes.
    /// Default is 25 minute.
    /// </summary>
    public int WorkDuration { get; set; } = 25; 
    /// Gets or sets the duration of a short break in minutes.
    /// Default is 5 minutes.
    /// </summary>
    public int ShortBreakDuration { get; set; } = 5; 
    /// <summary>
    /// Gets or sets the duration of a long break in minutes.
    /// Default is 15 minutes.
    /// </summary>
    public int LongBreakDuration { get; set; } = 15; // Duración del descanso largo en minutos
    /// <summary>
    /// Gets or sets the number of Pomodoro repetitions before a long break.
    /// Default is 4 repetitions.
    /// </summary>
    public int Repetitions { get; set; } = 4; 
}


