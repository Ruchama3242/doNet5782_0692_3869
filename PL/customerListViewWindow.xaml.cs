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
   // private List<DO.Customer> myCollection = new List<DO.Customer>();
    public partial class customerListView : Window
    {
        private BlApi.IBL bl = BlApi.BlFactory.GetBl();

        private ObservableCollection<BO.Customer> myCollection =
      new ObservableCollection<BO.Customer>();
        
       
        public customerListView()
        {
            InitializeComponent();
            //this.myCollection = bl.viewListCustomer();
            DataContext = myCollection;
        }

        public customerListView(BlApi.IBL bl)
        {
            InitializeComponent();
            DataContext = bl.viewListCustomer();
            this.bl = bl;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            new regist().ShowDialog();
           
           // DataContext = this.myCollection;
          // lstBoxCustomer.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            // myCollection.Add(bl.findCustomer
        }

        private void ListBox_SourceUpdated(object sender, DataTransferEventArgs e)
        {

        }

        private void lstBoxCustomer_SourceUpdated(object sender, DataTransferEventArgs e)
        {

        }
    }
}
