using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SongPad.Resources.Behaviors
{
    public static class DragBehavior
    {
		public static readonly DependencyProperty LeftMouseButtonDrag = DependencyProperty.RegisterAttached("LeftMouseButtonDrag", typeof(Window), typeof(DragBehavior), new UIPropertyMetadata(null, OnLeftMouseButtonDragChanged));

		public static Window GetLeftMouseButtonDrag(DependencyObject obj)
		{
			return (Window)obj.GetValue(LeftMouseButtonDrag);
		}

		public static void SetLeftMouseButtonDrag(DependencyObject obj, Window window)
		{
			obj.SetValue(LeftMouseButtonDrag, window);
		}

		private static void OnLeftMouseButtonDragChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			var element = sender as UIElement;

			if (element != null)
				element.MouseMove += MouseMove;
		}

		private static void MouseMove(object sender, MouseEventArgs e)
		{
			var window = ((UIElement)sender).GetValue(LeftMouseButtonDrag) as Window;

			if (e.LeftButton == MouseButtonState.Pressed && window != null)
			{
				if (window.WindowState == WindowState.Maximized)
				{
					var pos = e.GetPosition(window);

					window.WindowState = WindowState.Normal;

					if (window.PointToScreen(pos).X < (SystemParameters.WorkArea.Width / 2))
						window.Left = 0;
					else
						window.Left = SystemParameters.WorkArea.Width - window.ActualWidth;

					window.Top = 0;
				}
				window.DragMove();
			}
		}
	}
}
