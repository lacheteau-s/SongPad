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
		private string _defaultProjectName => "Untitled";

		public string ProjectName { get; set; }

		public ICommand OkCommand => new Command(OnOk);

		public NewProjectDialogViewModel()
		{
			Title = "New project";
			ProjectName = _defaultProjectName;
		}

		private void OnOk()
		{
			if (string.IsNullOrWhiteSpace(ProjectName))
				ProjectName = _defaultProjectName;

			Result = new NewProjectDialogResult(ProjectName);
			RaiseClose();
		}
	}
}
