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
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for addCustomerWindoes.xaml
    /// </summary>
    public partial class addCustomerWindoes : Window
    {
        private BlApi.IBL bl;
        BO.Customer c;
        private List<BO.ParcelAtCustomer> myCollection;
        public addCustomerWindoes()
        {
            InitializeComponent();
            bl = BlApi.BlFactory.GetBl();
            c = new Customer();
            c.location = new BO.Location();
            this.DataContext = c;
            this.updateBtn.Visibility = Visibility.Hidden;
            this.deleteBtn.Visibility = Visibility.Hidden;
            this.parcelLstView.Visibility = Visibility.Hidden;
            parcelToLstView.Visibility = Visibility.Hidden;
          fromLbl.Visibility = Visibility.Hidden;
            toLbl.Visibility = Visibility.Hidden;
            sendParBtn.Visibility = Visibility.Hidden;
        }

        public addCustomerWindoes(Customer cus, string permission)
        {
            InitializeComponent();
            bl = BlApi.BlFactory.GetBl();
            c = new Customer();
            c = bl.findCustomer(cus.ID);
            DataContext = c;
            this.updateBtn.Visibility = Visibility.Visible;
            this.deleteBtn.Visibility = Visibility.Visible;
            this.addBtn.Visibility = Visibility.Hidden;
            cencelBtn.Visibility = Visibility.Hidden;
            this.nameBtn.IsReadOnly = false;
            this.IDBtn.IsReadOnly = true;
            this.phoneBtn.IsReadOnly = false;
            longitudeBtn.IsReadOnly = true;
            lattitudeBtn.IsReadOnly = true;
            myCollection = new List<ParcelAtCustomer>();
            myCollection = c.fromCustomer;
            parcelLstView.DataContext = myCollection;
            parcelToLstView.ItemsSource = c.toCustomer;
            sendParBtn.Visibility = Visibility.Hidden;

            if ((c.toCustomer.Where(x => x.status == ParcelStatus.Delivred).Count() > 0))
            {
                confirmParBtn.Visibility = Visibility.Visible;
                confirmParBtn.Content = @$"Click to confirm receipt of packages marked ""delivered""";
            }

            if (permission=="user")
            {
                sendParBtn.Visibility = Visibility.Visible;
            }
            if (permission == "maneger")
            {
                
            }
            

        }
        public addCustomerWindoes( CustomerToList cus, string permission)
        {
            InitializeComponent();
            bl = BlApi.BlFactory.GetBl();
            c = new Customer();
            c.fromCustomer = new List<ParcelAtCustomer>();
            c.toCustomer = new List<ParcelAtCustomer>();
            c = bl.findCustomer(cus.ID);
            DataContext = c;
            this.updateBtn.Visibility = Visibility.Visible;
            this.deleteBtn.Visibility = Visibility.Visible;
            this.addBtn.Visibility = Visibility.Hidden;
            cencelBtn.Visibility = Visibility.Hidden;
            this.nameBtn.IsReadOnly = false;
            this.IDBtn.IsReadOnly = true;
            this.phoneBtn.IsReadOnly = false;
            longitudeBtn.IsReadOnly = true;
            lattitudeBtn.IsReadOnly = true;
            sendParBtn.Visibility = Visibility.Hidden;
            myCollection = new List<ParcelAtCustomer>();
            myCollection = c.fromCustomer;
            parcelLstView.DataContext = myCollection;
            parcelToLstView.ItemsSource = c.toCustomer;
            if (permission == "user")
            {
                sendParBtn.Visibility = Visibility.Visible;
            }
            if (permission == "maneger")
            {
                
            }

        }


        public addCustomerWindoes(int id)
        {
            InitializeComponent();
            bl = BlApi.BlFactory.GetBl();
            c = new Customer();
            c.fromCustomer = new List<ParcelAtCustomer>();
            c.toCustomer = new List<ParcelAtCustomer>();
            c = bl.findCustomer(id);
            DataContext = c;
            this.updateBtn.Visibility = Visibility.Hidden;
            this.deleteBtn.Visibility = Visibility.Hidden ;
            this.addBtn.Visibility = Visibility.Hidden;
            this.nameBtn.IsReadOnly = true;
            this.IDBtn.IsReadOnly = true;
            this.phoneBtn.IsReadOnly = true;
            longitudeBtn.IsReadOnly = true;
            lattitudeBtn.IsReadOnly = true;
             parcelLstView.ItemsSource = c.fromCustomer;
            parcelToLstView.ItemsSource = c.toCustomer;
            sendParBtn.Visibility = Visibility.Hidden;

            //var lst = bl.getAllParcels();
            //myCollection = new ObservableCollection<BO.Parcel>();
            //foreach (var item in c.fromCustomer)
            //{
            //    myCollection.Add(bl.findParcel(item.ID));
            //}
            // parcelLstView.DataContext = c.fromCustomer ;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.addCustomer(c);
                MessageBox.Show($"{c.name} successfully added");
                c = new BO.Customer();
                this.DataContext = c;
                this.Close();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.deleteCustomer(c.ID);
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.updateCustomer(c.ID, c.name, c.phone);
                MessageBox.Show("The customer successfuly updated ");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void parcelLstView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (parcelLstView.SelectedItem == null)
                MessageBox.Show("Error! choose an item");
            else
            {
                ParcelAtCustomer p = new ParcelAtCustomer();
                p = (ParcelAtCustomer)parcelLstView.SelectedItem;
                new parcelWindow(p.ID).ShowDialog();
            }
        }

        private void parcelToLstView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (parcelToLstView.SelectedItem == null)
                MessageBox.Show("Error! choose an item");
            else
            {
                ParcelAtCustomer p = new ParcelAtCustomer();
                p = (ParcelAtCustomer)parcelToLstView.SelectedItem;
                new parcelWindow(p.ID).ShowDialog();
            }

            
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

        private void checkInputLetters(KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.Right || e.Key == Key.Left)//allow back,delete and errors keys
                return;
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            if (char.IsLetter(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                    return;
            e.Handled = true;
            MessageBox.Show("The input must be only letters");
        }

        private void IDBtn_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            checkInputdigit(e);
        }

        private void nameBtn_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            checkInputLetters(e);
        }

        private void phoneBtn_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            checkInputdigit(e);
        }

        private void sendParBtn_Click(object sender, RoutedEventArgs e)
        {
            new parcelWindow("user",c.ID).ShowDialog();
        }

        private void confirmParBtn_Click(object sender, RoutedEventArgs e)
        {
            this.confirmParBtn.Visibility = Visibility.Hidden;
        }
    }
}
