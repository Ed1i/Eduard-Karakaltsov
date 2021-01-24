using GalaSoft.MvvmLight.Threading;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace CanTest.Models
{
    public enum CommandStatus
    {
        Read = 1,
        Write = 2,
        NotSelected = 0
    }



    public class Communicator : IDisposable
    {
        private static SerialPort port = new SerialPort();
        public static string ResponseValue { get; private set; }
        public static bool Val = false;
        private CommandStatus _status;

        public Communicator()
        {
            port = new SerialPort();
            port.DataReceived += new SerialDataReceivedEventHandler(OnDataReceived);
        }

        public CommandStatus commandStatus
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                }
            }
        }
        private static void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (Devices.CurrentDevice != null)
            {
                ResponseValue = port.ReadExisting();

                if (Val)
                {
                    Devices.CurrentDevice.Rx = ResponseValue;
                    Val = false;
                }
                else
                {
                    Devices.CurrentDevice.Messages = ResponseValue;
                }
            }
        }

        public static string Test { get; set; } = String.Empty;
        public static string PortName
        {
            get
            {
                return port.PortName;
            }
            set
            {
                port.Close();
               port.PortName = value;
                
               port.Open();

            }
        }
        public static byte[] CmdBytes { get; set; }

        public static Thread ReadWriteThread { get; set; }

        public void SendData(CanOpenDevice cAN)
        {
            try
            {
                if (port.IsOpen)
                {
                    if (commandStatus == CommandStatus.Read)
                    {
                        port.Write(cAN.BuildReadString());
                    }
                    else if (commandStatus == CommandStatus.Write)
                    {
                        port.Write(cAN.BuildWriteString());
                    }
                    else
                    {
                        MessageBox.Show("Please select command");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
        }

        public void SendDataAsync(CanOpenDevice cAN)
        {
            ReadWriteThread = new Thread(() => SendData(cAN));
            ReadWriteThread.Start();
        }

        public void Dispose()
        {
            port.Close();
        }
    }
}
