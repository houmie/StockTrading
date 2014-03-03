using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace WCFServiceWebRoleStockTrading
{
    public interface IClient
    {
        [OperationContract(IsOneWay = true)]
        void PriceChange(List<Model> quotes);
    }

    public class PriceChangeEventArgs : EventArgs
    {
        public readonly List<Model> Quotes;

        public PriceChangeEventArgs(List<Model> quotes)
        {
            Quotes = quotes;
        }
    }

}
