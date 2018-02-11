using SongPad.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

		public ObservableCollection<string> Lines { get; set; }

		public ICommand AddCommand => new Command(OnAdd);
		public ICommand RemoveCommand => new Command(OnRemove);

		public event PropertyChangedEventHandler PropertyChanged;
		public event EventHandler RemoveEventHandler;

		public CardViewModel()
		{
			Title = "Untitled";
			Lines = new ObservableCollection<string>();
		}

		private void OnAdd()
		{
			Lines.Add($"Lines {Lines.Count + 1}");
		}

		private void OnRemove()
		{
			// TODO : Clear lines
			RemoveEventHandler?.Invoke(this, EventArgs.Empty);
		}

		private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
