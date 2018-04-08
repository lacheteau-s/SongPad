using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongPad.Tools
{
	public interface IActive
	{
		bool IsActive { get; }

		void SetActive(bool isActive);
	}
}
