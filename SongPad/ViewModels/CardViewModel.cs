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

		public ObservableCollection<LineViewModel> Lines { get; set; }

		public ICommand AddCommand => new Command(OnAdd);
		public ICommand RemoveCommand => new Command(OnRemove);

		public event PropertyChangedEventHandler PropertyChanged;
		public event EventHandler RemoveEventHandler;

		public CardViewModel()
		{
			Title = "Untitled";
			Lines = new ObservableCollection<LineViewModel>();
		}

		public void RemoveLines(LineViewModel[] items)
		{
			foreach (var item in items)
				Lines.Remove(item);
		}

		public void MoveLinesUp(LineViewModel[] items)
		{
			var indexes = items.Select(i => Lines.IndexOf(i)).OrderBy(i => i).ToArray();

			for (int i = 0; i < items.Length; ++i)
			{
				if (i == 0 && indexes[i] > 0)
					Lines.Move(indexes[i], --indexes[i]);
				else if (i > 0 && indexes[i] > (indexes[i - 1] + 1))
					Lines.Move(indexes[i], --indexes[i]);
			}
		}

		public void MoveLinesDown(LineViewModel[] items)
		{
			var indexes = items.Select(i => Lines.IndexOf(i)).OrderByDescending(i => i).ToArray();

			for (int i = 0; i < items.Length; ++i)
			{
				if (i == 0 && indexes[i] < (Lines.Count - 1))
					Lines.Move(indexes[i], ++indexes[i]);
				else if (i > 0 && indexes[i] < (indexes[i - 1] - 1))
					Lines.Move(indexes[i], ++indexes[i]);
			}
		}

		private void OnAdd()
		{
			var line = IoC.GetInstance<LineViewModel>();
			line.Line = $"Test {Lines.Count + 1}";
			Lines.Add(line);
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
