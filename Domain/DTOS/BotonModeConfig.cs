namespace Pomoro.Domain.DTOS;

public record BotonModeConfig(
    string IconSource = "default_icon.png",
    string Text = "Modo",
    string Color = "#888888",
    Command? Command = null
);