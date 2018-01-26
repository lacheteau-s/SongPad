using SongPad.Services;
using SongPad.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SongPad.ViewModels
{
	public class MainWindowViewModel : INotifyPropertyChanged
	{
		private IDialogService _dialogService;

		public MenuViewModel MenuViewModel { get; private set; }

		private ProjectViewModel _projectViewModel;

		public ProjectViewModel ProjectViewModel
		{
			get
			{
				return _projectViewModel;
			}
			private set
			{
				_projectViewModel = value;
				RaisePropertyChanged(nameof(ProjectViewModel));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public MainWindowViewModel()
		{
			_dialogService = IoC.GetInstance<IDialogService>(); // TODO: ViewModel locator to allow passing the service in ctor

			MenuViewModel = IoC.GetInstance<MenuViewModel>();
		}

		public void Initialize()
		{
			// TODO: replace with messaging
			MenuViewModel.NewEventHandler += OnMenuNew;
			MenuViewModel.OpenEventHandler += OnMenuOpen;
			MenuViewModel.SaveEventHandler += OnMenuSave;
			MenuViewModel.QuitEventHandler += OnMenuQuit;
		}

		public void Shutdown()
		{
			MenuViewModel.NewEventHandler -= OnMenuNew;
			MenuViewModel.OpenEventHandler -= OnMenuOpen;
			MenuViewModel.SaveEventHandler -= OnMenuSave;
			MenuViewModel.QuitEventHandler -= OnMenuQuit;
		}

		private void OnMenuNew(object sender, EventArgs e)
		{
			_dialogService.ShowDialog<NewProjectDialogViewModel>();

			// Check if another project is currently opened with unsaved changes
			ProjectViewModel = IoC.GetInstance<ProjectViewModel>();
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

		protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
