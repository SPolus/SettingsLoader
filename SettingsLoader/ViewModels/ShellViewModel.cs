using Caliburn.Micro;
using Microsoft.Win32;
using SettingsLoader.Models;
using SettingsLoader.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        private string _path = $"{Environment.CurrentDirectory}\\LastSession\\DeviceConfig.json";

        private JsonFileService<BindableCollection<TableModel>> _jsonFileService = new JsonFileService<BindableCollection<TableModel>>();

        private bool _isFileOpened;

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
            }
        }

        private BindableCollection<TableModel> _registers = new BindableCollection<TableModel>();

        public BindableCollection<TableModel> Registers
        {
            get { return _registers; }
            set { _registers = value; }
        }

        public void FileNew()
        {

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
                _path = ofd.FileName;
                OpenFile(_path);
            }
        }

        private void OpenFile(string path)
        {
            try
            {
                Registers = _jsonFileService.LoadData(path);

                if (PortSettings != null)
                {
                    ActivateItem(IoC.Get<TableViewModel>());
                }

                _isFileOpened = true;
                NotifyOfPropertyChange(() => CanEditConfiguration);
                NotifyOfPropertyChange(() => CanFileSave);
                NotifyOfPropertyChange(() => CanFileSaveAs);

            }

            catch (FileNotFoundException)
            {
                var result = MessageBox.Show("Create File?", "File not found", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        if (string.IsNullOrEmpty(path))
                        {
                            TryClose();
                        }

                        File.CreateText(path).Dispose();
                        break;

                    case MessageBoxResult.No:
                        TryClose();
                        break;
                }

                Registers = new BindableCollection<TableModel>();
            }

            catch (FileFormatException ex)
            {
                Registers = new BindableCollection<TableModel>();
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                TryClose();
            }
        }

        public bool CanFileSave => _isFileOpened;
        public void FileSave()
        {
            try
            {
                _jsonFileService.SaveData(_path, Registers);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public bool CanFileSaveAs => _isFileOpened;
        public void FileSaveAs()
        {
            var sfd = new SaveFileDialog()
            {
                DefaultExt = ".json",
                Filter = "Json files (*.json)|*.json",
                InitialDirectory = Environment.CurrentDirectory
            };

            var result = sfd.ShowDialog();

            if (result == true)
            {
                _path = sfd.FileName;
                FileSave();
            }
        }

        public void FileExit()
        {
            TryClose();
        }

        public bool CanEditPortSettings => true;
        public void EditPortSettings()
        {
            ActivateItem(IoC.Get<PortSettingsViewModel>());
        }

        public bool CanEditConfiguration => _isFileOpened;
        public void EditConfiguration()
        {
            ActivateItem(IoC.Get<ConfigurationViewModel>());
        }

        //public bool CanViewMode => PortSettings != null;
        //public void ViewMode()
        //{
        //    ActivateItem(IoC.Get<TableViewModel>());
        //}

        //public bool CanEditMode => true;
        //public void EditMode()
        //{
        //    ActivateItem(IoC.Get<TableViewModel>());
        //}

        public void Help()
        {
            ActivateItem(IoC.Get<HelpViewModel>());
        }

        public void About()
        {
            ActivateItem(IoC.Get<AboutViewModel>());
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

                if (_isFileOpened)
                {
                    ActivateItem(IoC.Get<TableViewModel>());
                    return;
                }
                
                ActivateItem(IoC.Get<HelpViewModel>());
            }

        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            // TODO: Create initial config file
        }
    }
}
