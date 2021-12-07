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
using IBL.BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for droneView.xaml
    /// </summary>
    public partial class droneView : Window
    {
        private IBL.IBL bl;
        private DroneToList dr;
        public droneView()
        {
            InitializeComponent();
        }

        public droneView(IBL.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            updateGrid.Visibility = Visibility.Hidden;
            realeseFromCharg.Visibility = Visibility.Hidden;
            cmbStation.ItemsSource = bl.veiwListStation();
           DroneMaxWeigtCmb.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            
        }

        public droneView(IBL.IBL bl,DroneToList d)
        {
            InitializeComponent();
            this.bl = bl;
            dr = d;
            addGrid.Visibility = Visibility.Hidden;
            realeseFromCharg.Visibility = Visibility.Hidden;
            fillTextbox(d);
            if(dr.status==DroneStatus.Available)
            {
                droneChargeBtn.Visibility = Visibility.Visible;
                sendToDeliveryBtn.Visibility = Visibility.Visible;
            }

            if(dr.status==DroneStatus.Maintenace)
            {
                relaseBtn.Visibility = Visibility.Visible;
            }

            if(dr.status==DroneStatus.Delivery)
            {
                collectBtn.Visibility = Visibility.Visible;
                parcelDeliveryBtn.Visibility = Visibility.Visible;
            }
        }

        private void addDroneBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StationToList s = (StationToList)cmbStation.SelectedItem;
                bl.addDrone(Convert.ToInt32(idTxt.Text), Convert.ToInt32(modelTxt.Text), Convert.ToInt32(DroneMaxWeigtCmb.SelectedItem), Convert.ToInt32(s.ID));
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

        private void updateModel(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.updateNameDrone(dr.ID, Convert.ToInt32(updateModeltxt.Text));
                MessageBox.Show("the model of the drone was successfully updated");

                Drone drone = bl.findDrone(dr.ID);
                fillTextbox(drone);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
               
            }
        }

        private void fillTextbox(DroneToList d)
        {
            
            statusTxt.Text = d.status.ToString();
            weightTxt.Text = d.weight.ToString();
            updateIdtxt.Text = d.ID.ToString();
            updateModeltxt.Text = d.droneModel.ToString();
            batteryTxt.Text = d.battery.ToString()+"%";
            parcelIdTxt.Text = d.parcelNumber.ToString();
            longitudeTxt.Text = d.currentLocation.longitude.ToString();
            latitudeTxt.Text = d.currentLocation.latitude.ToString();
        }

        private void fillTextbox(Drone d)
        {
            statusTxt.Text = d.status.ToString();
            updateIdtxt.Text = d.ID.ToString();
            weightTxt.Text = d.weight.ToString();
            updateModeltxt.Text = d.model.ToString();
            batteryTxt.Text = d.battery.ToString()+"%";
            if (d.parcel != null)
            {
                parcelIdTxt.Text = d.parcel.ID.ToString();

            }
            else
                parcelIdTxt.Text = 0.ToString();
            longitudeTxt.Text = d.location.longitude.ToString();
            latitudeTxt.Text = d.location.latitude.ToString();
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void sendToCharge(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.sendToCharge(dr.ID);
                MessageBox.Show("the drone was sent to charge");
                Drone drone = bl.findDrone(dr.ID);
                fillTextbox(drone);
                relaseBtn.Visibility = Visibility.Visible;
                droneChargeBtn.Visibility = Visibility.Hidden;
                sendToDeliveryBtn.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
        }

        private void relaseFromCharge(object sender, RoutedEventArgs e)
        {
            updateGrid.Visibility = Visibility.Hidden;
            realeseFromCharg.Visibility = Visibility.Visible;
            //try
            //{
            //    lbl.Visibility = Visibility.Visible;
            //    timeInChargeTxt.Visibility = Visibility.Visible;
            //    okBtn.Visibility = Visibility.Visible;
            //    MessageBox.Show("enter time(in minuits) that drone was in charging");

            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.Message);
            //}
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            bl.releaseFromCharge(dr.ID, Convert.ToInt32(timeInChargeTxt.Text));
            MessageBox.Show("the drone was relase from charge");
            Drone drone = bl.findDrone(dr.ID);
            fillTextbox(drone);
            relaseBtn.Visibility = Visibility.Hidden;
            //lbl.Visibility = Visibility.Hidden;
            //timeInChargeTxt.Visibility = Visibility.Hidden;
            //okBtn.Visibility = Visibility.Hidden;
            droneChargeBtn.Visibility = Visibility.Visible;
            sendToDeliveryBtn.Visibility = Visibility.Visible;
            realeseFromCharg.Visibility = Visibility.Hidden;
            updateGrid.Visibility = Visibility.Visible;
        }

        private void sendToDelivery(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.parcelToDrone(dr.ID);
                MessageBox.Show("the drone belongs to parcel");
                Drone drone = bl.findDrone(dr.ID);
                fillTextbox(drone);
                collectBtn.Visibility = Visibility.Visible;
                parcelDeliveryBtn.Visibility = Visibility.Visible;
                droneChargeBtn.Visibility = Visibility.Hidden;
                sendToDeliveryBtn.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void collectParcel(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.packageCollection(dr.ID);
                Drone drone = bl.findDrone(dr.ID);
                fillTextbox(drone);
                MessageBox.Show("the parcel was collected by the parcel");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
               
            }
        }

        private void parcelDelivery(object sender, RoutedEventArgs e)
        {
            try
            {

                bl.packageDelivery(dr.ID);
                MessageBox.Show("the parcel was delivered to the customer");
                Drone drone = bl.findDrone(dr.ID);
                fillTextbox(drone);
               
                collectBtn.Visibility = Visibility.Hidden;
                parcelDeliveryBtn.Visibility = Visibility.Hidden;
                droneChargeBtn.Visibility = Visibility.Visible;
                sendToDeliveryBtn.Visibility = Visibility.Visible;
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void timeInChargeTxt_KeyDown(object sender, KeyEventArgs e)
        {
            ;
        }
    }
}

