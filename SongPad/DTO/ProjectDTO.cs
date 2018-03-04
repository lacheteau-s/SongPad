using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SongPad.DTO
{
	public class ProjectDTO
	{
		public string Title;

		public CardDTO[] Cards;
	}
}
