using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongPad.Messages
{
	public class ProjectEvent
	{
		public enum InstructionType
		{
			New,
			Open,
			Save
		}

		public ProjectEvent(InstructionType instruction)
		{
			Instruction = instruction;
		}

		public InstructionType Instruction { get; private set; }
	}
}
