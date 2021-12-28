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
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for parcelsListWindow.xaml
    /// </summary>
    public partial class parcelsListWindow : Window
    {
        private BlApi.IBL bl;
        private ObservableCollection<BO.ParcelToList> myObservableCollection;
        public parcelsListWindow()
        {
            InitializeComponent();
            bl = BlApi.BlFactory.GetBl();
            myObservableCollection = new ObservableCollection<BO.ParcelToList>(bl.getAllParcels());
            DataContext = myObservableCollection;
            statusCmb.ItemsSource= Enum.GetValues(typeof(BO.ParcelStatus));
        }

        //private void filterByStatus_Checked(object sender, RoutedEventArgs e)
        //{
        //    if(filterByStatus.IsChecked==true)
        //    {
        //        IEnumerable<BO.ParcelToList> p = new List<BO.ParcelToList>();
        //        p = bl.getAllParcels().Where(x => x.status == (BO.ParcelStatus)statusCmb.SelectedItem);
        //        myObservableCollection = new ObservableCollection<BO.ParcelToList>(p);
        //        DataContext = myObservableCollection;
        //    }
        //    else
        //    {
        //        myObservableCollection = new ObservableCollection<BO.ParcelToList>(bl.getAllParcels());
        //        DataContext = myObservableCollection;
        //    }
        //}

        private void statusCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (filterByStatus.IsChecked == true)
            {
                IEnumerable<BO.ParcelToList> p = new List<BO.ParcelToList>();
                p = bl.getAllParcels().Where(x => x.status == (BO.ParcelStatus)statusCmb.SelectedItem);
                myObservableCollection = new ObservableCollection<BO.ParcelToList>(p);
                DataContext = myObservableCollection;
            }
            else
            {
                myObservableCollection = new ObservableCollection<BO.ParcelToList>(bl.getAllParcels());
                DataContext = myObservableCollection;
            }
        }

        private void serchTxtBx_KeyUp(object sender, KeyEventArgs e)
        {
            IEnumerable<BO.ParcelToList> p = new List<BO.ParcelToList>();
            p = bl.getAllParcels().Where(x =>Convert.ToString(x.ID).StartsWith(serchTxtBx.Text));
            myObservableCollection = new ObservableCollection<BO.ParcelToList>(p);
            DataContext = myObservableCollection;
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
   
}


