using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using IBL.BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for droneView.xaml
    /// </summary>
    public partial class droneView : Window
    {
        public droneView()
        {
            InitializeComponent();
        }

        public droneView(IBL.interfaceIBL bl,string s)
        {
            if (s == "add")
               
            InitializeComponent();
            DroneMaxWeigt.ItemsSource = Enum.GetValues(typeof(WeightCategories));
        }
      
      
    }
}
