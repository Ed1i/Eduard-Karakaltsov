using System;
using System.ComponentModel;

namespace CanTest.Models
{


    public enum DataType
    {
        u8 =0,
        u16 =1,
        u32=2,
        i8=3,
        i16=4,
        i32=5
    }

    public class CanOpenDevice : INotifyPropertyChanged, IDataErrorInfo
    {
        private DataType _DataType;
        private UInt16 nodeID;
        private UInt16 index;
        private byte subindex;
        private string _Tx;
        private string _Rx;
        private string Dsc;
        public UInt16 NodeID
        {
            get { return nodeID; }
            set
            {
                if (nodeID != value)
                {
                    nodeID = value;
                    RaisePropertyChanged("NodeID");
                }
            }
        }
        public UInt16 Index
        {
            get { return index; }
            set
            {
                if (index != value)
                {
                    index = value;
                    RaisePropertyChanged("Index");
                }
            }
        }

        public byte Subindex
        {
            get { return subindex; }
            set
            {
                if (subindex != value)
                {
                    subindex = value;
                    RaisePropertyChanged("SubIndex");
                }
            }
        }
        public string Rx
        {
            get { return _Rx; }
            set
            {
                if (_Rx != value)
                {
                    _Rx = value;
                    RaisePropertyChanged("Rx");
                }
            }
        }
        public string Tx
        {
            get { return _Tx; }
            set
            {
                if (_Tx != value)
                {
                    _Tx = value;
                    RaisePropertyChanged("Tx");
                }

            }
        }
        public string Description
        {
            get { return Dsc; }
            set
            {
                if(Dsc != value)
                {
                    Dsc = value;
                    RaisePropertyChanged("Description");
                }
            }
        }
        public DataType Datatype
        {
            get { return _DataType; }
            set
            {
                if (_DataType != value)
                {
                    _DataType = value;
                    RaisePropertyChanged("DataType");
                }
            }
        }

        private int _value;
        public int Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    RaisePropertyChanged("Value");
                }
            }
        }

        private string _Messages;
        public string Messages
        {
            get { return _Messages; }
            set
            {
                 if(_Messages != value)
                {
                    _Messages = value;
                    RaisePropertyChanged("Messages");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string property)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
       
        public string BuildWriteString()
        {
           Tx = $"{NodeID} w {Index} {Subindex} {Datatype} {Value}\r\n";
            Communicator.Val = true;
            return Tx;
        }

        public string BuildReadString()
        {
            Tx= $"{NodeID} r {Index} {Subindex} {Datatype}\r\n";
            Communicator.Val = true;
            return Tx;
        }

        public string NewNodeID(int ID)
        {
            Tx= $"set node {ID}\r\n";
            Communicator.Val = true;
            return Tx ;
        }

       public string Initialize()
        {
            Tx = "init 0\r\n";
            Communicator.Val = true;
            return Tx;
        }

        public string State()
        {
            Tx = "info state\r\n";
            Communicator.Val = true;
            return Tx;
        }

        public string Start(uint ID)
        {
            Tx = $"{ID} start\r\n";
            return Tx;
        }

        public string Error
        {
            get { return "..."; }
        }

        public string this[string columnName]
        {
            get
            {
                return Validate(columnName);
            }
        }

        private string Validate(string propertyName)
        {

            string validationMessage = string.Empty;
            switch (propertyName)
            {
                case "Name":
                    validationMessage = "Error";
                    break;
            }

            return validationMessage;
        }

    }
}
