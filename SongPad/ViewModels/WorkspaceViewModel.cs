using SongPad.Messages;
using SongPad.Services;
using SongPad.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace SongPad.ViewModels
{
	public class WorkspaceViewModel : BaseViewModel
	{
		private IEventDispatcher _eventDispatcher;
		private IDialogService _dialogService;

		private readonly string PROJECT_FORMAT_FILTER = "SongPad file (*.sgp)|*.sgp";

		private ProjectViewModel _selectedProject;

		public ProjectViewModel SelectedProject
		{
			get { return _selectedProject; }
			set { SetProperty(ref _selectedProject, value); }
		}

		public ObservableCollection<ProjectViewModel> Projects { get; set; }

		public bool HasItems => Projects?.Count > 0;

		public event EventHandler SelectionChanged;

		public ICommand CloseProjectCommand => new Command(OnCloseProject);

		public WorkspaceViewModel(IEventDispatcher eventDispatcher, IDialogService dialogService)
		{
			_eventDispatcher = eventDispatcher;
			_dialogService = dialogService;
		}

		#region Lifecycle

		public override void Initialize()
		{
			Projects = new ObservableCollection<ProjectViewModel>();

			base.Initialize();
		}

		public override void Cleanup()
		{
			base.Cleanup();

			foreach (var project in Projects)
				project.Cleanup();

			Projects.Clear();
		}

		protected override void Subscribe()
		{
			base.Subscribe();

			PropertyChanged += OnPropertyChanged;

			_eventDispatcher.Subscribe<ProjectEvent>(this, OnProjectEvent);
		}

		protected override void Unsubscribe()
		{
			base.Unsubscribe();

			PropertyChanged -= OnPropertyChanged;

			_eventDispatcher.Unsubscribe<ProjectEvent>(this);
		}

		#endregion

		// BaseViewModel (Subscriber pattern ?) with an OnEvent method
		private void OnProjectEvent(ProjectEvent evt)
		{
			var dict = new Dictionary<ProjectEvent.InstructionType, Action>
			{
				{ ProjectEvent.InstructionType.New, AddProject },
				{ ProjectEvent.InstructionType.Save, SaveCurrentProject },
				{ ProjectEvent.InstructionType.Open, OpenProject }
			};

			if (!dict.ContainsKey(evt.Instruction))
				return;

			dict[evt.Instruction]();
		}

		private void AddProject()
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

		private void OpenProject()
		{
			var result = (FileDialogResult)_dialogService.OpenFileDialog(PROJECT_FORMAT_FILTER, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

			if (!result.IsOk)
				return;

			var vm = IoC.GetInstance<ProjectViewModel>();
			vm.Initialize(result.FilePath);

			Projects.Add(vm);
			SelectedProject = vm;
			RaisePropertyChanged(nameof(HasItems));
		}

		private void SaveCurrentProject()
		{
			// TODO : async
			SelectedProject.Save();
		}

		private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(SelectedProject))
				SelectionChanged?.Invoke(this, EventArgs.Empty);
		}

		private void OnCloseProject(object parameter)
		{
			var project = (ProjectViewModel)parameter;

			if (project.HasChanges)
			{
				var result = _dialogService.ShowDialog($"Project \"{project.Title.TrimEnd('*')}\" has been modified.{Environment.NewLine}{Environment.NewLine}Do you want to save the changes ?", "Save changes ?", DialogButton.YesNoCancel, DialogImage.Question);

				if (result == DialogResult.Yes)
					project.Save();
				else if (result == DialogResult.Cancel)
					return;
			}

			project.Cleanup();
			Projects.Remove(project);
		}
	}
}
