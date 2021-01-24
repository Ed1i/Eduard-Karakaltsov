using CanTest.Models;
using CanTest.ViewModel;
using CanTest.Views;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace CanTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private CanOpenDevice currentCanOpen = null;
        private Communicator communicator = new Communicator();


        public MainWindow()
        {
            InitializeComponent();
        }

        private void btW_Click(object sender, RoutedEventArgs e)
        {
            if(currentCanOpen != null)
            {
                Communicator.PortName = Convert.ToString(cbDevices.SelectedItem);
                LBMSG.Visibility = Visibility.Visible;
                communicator.SendDataAsync(currentCanOpen);
            }
            else
            {
                MessageBox.Show("You must choose a device before sending commands");
            }
            //if (Communicator.Test.Length != 0)
            //{

            //    LBMSG.Visibility = Visibility.Visible;
            //    communicator.SendDataAsync(currentCanOpen);
            //}
            //else
            //{
            //    COMportSett open = new COMportSett();
            //    open.lbWarning.Visibility = Visibility.Visible;
            //    open.ShowDialog();
            //    // MessageBox.Show("Please choose a COM Port");
            //}
        }

        private void ComSettings_Click(object sender, RoutedEventArgs e)
        {
            COMportSett sett = new COMportSett();
            sett.Show();
            
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count != 0)
            {
                currentCanOpen = (CanOpenDevice)e.AddedItems[0];

                lb1.Content = currentCanOpen.NodeID;
                
                tb1.Text = currentCanOpen.Value.ToString();

                Devices.CurrentDevice = currentCanOpen;
            }
            else
            {
                currentCanOpen = null;
                lb1.Content = "";
                tb1.Text = "0";
                Devices.CurrentDevice = null;
            }
        }

        private void ValueChanged(object sender, TextChangedEventArgs e)
        {
            if (currentCanOpen != null)
            {
                try
                {
                    currentCanOpen.Value = Convert.ToInt32(tb1.Text);
                }
                catch
                {
                    currentCanOpen.Value = 0;
                }
            }
            
        }

        private void listView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
             //  Devices.CanOpenDevices.Remove((CanOpenDevice)lv.SelectedItem);
                  Devices.CanOpenDevices.Remove(currentCanOpen);
            }
        }

        private void btAddRow_Click(object sender, RoutedEventArgs e)
        {
            AddItem add = new AddItem();
            add.ShowDialog();
        }

        private void lv_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (currentCanOpen != null)
            {
                EditItem edit = new EditItem();
                edit.tbID.Text = currentCanOpen.NodeID.ToString();
                edit.tbIndex.Text = currentCanOpen.Index.ToString();
                edit.tbSubindex.Text = currentCanOpen.Subindex.ToString();
                edit.cb.SelectedIndex = (int)currentCanOpen.Datatype;
                edit.tbDsc.Text = currentCanOpen.Description.ToString();

                edit.ShowDialog();
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void cbReadWrite_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbRead.IsSelected)
            {
                lbValue.Visibility = Visibility.Hidden;
                tb1.Visibility = Visibility.Hidden;
                communicator.commandStatus = CommandStatus.Read;
            }
            else
            {
                lbValue.Visibility = Visibility.Visible;
                tb1.Visibility = Visibility.Visible;
                communicator.commandStatus = CommandStatus.Write;
            }
        }
    }
}
