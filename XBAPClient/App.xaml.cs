using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace XBAPClient
{ 
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            ServiceProxy.Instance.Client.Unsubscribe();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //Subscribe.
            Console.WriteLine("Subscribing");

            Bootstrapper bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }
    }
}
