using SongPad.Messages;
using SongPad.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongPad.Services
{
	public interface IDialogService
	{
		DialogResult ShowDialog(string message, string title, DialogButton buttons, DialogImage image);

		IDialogResult ShowDialog<T>() where T : DialogViewModelBase;

		IDialogResult ShowSaveFileDialog(string filter, string initialDirectory);

		void ShowErrorDialog(string error);
	}
}
