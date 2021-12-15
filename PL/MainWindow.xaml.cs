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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BO;


namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // BlApi.BL bl;
        BlApi.IBL bl = BL.BlFactory.GetBl();
        public MainWindow()
        {
            InitializeComponent();
            // bl = new BlApi.BL();
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new droneListView(bl).Show();
           
        }
    
        
    }
}
