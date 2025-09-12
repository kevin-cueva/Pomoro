
using Pomoro.Domain.Enums;

namespace Pomoro.Services.Interfaces
{
    public interface IModoOperationService
    {
        ModoOperation ChangeState(ModoOperation modoOperation);
        int GetOperationDuration(ModoOperation modoOperation);
    }
}
