using System.Reflection.Metadata;
using System.Text.Json;
using Pomoro.Domain.DTOS;
using Pomoro.Domain.Enums;
using Pomoro.Domain.Constants;

namespace Pomoro.Helpers;

public static class AppStorage
{
    /// <summary>
    /// Obtiene un valor de las preferencias de la aplicación.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string GetData(string key)
    {
        return Preferences.Get(key, string.Empty);
    }
    /// <summary>
    /// Guarda un valor en las preferencias de la aplicación.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SaveData(string key, string value)
    {
        Preferences.Set(key, value);
    }

    /// <summary>
    /// Verifica si es la primera vez que se ejecuta la aplicación.
    /// </summary>
    /// <returns></returns>
    public static bool IsFirstRun()
    {
        return !Preferences.ContainsKey(KeyStorage.FirstRunKey);
    }
    /// <summary>
    /// Marca que la primera ejecución ya se ha completado.
    /// </summary>
    public static void SetFirstRunDone()
    {
        Preferences.Set(KeyStorage.FirstRunKey, true);
    }
    // Guardar objeto Pomodoro
    public static void SavePomodoro(ModoPomodoro modoPomodoro, TimePomodoros pomodoro)
    {
        string json = JsonSerializer.Serialize(pomodoro);
        Preferences.Set(modoPomodoro.ToString(), json);
    }

    /// <summary>
    /// Obtiene el TimePomodoros guardado para un modo específico.
    /// </summary>
    /// <param name="modoPomodoro"></param>
    /// <returns></returns>
    public static TimePomodoros GetTimePomodoros(ModoPomodoro modoPomodoro)
    {
        string json = Preferences.Get(modoPomodoro.ToString(), string.Empty);
        if (string.IsNullOrEmpty(json))
            return new TimePomodoros(); // valores por defecto

        return JsonSerializer.Deserialize<TimePomodoros>(json) ?? new TimePomodoros();
    }
    /// <summary>
    /// Obtiene todos los TimePomodoros guardados en las preferencias.
    /// </summary>
    /// <returns></returns>
    public static Dictionary<ModoPomodoro,List<TimePomodoros>> GetAllTimePomodoros()
    {
        Dictionary<ModoPomodoro,List<TimePomodoros>>  pomodorosList = new Dictionary<ModoPomodoro,List<TimePomodoros>>();
        foreach (ModoPomodoro modo in Enum.GetValues<ModoPomodoro>())
        {
            TimePomodoros timePomodoro = GetTimePomodoros(modo);
            pomodorosList.Add(modo, [timePomodoro]);
        }
        return pomodorosList;
    }

}