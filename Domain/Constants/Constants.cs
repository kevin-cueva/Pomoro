namespace Pomoro.Domain.Constants;

public static class Constants
{
    /// <summary>
    /// Íconos utilizados en la aplicación. Usa estos nombres para referenciar íconos en lugar de rutas o nombres de archivos.
    /// se deben agregar a la carpeta Resources/Images
    /// </summary>
    public static class Icons
    {
        public const string BookTab = "ibook_icon";
        public const string ChartTab = "ichart_icon";
        public const string SettingsTab = "isettings_icon";
        public const string SettingsWhite = "isettings_icon_white";
        public const string PomodoroVerde = "ipomodoro_verde";
        public const string PomodoroMarron = "ipomodoro_marron";
        public const string SunriseWhite = "isunrise_white";
        public const string SunriseMarron = "isunrise_marron";
        public const string SunriseVerde = "isunrise_verde";
        public const string MoonWhite = "imoon_white";
        public const string MoonMarron = "imoon_marron";
        public const string MoonVerde = "imoon_verde";
        public const string ManualWhite = "isettings_maul_white";
        public const string SolutionVerde = "isolution_verde";
        public const string SolutionMarron = "isolution_marron";
        public const string PlayWhite = "iplay_white";
        public const string PauseWhire = "ipause_white";
        public const string ReloadWhite = "ireload_white";
        // 👉 agrega aquí más íconos
    }

    /// <summary>
    /// Colores utilizados en la aplicación. Usa estos nombres para referenciar colores en lugar de valores hexadecimales.
    /// se deben definir en Resources/Styles/Colors.xaml
    /// </summary>
    public static class Colors
    {
        public const string Primary = "Primary";
        public const string BotonSelect = "Black";
        public const string BotonNoSelect = "Gray500";
        public const string Secondary = "Secondary";
        public const string Accent = "Accent";
        public const string White = "White";
        public const string Gray600 = "Gray600";
        public const string Error = "Error";
        public const string BotonPlay = "ButtonPlay";
        public const string Background = "Background";
        public const string CircleBorder = "CircleBorder";
        public const string ButtonSecondary = "ButtonSecondary";
        public const string ModeButton = "ModeButton";
        public const string ModeButtonIcon = "ModeButtonIcon";
        public const string ModeButtonActive = "ModeButtonActive";
        public const string ModeButtonActiveIcon = "ModeButtonActiveIcon";
        public const string Text = "Text";

        
        // 👉 agrega aquí más colores
    }

    public static class Tiempos
    {
        public const int MinutosMinTrabajo = 1;
        public const int MinutosMaxTrabajo = 60;
        public const int MinutosMinDescanso = 1;
        public const int MinutosMaxDescanso = 30;
        public const int MinutosMinDescansoLargo = 5;
        public const int MinutosMaxDescansoLargo = 60;
        public const int VecesMinimasParaDescansoLargo = 2;
        public const int VecesMaximasParaDescansoLargo = 10;
    }
}
