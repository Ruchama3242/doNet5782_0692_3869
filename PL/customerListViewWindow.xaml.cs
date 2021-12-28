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
using System.Data;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for customerListView.xaml
    /// </summary>
    /// 
    public partial class customerListView : Window
    {
        private BlApi.IBL bl;

        private ObservableCollection<BO.CustomerToList> myCollection;
      //new ObservableCollection<BO.Customer>();
        
       
        public customerListView()
        {
            InitializeComponent();
            bl = BlApi.BlFactory.GetBl();
            myCollection = new ObservableCollection<BO.CustomerToList>(bl.viewListCustomer());
            customerLstView.DataContext = myCollection;
            //customerLstView.ItemsSource = bl.viewListCustomer();
           //costomerLstBx.DataContext = myCollection;
        }


        private void add_Click(object sender, RoutedEventArgs e)
        {
            new addCustomerWindoes().ShowDialog();
            myCollection = new ObservableCollection<BO.CustomerToList>(bl.viewListCustomer());
            customerLstView.DataContext = myCollection;
            //customerLstView.ItemsSource = bl.viewListCustomer();
           // customerListView.DataContext = bl.viewListCustomer();
           
        }

        private void ListBox_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            //customerLstView.ItemsSource = bl.viewListCustomer();
            customerLstView.DataContext = myCollection;
        }

        private void Image_TouchEnter(object sender, TouchEventArgs e)
        {

        }

        private void customerLstView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CustomerToList c = new CustomerToList();
            c = (CustomerToList)customerLstView.SelectedItem;
            new addCustomerWindoes(c).ShowDialog();
           // dr = (DroneToList)DronesListView.SelectedItem;
            //new droneView(bl, dr).ShowDialog();
            //DroneToList dr = new DroneToList();
            //dr = (DroneToList)DronesListView.SelectedItem;
            //new droneView(bl, dr).ShowDialog();
        }
    }
}
