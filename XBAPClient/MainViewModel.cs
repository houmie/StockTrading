using Microsoft.Practices.Prism.Mvvm;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using System.ComponentModel.Composition;
using XBAPClient.ServiceReferenceXBAP;


namespace XBAPClient
{
    [Export]
    public class MainViewModel : BindableBase
    {
        public ObservableCollection<RowViewModel> DatagridSource { get; set; }
        List<LineSeries> _lineSeries;

        //public IList<DataPoint> Chart { get; private set; }
        //public IList<DataPoint> Chart1 { get; private set; }
        //public IList<DataPoint> Chart2 { get; private set; }

        private PlotModel _chartModel;

        public PlotModel ChartModel
        {
            get { return _chartModel; }
            set 
            {
                if (value != _chartModel)
                {
                    _chartModel = value;
                    OnPropertyChanged("ChartModel");
                }                
            }
        }

                    
        
        public MainViewModel()
        {            
            EventAggregatorHelper.Instance.GetEvent<PriceChangedEvent>().Subscribe(PriceChangedEventHandler, ThreadOption.PublisherThread, false, null);

            DatagridSource = new ObservableCollection<RowViewModel>();
            ChartModel = new PlotModel();
            _lineSeries = new List<LineSeries>();
            SetupChartModel();
            

            //Chart = new List<DataPoint>();
            //Chart1 = new List<DataPoint>();
            //Chart2 = new List<DataPoint>();


            ServiceProxy.Instance.Client.Subscribe();            
        }


        private void SetupChartModel()
        {
            ChartModel.LegendTitle = "Legend";
            ChartModel.LegendOrientation = LegendOrientation.Horizontal;
            ChartModel.LegendPlacement = LegendPlacement.Outside;
            ChartModel.LegendPosition = LegendPosition.TopRight;
            ChartModel.LegendBackground = OxyColors.Black;
            ChartModel.LegendBorder = OxyColors.White;
            ChartModel.TextColor = OxyColors.White;
            ChartModel.Background = OxyColors.Black;
            ChartModel.PlotAreaBorderColor = OxyColors.White;
            var dateAxis = new DateTimeAxis(AxisPosition.Bottom, "Time", "HH:mm") { MaximumPadding=0, MinimumPadding=0, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, IntervalLength = 80, MajorGridlineColor = OxyColor.FromArgb(50, 255, 255, 255), MinorGridlineColor = OxyColor.FromArgb(30, 255, 255, 255)};
            ChartModel.Axes.Add(dateAxis);
            var valueAxis = new LinearAxis(AxisPosition.Left, 0) { MaximumPadding = 0, MinimumPadding = 0, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Title = "Price", MajorGridlineColor = OxyColor.FromArgb(50, 255, 255, 255), MinorGridlineColor = OxyColor.FromArgb(30, 255, 255, 255) };
            ChartModel.Axes.Add(valueAxis);

            _lineSeries.Add(new LineSeries { StrokeThickness = 2, MarkerSize = 1, MarkerType = MarkerType.Circle, Smooth = true });
            _lineSeries.Add(new LineSeries { StrokeThickness = 2, MarkerSize = 1, MarkerType = MarkerType.Circle, Smooth = true });
            _lineSeries.Add(new LineSeries { StrokeThickness = 2, MarkerSize = 1, MarkerType = MarkerType.Circle, Smooth = true });

            foreach (var lineSerie in _lineSeries)
            {
                ChartModel.Series.Add(lineSerie);    
            }
            
        }


        private void PriceChangedEventHandler(Model[] quotes)
        {
            if (DatagridSource.Count == 0)
            {
                for (int i = 0; i < quotes.Length; i++)
                {
                    DatagridSource.Add(new RowViewModel(quotes[i]));
                }
            }
            else
            {
                for (int i = 0; i < quotes.Length; i++)
                {
                    DatagridSource[i].DayHighPrice = quotes[i].DayHighPrice;
                    DatagridSource[i].DayLowPrice = quotes[i].DayLowPrice;
                    DatagridSource[i].LastTradeDate = quotes[i].LastTradeDate;
                    DatagridSource[i].LastTradePrice = quotes[i].LastTradePrice;
                    DatagridSource[i].Name = quotes[i].Name;
                    DatagridSource[i].OpenPrice = quotes[i].OpenPrice;
                    DatagridSource[i].Symbol = quotes[i].Symbol;                    
                }
                
            }
                                    
            for (int i = 0; i < _lineSeries.Count; i++)
            {
                _lineSeries[i].Title = quotes[i].Symbol;
                _lineSeries[i].Points.Add(new DataPoint(DateTimeAxis.ToDouble(quotes[i].TradeTime), quotes[i].LastTradePrice));
            }
            
            //Chart.Add(new DataPoint(Refresh, quotes[0].LastTradePrice));
            //Chart1.Add(new DataPoint(Refresh, quotes[1].LastTradePrice));
            //Chart2.Add(new DataPoint(Refresh, quotes[2].LastTradePrice));
            Refresh++;
            
        }

        private int refresh;
        public int Refresh
        {
            get
            {
                return this.refresh;
            }

            set
            {
                if (this.refresh == value)
                {
                    return;
                }

                this.refresh = value;
                OnPropertyChanged("Refresh");
            }
        }
                

        
    }
}
