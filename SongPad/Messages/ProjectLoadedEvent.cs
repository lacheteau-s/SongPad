using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongPad.Messages
{
	public class ProjectLoadedEvent
	{
		public ICollection Cards { get; private set; }

		public ProjectLoadedEvent(ICollection cards)
		{
			Cards = cards;
		}
	}
}
