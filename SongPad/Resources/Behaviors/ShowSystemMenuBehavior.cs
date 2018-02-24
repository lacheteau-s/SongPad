using SongPad.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace SongPad.Resources.Behaviors
{
    public class ShowSystemMenuBehavior
    {
		static bool showMenu = false;

		public static readonly DependencyProperty TargetWindow = DependencyProperty.RegisterAttached("TargetWindow", typeof(Window), typeof(ShowSystemMenuBehavior));
		public static readonly DependencyProperty LeftButtonShowAt = DependencyProperty.RegisterAttached("LeftButtonShowAt", typeof(UIElement), typeof(ShowSystemMenuBehavior), new UIPropertyMetadata(null, LeftButtonShowAtChanged));
		public static readonly DependencyProperty RightButtonShow = DependencyProperty.RegisterAttached("RightButtonShow", typeof(bool), typeof(ShowSystemMenuBehavior), new UIPropertyMetadata(false, RightButtonShowChanged));

		public static Window GetTargetWindow(DependencyObject obj)
		{
			return (Window)obj.GetValue(TargetWindow);
		}

		public static void SetTargetWindow(DependencyObject obj, Window window)
		{
			obj.SetValue(TargetWindow, window);
		}

		public static UIElement GetLeftButtonShowAt(DependencyObject obj)
		{
			return (UIElement)obj.GetValue(LeftButtonShowAt);
		}

		public static void SetLeftButtonShowAt(DependencyObject obj, UIElement element)
		{
			obj.SetValue(LeftButtonShowAt, element);
		}

		public static bool GetRightButtonShow(DependencyObject obj)
		{
			return (bool)obj.GetValue(RightButtonShow);
		}

		public static void SetRightButtonShow(DependencyObject obj, bool arg)
		{
			obj.SetValue(RightButtonShow, arg);
		}

		static void LeftButtonShowAtChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			var element = sender as UIElement;

			if (element != null)
				element.MouseLeftButtonDown += LeftButtonDownShow;
		}

		private static void RightButtonShowChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			var element = sender as UIElement;

			if (element != null)
				element.MouseRightButtonDown += RightButtonDownShow;
		}

		static void LeftButtonDownShow(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (e.ClickCount > 1)
				return;

			if (!showMenu)
			{
				var element = ((UIElement)sender).GetValue(LeftButtonShowAt);
				var showMenuAt = ((Visual)element).PointToScreen(new Point(0, 0));
				var targetWindow = ((UIElement)sender).GetValue(TargetWindow) as Window;

				SystemMenuManager.ShowMenu(targetWindow, showMenuAt);
			}

			showMenu = !showMenu;
		}

		static void RightButtonDownShow(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			var element = (UIElement)sender;
			var targetWindow = element.GetValue(TargetWindow) as Window;
			var showMenuAt = targetWindow.PointToScreen(Mouse.GetPosition((targetWindow)));

			SystemMenuManager.ShowMenu(targetWindow, showMenuAt);
		}
	}
}
