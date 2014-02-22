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
using WPFClient.ServiceReferenceWPF;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using System.ComponentModel.Composition;


namespace WPFClient
{
    [Export]
    public class MainViewModel : BindableBase
    {
        public ObservableCollection<RowViewModel> DatagridSource { get; set; }

        //public ObservableCollection<DataPoint> Chart { get; private set; }
        public PlotModel Chart;
                    
        
        public MainViewModel()
        {            
            EventAggregatorHelper.Instance.GetEvent<PriceChangedEvent>().Subscribe(PriceChangedEventHandler, ThreadOption.PublisherThread, false, null);

            DatagridSource = new ObservableCollection<RowViewModel>();

            //Chart = new ObservableCollection<DataPoint>() { new DataPoint(12, 14), new DataPoint(20, 26)};
            Chart = new PlotModel();
            Chart.Axes.Add(new LinearAxis(AxisPosition.Left, 0, 50));
            Chart.Series.Add(new LineSeries());

            ServiceProxy.Instance.Client.Subscribe();            
        }

        private void PriceChangedEventHandler(Model[] quotes)
        {
            DatagridSource.Clear();
            for (int i = 0; i < quotes.Length; i++)
            {
                DatagridSource.Add(new RowViewModel(quotes[i]));
                //Chart.Add(new DataPoint(quotes[i].LastTradePrice, i*10));
                OnPropertyChanged("Chart");
            }
        }

        
                

        
    }
}
