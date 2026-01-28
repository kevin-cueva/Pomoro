using System.Collections.ObjectModel;
using Pomoro.Domain.DTOS;
using Pomoro.Domain.Enums;

namespace Pomoro.ViewModels.Setting;

public class TimeSliderCardViewModel
{
    public List<SliderDto> SlidersData { get; private set; }
    public ModoPomodoro ModoPomodoroUpdate { get; private set; }

    public TimeSliderCardViewModel()
    {
        SlidersData = [];
    }

    public bool HasData()
    {
        return SlidersData != null && SlidersData.Count > 0;
    }

    public string GetNoDataMessage()
    {
        return "No hay datos para mostrar.";
    }

    public void UpdateSlidersData(List<SliderDto> newData, ModoPomodoro modoPomodoro)
    {
        SlidersData = newData;
        ModoPomodoroUpdate = modoPomodoro;
    }

    public List<SliderDto> GetCurrentSlidersData()
    {
        return SlidersData;
    }

    public string FormatTimeValue(double value)
    {
        return $"{(int)value} min";
    }

    public string GetSliderTitle(SliderDto sliderData)
    {
        return sliderData?.TipoAjuste ?? "Trabajo";
    }

    public string GetSliderIcon(SliderDto sliderData)
    {
        return sliderData?.Icono ?? Domain.Constants.Constants.Icons.BookTab;
    }

    public double GetSliderMinimum(SliderDto sliderData)
    {
        return sliderData?.MinValue ?? 1;
    }

    public double GetSliderMaximum(SliderDto sliderData)
    {
        return sliderData?.MaxValue ?? 60;
    }

    public double GetSliderValue(SliderDto sliderData)
    {
        return sliderData?.ValorActual ?? 25;
    }

    public string GetMinTimeLabel(SliderDto sliderData)
    {
        return $"{(sliderData?.MinValue ?? 1)} min";
    }

    public string GetMaxTimeLabel(SliderDto sliderData)
    {
        return $"{(sliderData?.MaxValue ?? 60)} min";
    }

    public string GetSuffixDescription(SliderDto sliderData)
    {
        return sliderData?.SufijoDescripcion ?? "min";
    }

    public bool ShowSuffixDescription(string suffix)
    {
        return !string.IsNullOrEmpty(suffix);
    }
}