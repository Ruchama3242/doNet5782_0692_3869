﻿using System;
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
    /// Interaction logic for droneView.xaml
    /// </summary>
    public partial class droneView : Window
    {
        private BlApi.IBL bl = BlApi.BlFactory.GetBl();
        private Drone dr;
      
        public droneView()
        {
            InitializeComponent();
        }

        public droneView(BlApi.IBL bl)
        {
            InitializeComponent();
            //this.bl = bl;
            addGrid.Visibility = Visibility.Visible;
            updateGrid.Visibility = Visibility.Hidden;
            realeseFromCharg.Visibility = Visibility.Hidden;
            cmbStation.ItemsSource = bl.avilableCharginStation().Select(s => s.ID);
           DroneMaxWeigtCmb.ItemsSource = Enum.GetValues(typeof(WeightCategorie));
            
        }

        public droneView(DroneToList d)
        {
            InitializeComponent();
           // this.bl = bl;
            dr =new Drone();
            dr = bl.findDrone(d.ID);
            DataContext = dr;
            addGrid.Visibility = Visibility.Hidden;
            realeseFromCharg.Visibility = Visibility.Hidden;
           // fillTextbox(d);
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
                viewParcelbtn.Visibility = Visibility.Visible;
            }
        }

        public droneView(BO.DroneInCharge d)
        {
            InitializeComponent();
            // this.bl = bl;
            dr = new Drone();
            dr = bl.findDrone(d.ID);
            DataContext = dr;
            addGrid.Visibility = Visibility.Hidden;
            realeseFromCharg.Visibility = Visibility.Hidden;
                droneChargeBtn.Visibility = Visibility.Hidden;
                sendToDeliveryBtn.Visibility = Visibility.Hidden;
                relaseBtn.Visibility = Visibility.Hidden;
                collectBtn.Visibility = Visibility.Hidden;
                parcelDeliveryBtn.Visibility = Visibility.Hidden;
                viewParcelbtn.Visibility = Visibility.Hidden;
            updateModeltxt.IsReadOnly = true;
            updateModelBtn.Visibility = Visibility.Hidden;
        }

        private void addDroneBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //StationToList s = (StationToList)cmbStation.SelectedItem;
                bl.addDrone(Convert.ToInt32(idTxt.Text), Convert.ToInt32(modelTxt.Text), Convert.ToInt32(DroneMaxWeigtCmb.SelectedItem), Convert.ToInt32(cmbStation.SelectedItem));
              
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
                bl.updateNameDrone(dr.ID, dr.model);
                MessageBox.Show("the model of the drone was successfully updated");

                dr = bl.findDrone(dr.ID);
                //fillTextbox(drone);
                DataContext = dr;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
               
            }
        }

        //private void fillTextbox(DroneToList d)
        //{
            
        //    statusTxt.Text = d.status.ToString();
        //    weightTxt.Text = d.weight.ToString();
        //    updateIdtxt.Text = d.ID.ToString();
        //    updateModeltxt.Text = d.droneModel.ToString();
        //    batteryTxt.Text = d.battery.ToString()+"%";
        //    parcelIdTxt.Text = d.parcelNumber.ToString();
        //    longitudeTxt.Text = d.currentLocation.longitude.ToString();
        //    latitudeTxt.Text = d.currentLocation.latitude.ToString();
        //}

        //private void fillTextbox(Drone d)
        //{
        //    statusTxt.Text = d.status.ToString();
        //    updateIdtxt.Text = d.ID.ToString();
        //    weightTxt.Text = d.weight.ToString();
        //    updateModeltxt.Text = d.model.ToString();
        //    batteryTxt.Text = d.battery.ToString()+"%";
        //    if (d.parcel != null)
        //    {
        //        parcelIdTxt.Text = d.parcel.ID.ToString();

        //    }
        //    else
        //        parcelIdTxt.Text = 0.ToString();
        //    longitudeTxt.Text = d.location.longitude.ToString();
        //    latitudeTxt.Text = d.location.latitude.ToString();
        //}

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
                dr = bl.findDrone(dr.ID);
                //fillTextbox(drone);
                DataContext = dr;
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
           // updateGrid.Visibility = Visibility.Hidden;


            try
            {
             
                bl.releaseFromCharge(dr.ID);
                MessageBox.Show("the drone was relase from charge");
                dr = bl.findDrone(dr.ID);
                //fillTextbox(drone);
                DataContext = dr;
                relaseBtn.Visibility = Visibility.Hidden;
                //lbl.Visibility = Visibility.Hidden;
                //timeInChargeTxt.Visibility = Visibility.Hidden;
                //okBtn.Visibility = Visibility.Hidden;
                droneChargeBtn.Visibility = Visibility.Visible;
                sendToDeliveryBtn.Visibility = Visibility.Visible;
                realeseFromCharg.Visibility = Visibility.Hidden;
                updateGrid.Visibility = Visibility.Visible;
                //tp = 0;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
           
            MessageBox.Show("the drone was relase from charge");
            dr = bl.findDrone(dr.ID);
            //fillTextbox(drone);
            DataContext = dr;
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
                dr = bl.findDrone(dr.ID);
                //fillTextbox(drone);
                DataContext = dr;
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
                dr = bl.findDrone(dr.ID);
                //fillTextbox(drone);
                DataContext = dr;
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
                dr = bl.findDrone(dr.ID);
                //fillTextbox(drone);
                DataContext = dr;

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

        private void viewParcelbtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

