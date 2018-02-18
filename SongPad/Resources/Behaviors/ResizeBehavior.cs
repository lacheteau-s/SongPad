using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace SongPad.Resources.Behaviors
{
    public static class ResizeBehavior
    {
		public static readonly DependencyProperty LeftResize = DependencyProperty.RegisterAttached("LeftResize", typeof(Window), typeof(ResizeBehavior), new UIPropertyMetadata(null, OnLeftResizeChanged));
		public static readonly DependencyProperty TopLeftResize = DependencyProperty.RegisterAttached("TopLeftResize", typeof(Window), typeof(ResizeBehavior), new UIPropertyMetadata(null, OnTopLeftResizeChanged));
		public static readonly DependencyProperty TopResize = DependencyProperty.RegisterAttached("TopResize", typeof(Window), typeof(ResizeBehavior), new UIPropertyMetadata(null, OnTopResizeChanged));
		public static readonly DependencyProperty TopRightResize = DependencyProperty.RegisterAttached("TopRightResize", typeof(Window), typeof(ResizeBehavior), new UIPropertyMetadata(null, OnTopRightResizeChanged));
		public static readonly DependencyProperty RightResize = DependencyProperty.RegisterAttached("RightResize", typeof(Window), typeof(ResizeBehavior), new UIPropertyMetadata(null, OnRightResizeChanged));
		public static readonly DependencyProperty BottomRightResize = DependencyProperty.RegisterAttached("BottomRightResize", typeof(Window), typeof(ResizeBehavior), new UIPropertyMetadata(null, OnBottomRightResizeChanged));
		public static readonly DependencyProperty BottomResize = DependencyProperty.RegisterAttached("BottomResize", typeof(Window), typeof(ResizeBehavior), new UIPropertyMetadata(null, OnBottomResizeChanged));
		public static readonly DependencyProperty BottomLeftResize = DependencyProperty.RegisterAttached("BottomLeftResize", typeof(Window), typeof(ResizeBehavior), new UIPropertyMetadata(null, OnBottomLeftResizeChanged));

		#region GET

		public static Window GetLeftResize(DependencyObject obj)
		{
			return (Window)obj.GetValue(LeftResize);
		}

		public static Window GetTopLeftResize(DependencyObject obj)
		{
			return (Window)obj.GetValue(TopLeftResize);
		}

		public static Window GetTopResize(DependencyObject obj)
		{
			return (Window)obj.GetValue(TopResize);
		}

		public static Window GetTopRightResize(DependencyObject obj)
		{
			return (Window)obj.GetValue(TopRightResize);
		}

		public static Window GetRightResize(DependencyObject obj)
		{
			return (Window)obj.GetValue(RightResize);
		}

		public static Window GetBottomRightResize(DependencyObject obj)
		{
			return (Window)obj.GetValue(BottomRightResize);
		}

		public static Window GetBottomResize(DependencyObject obj)
		{
			return (Window)obj.GetValue(BottomResize);
		}

		public static Window GetBottomLeftResize(DependencyObject obj)
		{
			return (Window)obj.GetValue(BottomLeftResize);
		}

		#endregion

		#region SET

		public static void SetLeftResize(DependencyObject obj, Window window)
		{
			obj.SetValue(LeftResize, window);
		}

		public static void SetTopLeftResize(DependencyObject obj, Window window)
		{
			obj.SetValue(TopLeftResize, window);
		}

		public static void SetTopResize(DependencyObject obj, Window window)
		{
			obj.SetValue(TopResize, window);
		}

		public static void SetTopRightResize(DependencyObject obj, Window window)
		{
			obj.SetValue(TopRightResize, window);
		}

		public static void SetRightResize(DependencyObject obj, Window window)
		{
			obj.SetValue(RightResize, window);
		}

		public static void SetBottomRightResize(DependencyObject obj, Window window)
		{
			obj.SetValue(BottomRightResize, window);
		}

		public static void SetBottomResize(DependencyObject obj, Window window)
		{
			obj.SetValue(BottomResize, window);
		}

		public static void SetBottomLeftResize(DependencyObject obj, Window window)
		{
			obj.SetValue(BottomLeftResize, window);
		}

		#endregion

		#region RESIZE CHANGED

		private static void OnLeftResizeChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			var thumb = sender as Thumb;

			if (thumb != null)
				thumb.DragDelta += DragLeft;
		}

		private static void OnTopLeftResizeChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			var thumb = sender as Thumb;

			if (thumb != null)
				thumb.DragDelta += DragTopLeft;
		}

		private static void OnTopResizeChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			var thumb = sender as Thumb;

			if (thumb != null)
				thumb.DragDelta += DragTop;
		}

		private static void OnTopRightResizeChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			var thumb = sender as Thumb;

			if (thumb != null)
				thumb.DragDelta += DragTopRight;
		}

		private static void OnRightResizeChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			var thumb = sender as Thumb;

			if (thumb != null)
				thumb.DragDelta += DragRight;
		}

		private static void OnBottomRightResizeChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			var thumb = sender as Thumb;

			if (thumb != null)
				thumb.DragDelta += DragBottomRight;
		}

		private static void OnBottomResizeChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			var thumb = sender as Thumb;

			if (thumb != null)
				thumb.DragDelta += DragBottom;
		}

		private static void OnBottomLeftResizeChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			var thumb = sender as Thumb;

			if (thumb != null)
				thumb.DragDelta += DragBottomLeft;
		}

		#endregion

		#region DRAG

		private static void DragLeft(object sender, DragDeltaEventArgs e)
		{
			var window = ((Thumb)sender).GetValue(LeftResize) as Window;

			if (window != null)
			{
				var horizontalChange = window.SafeWidthChange(e.HorizontalChange, false);

				window.Width -= horizontalChange;
				window.Left += horizontalChange;
			}
		}

		private static void DragTop(object sender, DragDeltaEventArgs e)
		{
			var window = ((Thumb)sender).GetValue(TopResize) as Window;

			if (window != null)
			{
				var verticalChange = window.SafeHeightChange(e.VerticalChange, false);

				window.Height -= verticalChange;
				window.Top += verticalChange;
			}
		}

		private static void DragRight(object sender, DragDeltaEventArgs e)
		{
			var window = ((Thumb)sender).GetValue(RightResize) as Window;

			if (window != null)
				window.Width += window.SafeWidthChange(e.HorizontalChange, false);
		}

		private static void DragBottom(object sender, DragDeltaEventArgs e)
		{
			var window = ((Thumb)sender).GetValue(BottomResize) as Window;

			if (window != null)
				window.Height += window.SafeHeightChange(e.VerticalChange, false);
		}

		private static void DragTopLeft(object sender, DragDeltaEventArgs e)
		{
			var window = ((Thumb)sender).GetValue(TopLeftResize) as Window;

			if (window != null)
			{
				var horizontalChange = window.SafeWidthChange(e.HorizontalChange, false);
				var verticalChange = window.SafeHeightChange(e.VerticalChange, false);

				window.Width -= horizontalChange;
				window.Left += horizontalChange;
				window.Height -= verticalChange;
				window.Top += verticalChange;
			}
		}

		private static void DragTopRight(object sender, DragDeltaEventArgs e)
		{
			var window = ((Thumb)sender).GetValue(TopRightResize) as Window;

			if (window != null)
			{
				var verticalChange = window.SafeHeightChange(e.VerticalChange, false);

				window.Width += window.SafeWidthChange(e.HorizontalChange, false);
				window.Height -= verticalChange;
				window.Top += verticalChange;
			}
		}

		private static void DragBottomRight(object sender, DragDeltaEventArgs e)
		{
			var window = ((Thumb)sender).GetValue(BottomRightResize) as Window;

			if (window != null)
			{
				window.Width += window.SafeWidthChange(e.HorizontalChange, false);
				window.Height += window.SafeHeightChange(e.VerticalChange, false);
			}
		}

		private static void DragBottomLeft(object sender, DragDeltaEventArgs e)
		{
			var window = ((Thumb)sender).GetValue(BottomLeftResize) as Window;

			if (window != null)
			{
				var horizontalChange = window.SafeWidthChange(e.HorizontalChange, false);

				window.Width -= horizontalChange;
				window.Left += horizontalChange;
				window.Top += window.SafeHeightChange(e.VerticalChange, false);
			}
		}

		#endregion

		private static double SafeWidthChange(this Window window, double change, bool positive = true)
		{
			var result = positive ? (window.Width + change) : (window.Width - change);
			var ret = 0.0;

			if (result <= window.MinWidth)
				ret = 0;
			else if (result >= window.MaxWidth)
				ret = 0;
			else if (result < 0)
				ret = 0;

			ret = change;
			System.Diagnostics.Debug.WriteLine($"Horizontal ret = {ret}");
			return ret;
		}

		private static double SafeHeightChange(this Window window, double change, bool positive = true)
		{
			var result = positive ? (window.Height + change) : (window.Height - change);
			var ret = 0.0;

			if (result <= window.MinHeight)
				ret = 0;
			else if (result >= window.MaxHeight)
				ret = 0;
			else if (result < 0)
				ret = 0;

			ret = change;
			System.Diagnostics.Debug.WriteLine($"Vertical ret = {ret}");
			return ret;
		}
	}
}
