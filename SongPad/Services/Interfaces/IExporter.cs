﻿using SongPad.DTO;
using SongPad.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SongPad.Services
{
	public interface IExporter
	{
		void Export(string path, ProjectDTO project);
	}
}
