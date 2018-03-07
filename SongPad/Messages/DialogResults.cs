using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongPad.Messages
{
	public class NewProjectDialogResult : IDialogResult
	{
		public string ProjectTitle { get; set; }

		public NewProjectDialogResult(string projectTitle)
		{
			ProjectTitle = projectTitle;
		}
	}

	public class FileDialogResult : IDialogResult
	{
		public bool IsOk { get; set; }

		public string FilePath { get; set; }

		public FileDialogResult(bool isOk, string filePath)
		{
			IsOk = isOk;
			FilePath = filePath;
		}
	}
}
