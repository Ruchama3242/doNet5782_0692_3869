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

namespace PL
{


    /// <summary>
    /// Interaction logic for stationWindow.xaml
    /// </summary>
    public partial class stationWindow : Window
    {
        private BlApi.IBL bl;
        BO.Station s;
        public stationWindow()
        {
            InitializeComponent();
            bl = BlApi.BlFactory.GetBl();
            s = new BO.Station();
            s.location = new BO.Location();
            DataContext = s;
            lstLbl.Visibility = Visibility.Hidden;
            droneslst.Visibility = Visibility.Hidden;
            updateBtn.Visibility = Visibility.Hidden;
           // updateGrid.Visibility = Visibility.Hidden;
        }


        public stationWindow(BO.StationToList st)
        {
            InitializeComponent();
            bl = BlApi.BlFactory.GetBl();
            s = new BO.Station();
            s = bl.findStation(st.ID);
            //s.location = new BO.Location();
            DataContext = s;
            droneslst.ItemsSource = s.dronesInChargeList;
            idTxt.IsReadOnly = true;
            longitudtTxt.IsReadOnly = true;
            latitudeTxt.IsReadOnly = true;
            addBtn.Visibility = Visibility.Hidden;
            closeBtn.Content = "close";
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
          try
            {
                bl.addStation(s);
                MessageBox.Show("the station successfully added");
                s = new BO.Station();
                s.location = new BO.Location();
                DataContext = s;
                Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void up(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.updateStation(s.ID, s.name, s.chargeSlots);
                MessageBox.Show("the station successfully updated");
               // s = new BO.Station();
                s = bl.findStation(s.ID);
               // s.location = new BO.Location();
                DataContext = s;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void droneslst_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new droneView((BO.DroneInCharge)droneslst.SelectedItem).ShowDialog();
        }
    }
}
