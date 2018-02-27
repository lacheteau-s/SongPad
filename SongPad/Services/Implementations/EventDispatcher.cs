using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongPad.Services
{
	public class EventDispatcher : IEventDispatcher
	{
		private struct Subscriber
		{
			public object Action { get; set; }

			public WeakReference<object> Target { get; set; }
		}

		private Dictionary<Type, List<Subscriber>> _subscribers = new Dictionary<Type, List<Subscriber>>();

		public void Invoke<TMessage>(TMessage message)
		{
			var type = typeof(TMessage);

			if (_subscribers.ContainsKey(type))
			{
				var subscribers = _subscribers[type].ToList();

				foreach (var subscriber in subscribers)
				{
					var action = (Action<TMessage>)subscriber.Action;

					action.Invoke(message);
				}
			}
		}

		public void Subscribe<TMessage>(object recipient, Action<TMessage> action)
		{
			var type = typeof(TMessage);

			if (!_subscribers.ContainsKey(type))
				_subscribers.Add(type, new List<Subscriber>());

			if (!_subscribers[type].Any(m => m.Target == recipient))
				_subscribers[type].Add(new Subscriber
				{
					Action = action,
					Target = new WeakReference<object>(recipient)
				});
		}

		public void Unsubscribe<TMessage>(object recipient)
		{
			var type = typeof(TMessage);

			if (_subscribers.ContainsKey(type))
			{
				var receivers = _subscribers[type];
				var toRemove = receivers.Where(m => m.Target.TryGetTarget(out object obj) ? (obj == recipient) : false);

				foreach (var obj in toRemove)
					receivers.Remove(obj);
			}
		}
	}
}
