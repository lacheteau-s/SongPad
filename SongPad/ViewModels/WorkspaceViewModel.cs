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
		private ProjectViewModel _selectedProject;

		public ProjectViewModel SelectedProject
		{
			get { return _selectedProject; }
			set { SetProperty(ref _selectedProject, value); }
		}

		public ObservableCollection<ProjectViewModel> Projects { get; set; }

		public bool HasItems => Projects.Count > 0;

		public event EventHandler SelectionChanged;

		public WorkspaceViewModel()
		{
			Projects = new ObservableCollection<ProjectViewModel>();
		}

		public void Initialize()
		{
			PropertyChanged += OnPropertyChanged; // TODO : Unsubscribe
		}

		public void AddProject(string title)
		{
			var project = IoC.GetInstance<ProjectViewModel>();
			project.Initialize();
			project.Title = title;

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
