using System;
using Pomoro.Domain.Enums;

namespace Pomoro.Domain.DTOS;

public class SliderDto
{
    public string? SufijoDescripcion { get; set; } = null;
    public  int MinValue { get; set; } = 1;
    public  int MaxValue { get; set; } = 60;
    public  string Icono { get; set; } = "book_tab_icon.png";
    public required int ValorActual { get; set; }
    public required string TipoAjuste { get; set; } = "Repeticiones";
}
