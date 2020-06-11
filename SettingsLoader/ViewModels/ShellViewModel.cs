using Caliburn.Micro;
using Microsoft.Win32;
using SettingsLoader.Models;
using SettingsLoader.Services;
using SettingsLoader.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SettingsLoader.ViewModels
{
    public class ShellViewModel : Conductor<Screen>.Collection.OneActive, IHandle<string>
    {
        private readonly IEventAggregator _events;
        private readonly IFileService<TableModel> _fileService;
        
        private string _path = Environment.CurrentDirectory;
        
        public bool IsFileOpen { get; private set; }
        public bool IsFileNew { get; private set; }

        public ShellViewModel(IEventAggregator events, IFileService<TableModel> fileService)
        {
            _events = events;
            _events.Subscribe(this);

            _fileService = fileService;
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

        private BindingList<TableModel> _registers = new BindingList<TableModel>();

        public BindingList<TableModel> Registers
        {
            get { return _registers; }
            set { _registers = value; }
        }

        public void FileNew()
        {
            Registers = new BindingList<TableModel>();

            ActivateItem(IoC.Get<ConfigurationViewModel>());

            IsFileNew = true;
            IsFileOpen = false;

            NotifyOfPropertyChange(() => CanEditConfiguration);
            NotifyOfPropertyChange(() => CanFileSave);
            NotifyOfPropertyChange(() => CanFileSaveAs);
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
            Registers = new BindingList<TableModel>();

            try
            {
                _fileService.LoadData(path).ToList().ForEach(row => Registers.Add(row));

                ActivateItem(IoC.Get<TableViewModel>());

                IsFileOpen = true;
                NotifyOfPropertyChange(() => CanEditConfiguration);
                NotifyOfPropertyChange(() => CanFileSave);
                NotifyOfPropertyChange(() => CanFileSaveAs);
            }

            catch (FileNotFoundException ex)
            {
                IsFileOpen = false;
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            catch (FileFormatException ex)
            {
                IsFileOpen = false;
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            catch(Exception ex)
            {
                IsFileOpen = false;
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                TryClose();
            }
        }

        public bool CanFileSave => IsFileOpen || IsFileNew;
        public void FileSave()
        {
            Registers = IoC.Get<ConfigurationViewModel>().Registers;

            if (IsFileNew)
            {
                FileSaveAs();

                return;
            }

            try
            {
                _fileService.SaveData(_path, Registers);
                IoC.Get<ConfigurationViewModel>().IsModified = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public bool CanFileSaveAs => IsFileOpen || IsFileNew;
        public void FileSaveAs()
        {
            Registers = IoC.Get<ConfigurationViewModel>().Registers;

            var sfd = new SaveFileDialog()
            {
                DefaultExt = ".json",
                Filter = "Json files (*.json)|*.json",
                InitialDirectory = Environment.CurrentDirectory
            };

            var result = sfd.ShowDialog();

            if (result == true)
            {
                if (IsFileNew)
                {
                    IsFileNew = false;
                    NotifyOfPropertyChange(() => CanEditConfiguration);
                    NotifyOfPropertyChange(() => CanFileSave);
                    NotifyOfPropertyChange(() => CanFileSaveAs);
                }

                if (!IsFileOpen)
                {
                    IsFileOpen = true;
                    NotifyOfPropertyChange(() => CanEditConfiguration);
                    NotifyOfPropertyChange(() => CanFileSave);
                    NotifyOfPropertyChange(() => CanFileSaveAs);
                }

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

        public bool CanEditConfiguration => IsFileOpen || IsFileNew;
        public void EditConfiguration()
        {
            ActivateItem(IoC.Get<ConfigurationViewModel>());
        }

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

                if (IsFileOpen)
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
            ActivateItem(IoC.Get<PortSettingsViewModel>());

            // TODO: Create initial config file
        }

    }
}
