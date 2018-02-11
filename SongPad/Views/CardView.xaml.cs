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
	}
}
