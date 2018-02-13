using SongPad.Messages;
using SongPad.Services;
using SongPad.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SongPad.ViewModels
{
	public class MainWindowViewModel : BaseViewModel
	{
		private IDialogService _dialogService;

		public string Title
		{
			get
			{
				var title = "SongPad";

				if (WorkspaceViewModel.SelectedProject != null)
					title += $" - {WorkspaceViewModel.SelectedProject.Title}";
				return title;
			}
		}

		public MenuViewModel MenuViewModel { get; private set; }

		public WorkspaceViewModel WorkspaceViewModel { get; private set; }

		public MainWindowViewModel()
		{
			_dialogService = IoC.GetInstance<IDialogService>(); // TODO: ViewModel locator to allow passing the service in ctor

			MenuViewModel = IoC.GetInstance<MenuViewModel>();
			WorkspaceViewModel = IoC.GetInstance<WorkspaceViewModel>();
		}

		public void Initialize()
		{
			WorkspaceViewModel.Initialize();

			// TODO: replace with messaging
			MenuViewModel.NewEventHandler += OnMenuNew;
			MenuViewModel.OpenEventHandler += OnMenuOpen;
			MenuViewModel.SaveEventHandler += OnMenuSave;
			MenuViewModel.QuitEventHandler += OnMenuQuit;
			WorkspaceViewModel.SelectionChanged += OnProjectSelectionChanged;
		}

		public void Shutdown()
		{
			// TODO : dispose subviewmodels
			MenuViewModel.NewEventHandler -= OnMenuNew;
			MenuViewModel.OpenEventHandler -= OnMenuOpen;
			MenuViewModel.SaveEventHandler -= OnMenuSave;
			MenuViewModel.QuitEventHandler -= OnMenuQuit;
			WorkspaceViewModel.SelectionChanged -= OnProjectSelectionChanged;
		}

		private void OnMenuNew(object sender, EventArgs e)
		{
			var result = _dialogService.ShowDialog<NewProjectDialogViewModel>() as NewProjectDialogResult;

			if (result != null)
				WorkspaceViewModel.AddProject(result.ProjectTitle);
		}

		private void OnMenuOpen(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		private void OnMenuSave(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		private void OnMenuQuit(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		private void OnProjectSelectionChanged(object sender, EventArgs e)
		{
			RaisePropertyChanged(nameof(Title));
		}
	}
}
