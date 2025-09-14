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
    
    ModoPomodoro mode = Utils.ParseEnum(savedMode, ModoPomodoro.Automatic);

    // Obtener los tiempos según el modo (ya sea el parseado o el por defecto)
    TimePomodoros timesPomodoro = AppStorage.GetTimePomodoros(mode);
    return timesPomodoro;
}
}