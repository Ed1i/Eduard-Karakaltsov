using CanTest.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace CanTest.Services
{
    public class ReadWrite
    {
        private ObservableCollection<CanOpenDevice> _cans;

        public ObservableCollection<CanOpenDevice> canP
        {
            get { return _cans; }
            set
            {
                _cans = value;
            }
        }

        public void Save()
        {
            XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<CanOpenDevice>));

            using (StreamWriter sw = new StreamWriter($@"{AppDomain.CurrentDomain.BaseDirectory}\SerializedBind.xml"))
            {
                xs.Serialize(sw, canP);
                sw.Close();
            }
        }


        public void OpenDialog()
        {

                XmlSerializer ds = new XmlSerializer(typeof(ObservableCollection<CanOpenDevice>));

                using (Stream rd = new FileStream("Serialized", FileMode.Open))
                {

                    canP = (ObservableCollection<CanOpenDevice>)ds.Deserialize(rd);
                }
            
        }


    }
}
