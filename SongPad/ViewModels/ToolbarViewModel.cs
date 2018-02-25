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
		public ICommand AddCardCommand => new Command(OnAddCard);

		public event EventHandler AddCardEventHandler;

		private void OnAddCard()
		{
			AddCardEventHandler?.Invoke(this, EventArgs.Empty);
		}
	}
}
