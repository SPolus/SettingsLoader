using Caliburn.Micro;
using Microsoft.Win32;
using SettingsLoader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsLoader.ViewModels
{
    public class ShellViewModel : Conductor<Screen>.Collection.OneActive, IHandle<string>
    {
        //private readonly string[] FUNCTIONS = new string[8]
        //{
        //    "Read Coils",
        //    "Read Discrete",
        //    "Read Holding Registers",
        //    "Read Input Registers",
        //    "Write Single Coil",
        //    "Write Multiple Coils",
        //    "Write Single Register",
        //    "Write Multiple Registers"
        //};

        private readonly IEventAggregator _events;

        public ShellViewModel(IEventAggregator events)
        {
            _events = events;
            _events.Subscribe(this);

            ActivateItem(IoC.Get<PortSettingsViewModel>());
        }

        private string _portSettings;
        public string PortSettings
        {
            get { return _portSettings; }
            set 
            {
                _portSettings = value;
                NotifyOfPropertyChange(() => PortSettings);
                NotifyOfPropertyChange(() => CanEditMode);
                NotifyOfPropertyChange(() => CanEditPortSettings);
            }
        }

        private BindableCollection<TableModel> _registers = new BindableCollection<TableModel>();

        public BindableCollection<TableModel> Registers
        {
            get { return _registers; }
            set { _registers = value; }
        }


        public void FileExit()
        {
            TryClose();
        }
        public void FileOpen()
        {
            var ofd = new OpenFileDialog()
            {
                DefaultExt = ".json",
                Filter = "Json files (*.json)|*.json",
                InitialDirectory = Environment.CurrentDirectory,
            };

            var result = ofd.ShowDialog();

            if (result == true)
            {
                var filename = ofd.FileName;
                //OpenFile(filename);
            }
        }

        public bool CanEditPortSettings => true;
        public void EditPortSettings()
        {
            ActivateItem(IoC.Get<PortSettingsViewModel>());
        }

        public bool CanViewMode => PortSettings != null;
        public void ViewMode()
        {
            ActivateItem(IoC.Get<TableViewModel>());
        }

        public bool CanEditMode => true;
        public void EditMode()
        {
            ActivateItem(IoC.Get<TableViewModel>());
        }

        public void Handle(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            if (message.StartsWith("COM"))
            {
                PortSettings = message;
                ActivateItem(IoC.Get<TableViewModel>());
            }

        }
    }
}
