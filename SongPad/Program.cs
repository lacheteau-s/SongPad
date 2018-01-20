using SimpleInjector;
using SongPad.Tools;
using SongPad.ViewModels;
using SongPad.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongPad
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			RegisterIoC();
			RunApplication();
		}

		private static void RegisterIoC()
		{
			IoC.Register<MainWindow>();
			IoC.Register<MainWindowViewModel>();
		}

		private static void RunApplication()
		{
			try
			{
				var app = new App();
				var window = IoC.GetInstance<MainWindow>();

				app.Run(window);
			}
			catch (Exception)
			{

			}
		}
	}
}
