using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongPad.Tools
{
	public static class IoC
	{
		private static readonly object _lock = new object();
		private static volatile Container _container;

		private static Container IoCContainer
		{
			get
			{
				if (_container != null)
					return _container;

				lock (_lock)
				{
					if (_container == null)
					{
						_container = new Container();
						_container.ResolveUnregisteredType += (sender, e) => throw new ArgumentException($"{e.UnregisteredServiceType} is not registered in the IoC container");
					}
				}

				return _container;

			}
		}

		public static void Register<T>() where T : class
		{
			IoCContainer.Register<T>();
		}

		public static void Register<TInterface, TImplementation>() where TInterface : class where TImplementation : class, TInterface
		{
			IoCContainer.Register<TInterface, TImplementation>();
		}

		public static void RegisterSingleton<T>() where T : class
		{
			IoCContainer.RegisterSingleton<T>();
		}

		public static void RegisterSingleton<TInterface, TImplementation>() where TInterface : class where TImplementation : class, TInterface
		{
			IoCContainer.RegisterSingleton<TInterface, TImplementation>();
		}

		public static T GetInstance<T>(bool throwIfUnregistered = true) where T : class
		{
			if (throwIfUnregistered)
				return IoCContainer.GetInstance<T>();

			return IsRegistered<T>() ? IoCContainer.GetInstance<T>() : null;
		}

		public static bool IsRegistered<T>() where T : class
		{
			return IoCContainer.GetCurrentRegistrations().Count(p => p.ServiceType == typeof(T)) > 0;
		}
	}
}
