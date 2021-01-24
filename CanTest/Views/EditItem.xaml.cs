using CanTest.Models;
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

namespace CanTest.Views
{
    /// <summary>
    /// Interaction logic for EditItem.xaml
    /// </summary>
    public partial class EditItem : Window
    {
        public EditItem()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

       
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
        var item = Devices.CanOpenDevices.FirstOrDefault(i => i.NodeID == Devices.CurrentDevice.NodeID);
            if (item != null)
            {
                if (tbID.Text != "" && tbIndex.Text != "")
                {
                    item.NodeID = Convert.ToUInt16(tbID.Text);
                    item.Index = Convert.ToUInt16(tbIndex.Text);
                    item.Subindex = Convert.ToByte(tbSubindex.Text);
                    item.Datatype = (DataType)Enum.Parse(typeof(DataType), cb.SelectedItem.ToString());
                    item.Description = tbDsc.Text;

                    this.Close();
                }
            }
        }
    }
}
