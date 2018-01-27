using SongPad.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SongPad.ViewModels
{
	public class WorkspaceViewModel : INotifyPropertyChanged
	{
		private ProjectViewModel _selectedProject;

		public ObservableCollection<ProjectViewModel> Projects { get; set; }

		public ProjectViewModel SelectedProject
		{
			get { return _selectedProject; }
			set
			{
				_selectedProject = value;
				RaisePropertyChanged(nameof(SelectedProject));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public WorkspaceViewModel()
		{
			Projects = new ObservableCollection<ProjectViewModel>();
		}

		public void AddProject(string title)
		{
			var project = IoC.GetInstance<ProjectViewModel>();
			project.Title = title;

			Projects.Add(project);
			SelectedProject = project;
		}

		private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
