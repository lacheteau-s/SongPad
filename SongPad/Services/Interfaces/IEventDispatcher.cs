using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongPad.Services
{
    public interface IEventDispatcher
    {
		void Subscribe<TEvent>(object recipient, Action<TEvent> action);

		void Invoke<TEvent>(TEvent evt);

		void Unsubscribe<TEvent>(object recipient);
    }
}
