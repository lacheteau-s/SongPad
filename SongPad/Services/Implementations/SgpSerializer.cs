﻿using SongPad.DTO;
using SongPad.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SongPad.Services
{
	public class SgpSerializer : ISerializer
	{
		public void Export(string path, ProjectDTO project)
		{
			var backup = BackupIfExists(path); // In case of error: first, make a duplicate of the file

			File.WriteAllText(path, string.Empty);

			using (var stream = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
			{
				var serializer = new XmlSerializer(typeof(ProjectDTO));

				serializer.Serialize(stream, project);
			}

			if (!string.IsNullOrWhiteSpace(backup))
				File.Delete(backup); // TODO : In case of error: rewrite old file
		}

		public ProjectDTO Import(string path)
		{
			ProjectDTO project = null;

			using (var stream = File.OpenRead(path))
			{
				var serializer = new XmlSerializer(typeof(ProjectDTO));
				project = (ProjectDTO)serializer.Deserialize(stream);
			}

			return project;
		}

		private string BackupIfExists(string path)
		{
			var backup = string.Empty;

			if (File.Exists(path))
			{
				backup = Path.Combine(Path.GetDirectoryName(path), $".{Path.GetFileNameWithoutExtension(path)}_TMP{Path.GetExtension(path)}");
				File.Copy(path, backup);
			}

			return backup;
		}
	}
}
