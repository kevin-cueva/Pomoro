using System.Security.Cryptography.X509Certificates;
using Pomoro.Domain.Enums;

namespace Pomoro.Domain.DTOS;

public record BotonModeConfig(
    string IconSource = "default_icon.png",
    string Text = "Modo",
    ModoPomodoro Modo = ModoPomodoro.PorDefecto,
    Command? Command = null
);