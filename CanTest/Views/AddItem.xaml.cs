using CanTest.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CanTest.Views
{
    /// <summary>
    /// Interaction logic for AddItem.xaml
    /// </summary>
    ///    


    public partial class AddItem : Window
    {

        public AddItem()
        {
            InitializeComponent();
        }



        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Devices.CanOpenDevices.Add(new CanOpenDevice { NodeID = Convert.ToUInt16(tbID.Text), Index = Convert.ToUInt16(tbIndex.Text), Subindex = Convert.ToByte(tbSubindex.Text), Datatype = (DataType)Enum.Parse(typeof(DataType), cb.SelectedItem.ToString()), Description = tbDsc.Text });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error parsing" + ex);
                return;
            }
            finally
            {
                tbID.Clear();
                tbIndex.Clear();
                tbSubindex.Clear();
                cb.SelectedIndex = -1;
                tbDsc.Clear();
                this.Close();
            }
        }

        private void tbID_TextChanged(object sender, TextChangedEventArgs e)
        {
           if((tbID != null) && (tbIndex != null ) && (tbSubindex != null) && (AddButton != null))
            {
                if ((tbID.Text != "0" && tbID.Text != "") && (tbIndex.Text != "0" && tbIndex.Text != "") && (tbSubindex.Text != ""))
                {
                    AddButton.IsEnabled = true;
                }
                else
                {
                    AddButton.IsEnabled = false;
                }
            }
        }
    }
 }
