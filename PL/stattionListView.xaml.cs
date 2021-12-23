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
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            new stationWindow().ShowDialog();
            myObservableCollection = new ObservableCollection<BO.StationToList>(bl.veiwListStation());

            DataContext = myObservableCollection;
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //private  ObservableCollection<BO.StationToList> ToObservableCollection<T>(this IEnumerable<BO.StationToList> col)
        //{
        //    return new ObservableCollection<BO.StationToList>(col);
        //}
    }
}
