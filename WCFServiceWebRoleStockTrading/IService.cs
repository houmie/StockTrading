using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFServiceWebRoleStockTrading
{
    [ServiceContract(Namespace="http://venuscloud.com/StockTrading", SessionMode=SessionMode.Required, CallbackContract=typeof(IClient))]
    public interface IService
    {
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        void Subscribe();

        [OperationContract(IsOneWay=false, IsTerminating=true)]
        void Unsubscribe();              

    }



}
