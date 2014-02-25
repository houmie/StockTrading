using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using System.Net;
using System.IO;
using System.ServiceModel;
using System.Threading;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class Service : IService
    {
        readonly string _path = @"quotes.csv";        
        public static event EventHandler<PriceChangeEventArgs> PriceChanged;
        IClient _callback = null;
        List<Model> _quotes;
        readonly object _locker = new object();

        public Service()
        {
            _quotes = ReadStockPriceFromCSV();
        }


        public void DownloadStockPriceFromYahoo()
        {
            if (File.Exists(_path))
            {
                File.Delete(_path);
            }
            using (WebClient Client = new WebClient())
            {
                Client.DownloadFile("http://finance.yahoo.com/d/quotes.csv?s=MSFT+INTC+GM&f=snl1d1ohg", _path);
            }
        }


        public List<Model> ReadStockPriceFromCSV()
        {
            var result = new List<Model>();
            DownloadStockPriceFromYahoo();
            if (File.Exists(_path))
            {                
                using (var myReader = new TextFieldParser(_path))
                {
                    myReader.TextFieldType = FieldType.Delimited;
                    myReader.Delimiters = new string[] { "," };
                    string[] row;
                    while (!myReader.EndOfData)
                    {
                        try
                        {
                            row = myReader.ReadFields();
                            Model model = new Model() { 
                                                        Symbol = row[0], 
                                                        Name = row[1],                                                         
                            };
                            double lastTradePrice;
                            double.TryParse(row[2], out lastTradePrice);
                            model.LastTradePrice = lastTradePrice;

                            DateTime lastTradeDate;
                            DateTime.TryParseExact(row[3], "M/dd/yyyy", null, DateTimeStyles.None, out lastTradeDate);
                            model.LastTradeDate = lastTradeDate;

                            double openPrice;
                            double.TryParse(row[4], out openPrice);
                            model.OpenPrice = openPrice;

                            double dayHighPrice;
                            double.TryParse(row[5], out dayHighPrice);
                            model.DayHighPrice = dayHighPrice;

                            double dayLowPrice;
                            double.TryParse(row[6], out dayLowPrice);
                            model.DayLowPrice = dayLowPrice;
                            model.TradeTime = DateTime.Today.Date + new TimeSpan(8, 0, 0);
                            result.Add(model);
                        }
                        catch (MalformedLineException)
                        {
                            throw;
                        }
                    }
                }                
            }
            return result;
        }

        public enum PriceTendency
        {
            Up,
            Down,
            None
        }

        public void TransformPrices(List<Model> quotes, Random random)
        {
            foreach (Model model in quotes)
            {
                double lastTradeMinus5p = model.LastTradePrice - model.LastTradePrice * 0.05;
                double lastTradePlus5p = model.LastTradePrice + model.LastTradePrice * 0.05;
                model.LastTradePrice = random.NextDouble() * (lastTradePlus5p - lastTradeMinus5p) + lastTradeMinus5p;
                if (model.LastTradeDate < DateTime.Now.Date)
                {
                    model.DayHighPrice = model.LastTradePrice;
                    model.DayLowPrice = model.LastTradePrice;
                    model.OpenPrice = model.LastTradePrice;
                }
                model.LastTradeDate = DateTime.Now.Date;
                model.DayHighPrice = model.LastTradePrice > model.DayHighPrice ? model.LastTradePrice : model.DayHighPrice;
                model.DayLowPrice = model.LastTradePrice < model.DayLowPrice ? model.LastTradePrice : model.DayLowPrice;
                model.TradeTime = model.TradeTime + TimeSpan.FromMinutes(2);
            }
        }


        public void Subscribe()
        {
            _callback = OperationContext.Current.GetCallbackChannel<IClient>();            
            PriceChanged += PriceChangeHandler;

            Task task = PublishPriceChange();            
        }

        public void PriceChangeHandler(object sender, PriceChangeEventArgs e)
        {
            _callback.PriceChange(e.Quotes);
        }

        public void Unsubscribe()
        {
            PriceChanged -= PriceChangeHandler;
        }

        private async Task<bool> PublishPriceChange()
        {
            bool isFinished = false;
            await Task.Run(() =>
                {
                    try
                    {
                        lock (_locker)
                        {
                            Random random = RandomProvider.GetThreadRandom();
                            for (int i = 0; i < 240; i++) // 8 hours => 4 minutes = 240 seconds
                            {
                                Thread.Sleep(1000);
                                TransformPrices(_quotes, random);
                                PriceChangeEventArgs e = new PriceChangeEventArgs(_quotes);
                                PriceChanged(this, e);
                            }
                            isFinished = true;
                        }
                    }
                    catch (Exception)
                    {                        
                        
                    }                                        
                });
            return isFinished;
        }
    }
}
