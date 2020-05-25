using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsLoader.ViewModels
{
    public class PortSettingsViewModel : Screen, IHandle<string>
    {
        private IEventAggregator _events;

        public PortSettingsViewModel(IEventAggregator events)
        {
            _events = events;
            _events.Subscribe(this);
        }

        private BindableCollection<string> _ports = new BindableCollection<string>();
        public BindableCollection<string> Ports
        {
            get { return _ports; }
            set { _ports = value; }
        }

        private string _selectedPort;

        public string SelectedPort
        {
            get { return _selectedPort; }
            set 
            {
                _selectedPort = value;
                NotifyOfPropertyChange(() => SelectedPort);
                NotifyOfPropertyChange(() => CanApply);
                NotifyOfPropertyChange(() => PortSettings);
            }
        }

        private BindableCollection<string> _baudRates = new BindableCollection<string>
        {
            "9600", "19200", "38400", "57600", "115200"
        };

        public BindableCollection<string> BaudRates
        {
            get { return _baudRates; }
            set { _baudRates = value; }
        }

        private string _selectedBaudRate;

        public string SelectedBaudRate
        {
            get { return _selectedBaudRate; }
            set 
            {
                _selectedBaudRate = value;
                NotifyOfPropertyChange(() => SelectedBaudRate);
                NotifyOfPropertyChange(() => CanApply);
                NotifyOfPropertyChange(() => PortSettings);
            }
        }

        private BindableCollection<string> _parities = new BindableCollection<string>
        {
            "None", "Even", "Odd"
        };

        public BindableCollection<string> Parities
        {
            get { return _parities; }
            set { _parities = value; }
        }

        private string _selectedParity;

        public string SelectedParity
        {
            get { return _selectedParity; }
            set 
            {
                _selectedParity = value;
                NotifyOfPropertyChange(() => SelectedParity);
                NotifyOfPropertyChange(() => CanApply);
                NotifyOfPropertyChange(() => PortSettings);
            }
        }

        private BindableCollection<string> _stopBits = new BindableCollection<string> { "1", "2" };

        public BindableCollection<string> StopBits
        {
            get { return _stopBits; }
            set { _stopBits = value; }
        }

        private string _selectedStopBit;

        public string SelectedStopBit
        {
            get { return _selectedStopBit; }
            set 
            {
                _selectedStopBit = value;
                NotifyOfPropertyChange(() => SelectedStopBit);
                NotifyOfPropertyChange(() => CanApply);
                NotifyOfPropertyChange(() => PortSettings);
            }
        }

        public string PortSettings => CanApply 
            ? $"{SelectedPort}/{SelectedBaudRate}/{SelectedParity.FirstOrDefault()}/8/{SelectedStopBit}"
            : "";

        public bool CanApply => !string.IsNullOrEmpty(SelectedPort)
            && !string.IsNullOrEmpty(SelectedBaudRate)
            && !string.IsNullOrEmpty(SelectedParity)
            && !string.IsNullOrEmpty(SelectedStopBit);

        public void Apply()
        {
            _events.PublishOnUIThread(PortSettings);
        }



        protected override void OnInitialize()
        {
            string[] ports = SerialPort.GetPortNames();
            //ports?.ToList().ForEach(port => Ports.Add(port));
            
            if (ports != null)
            {
                foreach (var port in ports)
                {
                    Ports.Add(port);
                }
            }
        }



        protected override void OnActivate()
        {
            base.OnActivate();

            if (IoC.Get<ShellViewModel>().PortSettings != null)
            {
                string[] comPortSettings = IoC.Get<ShellViewModel>().PortSettings.Split('/');
                SelectedPort = comPortSettings[0];
                SelectedBaudRate = comPortSettings[1];
                SelectedParity = comPortSettings[2].Length > 1
                    ? comPortSettings[2]
                    : comPortSettings[2]
                    .Replace("N", "None")
                    .Replace("E", "Even")
                    .Replace("O", "Odd");

                SelectedStopBit = comPortSettings[4];
            }
        }

        public void Handle(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }
        }
    }
}
