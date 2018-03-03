using SongPad.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SongPad.ViewModels
{
	public abstract class BaseViewModel : ObservableObject
	{
		public virtual void Initialize()
		{
			Subscribe();
		}

		public virtual void Cleanup()
		{
			Unsubscribe();
		}

		protected virtual void Subscribe()
		{

		}

		protected virtual void Unsubscribe()
		{

		}
	}
}
