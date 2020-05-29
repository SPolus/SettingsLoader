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


        public void Registers_PreviewKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Up || e.Key == Key.Down)
            {
                e.Handled = true;
            }

            Registers_KeyDown(e);
        }

        private void Registers_KeyDown(KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.Up)
            {
                MessageBox.Show("CTRL + UP");
            }

            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.Down)
            {
                MessageBox.Show("CTRL + DOWN");
            }
            
            e.Handled = false;
        }

        public void AddRow()
        {
            Registers.Add(new TableModel());
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            Registers = IoC.Get<ShellViewModel>().Registers;
            Application.Current.MainWindow.WindowState = WindowState.Maximized;
        }
    }
}
