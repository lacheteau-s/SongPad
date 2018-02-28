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
	public class ProjectViewModel
	{
		private IEventDispatcher _eventDispatcher;

		public string Title { get; set; }

		public ObservableCollection<CardViewModel> Cards { get; set; }

		public ProjectViewModel(IEventDispatcher eventDispatcher)
		{
			_eventDispatcher = eventDispatcher;

			Cards = new ObservableCollection<CardViewModel>();
			Cards.Add(new CardViewModel());
		}

		public void Initialize()
		{
			// TODO : Unsubscribe
			_eventDispatcher.Subscribe<AddCardEvent>(this, OnAddCard);

			foreach (var card in Cards)
				card.RemoveEventHandler += OnCardRemove;
		}

		private void OnAddCard(AddCardEvent message)
		{
			Cards.Add(new CardViewModel());
		}

		private void OnCardRemove(object sender, EventArgs e)
		{
			var card = (CardViewModel)sender;

			card.RemoveEventHandler -= OnCardRemove;
			Cards.Remove(card);
		}
	}
}
