using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BO;

 //enum AreaCode { 050, 052, 053, 054, 055, 058 };

namespace PL
{
    /// <summary>
    /// Interaction logic for regist.xaml
    /// </summary>
    public partial class regist : Window
    {
        private BlApi.IBL bl = BlApi.BlFactory.GetBl();
        public regist()
        {
            InitializeComponent();
            
        }

        public regist(BlApi.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
        }

        BO.Customer tmp = new BO.Customer();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tmp.ID = Convert.ToInt32(ID.Text);
            tmp.name = name.Text;
            tmp.phone = Convert.ToString(phone.Text);
            tmp.location = new Location { latitude = Convert.ToDouble(lattitude.Text), longitude = Convert.ToDouble(longitude.Text) };
            bl.addCustomer(tmp);
        }

       
        private void longitude_MouseLeave(object sender, MouseEventArgs e)
        {
            //if (Convert.ToDouble(longitude.Text) < 29.3 || Convert.ToDouble(longitude.Text) > 33.5)
              //  MessageBox.Show("Please enter a location located in the State of Israel (between 29.3 and 33.5)");
        }

        private void lattitude_MouseLeave(object sender, MouseEventArgs e)
        {
         //   if (Convert.ToDouble(lattitude.Text) < 33.7 || Convert.ToDouble(lattitude.Text) > 36.3)
           //     MessageBox.Show("Please enter a location located in the State of Israel (between 33.7 and 36.3)");
        }
    }
}
