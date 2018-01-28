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
	/// Interaction logic for ProjectView.xaml
	/// </summary>
	public partial class ProjectView : UserControl
	{
		private Rectangle _card;

		private Point _offset;

		public ProjectView()
		{
			InitializeComponent();
		}

		private void OnCardMouseDown(object sender, MouseButtonEventArgs e)
		{
			var pos = e.GetPosition(canvas);

			_card = ((Rectangle)sender);
			// Get the offset between the mouse and the card's top-left corner to prevent the card from "jumping" to the cursor's position
			_offset = new Point(pos.X - _card.Margin.Left, pos.Y - _card.Margin.Top);
		}

		private void OnCanvasMouseUp(object sender, MouseButtonEventArgs e)
		{
			_card = null;
		}

		private void OnCanvasMouseLeave(object sender, MouseEventArgs e)
		{
			_card = null;
		}

		private void OnCanvasMouseMove(object sender, MouseEventArgs e)
		{
			if (_card != null && e.LeftButton == MouseButtonState.Pressed)
				Move(e);
		}

		private void Move(MouseEventArgs e)
		{
			var pos = e.GetPosition(canvas);
			var left = pos.X - _offset.X;
			var top = pos.Y - _offset.Y;
			var right = canvas.ActualWidth - (left + _card.ActualWidth);
			var bottom = canvas.ActualHeight - (top + _card.ActualHeight);

			// Check if we hit any border
			if (left < 0)
			{
				right += left;
				left = 0;
			}
			else if (right < 0)
			{
				left += right;
				right = 0;
			}

			if (top < 0)
			{
				bottom += top;
				top = 0;
			}
			else if (bottom < 0)
			{
				top += bottom;
				bottom = 0;
			}

			_card.Margin = new Thickness(left, top, right, bottom);
		}
	}
}
