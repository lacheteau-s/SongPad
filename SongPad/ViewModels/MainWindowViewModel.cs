using SongPad.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongPad.ViewModels
{
	public class MainWindowViewModel
	{
		public MenuViewModel MenuViewModel { get; private set; }

		public MainWindowViewModel()
		{
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
			throw new NotImplementedException();
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
	}
}
