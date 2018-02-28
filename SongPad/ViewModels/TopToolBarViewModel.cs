using SongPad.Messages;
using SongPad.Services;
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
		private IEventDispatcher _eventDispatcher;

		public ICommand NewProjectCommand => new Command(OnNewProject);
		public ICommand OpenProjectCommand => new Command(OnOpenProject);
		public ICommand SaveProjectCommand => new Command(OnSaveProject);
		public ICommand SaveAllCommand => new Command(OnSaveAllProject);
		public ICommand ExportCommand => new Command(OnExport);

		public TopToolBarViewModel(IEventDispatcher eventDispatcher)
		{
			_eventDispatcher = eventDispatcher;
		}

		private void OnNewProject()
		{
			_eventDispatcher.Invoke(new ProjectEvent(ProjectEvent.InstructionType.New));
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
