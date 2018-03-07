using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SongPad
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
		public App()
		{
			InitializeComponent();
			ApplySkin();
		}

		// https://www.codeproject.com/Articles/19782/Creating-a-Skinned-User-Interface-in-WPF
		public void ApplySkin()
		{
			var skin = LoadComponent(new Uri(@".\Resources\Skins\DarkSkin.xaml", UriKind.Relative)) as ResourceDictionary;
			Resources.MergedDictionaries.Add(skin);
		}
    }
}
