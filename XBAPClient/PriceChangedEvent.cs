using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBAPClient.ServiceReferenceXBAP;


namespace XBAPClient
{
    public class PriceChangedEvent : PubSubEvent<Model[]>
    {
    }
}
