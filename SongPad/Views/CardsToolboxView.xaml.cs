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
	/// Interaction logic for CardsToolboxView.xaml
	/// </summary>
	public partial class CardsToolboxView : UserControl
	{
		private CardsToolboxViewModel viewModel => (CardsToolboxViewModel)DataContext;

		public CardsToolboxView()
		{
			InitializeComponent();
		}

		private void OnListBoxKeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Delete)
				viewModel.RemoveSelectedCards();
		}

		private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			viewModel.UpdateSelection(e.AddedItems, e.RemovedItems);
		}
	}
}
