using SongPad.Messages;
using SongPad.Services;
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
		private IEventDispatcher _eventDispatcher;

		private string _line;

		public string Line
		{
			get { return _line; }
			set { SetProperty(ref _line, value); }
		}

		public LineViewModel(IEventDispatcher eventDispatcher)
		{
			_eventDispatcher = eventDispatcher;
		}

		protected override void Subscribe()
		{
			PropertyChanged += OnPropertyChanged;
		}

		protected override void Unsubscribe()
		{
			PropertyChanged -= OnPropertyChanged;
		}

		private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(Line))
				_eventDispatcher.Invoke<ProjectChangedEvent>();
		}
	}
}
