using SongPad.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongPad.Services
{
    public interface IEventDispatcher
    {
		void Invoke<TEvent>(TEvent evt = null) where TEvent : class;

		void Subscribe<TEvent>(object recipient, Action<TEvent> action) where TEvent : class;

		void Unsubscribe<TEvent>(object recipient) where TEvent : class;
    }
}
