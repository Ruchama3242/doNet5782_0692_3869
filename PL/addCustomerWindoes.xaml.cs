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
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for addCustomerWindoes.xaml
    /// </summary>
    public partial class addCustomerWindoes : Window
    {
        private BlApi.IBL bl;
        BO.Customer c;
        private List<BO.ParcelAtCustomer> myCollection;
        public addCustomerWindoes()
        {
            InitializeComponent();
            bl = BlApi.BlFactory.GetBl();
            c = new Customer();
            c.location = new BO.Location();
            this.DataContext = c;
            this.updateBtn.Visibility = Visibility.Hidden;
            this.deleteBtn.Visibility = Visibility.Hidden;
            this.parcelLstView.Visibility = Visibility.Hidden;
            parcelToLstView.Visibility = Visibility.Hidden;
          fromLbl.Visibility = Visibility.Hidden;
            toLbl.Visibility = Visibility.Hidden;
        }

        public addCustomerWindoes(Customer cus)
        {
            InitializeComponent();
            bl = BlApi.BlFactory.GetBl();
            c = new Customer();
            c = bl.findCustomer(cus.ID);
            DataContext = c;
            this.updateBtn.Visibility = Visibility.Visible;
            this.deleteBtn.Visibility = Visibility.Visible;
            this.addBtn.Visibility = Visibility.Hidden;
            this.nameBtn.IsReadOnly = false;
            this.IDBtn.IsReadOnly = false;
            this.phoneBtn.IsReadOnly = false;
            longitudeBtn.IsReadOnly = true;
            lattitudeBtn.IsReadOnly = true;
            ;
            myCollection = new List<ParcelAtCustomer>();
            myCollection = c.fromCustomer;

            parcelLstView.DataContext = myCollection;
            
            parcelToLstView.ItemsSource = c.toCustomer;

        }
        public addCustomerWindoes( CustomerToList cus)
        {
            InitializeComponent();
            bl = BlApi.BlFactory.GetBl();
            c = new Customer();
            c.fromCustomer = new List<ParcelAtCustomer>();
            c.toCustomer = new List<ParcelAtCustomer>();
            c = bl.findCustomer(cus.ID);
            DataContext = c;
            this.updateBtn.Visibility = Visibility.Visible;
            this.deleteBtn.Visibility = Visibility.Visible;
            this.addBtn.Visibility = Visibility.Hidden;
            this.nameBtn.IsReadOnly = false;
            this.IDBtn.IsReadOnly = false;
            this.phoneBtn.IsReadOnly = false;
            longitudeBtn.IsReadOnly = true;
            lattitudeBtn.IsReadOnly = true;
            ;
            myCollection = new List<ParcelAtCustomer>();
            myCollection = c.fromCustomer;
        
            parcelLstView.DataContext = myCollection;
          
            parcelToLstView.ItemsSource = c.toCustomer;

        }


        public addCustomerWindoes(int id)
        {
            InitializeComponent();
            bl = BlApi.BlFactory.GetBl();
            c = new Customer();
            c.fromCustomer = new List<ParcelAtCustomer>();
            c.toCustomer = new List<ParcelAtCustomer>();
            c = bl.findCustomer(id);
            DataContext = c;
            this.updateBtn.Visibility = Visibility.Hidden;
            this.deleteBtn.Visibility = Visibility.Hidden ;
            this.addBtn.Visibility = Visibility.Hidden;
            this.nameBtn.IsReadOnly = true;
            this.IDBtn.IsReadOnly = true;
            this.phoneBtn.IsReadOnly = true;
            longitudeBtn.IsReadOnly = true;
            lattitudeBtn.IsReadOnly = true;
             parcelLstView.ItemsSource = c.fromCustomer;
            //var lst = bl.getAllParcels();
            //myCollection = new ObservableCollection<BO.Parcel>();
            //foreach (var item in c.fromCustomer)
            //{
            //    myCollection.Add(bl.findParcel(item.ID));
            //}
           // parcelLstView.DataContext = c.fromCustomer ;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.addCustomer(c);
                MessageBox.Show($"{c.name} successfully added");
                c = new BO.Customer();
                this.DataContext = c;
                this.Close();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.deleteCustomer(c.ID);
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.updateCustomer(c.ID, c.name, c.phone);
                MessageBox.Show("The customer successfuly updated ");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void parcelLstView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ParcelAtCustomer p = new ParcelAtCustomer();
            p = (ParcelAtCustomer)parcelLstView.SelectedItem;
            new parcelWindow(p.ID).ShowDialog();
        }

        private void parcelToLstView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ParcelAtCustomer p = new ParcelAtCustomer();
            p = (ParcelAtCustomer)parcelToLstView.SelectedItem;
            new parcelWindow(p.ID).ShowDialog();
        }
    }
}
