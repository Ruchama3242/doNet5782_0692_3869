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
    }
}
