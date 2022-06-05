namespace Microcharts.Samples.Maui;

public partial class ChartConfigurationPage : ContentPage
{
    public ChartConfigurationPage(string chartType)
    {
        Items = Data.CreateXamarinExampleChartItem(chartType).ToList();
        InitializeComponent();
        Title = chartType;
    }

    public List<ExampleChartItem> Items { get; }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        var frame = sender as Frame;
        var exChartItem = frame.BindingContext as ExampleChartItem;
        Navigation.PushAsync(new ChartPage(exChartItem));
    }
}