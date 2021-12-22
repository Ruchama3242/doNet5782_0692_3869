using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for regist.xaml
    /// </summary>
    public partial class regist : Window
    {
        private BlApi.IBL bl ;
        BO.Customer c;
       // private ObservableCollection<BO.Customer> myCollection =
     // new ObservableCollection<BO.Customer>();

        public regist()
        {
            InitializeComponent();
            bl = BlApi.BlFactory.GetBl();
            c = new Customer();
            c.location = new BO.Location();
            this.DataContext = c;
            //this.myCollection = myCollection;

        }

        //public regist(BlApi.IBL bl, ObservableCollection<BO.Customer> myCollection)
        //{
        //    InitializeComponent();
        //    this.bl = bl;
        //    this.myCollection = myCollection;
        //}
        //public regist(BlApi.IBL bl)
        //{
        //    InitializeComponent();
        //    this.bl = bl;
        //}

      
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           // BO.Customer tmp = new BO.Customer();
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
    }
}
