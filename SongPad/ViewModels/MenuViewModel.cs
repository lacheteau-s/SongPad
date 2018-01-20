using SongPad.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SongPad.ViewModels
{
	public class MenuViewModel
	{
		public ICommand NewCommand => new Command(OnNew);
		public ICommand OpenCommand => new Command(OnOpen);
		public ICommand SaveCommand => new Command(OnSave);
		public ICommand QuitCommand => new Command(OnQuit);

		public void OnNew()
		{

		}

		public void OnOpen()
		{

		}

		public void OnSave()
		{

		}

		public void OnQuit()
		{

		}
	}
}
