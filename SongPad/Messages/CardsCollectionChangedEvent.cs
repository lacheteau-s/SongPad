using SongPad.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongPad.Messages
{
	public class CardsCollectionChangedEvent
	{
		public enum InstructionType
		{
			Added,
			Removed,
			Moved,
			Changed
		}

		public InstructionType Instruction { get; private set; }

		public IList Items { get; private set; }

		public CardsCollectionChangedEvent(NotifyCollectionChangedEventArgs e)
		{
			if (e.NewItems != null)
			{
				Instruction = InstructionType.Added;
				Items = e.NewItems;
			}
			else if (e.OldItems != null)
			{
				Instruction = InstructionType.Removed;
				Items = e.OldItems;
			}
		}
	}
}
