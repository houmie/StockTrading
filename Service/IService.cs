using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        List<string[]> ReadStockPriceFromCSV();
    }


}
