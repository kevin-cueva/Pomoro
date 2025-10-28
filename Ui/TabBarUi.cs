using Microsoft.Maui.Controls.Shapes;
using Microsoft.VisualBasic;
using Pomoro.Views.Home;

namespace Pomoro.Ui;

public class TabBarUi : Shell
{
    public TabBarUi()
    {
        SetNavBarIsVisible(this, false); // Oculta la barra superior globalmente

        // Definir colores globales del TabBar
        // Fondo del TabBar
        this.SetAppThemeColor(TabBarBackgroundColorProperty, (Color)Application.Current!.Resources[Domain.Constants.Constants.Colors.Background], Colors.Black);
        // Color de los íconos y texto seleccionados
        this.SetAppThemeColor(TabBarForegroundColorProperty, (Color)Application.Current!.Resources[Domain.Constants.Constants.Colors.ModeButtonIcon], Colors.Orange); // Claro y oscuro
        // Color de los íconos y texto no seleccionados
        this.SetAppThemeColor(TabBarUnselectedColorProperty, Colors.Gray, Colors.Gray);// Claro y oscuro

        var tabBar = new TabBar();
        // Tab 1: Pomodoro
        var pomodoroTab = new ShellContent
        {
            Title = "Pomodoro",
            Content = new HomePrincipal(),
            Icon = Domain.Constants.Constants.Icons.PomodoroVerde
        };

        // Tab 2: Temas de Estudio
        var temasTab = new ShellContent
        {
            Title = "Temas",
            //Content = new TemasDeEstudioPage(),
            Icon = "study_icon.png"
        };

        // Tab 3: Gráfica
        var graficaTab = new ShellContent
        {
            Title = "Gráfica",
            //Content = new GraficaPage(),
            Icon = "chart_icon.png"
        };

        // Tab 4: Configuración
        var configuracionTab = new ShellContent
        {
            Title = "Configuración",
            //Content = new ConfiguracionPage(),
            Icon = "settings_icon.png"
        };
        // Agregar los tabs al TabBar
        tabBar.Items.Add(pomodoroTab);
        tabBar.Items.Add(temasTab);
        tabBar.Items.Add(graficaTab);
        tabBar.Items.Add(configuracionTab);

        Shell.SetNavBarIsVisible(pomodoroTab, false); // 👈 Oculta la barra superior
        Items.Add(tabBar);
    }
}