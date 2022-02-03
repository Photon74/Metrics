using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace MetricsView
{
    /// <summary>
    /// Interaction logic for MaterialCards.xaml
    /// </summary>
    public partial class MaterialCards : UserControl
    {
        public MaterialCards()
        {
            InitializeComponent();

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Cpu Metrics",
                    Values = new ChartValues<double> { 4, 6, 5, 2 ,4 },
                    LineSmoothness = 1, //0: straight lines, 1: really smooth lines
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 5
                },
            };

            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
            YFormatter = value => value.ToString("C");

            //modifying the series collection will animate and update the chart
            //SeriesCollection.Add(new LineSeries
            //{
            //    Title = "Cpu Metrics",
            //    Values = new ChartValues<double> { 8, 3, 5, 4 },
            //    //LineSmoothness = 1, //0: straight lines, 1: really smooth lines
            //    //PointGeometry = Geometry.Parse("m 25 70.36218 20 -28 -20 22 -8 -6 z"),
            //    PointGeometrySize = 15,
            //    PointForeground = Brushes.Gray
            //});

            //modifying any series values will also animate and update the chart
            SeriesCollection[0].Values.Add(3d);

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
    }
}
