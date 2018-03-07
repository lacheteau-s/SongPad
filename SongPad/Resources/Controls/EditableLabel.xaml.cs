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

namespace SongPad.Resources.Controls
{
    /// <summary>
    /// Interaction logic for EditableLabel.xaml
    /// </summary>
    public partial class EditableLabel : UserControl
    {
		public static readonly DependencyProperty IsEditingDependencyProperty = DependencyProperty.Register("IsEditing", typeof(bool), typeof(EditableLabel));
		public static readonly DependencyProperty TextDependencyProperty = DependencyProperty.Register("Text", typeof(string), typeof(EditableLabel));

		public bool IsEditing
		{
			get { return (bool)GetValue(IsEditingDependencyProperty); }
			set { SetValue(IsEditingDependencyProperty, value); }
		}

		public string Text
		{
			get { return (string)GetValue(TextDependencyProperty); }
			set { SetValue(TextDependencyProperty, value); }
		}

        public EditableLabel()
        {
            InitializeComponent();
			IsEditing = false;
        }

		private void OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (IsEditing)
				return;

			IsEditing = true;
			// TODO : handle visibility in XAML ?
			textBox.Visibility = Visibility.Visible;
			textBlock.Visibility = Visibility.Collapsed;
			textBox.Focus();
		}

		private void OnKeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				IsEditing = false;
				// TODO : handle visibility in XAML ?
				textBlock.Visibility = Visibility.Visible;
				textBox.Visibility = Visibility.Collapsed;
			}
		}

		private void OnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
				e.Handled = true;
		}
    }
}
