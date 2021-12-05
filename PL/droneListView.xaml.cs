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

            fillListView();
            statusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatus));
            weightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
           
        }

        


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            new droneView(bl).Show();
            fillListView();
        }



        //private void DronesListView_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        //{
        //    DroneToList dr = new DroneToList();
        //    dr = (DroneToList)DronesListView.SelectedItem;
        //    new droneView(bl, dr).ShowDialog();
        //    fillListView();
        //}

        private void DronesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void fillListView()
        {
            DronesListView.ItemsSource = bl.getAllDrones();
        }

        private void close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //private void DronesListView_Selected(object sender, RoutedEventArgs e)
        //{
        //    DroneToList dr = new DroneToList();
        //    dr = (DroneToList)DronesListView.SelectedItem;
        //    new droneView(bl, dr).ShowDialog();
        //    fillListView();
        //}

        private void mouse(object sender, MouseButtonEventArgs e)
        {
            DroneToList dr = new DroneToList();
            dr = (DroneToList)DronesListView.SelectedItem;
            new droneView(bl, dr).ShowDialog();
            fillListView();
        }
    }
}
