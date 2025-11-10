using System;

namespace Pomoro.Domain.DTOS;

public class PomodoroItemDto
{
	public required string NombreModo { get; set; }
	public required List<TimePomodoros> Tiempos { get; set; }
}