using SongPad.Messages;
using SongPad.Services;
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
    public class CardViewModel : BaseViewModel
    {
		private IEventDispatcher _eventDispatcher;

		private string _title;

		public string Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value); }
		}

		private bool _isEditingTitle;

		public bool IsEditingTitle
		{
			get { return _isEditingTitle; }
			set { SetProperty(ref _isEditingTitle, value); }
		}

		public ObservableCollection<LineViewModel> Lines { get; set; }

		public ICommand AddLineCommand => new Command(OnAddLine);
		public ICommand RemoveCardCommand => new Command(OnRemoveCard);

		public event EventHandler RemoveEventHandler;

		public CardViewModel(IEventDispatcher eventDispatcher)
		{
			_eventDispatcher = eventDispatcher;
		}

		public override void Initialize()
		{
			Title = "Untitled";
			Lines = new ObservableCollection<LineViewModel>();

			base.Initialize();
		}

		public override void Cleanup()
		{
			base.Cleanup();

			foreach (var line in Lines)
				line.Cleanup();

			Lines.Clear();
		}

		public void RemoveLines(LineViewModel[] lines)
		{
			foreach (var line in lines)
			{
				line.Cleanup();
				Lines.Remove(line);
			}

			_eventDispatcher.Invoke<ProjectChangedEvent>();
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

			_eventDispatcher.Invoke<ProjectChangedEvent>();
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

			_eventDispatcher.Invoke<ProjectChangedEvent>();
		}

		private void OnAddLine()
		{
			var line = IoC.GetInstance<LineViewModel>();

			line.Initialize();
			line.Line = $"Test {Lines.Count + 1}";
			Lines.Add(line);

			_eventDispatcher.Invoke<ProjectChangedEvent>();
		}

		private void OnRemoveCard()
		{
			// TODO : Clear lines
			RemoveEventHandler?.Invoke(this, EventArgs.Empty);
		}
	}
}
