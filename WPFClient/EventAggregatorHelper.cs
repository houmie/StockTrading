using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient
{
    public sealed class EventAggregatorHelper
    {
        private static IEventAggregator _instance = new EventAggregator();

        private EventAggregatorHelper()
        {

        }

        static EventAggregatorHelper()
        {

        }

        

        public static IEventAggregator Instance
        {
            get { return _instance; }            
        }
        
    }
}
