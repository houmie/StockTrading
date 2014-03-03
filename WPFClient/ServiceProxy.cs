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
            //Uri baseAddress = new Uri("http://4c6fff6548dc4e249ce4671122dafd33.cloudapp.net/Service.svc");
            ////Uri baseAddress = new Uri("http://127.0.0.1:81/Service.svc");        
            //WSDualHttpBinding wsd = new WSDualHttpBinding();
            //wsd.CloseTimeout = new TimeSpan(0, 1, 0);
            //wsd.OpenTimeout = new TimeSpan(0, 1, 0);
            //wsd.ReceiveTimeout = new TimeSpan(0, 10, 0);
            //wsd.SendTimeout = new TimeSpan(0, 1, 0);
            //wsd.BypassProxyOnLocal = false;
            //wsd.TransactionFlow = false;
            //wsd.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
            //wsd.MaxBufferPoolSize = 524288;
            //wsd.MaxReceivedMessageSize = 65536;
            //wsd.MessageEncoding = WSMessageEncoding.Text;
            //wsd.TextEncoding = Encoding.UTF8;
            //wsd.UseDefaultWebProxy = true;
            //wsd.ReaderQuotas = new System.Xml.XmlDictionaryReaderQuotas() { MaxDepth = 32, MaxStringContentLength = 8192, MaxArrayLength = 16384, MaxBytesPerRead = 4096, MaxNameTableCharCount = 16384 };
            //wsd.ReliableSession = new ReliableSession() { Ordered = true, InactivityTimeout = new TimeSpan(0, 10, 0) };
            //wsd.Security = new WSDualHttpSecurity() { Mode = WSDualHttpSecurityMode.None, Message = new MessageSecurityOverHttp() { ClientCredentialType = MessageCredentialType.None, NegotiateServiceCredential = false } };

            //EndpointAddress ea = new EndpointAddress(baseAddress);

            InstanceContext site = new InstanceContext(this);
            _client = new ServiceClient(site);//, wsd, ea);         
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
