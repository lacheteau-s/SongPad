using SongPad.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SongPad.ViewModels
{
	public class MenuViewModel
	{
		public ICommand NewCommand => new Command(OnNew);
		public ICommand OpenCommand => new Command(OnOpen);
		public ICommand SaveCommand => new Command(OnSave);
		public ICommand QuitCommand => new Command(OnQuit);

		// TODO: replace with MessagingService
		public event EventHandler NewEventHandler;
		public event EventHandler OpenEventHandler;
		public event EventHandler SaveEventHandler;
		public event EventHandler QuitEventHandler;

		public void OnNew()
		{
			NewEventHandler?.Invoke(this, EventArgs.Empty);
		}

		public void OnOpen()
		{
			OpenEventHandler?.Invoke(this, EventArgs.Empty);
		}

		public void OnSave()
		{
			SaveEventHandler?.Invoke(this, EventArgs.Empty);
		}

		public void OnQuit()
		{
			QuitEventHandler?.Invoke(this, EventArgs.Empty);
		}
	}
}
