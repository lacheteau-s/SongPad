using Microsoft.Win32;
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
	public enum DialogButton
	{
		OK = 0,
		OKCancel = 1,
		YesNoCancel = 3,
		YesNo = 4
	};

	public enum DialogImage
	{
		Error = 16,
		Question = 32,
		Warning = 48,
		Information = 64
	};

	public enum DialogResult
	{
		None = 0,
		OK = 1,
		Cancel = 2,
		Yes = 6,
		No = 7
	};

	public class DialogService : IDialogService
	{
		public DialogResult ShowDialog(string message, string title, DialogButton buttons, DialogImage image)
		{
			return (DialogResult)MessageBox.Show(message, title, (MessageBoxButton)buttons, (MessageBoxImage)image);
		}

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

		public IDialogResult SaveFileDialog(string filter, string initialDirectory)
		{
			var dialog = new SaveFileDialog();

			dialog.Filter = filter;
			dialog.InitialDirectory = initialDirectory;

			var result = dialog.ShowDialog();

			return new FileDialogResult(result.Value, dialog.FileName);
		}

		public IDialogResult OpenFileDialog(string filter, string initialDirectory)
		{
			var dialog = new OpenFileDialog();

			dialog.Multiselect = true;
			dialog.Filter = filter;
			dialog.InitialDirectory = initialDirectory;

			var result = dialog.ShowDialog();

			return new FileDialogResult(result.Value, dialog.FileName);
		}

		public void ShowErrorDialog(string error)
		{
			MessageBox.Show($"An error has occured:{Environment.NewLine}{error}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}
}
