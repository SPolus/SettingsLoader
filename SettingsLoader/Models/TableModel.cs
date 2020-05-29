using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsLoader.Models
{
    public class TableModel : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private int _register;
        public int Register
        {
            get { return _register; }
            set 
            {
                if (_register == value) return;
                
                _register = value;
                OnProppertyChanged(nameof(Register));
            }
        }

        private int _function;
        public int Function
        {
            get { return _function; }
            set 
            {
                if (_function == value) return;

                _function = value;
                OnProppertyChanged(nameof(Function));
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set 
            {
                if (_name == value) return;

                _name = value;
                OnProppertyChanged(nameof(Name));
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set 
            {
                if (_description == value) return;

                _description = value;
                OnProppertyChanged(nameof(Description));
            }
        }
        protected virtual void OnProppertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
