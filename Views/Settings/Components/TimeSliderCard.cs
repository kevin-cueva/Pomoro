using Microsoft.Maui.Controls.Shapes;
using Pomoro.ViewModels.Setting;

namespace Pomoro.Views.Settings.Components;

public class TimeSliderCard : ContentView
{
    public TimeSliderCardViewModel ViewModel { get; }

    public TimeSliderCard()
    {
        ViewModel = new TimeSliderCardViewModel();
        Content = new VerticalStackLayout { Spacing = 10 };
    }

    public void Initialize()
    {
        var layout = (VerticalStackLayout)Content;
        layout.Children.Clear();

        foreach (var slider in ViewModel.SlidersData)
        {
            layout.Children.Add(CreateSliderCard(slider));
        }
    }

    private View CreateSliderCard(SliderDto slider)
    {
        var icon = new Image
        {
            WidthRequest = 24,
            HeightRequest = 24,
            Source = slider.Icono
        };

        var title = new Label
        {
            FontSize = 18,
            VerticalOptions = LayoutOptions.Center
        };
        title.SetBinding(Label.TextProperty, nameof(SliderDto.TipoAjuste));

        var valueLabel = new Label
        {
            FontSize = 16,
            HorizontalOptions = LayoutOptions.End
        };
        valueLabel.SetBinding(
            Label.TextProperty,
            nameof(SliderDto.ValorActual),
            stringFormat: "{0} min"
        );

        var sliderControl = new Slider
        {
            HorizontalOptions = LayoutOptions.Fill,
            BindingContext = slider
        };
        sliderControl.SetBinding(Slider.MinimumProperty, nameof(SliderDto.MinValue));
        sliderControl.SetBinding(Slider.MaximumProperty, nameof(SliderDto.MaxValue));
        sliderControl.SetBinding(
            Slider.ValueProperty,
            nameof(SliderDto.ValorActual),
            BindingMode.TwoWay
        );

        var header = new HorizontalStackLayout
        {
            Spacing = 10,
            Children = { icon, title, valueLabel }
        };

        var minLabel = new Label { FontSize = 12 };
        minLabel.SetBinding(
            Label.TextProperty,
            nameof(SliderDto.MinValue),
            stringFormat: "{0} min"
        );

        var maxLabel = new Label
        {
            FontSize = 12,
            HorizontalOptions = LayoutOptions.End
        };
        maxLabel.SetBinding(
            Label.TextProperty,
            nameof(SliderDto.MaxValue),
            stringFormat: "{0} min"
        );

        var range = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Auto)
            }
        };
        range.Add(minLabel, 0, 0);
        range.Add(maxLabel, 1, 0);

        var suffix = new Label { FontSize = 12 };
        suffix.SetBinding(Label.TextProperty, nameof(SliderDto.SufijoDescripcion));

        suffix.BindingContextChanged += (s, e) =>
        {
            if (suffix.BindingContext is SliderDto dto)
                suffix.IsVisible = !string.IsNullOrWhiteSpace(dto.SufijoDescripcion);
        };

        // 🔑 AQUÍ está la clave
        return new Border
        {
            BindingContext = slider, // ✅ cada card tiene su propio contexto
            Stroke = Colors.LightGray,
            StrokeThickness = 1,
            Padding = 15,
            Margin = new Thickness(0, 10),
            StrokeShape = new RoundRectangle { CornerRadius = 12 },
            Content = new VerticalStackLayout
            {
                Spacing = 10,
                Children = { header, sliderControl, range, suffix }
            }
        };
    }
}