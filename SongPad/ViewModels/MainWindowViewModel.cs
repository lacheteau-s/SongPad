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
using System.Windows.Input;

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

		public ActionMenuViewModel ActionMenuViewModel { get; private set; }

		public ToolBarViewModel ToolBarViewModel { get; private set; }

		public WorkspaceViewModel WorkspaceViewModel { get; private set; }

		public ICommand CloseCommand => new Command(Close);

		public event EventHandler CloseWindow;

		public MainWindowViewModel()
		{
			_dialogService = IoC.GetInstance<IDialogService>(); // TODO: ViewModel locator to allow passing the service in ctor

			MenuViewModel = IoC.GetInstance<MenuViewModel>();
			ActionMenuViewModel = IoC.GetInstance<ActionMenuViewModel>();
			ToolBarViewModel = IoC.GetInstance<ToolBarViewModel>();
			WorkspaceViewModel = IoC.GetInstance<WorkspaceViewModel>();
		}

		public void Initialize()
		{
			WorkspaceViewModel.Initialize();

			// TODO: replace with messaging
			MenuViewModel.QuitEventHandler += OnMenuQuit;

			WorkspaceViewModel.SelectionChanged += OnProjectSelectionChanged;
		}

		public void Shutdown()
		{
			// TODO : dispose subviewmodels
			MenuViewModel.QuitEventHandler -= OnMenuQuit;

			WorkspaceViewModel.SelectionChanged -= OnProjectSelectionChanged;
		}

		private void OnMenuQuit(object sender, EventArgs e)
		{
			Close();
		}

		private void OnProjectSelectionChanged(object sender, EventArgs e)
		{
			RaisePropertyChanged(nameof(Title));
		}

		private void Close()
		{
			CloseWindow?.Invoke(this, EventArgs.Empty);
		}
	}
}
