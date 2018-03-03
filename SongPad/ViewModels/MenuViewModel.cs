using SongPad.Messages;
using SongPad.Services;
using SongPad.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SongPad.ViewModels
{
	public class MenuViewModel : BaseViewModel
	{
		private IEventDispatcher _eventDispatcher;

		public ICommand NewCommand => new Command(OnNew);
		public ICommand OpenCommand => new Command(OnOpen);
		public ICommand SaveCommand => new Command(OnSave);
		public ICommand QuitCommand => new Command(OnQuit);

		public event EventHandler QuitEventHandler;

		public MenuViewModel(IEventDispatcher eventDispatcher)
		{
			_eventDispatcher = eventDispatcher;
		}

		public void OnNew()
		{
			_eventDispatcher.Invoke(new ProjectEvent(ProjectEvent.InstructionType.New));
		}

		public void OnOpen()
		{
			_eventDispatcher.Invoke(new ProjectEvent(ProjectEvent.InstructionType.Open));
		}

		public void OnSave()
		{
			_eventDispatcher.Invoke(new ProjectEvent(ProjectEvent.InstructionType.Save));
		}

		public void OnQuit()
		{
			QuitEventHandler?.Invoke(this, EventArgs.Empty);
		}
	}
}
