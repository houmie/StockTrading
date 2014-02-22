using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.Prism.Modularity;
using System.Windows;
using System.ComponentModel.Composition.Hosting;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Events;

namespace WPFClient
{
    public class Bootstrapper : MefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {            
            return this.Container.GetExportedValue<MainWindow>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            App.Current.MainWindow = (Window)Shell;
            App.Current.MainWindow.Show();
        }

        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();
                      

            // Prism's AggregateCatalog is a catalog of all MEF composable parts
            // within the application.
            // We add the parts corresponding to the current assembly to it
            
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(MainWindow).Assembly));
            //AggregateCatalog.Catalogs.Add(new AssemblyCatalog(this.GetType().Assembly));            
            AggregateCatalog.Catalogs.Add(new DirectoryCatalog(@".\"));
        }

        protected override void InitializeModules()
        {
            base.InitializeModules();
        }        

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
            //ModuleCatalog moduleCatalog = new ModuleCatalog();

            // this is the code responsible 
            // for adding Module1 to the application
            //moduleCatalog.AddModule
            //(
            //    new ModuleInfo
            //    {
            //        InitializationMode = InitializationMode.WhenAvailable,
            //        Ref = "Module1.xap",
            //        ModuleName = "Module1Impl",
            //        ModuleType = "Module1.Module1Impl, Module1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
            //    }
            //);

            //moduleCatalog.AddModule
            //(
            //    new ModuleInfo
            //    {
            //        InitializationMode = InitializationMode.WhenAvailable,
            //        Ref = "Module2.xap",
            //        ModuleName = "Module2Impl",
            //        ModuleType = "Module2.Module2Impl, Module2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
            //    }
            //);

            //return moduleCatalog;
        }
        
    }
}
