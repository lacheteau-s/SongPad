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
		void ShowDialog<T>() where T : DialogViewModelBase;
	}
}
