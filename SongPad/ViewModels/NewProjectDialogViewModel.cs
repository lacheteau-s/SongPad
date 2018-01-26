using SongPad.Messages;
using SongPad.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SongPad.ViewModels
{
	public class NewProjectDialogViewModel : DialogViewModelBase
	{
		public string ProjectName { get; set; }

		public ICommand OkCommand => new Command(OnOk);

		public NewProjectDialogViewModel()
		{
			Title = "New project";
		}

		private void OnOk()
		{
			Result = new NewProjectDialogResult(ProjectName);
			RaiseClose();
		}
	}
}
