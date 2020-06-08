using Caliburn.Micro;

namespace SettingsLoader.Models
{
    public class TableModel : PropertyChangedBase
    {
        private int _register;
        public int Register
        {
            get { return _register; }
            set 
            {
                if (_register == value) return;
                
                _register = value;

                NotifyOfPropertyChange(() => Register);
            }
        }

        private BindableCollection<int> _readFunctions = new BindableCollection<int> { 1, 2, 3, 4 };
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

        private BindableCollection<int> _writeFunctions = new BindableCollection<int> { 5, 6, 15, 16 };

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
    }
}
