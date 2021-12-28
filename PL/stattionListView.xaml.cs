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
using System.Collections.ObjectModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for stattionListView.xaml
    /// </summary>
    public partial class stattionListView : Window
    {
        private BlApi.IBL bl;
        // private ObservableCollection<BO.StationToList> _myCollection =
        //new ObservableCollection<BO.StationToList>();

       private ObservableCollection<BO.StationToList> myObservableCollection ;
      //  private ObservableCollection<BO.StationToList> myObservableCollection = new ObservableCollection<BO.StationToList>();

        public stattionListView()
        {
            InitializeComponent();
            bl = BlApi.BlFactory.GetBl();
            // _myCollection = (ObservableCollection<BO.StationToList>)bl.veiwListStation();
            myObservableCollection = new ObservableCollection<BO.StationToList>(bl.veiwListStation());

            DataContext = myObservableCollection;
            clearBtn.Visibility = Visibility.Hidden;
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            new stationWindow().ShowDialog();
            myObservableCollection = new ObservableCollection<BO.StationToList>(bl.veiwListStation());

            DataContext = myObservableCollection;
            if(clearBtn.Visibility==Visibility.Visible)
            {
                IEnumerable<BO.StationToList> query = bl.veiwListStation().OrderBy(x => x.availableChargeSlots);
                DataContext = query;
            }
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void stationLst_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new stationWindow((BO.StationToList)stationLst.SelectedItem).ShowDialog();
            myObservableCollection = new ObservableCollection<BO.StationToList>(bl.veiwListStation());

            DataContext = myObservableCollection;
            if (clearBtn.Visibility == Visibility.Visible)
            {
                IEnumerable<BO.StationToList> query = bl.veiwListStation().OrderBy(x => x.availableChargeSlots);
                DataContext = query;
            }
        }

        private void sortBtn_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<BO.StationToList> query = bl.veiwListStation().OrderBy(x => x.availableChargeSlots);
            DataContext = query;
            clearBtn.Visibility = Visibility.Visible;
            sortBtn.Visibility = Visibility.Hidden;
        }

        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            myObservableCollection = new ObservableCollection<BO.StationToList>(bl.veiwListStation());

            DataContext = myObservableCollection;
            clearBtn.Visibility = Visibility.Hidden;
            sortBtn.Visibility = Visibility.Visible;
        }

        private void serchTxtBx_KeyUp(object sender, KeyEventArgs e)
        {
              
            IEnumerable<BO.StationToList> p = new List<BO.StationToList>();
            p = bl.veiwListStation().Where(x => Convert.ToString(x.ID).StartsWith(serchTxtBx.Text));
            myObservableCollection = new ObservableCollection<BO.StationToList>(p);

            DataContext = myObservableCollection;
        }

        //private  ObservableCollection<BO.StationToList> ToObservableCollection<T>(this IEnumerable<BO.StationToList> col)
        //{
        //    return new ObservableCollection<BO.StationToList>(col);
        //}
    }
}
