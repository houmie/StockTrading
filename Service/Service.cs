using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using System.Net;
using System.IO;

namespace Service
{
    public class Service : IService
    {
        readonly string _path = @"quotes.csv";

        public void DownloadStockPriceFromYahoo()
        {
            if (File.Exists(_path))
            {
                File.Delete(_path);
            }
            using (WebClient Client = new WebClient())
            {
                Client.DownloadFile("http://finance.yahoo.com/d/quotes.csv?s=MSFT&f=snl1d1ohg", _path);
            }
        }


        public List<string[]> ReadStockPriceFromCSV()
        {
            DownloadStockPriceFromYahoo();
            if (File.Exists(_path))
            {
                using (var myReader = new TextFieldParser(_path))
                {
                    myReader.TextFieldType = FieldType.Delimited;
                    myReader.Delimiters = new string[] { "," };
                    string[] currentRow;
                    while (!myReader.EndOfData)
                    {
                        try
                        {
                            currentRow = myReader.ReadFields();
                            var list = new List<string[]>();
                            list.Add(currentRow);
                            return list;
                        }
                        catch (MalformedLineException ex)
                        {
                            throw;
                        }
                    }
                }
            }
            return null;
        }



    }
}
