using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XBAPClient
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    [Export]
    public partial class MainPage : Page, IView
    {
        public MainPage()
        {
            InitializeComponent();
            DataContext = ViewModel;
            
        }

        [Import]
        public MainViewModel ViewModel
        {
            set
            {
                this.DataContext = value;                
            }
            get
            {
                return DataContext as MainViewModel;
            }
        }
    }
}
