using SongPad.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SongPad.Views
{
	/// <summary>
	/// Interaction logic for CardView.xaml
	/// </summary>
	public partial class CardView : UserControl
	{
		public CardViewModel ViewModel => (CardViewModel)DataContext;

		public CardView()
		{
			InitializeComponent();
		}

		// TODO : Commands
		private void OnHeaderDoubleClick(object sender, MouseButtonEventArgs e)
		{
			ViewModel.IsEditingTitle = true;
			HeaderTextBox.Focus();
		}

		private void OnHeaderKeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
				ViewModel.IsEditingTitle = false;
		}

		private void OnListBoxKeyUp(object sender, KeyEventArgs e)
		{
			var dict = new Dictionary<Key, Action<LineViewModel[]>>
			{
				{ Key.Delete, ViewModel.RemoveLines },
				{ Key.Up, ViewModel.MoveLinesUp },
				{ Key.Down, ViewModel.MoveLinesDown }
			};

			if (dict.Keys.Contains(e.Key))
			{
				var selection = listBox.SelectedItems.Cast<LineViewModel>().ToArray();

				dict[e.Key](selection);

				// TODO : remove
				// This prevents the ListBox from being focused when the top/bottom has been reached
				// It should be replaced with a ListBox override or a customized ItemsControl with a custom selection behavior
				if (e.Key == Key.Up || e.Key == Key.Down)
				{
					var item = (ListBoxItem)listBox.ItemContainerGenerator.ContainerFromIndex(0);
					item.Focus();
				}
			}
		}
	}
}
