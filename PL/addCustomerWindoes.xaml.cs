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
        public addCustomerWindoes()
        {
            InitializeComponent();
            bl = BlApi.BlFactory.GetBl();
            c = new Customer();
            c.location = new BO.Location();
            this.DataContext = c;
            this.updateBtn.Visibility = Visibility.Hidden;
            this.deleteBtn.Visibility = Visibility.Hidden;
        }

        public addCustomerWindoes( CustomerToList c)
        {
            InitializeComponent();
            bl = BlApi.BlFactory.GetBl();
           // c = new CustomerToList();
            this.longitudeL.Visibility = Visibility.Hidden;
            this.lattitudeL.Visibility = Visibility.Hidden;
            this.lattitudeBtn.Visibility = Visibility.Hidden;
            this.longitudeBtn.Visibility = Visibility.Hidden;
            this.DataContext = c;
            this.updateBtn.Visibility = Visibility.Visible;
            this.deleteBtn.Visibility = Visibility.Visible;
            this.nameBtn.IsReadOnly = true;
            this.IDBtn.IsReadOnly = true;
            this.phoneBtn.IsReadOnly = true;
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
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
        }
    }
}
