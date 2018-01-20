using SimpleInjector;
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
			var container = Bootstrap();

			RunApplication(container);
		}

		private static Container Bootstrap()
		{
			var container = new Container();

			container.Register<MainWindow>();
			container.Register<MainWindowViewModel>();

			container.Verify();

			return container;
		}

		private static void RunApplication(Container container)
		{
			try
			{
				var app = new App();
				var window = container.GetInstance<MainWindow>();

				app.Run(window);
			}
			catch (Exception)
			{

			}
		}
	}
}
