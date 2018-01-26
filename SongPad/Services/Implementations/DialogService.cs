using SongPad.Messages;
using SongPad.Tools;
using SongPad.ViewModels;
using SongPad.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SongPad.Services
{
	public class DialogService : IDialogService
	{
		// It may be better in the long run to pass the ViewModel as a parameter in order to avoid coupling with the IoC
		public IDialogResult ShowDialog<T>() where T : DialogViewModelBase
		{
			var viewModel = IoC.GetInstance<T>();
			var view = new DialogViewBase();

			view.DataContext = viewModel;
			view.SubscribeEvents();
			view.ShowDialog();

			return viewModel.Result;
		}
	}
}
