using Pomoro.Views.Home;
using Pomoro.Views.Settings;

namespace Pomoro.Ui;

public class TabBarUi : Shell
{
    // Estilo para las pestañas
    private  Style tabStyle = null!;

    public TabBarUi()
    {
        StyleTabBar();
        SetNavBarIsVisible(this, false);
        // ✅ Todas las pestañas como 'Tab'
        var pomodoroTab = new Tab
        {
            //Title = "Pomodoro",
            Icon = Domain.Constants.Constants.Icons.PomodoroVerde,
            Items = { new ShellContent { Content = new HomePrincipal() } },
            Style = tabStyle

        };

        var temasTab = new Tab
        {
            //Title = "Temas",
            Icon = Domain.Constants.Constants.Icons.BookTab,
            Items = { new ShellContent { /* Content = new TemasDeEstudioPage() */ } }
        };

        var graficaTab = new Tab
        {
            //Title = "Gráfica",
            Icon = Domain.Constants.Constants.Icons.ChartTab,
            Items = { new ShellContent { /* Content = new GraficaPage() */ } }
        };

        var configuracionTab = new Tab
        {
            //Title = "Configuración",
            Icon = Domain.Constants.Constants.Icons.SettingsTab,
            Items = { new ShellContent {  Content = new Settings()  } }
        };

        // Añadir directamente al Shell (sin crear TabBar manualmente)
        Items.Add(pomodoroTab);
        Items.Add(temasTab);
        Items.Add(graficaTab);
        Items.Add(configuracionTab);
    }
    
    private void StyleTabBar()
    {
         // Configurar colores del TabBar
        this.SetAppThemeColor(Shell.TabBarBackgroundColorProperty,
            (Color)Application.Current!.Resources[Domain.Constants.Constants.Colors.Background],
            (Color)Application.Current!.Resources[Domain.Constants.Constants.Colors.Background]);

        this.SetAppThemeColor(Shell.TabBarForegroundColorProperty,
            (Color)Application.Current!.Resources[Domain.Constants.Constants.Colors.CircleBorder],
            (Color)Application.Current!.Resources[Domain.Constants.Constants.Colors.CircleBorder]);

        this.SetAppThemeColor(Shell.TabBarUnselectedColorProperty,
            (Color)Application.Current!.Resources[Domain.Constants.Constants.Colors.Gray600],
            (Color)Application.Current!.Resources[Domain.Constants.Constants.Colors.Gray600]);

        tabStyle = new Style(typeof(Tab))
        {
            Setters =
            {
                new Setter
                {
                    Property = Shell.TabBarTitleColorProperty,
                    Value = (Color)Application.Current.Resources[Domain.Constants.Constants.Colors.CircleBorder]
                },
                new Setter
                {
                    Property = Shell.TabBarUnselectedColorProperty,
                    Value = (Color)Application.Current.Resources[Domain.Constants.Constants.Colors.Gray600]
                },
                new Setter
                {
                    Property = Shell.TabBarForegroundColorProperty,
                    Value = (Color)Application.Current.Resources[Domain.Constants.Constants.Colors.CircleBorder]
                }
            }
        };
    }
}