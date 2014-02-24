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
        

        public IList<DataPoint> Chart { get; private set; }
        public IList<DataPoint> Chart1 { get; private set; }
        public IList<DataPoint> Chart2 { get; private set; }
        
                    
        
        public MainViewModel()
        {            
            EventAggregatorHelper.Instance.GetEvent<PriceChangedEvent>().Subscribe(PriceChangedEventHandler, ThreadOption.PublisherThread, false, null);

            DatagridSource = new ObservableCollection<RowViewModel>();

            Chart = new List<DataPoint>();
            Chart1 = new List<DataPoint>();
            Chart2 = new List<DataPoint>();

            ServiceProxy.Instance.Client.Subscribe();            
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
            //for (int i = 0; i < quotes.Length; i++)
            {                
                Chart.Add(new DataPoint(Refresh, quotes[0].LastTradePrice));
                Chart1.Add(new DataPoint(Refresh, quotes[1].LastTradePrice));
                Chart2.Add(new DataPoint(Refresh, quotes[2].LastTradePrice));
                Refresh++;
            }
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
