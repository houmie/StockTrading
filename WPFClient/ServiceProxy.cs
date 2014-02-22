using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WPFClient.ServiceReferenceWPF;

namespace WPFClient
{    
    public sealed class ServiceProxy : IServiceCallback
    {
        private static readonly ServiceProxy _instance = new ServiceProxy();
        private ServiceClient _client;        
        
        static ServiceProxy()
        {

        }
                
        private ServiceProxy()
        {            
            InstanceContext site = new InstanceContext(this);
            _client = new ServiceClient(site);
        }

        public static ServiceProxy Instance
        {
            get { return _instance; }
        }

        public ServiceClient Client { get { return _client; } }                
        
        public void PriceChange(Model[] quotes)
        {
            EventAggregatorHelper.Instance.GetEvent<PriceChangedEvent>().Publish(quotes);
        }
       
    }
}
