using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SongPad.ViewModels
{
    public class CardViewModel : INotifyPropertyChanged
    {
		private string _title;

		public string Title
		{
			get { return _title; }
			set
			{
				_title = value;
				RaisePropertyChanged(nameof(Title));
			}
		}

		private bool _isEditingTitle;

		public bool IsEditingTitle
		{
			get { return _isEditingTitle; }
			set
			{
				_isEditingTitle = value;
				RaisePropertyChanged(nameof(IsEditingTitle));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public CardViewModel()
		{
			Title = "Untitled";
		}

		public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
