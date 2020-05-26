using Caliburn.Micro;
using SettingsLoader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SettingsLoader.ViewModels
{
    public class TableViewModel : Screen
    {
        private readonly IEventAggregator _events;

        public string PortSettings => IoC.Get<ShellViewModel>().PortSettings;

        private BindableCollection<TableModel> _registers = new BindableCollection<TableModel>();

        public BindableCollection<TableModel> Registers
        {
            get => _registers;
            set 
            { 
                _registers = value; 
            }
        }

        public TableViewModel(IEventAggregator events)
        {
            _events = events;
            _events.Subscribe(this);
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            Registers = IoC.Get<ShellViewModel>().Registers;
            Application.Current.MainWindow.WindowState = WindowState.Maximized;
        }
    }
}
