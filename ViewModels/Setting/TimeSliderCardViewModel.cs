using System.Collections.ObjectModel;
using Pomoro.Domain.DTOS;

namespace Pomoro.ViewModels.Setting;

public class TimeSliderCardViewModel
{
    public ObservableCollection<SliderDto> SlidersData { get; } = new();
}
