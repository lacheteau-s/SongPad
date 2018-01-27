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
		public ObservableCollection<ProjectViewModel> Projects { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;

		public WorkspaceViewModel()
		{
			Projects = new ObservableCollection<ProjectViewModel>();
			Projects.Add(new ProjectViewModel { Title = "Toast" });
		}

		private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
