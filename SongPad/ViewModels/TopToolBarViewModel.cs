using SongPad.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SongPad.ViewModels
{
	public class TopToolBarViewModel : BaseViewModel
	{
		public ICommand NewProjectCommand => new Command(OnNewProject);
		public ICommand OpenProjectCommand => new Command(OnOpenProject);
		public ICommand SaveProjectCommand => new Command(OnSaveProject);
		public ICommand SaveAllCommand => new Command(OnSaveAllProject);
		public ICommand ExportCommand => new Command(OnExport);

		private void OnNewProject()
		{

		}

		private void OnOpenProject()
		{

		}

		private void OnSaveProject()
		{

		}

		private void OnSaveAllProject()
		{

		}

		private void OnExport()
		{

		}
	}
}
