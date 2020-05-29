using Caliburn.Micro;
using SettingsLoader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace SettingsLoader.ViewModels
{
    public class ConfigurationViewModel : Screen
    {
		public bool IsModified { get; private set; }

		private BindingList<TableModel> _registers = new BindingList<TableModel>();

		public BindingList<TableModel> Registers
		{
			get { return _registers; }
			set
			{
				if (_registers == value) return;

				_registers = value;
				NotifyOfPropertyChange(() => Registers);
			}
		}

		private TableModel _selectedRegister;

		public TableModel SelectedRegister
		{
			get { return _selectedRegister; }
			set
			{
				_selectedRegister = value;
				NotifyOfPropertyChange(() => SelectedRegister);
			}
		}

		public bool CanAddRow => true;
		public void AddRow()
		{
			Registers.Add(new TableModel());
			NotifyOfPropertyChange(() => Registers);
		}

		public bool CanRemoveRow => Registers.Count > 0;
		public void RemoveRow()
		{
			Registers.Remove(SelectedRegister);
			NotifyOfPropertyChange(() => Registers);
		}

		public bool CanMoveUp => true;
		public void MoveUp()
		{
			if (SelectedRegister != null)
			{
				BindingList<TableModel> tempList = new BindingList<TableModel>(Registers);
				var index = Registers.IndexOf(SelectedRegister);

				if (index > 0)
				{
					var temp = tempList[index - 1];
					tempList[index - 1] = tempList[index];
					tempList[index] = temp;

					Registers = tempList;

					//NotifyOfPropertyChange(() => Registers);
					//NotifyOfPropertyChange(() => SelectedRegister);
				}
			}
		}

		public bool CanMoveDown => true;
		public void MoveDown()
		{

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
				MoveUp();
			}

			if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.Down)
			{
				MoveDown();
			}

			e.Handled = false;
		}

		protected override void OnActivate()
		{
			base.OnActivate();

			foreach (var item in IoC.Get<ShellViewModel>().Registers)
			{
				Registers.Add(item);
			}

			_registers.ListChanged += Registers_ListChanged;
		}

		private void Registers_ListChanged(object sender, ListChangedEventArgs e)
		{
			IsModified = true;
		}
	}
}
