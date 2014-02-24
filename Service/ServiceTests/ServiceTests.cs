using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service;
using NUnit.Framework;
using System.IO;
using System.Threading;

namespace Service.Tests
{
    [TestFixture()]
    public class ServiceTests
    {
        Service _service;
        Model _model;
        readonly string _path = @"quotes.csv";

        [SetUp]
        public void Init()
        {
            _service = new Service();            
            _model = new Model() { Symbol = "MSFT", Name = "Microsoft Corpora", LastTradePrice = 37.575, LastTradeDate = DateTime.Now.AddDays(-1), OpenPrice = 37.65, DayLowPrice = 37.51, DayHighPrice = 37.78 };
        }

        [Test()]
        public void ReadStockPriceFromCSVTest()
        {
            if (!File.Exists(_path))
            {
                _service.DownloadStockPriceFromYahoo();
            }            
            List<Model> result = _service.ReadStockPriceFromCSV();
            Assert.True(result.Count > 0);
        }

        [Test()]
        public void DownloadStockPriceFromYahooTest()
        {
            if (File.Exists(_path))
            {
                File.Delete(_path);
            }
            _service.DownloadStockPriceFromYahoo();
            Assert.True(File.Exists("quotes.csv"));
        }

        [Test()]
        public void LastTradePriceChangeTest()
        {            
            List<Model> quotes = new List<Model> { _model };
            double lastTradePrice = quotes[0].LastTradePrice;                     
            _service.TransformPrices(quotes, Service.PriceTendency.Down);
            Assert.True(quotes[0].LastTradePrice <= lastTradePrice + (lastTradePrice * 0.05) && quotes[0].LastTradePrice >= lastTradePrice - (lastTradePrice * 0.05));
        }

        [Test()]
        public void LastTradeDateSetToTodayTest()
        {
            List<Model> quotes = new List<Model> { _model };            
            _service.TransformPrices(quotes);
            Assert.True(quotes[0].LastTradeDate == DateTime.Now.Date);
        }

        [Test()]
        public void DayHighPriceMustBeTheHighestDayTradePriceTest()
        {
            List<Model> quotes = new List<Model> { _model };
            _service.TransformPrices(quotes);            
            double lastTradePrice = quotes[0].LastTradePrice;
            _service.TransformPrices(quotes);
            double lastTradePrice2 = quotes[0].LastTradePrice;
            lastTradePrice = lastTradePrice > lastTradePrice2 ? lastTradePrice : lastTradePrice2;
            Assert.AreEqual(lastTradePrice, quotes[0].DayHighPrice);
        }

        [Test()]
        public void DayLowPriceMustBeTheLowestDayTradePriceTest()
        {
            List<Model> quotes = new List<Model> { _model };
            _service.TransformPrices(quotes);            
            double lastTradePrice = quotes[0].LastTradePrice;
            _service.TransformPrices(quotes);
            double lastTradePrice2 = quotes[0].LastTradePrice;
            lastTradePrice = lastTradePrice < lastTradePrice2 ? lastTradePrice : lastTradePrice2;
            Assert.AreEqual(lastTradePrice, quotes[0].DayLowPrice);
        }

        [Test()]
        public void DayLowPriceMustBeSetToTodaysLastTradeForFirstTime()
        {
            List<Model> quotes = new List<Model> { _model };
            _service.TransformPrices(quotes);
            Assert.AreEqual(_model.DayLowPrice, _model.LastTradePrice);
        }

        [Test()]
        public void DayhighPriceMustBeSetToTodaysLastTradeForFirstTime()
        {
            List<Model> quotes = new List<Model> { _model };
            _service.TransformPrices(quotes);
            Assert.AreEqual(_model.DayHighPrice, _model.LastTradePrice);
        }

        [Test()]
        public void DayOpenPriceMustBeSetToTodaysFirstTrade()
        {
            List<Model> quotes = new List<Model> { _model };            
            _service.TransformPrices(quotes);
            double firstTradePrice = _model.LastTradePrice;
            _service.TransformPrices(quotes);
            Assert.AreEqual(_model.OpenPrice, firstTradePrice);
        }

    }
}
