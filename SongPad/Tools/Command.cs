using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SongPad.Tools
{
	public class Command : ICommand
	{
		private readonly Func<object, bool> _canExecute;
		private Action<object> _execute;

		public event EventHandler CanExecuteChanged;

		public Command(Action<object> execute)
		{
			_execute = execute ?? throw new ArgumentNullException(nameof(execute));
		}

		public Command(Action execute) : this(parameter => execute())
		{
			if (execute == null)
				throw new ArgumentNullException(nameof(execute));
		}

		public Command(Action<object> execute, Func<object, bool> canExecute) : this(execute)
		{
			_canExecute = canExecute ?? throw new ArgumentNullException();
		}

		public Command(Action execute, Func<bool> canExecute) : this(parameter => execute(), parameter => canExecute())
		{
			if (execute == null)
				throw new ArgumentNullException(nameof(execute));

			if (canExecute == null)
				throw new ArgumentNullException(nameof(canExecute));
		}

		public bool CanExecute(object parameter)
		{
			if (_canExecute == null)
				return true;

			return _canExecute(parameter);
		}

		public void Execute(object parameter)
		{
			_execute(parameter);
		}

		public void RaiseCanExecuteChanged()
		{
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
