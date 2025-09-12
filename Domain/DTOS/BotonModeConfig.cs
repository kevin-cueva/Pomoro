using System.Security.Cryptography.X509Certificates;

namespace Pomoro.Domain.DTOS;

public record BotonModeConfig(
    string IconSource = "default_icon.png",
    string Text = "Modo",
    string Color = "Gra500",
    Command? Command = null
);