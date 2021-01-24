using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CanTest.Models
{
    public static class Devices
    {
        public static CanOpenDevice CurrentDevice { get; set; } = null;

        public static ObservableCollection<CanOpenDevice> CanOpenDevices { get; set; } = new ObservableCollection<CanOpenDevice>();
        

        static public void Open()
        {
            XmlSerializer ds = new XmlSerializer(typeof(ObservableCollection<CanOpenDevice>));

            using (Stream rd = new FileStream("SerializedBind.xml", FileMode.Open))
            {
                CanOpenDevices = (ObservableCollection<CanOpenDevice>)ds.Deserialize(rd);
            }
        }
     
        static public void Save()
        {

            XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<CanOpenDevice>));

            using (StreamWriter sw = new StreamWriter($@"{AppDomain.CurrentDomain.BaseDirectory}\SerializedBind.xml"))
            {
                xs.Serialize(sw, CanOpenDevices);
                sw.Close();
            }

        }
    }
}
