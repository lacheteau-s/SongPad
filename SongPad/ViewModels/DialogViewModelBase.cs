using SongPad.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SongPad.ViewModels
{
	public class DialogViewModelBase
	{
		public string Title { get; protected set; }

		public IDialogResult Result { get; protected set; }

		public event EventHandler Close;

		protected void RaiseClose()
		{
			Close?.Invoke(this, EventArgs.Empty);
		}
	}
}
