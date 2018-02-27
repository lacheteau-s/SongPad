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
	public class ToolBarViewModel : BaseViewModel
	{
		private IEventDispatcher _eventDispatcher;

		public ICommand AddCardCommand => new Command(OnAddCard);

		public event EventHandler AddCardEventHandler;

		public ToolBarViewModel(IEventDispatcher eventDispatcher)
		{
			_eventDispatcher = eventDispatcher;
		}

		private void OnAddCard()
		{
			_eventDispatcher.Invoke<AddCardMessage>(null);
		}
	}
}
