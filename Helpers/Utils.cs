using Pomoro.Domain.Enums;

namespace Pomoro.Helpers;
public static class Utils
{
    /// <summary>
    /// Convierte un string al valor de un enum especificado. Si falla, devuelve un valor por defecto.
    /// Ignora mayúsculas/minúsculas.
    /// </summary>
    /// <typeparam name="T">Tipo de enum</typeparam>
    /// <param name="value">Valor en string a convertir</param>
    /// <param name="defaultValue">Valor por defecto si el string es nulo, vacío o inválido</param>
    /// <returns>Valor parseado del enum, o valor por defecto si falla</returns>
    public static T ParseEnum<T>(string value, T defaultValue) where T : struct, Enum
    {
        if (!string.IsNullOrWhiteSpace(value) &&
            Enum.TryParse<T>(value, ignoreCase: true, out T parsedValue))
        {
            return parsedValue;
        }

        System.Diagnostics.Debug.WriteLine($"Valor inválido o vacío para enum {typeof(T).Name}: '{value}'. Usando valor por defecto: {defaultValue}.");
        return defaultValue;
    }

    /// <summary>
    /// Obtiene el nombre descriptivo de un modo de Pomodoro.
    /// </summary>
    /// <param name="modo"></param>
    /// <returns></returns>
    public static string NombreModoPomodoro(ModoPomodoro modo)
    {
        return modo switch
        {
            ModoPomodoro.PorDefecto => "Por defecto",
            ModoPomodoro.Morning => "Mañana",
            ModoPomodoro.Night => "Noche",
            ModoPomodoro.Flexible => "Flexible",
            _ => "Desconocido"
        };
    }

    /// <summary>
    /// Convierte un nombre descriptivo a su correspondiente ModoPomodoro.
    /// </summary>
    /// <param name="nombre">Nombre descriptivo del modo</param>
    /// <returns>ModoPomodoro correspondiente, o PorDefecto si no coincide</returns>
    public static ModoPomodoro ModoPomodoroPorNombre(string nombre)
    {
        return nombre switch
        {
            "Por defecto" => ModoPomodoro.PorDefecto,
            "Mañana" => ModoPomodoro.Morning,
            "Noche" => ModoPomodoro.Night,
            "Flexible" => ModoPomodoro.Flexible,
            _ => ModoPomodoro.PorDefecto
        };
    }

    
}