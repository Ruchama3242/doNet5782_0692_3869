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
  /*using System.Collections.ObjectModel;
using System.Windows;
 
namespace CollectionDemo
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private ObservableCollection<MyData> _myCollection = 
      new ObservableCollection<MyData>();
 
    public MainWindow()
    {
      InitializeComponent();
 
      DataContext = _myCollection;
      _myCollection.Add
*/
    public partial class customerListView : Window
    {
        private BlApi.IBL bl;

        //private ObservableCollection<BO.Customer> myCollection =
      //new ObservableCollection<BO.Customer>();
        
       
        public customerListView()
        {
            InitializeComponent();
            bl = BlApi.BlFactory.GetBl();
            customerLstView.DataContext = bl.viewListCustomer();
           //costomerLstBx.DataContext = myCollection;
        }


        private void add_Click(object sender, RoutedEventArgs e)
        {
            new addCustomerWindoes().ShowDialog();
            //costomerLstBx.DataContext = bl.viewListCustomer();
           
        }

        private void ListBox_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            //costomerLstBx.Items.Refresh();
            customerLstView.DataContext = bl.viewListCustomer();
        }

       
    }
}
