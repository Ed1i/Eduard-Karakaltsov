using CanTest.Models;
using CanTest.ViewModel;
using System;
using System.Collections.Generic;
using System.IO.Ports;
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

namespace CanTest
{
    /// <summary>
    /// Interaction logic for COMportSett.xaml
    /// </summary>
    public partial class COMportSett : Window
    {
       
        private Communicator communicator = new Communicator();
        CanOpenDevice devices = new CanOpenDevice();
        Communicator portsettings = new Communicator();
        public COMportSett()
        {
            InitializeComponent();
            
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            Communicator.PortName = cb1.Text;
            Communicator.Test = cb1.Text;
            this.Close();
        }

        

        private void btSend_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel main = new MainViewModel();
            if (cbRWcmd.SelectedItem == SdoRead)
            {
                devices.NodeID = Convert.ToUInt16(tbNodeID.Text);
                devices.Index = Convert.ToUInt16(tbIndex.Text);
                devices.Subindex = Convert.ToByte(tbSubindex.Text);
                Communicator.PortName = cb1.Text;
                //ReadTimeout = Convert.ToInt32(tbTimeout.Text);
            }
            else
            {
                tbValue.Visibility = Visibility.Visible;
                devices.NodeID = Convert.ToUInt16(tbNodeID.Text);
                devices.Index = Convert.ToUInt16(tbIndex.Text);
                devices.Subindex = Convert.ToByte(tbSubindex.Text);
              
                // port.ReadTimeout = Convert.ToInt32(tbTimeout.Text);
            }

        }

        private void cbRWcmd_GotFocus(object sender, RoutedEventArgs e)
        {
            if (cbRWcmd.SelectedIndex == 1)
            {
                tbValue.Visibility = Visibility.Visible;
            }
            else
            {
                tbValue.Visibility = Visibility.Hidden;
            }
        }
    }
}
