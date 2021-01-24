using CanTest.Models;
using CanTest.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace CanTest.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public RelayCommand CommandSave { get; set; }
        static public ObservableCollection<string> DataTypes { get; } = new ObservableCollection<string>(Enum.GetNames(typeof(DataType)));
        private ManagementEventWatcher _watcher;
        

        public MainViewModel()
        {
            Query();
            canP = Devices.CanOpenDevices;
            OpenCommand = new RelayCommand(OpenDialog);
            CommandSave = new RelayCommand(Save);
           
        }

        private static ObservableCollection<CanOpenDevice> _cans;

        public ObservableCollection<CanOpenDevice> canP
        {
            get { return _cans; }
            set
            {
                _cans = value;
                RaisePropertyChanged();
            }
        }

        public static CanOpenDevice GetInstance(int nodeID)
        {
            return _cans.Where(co => co.NodeID == nodeID).FirstOrDefault();
        }

        public void LoadObjects()
        {
            ObservableCollection<CanOpenDevice> cANOpens = new ObservableCollection<CanOpenDevice>();

            cANOpens.Add(new CanOpenDevice { NodeID = 10, Index = 0x6040, Subindex = 5, Datatype = DataType.i8 });
            cANOpens.Add(new CanOpenDevice { NodeID = 15, Index = 0x2030, Subindex = 1, Datatype = DataType.u16 });
            cANOpens.Add(new CanOpenDevice { NodeID = 20, Index = 0x1010, Subindex = 4, Datatype = DataType.i32 });

            canP = cANOpens;
            Devices.CanOpenDevices = cANOpens;
        }


        public ICommand SaveCommand { get; set; }
        public ICommand OpenCommand { get; set; }

        public void Save()
        {
            canP = Devices.CanOpenDevices;
            Devices.Save();
        }

        public void OpenDialog()
        {
            Devices.Open();
            canP = Devices.CanOpenDevices;
        }


        private CanOpenDevice _selectedNodeID;

        public CanOpenDevice selectedNodeID
        {
            get { return _selectedNodeID; }
            set
            {
                if (value != _selectedNodeID)
                {
                    _selectedNodeID = value;
                    RaisePropertyChanged("SelectedNodeID");
                }
            }
        }

        public void SendWriteRequest()
        {
            SerialPort serialPort = new SerialPort();

            serialPort.PortName = "COM2";
            serialPort.BaudRate = 9600;
            serialPort.WriteTimeout = 5000;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            serialPort.Handshake = Handshake.None;
            serialPort.Parity = Parity.None;
            byte[] commandBytes = Encoding.ASCII.GetBytes(canP[0].BuildWriteString());
            serialPort.Write(commandBytes, 0, commandBytes.Length);
        }

        private string Selected;
        public string SelectedItem 
        {
            get => Selected;
            set
            {
                if(Selected != value)
                {
                    Selected = value;
                    RaisePropertyChanged("SelectedItem");
                }
            }
        }
        
        
        static public ObservableCollection<string> PortNames { get; } = new ObservableCollection<string>(SerialPort.GetPortNames());

        private void RefreshPortNames()
        {

            Application.Current.Dispatcher.Invoke(() =>
            {
                PortNames.Clear();
                foreach (string portName in SerialPort.GetPortNames())
                {
                    PortNames.Add(portName);
                    SelectedItem = PortNames[0];
                }
            });
        }

        private void Query()
        {
            WqlEventQuery query = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent");
            _watcher = new ManagementEventWatcher(query);
            _watcher.EventArrived += (s, e) => RefreshPortNames();
            _watcher.Start();
        }

    }
}