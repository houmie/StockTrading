using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClient.ServiceReferenceWPF;

namespace WPFClient
{
    public class PriceChangedEvent : PubSubEvent<Model[]>
    {
    }
}
