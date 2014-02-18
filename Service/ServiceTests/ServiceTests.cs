using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service;
using NUnit.Framework;
using System.IO;

namespace Service.Tests
{
    [TestFixture()]
    public class ServiceTests
    {
        Service _service;
        readonly string _path = @"quotes.csv";

        [SetUp]
        public void Init()
        {
            _service = new Service();            
        }

        [Test()]
        public void ReadStockPriceFromCSVTest()
        {
            _service.DownloadStockPriceFromYahoo();
            List<string[]> result = _service.ReadStockPriceFromCSV();
            Assert.IsNotNull(result);
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


    }
}
