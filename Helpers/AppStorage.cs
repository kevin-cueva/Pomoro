using System.Reflection.Metadata;
using System.Text.Json;
using Pomoro.Domain.DTOS;
using Pomoro.Domain.Enums;
using Pomoro.Domain.Constants;

namespace Pomoro.Helpers;

public static class AppStorage
{
    public static string GetData(string key)
    {
        return Preferences.Get(key, string.Empty);
    }
    public static void SaveData(string key, string value)
    {
        Preferences.Set(key, value);
    }
    // Verificar si es primera vez
    public static bool IsFirstRun()
    {
        return !Preferences.ContainsKey(KeyStorage.FirstRunKey);
    }
    // Marcar que ya no es primera vez
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
    
     // Cargar objeto Pomodoro
    public static TimePomodoros GetTimePomodoros(ModoPomodoro modoPomodoro)
    {
        string json = Preferences.Get(modoPomodoro.ToString(), string.Empty);
        if (string.IsNullOrEmpty(json))
            return new TimePomodoros(); // valores por defecto

        return JsonSerializer.Deserialize<TimePomodoros>(json) ?? new TimePomodoros();
    }

}