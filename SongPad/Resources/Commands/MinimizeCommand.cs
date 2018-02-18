using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SongPad.Resources.Commands
{
	public class MinimizeCommand : ICommand
	{
		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			var window = parameter as Window;

			if (window != null)
				window.WindowState = WindowState.Minimized;
		}
	}
}
