using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SongPad.ViewModels
{
	public class LineViewModel : BaseViewModel
	{
		private string _line;

		public string Line
		{
			get { return _line; }
			set { SetProperty(ref _line, value); }
		}
	}
}
