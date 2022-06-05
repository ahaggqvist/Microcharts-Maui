using Microcharts.Samples.Maui.Model;

namespace Microcharts.Samples.Maui;

public partial class LegacyPage : ContentPage
{
    public LegacyPage()
    {
        var charts = Data.CreateXamarinLegacySample();
        var items = new List<ChartItem>();
        for (var i = 0; i < charts.Length; i++)
        {
            items.Add(new ChartItem(charts[i].GetType().Name, charts[i], i));
        }

        Items = items;
        InitializeComponent();
    }

    public List<ChartItem> Items { get; }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        var frame = sender as Frame;
        var chartItem = frame.BindingContext as ChartItem;
        Navigation.PushAsync(new ChartConfigurationPage(chartItem.Name));
    }
}