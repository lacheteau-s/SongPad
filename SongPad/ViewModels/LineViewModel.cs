using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SongPad.ViewModels
{
	public class LineViewModel : INotifyPropertyChanged
	{
		private string _line;

		public string Line
		{
			get { return _line; }
			set
			{
				_line = value;
				RaisePropertyChanged(nameof(Line));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
