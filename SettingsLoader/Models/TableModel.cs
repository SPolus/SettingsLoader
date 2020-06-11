using Caliburn.Micro;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace SettingsLoader.Models
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn, NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class TableModel : PropertyChangedBase
    {
        private System.UInt16 _register;
        [JsonProperty]
        public System.UInt16 Register
        {
            get { return _register; }
            set 
            {
                if (_register == value) return;
                
                _register = value;

                NotifyOfPropertyChange(() => Register);
            }
        }

        private DataFormat _selectedDataFormat;
        [JsonProperty]
        public DataFormat SelectedDataFormat
        {
            get { return _selectedDataFormat; }
            set
            {
                if (_selectedDataFormat == value) return;

                _selectedDataFormat = value;
                NotifyOfPropertyChange(() => SelectedDataFormat);
            }
        }


        private BindableCollection<int> _readFunctions = new BindableCollection<int> { 0, 1, 2, 3, 4 };
        public BindableCollection<int> ReadFunctions
        {
            get { return _readFunctions; }
            set 
            {
                if (_readFunctions == value) return;

                _readFunctions = value;

                NotifyOfPropertyChange(() => ReadFunctions);
            }
        }

        private int _selectedReadFunction;
        [JsonProperty]
        public int SelectedReadFunction
        {
            get { return _selectedReadFunction; }
            set
            {
                if (_selectedReadFunction == value) return;

                _selectedReadFunction = value;
                NotifyOfPropertyChange(() => SelectedReadFunction);
            }
        }

        private BindableCollection<int> _writeFunctions = new BindableCollection<int> { 0, 5, 6, 15, 16 };

        public BindableCollection<int> WriteFunctions
        {
            get { return _writeFunctions; }
            set 
            {
                if (_writeFunctions == value) return;
                
                _writeFunctions = value;
                NotifyOfPropertyChange(() => WriteFunctions);
            }
        }

        private int _selectedWriteFunction;
        [JsonProperty]
        public int SelectedWriteFunction
        {
            get { return _selectedWriteFunction; }
            set 
            {
                if (_selectedReadFunction == value) return;

                _selectedWriteFunction = value;
                NotifyOfPropertyChange(() => SelectedWriteFunction);
            }
        }

        private string _name;
        [JsonProperty]
        public string Name
        {
            get { return _name; }
            set 
            {
                if (_name == value) return;

                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        private string _description;
        [JsonProperty]
        public string Description
        {
            get { return _description; }
            set 
            {
                if (_description == value) return;

                _description = value;
                NotifyOfPropertyChange(() => Description);
            }
        }

        private double _writeToDevice;
        [JsonProperty]
        public double WriteToDevice
        {
            get { return _writeToDevice; }
            set 
            {
                if (_writeToDevice == value) return;

                if (SelectedDataFormat == DataFormat.uint16 && value < 0) return;

                if (SelectedDataFormat == DataFormat.uint16 || SelectedDataFormat == DataFormat.int16)
                {
                    var success = int.TryParse(value.ToString(), out int result);

                    if (!success) return;
                }

                _writeToDevice = value;
                NotifyOfPropertyChange(() => WriteToDevice);
            }
        }

        private double _readFromDevice;
        public double ReadFromDevice
        {
            get { return _readFromDevice; }
            set 
            {
                _readFromDevice = value;

                NotifyOfPropertyChange(() => ReadFromDevice);
            }
        }
    }
}
