using Pomoro.Domain.DTOS;
using Pomoro.Domain.Enums;
using Pomoro.Services.Interfaces;
using Pomoro.Helpers;
using Pomoro.Views.Home.Components;
using Pomoro.Domain.Constants;
namespace Pomoro.Services;

public class ModoOperationService : IModoOperationService
{
    private int WorkCount = 0;
    private TimePomodoros PomodoroModeStorage = new();
    public int GetOperationDuration(ModoOperation modoOperation)
    {
        PomodoroModeStorage = GetTimePomodorosStorage();
        return modoOperation switch
        {
            ModoOperation.Work => PomodoroModeStorage.WorkDuration,
            ModoOperation.ShortBreak => PomodoroModeStorage.ShortBreakDuration,
            ModoOperation.LongBreak => PomodoroModeStorage.LongBreakDuration,
            _ => throw new ArgumentOutOfRangeException(nameof(modoOperation), modoOperation, null)
        };
    }


    public ModoOperation ChangeState(ModoOperation modoOperation)
    {
        
        if (modoOperation == ModoOperation.Work)
        {
            WorkCount++;
            if (WorkCount >= PomodoroModeStorage.Repetitions)
            {
                WorkCount = 0;
                return ModoOperation.LongBreak;
            }
            else
            {
                return ModoOperation.ShortBreak;
            }

        }
        else
        {
            return ModoOperation.Work;
        }
    }

    private static TimePomodoros GetTimePomodorosStorage()
{
    string savedMode = AppStorage.GetData(KeyStorage.CurrentMode);

    // Valor por defecto si falla la lectura o conversión
    ModoPomodoro mode = ModoPomodoro.Automatic;

    // Intentar parsear solo si el valor no es nulo/vacío
    if (!string.IsNullOrWhiteSpace(savedMode))
    {
        if (Enum.TryParse<ModoPomodoro>(savedMode, ignoreCase: true, out ModoPomodoro parsedMode))
        {
            mode = parsedMode;
        }
        else
        {
            // Opcional: loguear que el modo guardado es inválido
            System.Diagnostics.Debug.WriteLine($"Modo Pomodoro desconocido: '{savedMode}'. Usando modo por defecto: {mode}.");
        }
    }

    // Obtener los tiempos según el modo (ya sea el parseado o el por defecto)
    TimePomodoros timesPomodoro = AppStorage.GetTimePomodoros(mode);
    return timesPomodoro;
}
}