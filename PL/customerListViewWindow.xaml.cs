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

        private List<BO.CustomerToList> lst;
      //new ObservableCollection<BO.Customer>();
        
       
        public customerListView()
        {
            InitializeComponent();
            bl = BlApi.BlFactory.GetBl();
            lst = new List<BO.CustomerToList>(bl.viewListCustomer());
            DataContext = lst;
            //customerLstView.ItemsSource = bl.viewListCustomer();
            // customerLstView.ItemsSource = myCollection;
            //costomerLstBx.DataContext = myCollection;
            //customerLstView.ItemsSource = bl.viewListCustomer();
        }


        private void add_Click(object sender, RoutedEventArgs e)
        {
            new addCustomerWindoes().ShowDialog();
            lst = new List<BO.CustomerToList>(bl.viewListCustomer());
            DataContext = lst;
            //customerLstView.ItemsSource = bl.viewListCustomer();
            // customerListView.DataContext = bl.viewListCustomer();

        }

        //private void ListBox_SourceUpdated(object sender, DataTransferEventArgs e)
        //{
        //    //customerLstView.ItemsSource = bl.viewListCustomer();
        //    DataContext =lst ;
        //}

        private void Image_TouchEnter(object sender, TouchEventArgs e)
        {

        }

        private void customerLstView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (customerLstView.SelectedItem == null)
                MessageBox.Show("Error! choose an item");
            else
            {
                CustomerToList c = new CustomerToList();
                c = (CustomerToList)customerLstView.SelectedItem;
                new addCustomerWindoes(c,"maneger").ShowDialog();
                lst = new List<BO.CustomerToList>(bl.viewListCustomer());
                DataContext = lst;
            }
            // dr = (DroneToList)DronesListView.SelectedItem;
            //new droneView(bl, dr).ShowDialog();
            //DroneToList dr = new DroneToList();
            //dr = (DroneToList)DronesListView.SelectedItem;
            //new droneView(bl, dr).ShowDialog();
        }

        private void serchTxtBx_KeyUp(object sender, KeyEventArgs e)
        {
            IEnumerable<BO.CustomerToList> p = new List<BO.CustomerToList>();
            p = bl.viewListCustomer().Where(x => Convert.ToString(x.ID).StartsWith(serchTxtBx.Text));
            lst = new List<BO.CustomerToList>(p);
            DataContext = lst;
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
