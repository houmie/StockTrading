using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class Model
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public double LastTradePrice { get; set; }
        public DateTime LastTradeDate { get; set; }
        public double OpenPrice { get; set; }
        public double DayHighPrice { get; set; }
        public double DayLowPrice { get; set; }
    }
}
