using Caliburn.Micro;
using SettingsLoader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SettingsLoader.ViewModels
{
    public class TableViewModel : Screen
    {
        private readonly IEventAggregator _events;

        public string PortSettings { get; set; }

        public BindableCollection<TableModel> Registers { get; set; }

        public TableViewModel(IEventAggregator events)
        {
            _events = events;
            _events.Subscribe(this);
        }

        private TableModel _selectedRegister;

        public TableModel SelectedRegister
        {
            get { return _selectedRegister; }
            set 
            {
                if (value != null)
                {
                    _selectedRegister = value;
                    NotifyOfPropertyChange(() => SelectedRegister);
                }
            }
        }

        public void Read()
        {
            // open comport

            foreach (var register in Registers)
            {

            }

            //close com port
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            Registers = IoC.Get<ShellViewModel>().Registers;
            PortSettings = IoC.Get<ShellViewModel>().PortSettings;
            Application.Current.MainWindow.WindowState = WindowState.Maximized;
        }
    }
}
