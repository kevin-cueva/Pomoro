using Pomoro.Domain.DTOS;
using Pomoro.Services.Interfaces;
namespace Pomoro.Services;

public class ModoOperationService : IModoOperationService
{
    private int WorkCount = 0;
    private readonly TimePomodoros _timePomodoros = new();
    public int GetOperationDuration(ModoOperation modoOperation)
    {
        return modoOperation switch
        {
            ModoOperation.Work => _timePomodoros.WorkDuration,
            ModoOperation.ShortBreak => _timePomodoros.ShortBreakDuration,
            ModoOperation.LongBreak => _timePomodoros.LongBreakDuration,
            _ => throw new ArgumentOutOfRangeException(nameof(modoOperation), modoOperation, null)
        };
    }

    
    public ModoOperation ChangeState(ModoOperation modoOperation)
    {
        if (modoOperation == ModoOperation.Work)
        {
            WorkCount++;
            if (WorkCount >= _timePomodoros.Repetitions)
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
}