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
    public partial class customerListView : Window
    {
        private BlApi.IBL bl = BlApi.BlFactory.GetBl();

        private ObservableCollection<BO.Customer> _myCollection =
      new ObservableCollection<BO.Customer>();

       
        public customerListView()
        {
            InitializeComponent();
        }

        public customerListView(BlApi.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            new regist(bl).ShowDialog();
        }
    }
}
