﻿using SongPad.ViewModels;
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
using System.ComponentModel;

namespace SongPad.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

		public MainWindow()
		{
			InitializeComponent();
		}

		protected override void OnInitialized(EventArgs e)
		{
			base.OnInitialized(e);

			ViewModel.Initialize();
			ViewModel.CloseWindow += OnClose;
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);

			ViewModel.CloseWindow -= OnClose;
			ViewModel.Shutdown();
		}

		private void OnClose(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
