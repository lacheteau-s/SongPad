using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongPad.Services
{
    public interface IEventDispatcher
    {
		void Subscribe<TMessage>(object recipient, Action<TMessage> action);

		void Invoke<TMessage>(TMessage message);

		void Unsubscribe<TMessage>(object recipient);
    }
}
