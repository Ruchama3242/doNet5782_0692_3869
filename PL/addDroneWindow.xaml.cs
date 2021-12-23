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
    /// Interaction logic for addDroneWindow.xaml
    /// </summary>
    public partial class addDroneWindow : Window
    {
        private BlApi.IBL bl;
        BO.Drone d;
        
        public addDroneWindow()
        {
            InitializeComponent();
            bl = BlApi.BlFactory.GetBl();
            d = new Drone();
            d.location = new BO.Location();
            cmbStation.ItemsSource = bl.avilableCharginStation().Select(s => s.ID);
            DroneMaxWeigtCmb.ItemsSource = Enum.GetValues(typeof(WeightCategorie));
            this.DataContext = d;
        }

        private void addDroneBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.addDrone(d.ID, d.model, Convert.ToInt32(DroneMaxWeigtCmb.SelectedItem), Convert.ToInt32(cmbStation.SelectedItem));
                MessageBox.Show("the drone was successfully added");
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

