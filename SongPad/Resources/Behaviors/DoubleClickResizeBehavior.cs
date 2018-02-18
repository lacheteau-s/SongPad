using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SongPad.Resources.Behaviors
{
	public static class DoubleClickResizeBehavior
	{
		public static readonly DependencyProperty ExecuteCommand = DependencyProperty.RegisterAttached("ExecuteCommand", typeof(ICommand), typeof(DoubleClickResizeBehavior), new UIPropertyMetadata(null, OnExecuteCommandChanged));
		public static readonly DependencyProperty ExecuteCommandParameter = DependencyProperty.RegisterAttached("ExecuteCommandParameter", typeof(Window), typeof(DoubleClickResizeBehavior));

		public static ICommand GetExecuteCommand(DependencyObject obj)
		{
			return (ICommand)obj.GetValue(ExecuteCommand);
		}

		public static void SetExecuteCommand(DependencyObject obj, ICommand command)
		{
			obj.SetValue(ExecuteCommand, command);
		}

		public static Window GetExecuteCommandParameter(DependencyObject obj)
		{
			return (Window)obj.GetValue(ExecuteCommandParameter);
		}

		public static void SetExecuteCommandParameter(DependencyObject obj, ICommand command)
		{
			obj.SetValue(ExecuteCommandParameter, command);
		}

		private static void OnExecuteCommandChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			var control = sender as Control;

			if (control != null)
				control.MouseDoubleClick += OnMouseDoubleClick;
		}

		private static void OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var control = sender as Control;

			if (control != null)
			{
				var command = control.GetValue(ExecuteCommand) as ICommand;
				var commandParameter = control.GetValue(ExecuteCommandParameter);

				if (command.CanExecute(e))
					command.Execute(commandParameter);
			}
		}
	}
}
