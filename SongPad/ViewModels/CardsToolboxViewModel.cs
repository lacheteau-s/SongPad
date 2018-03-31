using SongPad.Messages;
using SongPad.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongPad.ViewModels
{
	public class CardsToolboxViewModel : BaseViewModel
	{
		private readonly IEventDispatcher _eventDispatcher;

		public ObservableCollection<CardViewModel> Cards { get; set; } // TODO: cleanup

		public bool HasItems => Cards?.Count > 0;

		public CardsToolboxViewModel(IEventDispatcher eventDispatcher)
		{
			_eventDispatcher = eventDispatcher;
		}

		protected override void Subscribe()
		{
			base.Subscribe();

			_eventDispatcher.Subscribe<ProjectLoadedEvent>(this, OnProjectLoaded);
		}

		protected override void Unsubscribe()
		{
			base.Unsubscribe();

			_eventDispatcher.Unsubscribe<ProjectLoadedEvent>(this);
		}

		private void OnProjectLoaded(ProjectLoadedEvent evt)
		{
			Cards = (ObservableCollection<CardViewModel>)evt.Cards;
			RaisePropertyChanged(nameof(Cards));
		}
	}
}
