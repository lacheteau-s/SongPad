﻿using SongPad.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongPad.ViewModels
{
	public class MainWindowViewModel
	{
		public MenuViewModel MenuViewModel => IoC.GetInstance<MenuViewModel>();
	}
}
