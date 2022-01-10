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
using System.Threading;

using System.ComponentModel;
namespace PL
{
    /// <summary>
    /// Interaction logic for droneView.xaml
    /// </summary>
    public partial class droneView : Window
    {
        private BlApi.IBL bl = BlApi.BlFactory.GetBl();
        private Drone dr;
        BackgroundWorker worker;
       

        public droneView()
        {
            InitializeComponent();
            
        }

        public droneView(DroneToList d)
        {
            InitializeComponent();
            dr =new Drone();
            dr = bl.findDrone(d.ID);
           // dr.parcel = new ParcelInTransfer();
            DataContext = dr;
           // parLst.DataContext = dr.parcel;
            if(dr.status==DroneStatus.Available)
            {
                droneChargeBtn.Visibility = Visibility.Visible;
                sendToDeliveryBtn.Visibility = Visibility.Visible;
                lbl.Visibility = Visibility.Hidden;
                parLst.Visibility = Visibility.Hidden;
                distanceLbl.Visibility = Visibility.Hidden;
                priorityLbl.Visibility = Visibility.Hidden;
                weightLbl.Visibility = Visibility.Hidden;
                priorityTxt.Visibility = Visibility.Hidden;
                distanceTxt.Visibility = Visibility.Hidden;
                WeightTxt.Visibility = Visibility.Hidden;

            }

            if(dr.status==DroneStatus.Maintenace)
            {
                relaseBtn.Visibility = Visibility.Visible;
                lbl.Visibility = Visibility.Hidden;
                parLst.Visibility = Visibility.Hidden;
                distanceLbl.Visibility = Visibility.Hidden;
                priorityLbl.Visibility = Visibility.Hidden;
                weightLbl.Visibility = Visibility.Hidden;
                priorityTxt.Visibility = Visibility.Hidden;
                distanceTxt.Visibility = Visibility.Hidden;
                WeightTxt.Visibility = Visibility.Hidden;

            }

            if (dr.status==DroneStatus.Delivery)
            {
                collectBtn.Visibility = Visibility.Visible;
                parcelDeliveryBtn.Visibility = Visibility.Visible;
                viewParcelbtn.Visibility = Visibility.Visible;
                parcelBtn.Visibility = Visibility.Visible;
                //parLst.ItemsSource = dr.parcel;
            }
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
        }

        public droneView(int id)
        {
            InitializeComponent();
            // this.bl = bl;
            dr = new Drone();
            dr = bl.findDrone(id);
            DataContext = dr;
            //addGrid.Visibility = Visibility.Hidden;
            //realeseFromCharg.Visibility = Visibility.Hidden;
            // fillTextbox(d);
              droneChargeBtn.Visibility = Visibility.Hidden;
                sendToDeliveryBtn.Visibility = Visibility.Hidden;
            updateModeltxt.IsReadOnly = true;
            updateModelBtn.Visibility = Visibility.Hidden;

           
                relaseBtn.Visibility = Visibility.Hidden;
            

           
                collectBtn.Visibility = Visibility.Hidden;
                parcelDeliveryBtn.Visibility = Visibility.Hidden;
                viewParcelbtn.Visibility = Visibility.Hidden;
            
        }

        public droneView(BO.DroneInCharge d)
        {
            InitializeComponent();
            // this.bl = bl;
            dr = new Drone();
            dr = bl.findDrone(d.ID);
            DataContext = dr;
            //addGrid.Visibility = Visibility.Hidden;
            //realeseFromCharg.Visibility = Visibility.Hidden;
                droneChargeBtn.Visibility = Visibility.Hidden;
                sendToDeliveryBtn.Visibility = Visibility.Hidden;
                relaseBtn.Visibility = Visibility.Hidden;
                collectBtn.Visibility = Visibility.Hidden;
                parcelDeliveryBtn.Visibility = Visibility.Hidden;
                viewParcelbtn.Visibility = Visibility.Hidden;
            updateModeltxt.IsReadOnly = true;
            updateModelBtn.Visibility = Visibility.Hidden;
            lbl.Visibility = Visibility.Hidden;
            parLst.Visibility = Visibility.Hidden;
        }

        private void addDroneBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //StationToList s = (StationToList)cmbStation.SelectedItem;
              //  bl.addDrone(Convert.ToInt32(idTxt.Text), Convert.ToInt32(modelTxt.Text), Convert.ToInt32(DroneMaxWeigtCmb.SelectedItem), Convert.ToInt32(cmbStation.SelectedItem));
              
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
              //  realeseFromCharg.Visibility = Visibility.Hidden;
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
           // realeseFromCharg.Visibility = Visibility.Hidden;
            updateGrid.Visibility = Visibility.Visible;
        }

        private void sendToDelivery(object sender, RoutedEventArgs e)
        {
           try
            {
                bl.parcelToDrone(dr.ID);
                MessageBox.Show("the drone belongs to parcel");
                lbl.Visibility = Visibility.Visible;
                parLst.Visibility = Visibility.Visible;
                parcelBtn.Visibility = Visibility.Visible;
                dr = bl.findDrone(dr.ID);
                //fillTextbox(drone);
                DataContext = dr;
                collectBtn.Visibility = Visibility.Visible;
                //parcelDeliveryBtn.Visibility = Visibility.Visible;
                droneChargeBtn.Visibility = Visibility.Visible;
                parcelDeliveryBtn.Visibility = Visibility.Visible;
                distanceLbl.Visibility = Visibility.Visible;
                priorityLbl.Visibility = Visibility.Visible;
                weightLbl.Visibility = Visibility.Visible;
                priorityTxt.Visibility = Visibility.Visible;
                distanceTxt.Visibility = Visibility.Visible;
                WeightTxt.Visibility = Visibility.Visible;
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
                parcelDeliveryBtn.Visibility = Visibility.Visible;
                collectBtn.Visibility = Visibility.Hidden;

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
                lbl.Visibility = Visibility.Hidden;
                parLst.Visibility = Visibility.Hidden;
                parcelBtn.Visibility = Visibility.Hidden;
                distanceLbl.Visibility = Visibility.Hidden;
                priorityLbl.Visibility = Visibility.Hidden;
                weightLbl.Visibility = Visibility.Hidden;
                priorityTxt.Visibility = Visibility.Hidden;
                distanceTxt.Visibility = Visibility.Hidden;
                WeightTxt.Visibility = Visibility.Hidden;
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

        private void parLst_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //ParcelInTransfer p = new ParcelInTransfer();
           // p = (ParcelInTransfer)parLst.SelectedItem;
            //לא לשכוח כשרוחמה תסיים עם חלון חבילה להוסיף שם בנאי מתאים
            new parcelWindow().ShowDialog();
        }

        private void parcelBtn_Click(object sender, RoutedEventArgs e)
        {
            new parcelWindow(dr.parcel.ID).ShowDialog();

        }


        private void checkInputdigit(KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.Right || e.Key == Key.Left)//allow back,delete and errors keys
                return;
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            if (char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                    return;
            e.Handled = true;
            MessageBox.Show("The input must be only digits");
        }

       

        private void updateModeltxt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            checkInputdigit(e);
        }

        private void simolatorBtn_Click(object sender, RoutedEventArgs e)
        {
            worker.RunWorkerAsync();
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            dr = bl.findDrone(dr.ID);
            DataContext = dr;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            bl.playSimolator(dr.ID, Worker_ProgressChanged, worker.);
        }
    }
}

