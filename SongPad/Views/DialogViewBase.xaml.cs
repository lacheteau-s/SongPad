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
using System.Windows.Shapes;

namespace SongPad.Views
{
	/// <summary>
	/// Interaction logic for DialogViewBase.xaml
	/// </summary>
	public partial class DialogViewBase : Window
	{
		public DialogViewModelBase ViewModel => (DialogViewModelBase)DataContext;

		public DialogViewBase()
		{
			InitializeComponent();
		}

		public void SubscribeEvents()
		{
			ViewModel.Close += OnClose;
		}

		public void UnsubscribeEvents()
		{
			ViewModel.Close -= OnClose;
		}

		private void OnClose(object sender, EventArgs e)
		{
			UnsubscribeEvents();
			DialogResult = true;
		}
	}
}
