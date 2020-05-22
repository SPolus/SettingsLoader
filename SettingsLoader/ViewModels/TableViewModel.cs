using Caliburn.Micro;
using SettingsLoader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsLoader.ViewModels
{
    public class TableViewModel : Screen
    {
        private readonly IEventAggregator _events;

        public string PortSettings => IoC.Get<ShellViewModel>().PortSettings;

        public BindableCollection<TableModel> Registers => IoC.Get<ShellViewModel>().Registers;
        public TableViewModel(IEventAggregator events)
        {
            _events = events;
            _events.Subscribe(this);
        }
    }
}
