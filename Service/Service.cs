using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class Service : IService
    {
        public string SayHello(string name)
        {
            return string.Format("Hello, {0}", name);
        }
    }
}
