using Microcharts.Samples.Maui.Model;

namespace Microcharts.Samples.Maui;

public partial class MainPage
{
    private IEnumerable<ChartItem> _items;

    public MainPage()
    {
        InitializeComponent();

        BindingContext = this;

        var charts = Data.CreateXamarinSample();
        var items = new List<ChartItem>();
        for (var i = 0; i < charts.Length; i++)
        {
            items.Add(new ChartItem(charts[i].GetType().Name, charts[i], i));
        }

        Items = items;
    }

    public IEnumerable<ChartItem> Items
    {
        get => _items;
        set
        {
            _items = value;
            OnPropertyChanged(nameof(Items));
        }
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        var frame = sender as Frame;
        var chartItem = frame.BindingContext as ChartItem;
        Navigation.PushAsync(new ChartConfigurationPage(chartItem.Name));
    }

    private void TapGestureRecognizerLegacyChartsTapped(object sender, EventArgs e)
    {
        Navigation.PushAsync(new LegacyPage());
    }
}