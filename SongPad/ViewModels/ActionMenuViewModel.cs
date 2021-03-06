﻿using SongPad.Messages;
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
	public class ActionMenuViewModel : BaseViewModel
	{
		private IEventDispatcher _eventDispatcher;

		public ICommand NewProjectCommand => new Command(OnNewProject);
		public ICommand OpenProjectCommand => new Command(OnOpenProject);
		public ICommand SaveProjectCommand => new Command(OnSaveProject);
		public ICommand ExportCommand => new Command(OnExport);

		public ActionMenuViewModel(IEventDispatcher eventDispatcher)
		{
			_eventDispatcher = eventDispatcher;
		}

		private void OnNewProject()
		{
			_eventDispatcher.Invoke(new ProjectEvent(ProjectEvent.InstructionType.New));
		}

		private void OnOpenProject()
		{
			_eventDispatcher.Invoke(new ProjectEvent(ProjectEvent.InstructionType.Open));
		}

		private void OnSaveProject()
		{
			_eventDispatcher.Invoke(new ProjectEvent(ProjectEvent.InstructionType.Save));
		}

		private void OnExport()
		{

		}
	}
}
