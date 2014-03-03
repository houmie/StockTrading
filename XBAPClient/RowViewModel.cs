using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBAPClient.ServiceReferenceXBAP;



namespace XBAPClient
{
    public class RowViewModel : BindableBase
    {
        Model _model;

        public RowViewModel(Model model)
        {
            _model = model;        
        }

        public string Symbol 
        {
            get { return _model.Symbol; }
            set 
            {
                if(value != _model.Symbol)
                {
                    _model.Symbol = value;
                    OnPropertyChanged("Symbol");
                }
            } 
        }

        
        public string Name
        {
            get { return _model.Name; }
            set
            {
                if (value != _model.Name)
                {
                    _model.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        

        public double DayHighPrice
        {
            get { return _model.DayHighPrice; }
            set {
                if (value != _model.DayHighPrice)
                {
                    _model.DayHighPrice = value;
                    OnPropertyChanged("DayHighPrice");
                }
            }
        }

        public double DayLowPrice
        {
            get { return _model.DayLowPrice; }
            set
            {
                if (value != _model.DayLowPrice)
                {
                    _model.DayLowPrice = value;
                    OnPropertyChanged("DayLowPrice");
                }
            }
        }

        public double LastTradePrice
        {
            get { return _model.LastTradePrice; }
            set
            {
                if (value != _model.LastTradePrice)
                {
                    _model.LastTradePrice = value;
                    OnPropertyChanged("LastTradePrice");
                }
            }
        }

        public DateTime LastTradeDate
        {
            get { return _model.LastTradeDate; }
            set
            {
                if (value != _model.LastTradeDate)
                {
                    _model.LastTradeDate = value;
                    OnPropertyChanged("LastTradeDate");
                }
            }
        }

        public double OpenPrice
        {
            get { return _model.OpenPrice; }
            set
            {
                if (value != _model.OpenPrice)
                {
                    _model.OpenPrice = value;
                    OnPropertyChanged("OpenPrice");
                }
            }
        }

        
        
        
    }
}
