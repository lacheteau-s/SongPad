using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SongPad.Tools
{
	public class ObservableObject : INotifyPropertyChanged
	{
		private Dictionary<string, List<string>> _dependencies = new Dictionary<string, List<string>>();

		public event PropertyChangedEventHandler PropertyChanged;

		protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

			if (_dependencies.ContainsKey(propertyName))
				foreach (var dependency in _dependencies[propertyName])
					PropertyChanged.Invoke(this, new PropertyChangedEventArgs(dependency));
		}

		protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
		{
			if (Equals(field, value))
				return false;

			field = value;
			RaisePropertyChanged(propertyName);
			return true;
		}

		protected virtual void RegisterDependency(string sourceProperty, string dependentProperty)
		{
			if (!_dependencies.ContainsKey(sourceProperty))
				_dependencies.Add(sourceProperty, new List<string>());

			_dependencies[sourceProperty].Add(dependentProperty);
		}
	}
}
