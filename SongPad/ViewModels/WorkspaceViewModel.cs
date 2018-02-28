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
using System.Windows.Controls;

namespace SongPad.ViewModels
{
	public class WorkspaceViewModel : BaseViewModel
	{
		private IEventDispatcher _eventDispatcher;
		private IDialogService _dialogService;

		private ProjectViewModel _selectedProject;

		public ProjectViewModel SelectedProject
		{
			get { return _selectedProject; }
			set { SetProperty(ref _selectedProject, value); }
		}

		public ObservableCollection<ProjectViewModel> Projects { get; set; }

		public bool HasItems => Projects.Count > 0;

		public event EventHandler SelectionChanged;

		public WorkspaceViewModel(IEventDispatcher eventDispatcher, IDialogService dialogService)
		{
			_eventDispatcher = eventDispatcher;
			_dialogService = dialogService;

			Projects = new ObservableCollection<ProjectViewModel>();
		}

		public void Initialize()
		{
			PropertyChanged += OnPropertyChanged; // TODO : Unsubscribe

			_eventDispatcher.Subscribe<ProjectEvent>(this, OnProjectEvent); // TODO : Unsubscribe
		}

		// BaseViewModel (Subscriber pattern ?) with an OnEvent method
		private void OnProjectEvent(ProjectEvent evt)
		{
			var dict = new Dictionary<ProjectEvent.InstructionType, Action<ProjectEvent>>
			{
				{ ProjectEvent.InstructionType.New, AddProject }
			};

			if (!dict.ContainsKey(evt.Instruction))
				return;

			dict[evt.Instruction](evt);
		}

		public void AddProject(ProjectEvent evt)
		{
			var result = _dialogService.ShowDialog<NewProjectDialogViewModel>() as NewProjectDialogResult;

			if (result == null)
				return; // TODO : error

			var project = IoC.GetInstance<ProjectViewModel>();
			project.Initialize();
			project.Title = result.ProjectTitle;

			Projects.Add(project);
			SelectedProject = project;
			RaisePropertyChanged(nameof(HasItems));
		}

		private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(SelectedProject))
				SelectionChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
