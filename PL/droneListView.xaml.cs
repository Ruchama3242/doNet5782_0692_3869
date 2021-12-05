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
    /// Interaction logic for droneListView.xaml
    /// </summary>
    public partial class droneListView : Window
    {
        private IBL.interfaceIBL bl;

       // public static IEnumerable<DroneToList> ItemsSource { get; set; }

        public droneListView()
        {
            InitializeComponent();
        }

        public droneListView(IBL.interfaceIBL bl)
        {
            InitializeComponent();
            this.bl = bl;
           
            DronesListView.ItemsSource = this.bl.getAllDrones();
            statusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatus));
            weightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
           
        }

        


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            new droneView(bl,"add").Show();
        }



        private void DronesListView_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            new updateDrone().Show();
        }

        private void DronesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
