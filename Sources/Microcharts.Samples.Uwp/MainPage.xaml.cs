using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Microcharts.Samples.Uwp
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            var charts = Data.CreateQuickstart();
            chart1.Chart = charts[0];
            chart2.Chart = charts[1];
            chart3.Chart = charts[2];
            chart4.Chart = charts[3];
            chart5.Chart = charts[4];
            chart6.Chart = charts[5];
        }
    }
}