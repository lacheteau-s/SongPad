using SongPad.Messages;
using SongPad.Services;
using SongPad.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SongPad.ViewModels
{
	public class CardsToolboxViewModel : BaseViewModel
	{
		private readonly IEventDispatcher _eventDispatcher;

		public ObservableCollection<CardViewModel> Cards { get; set; } // TODO: cleanup

		public bool HasItems => Cards?.Count > 0;

		public ICommand AddCardCommand => new Command(OnAddCard);

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

		public void RemoveCards(CardViewModel[] cards)
		{
			foreach (var card in cards)
				Cards.Remove(card); // Project handles the cleanup when receiving the CollectionChanged event
		}

		private void OnProjectLoaded(ProjectLoadedEvent evt)
		{
			Cards = (ObservableCollection<CardViewModel>)evt.Cards;
			RaisePropertyChanged(nameof(Cards));
		}

		private void OnAddCard()
		{
			var card = IoC.GetInstance<CardViewModel>();

			card.Initialize();
			Cards.Add(card);
		}
	}
}
