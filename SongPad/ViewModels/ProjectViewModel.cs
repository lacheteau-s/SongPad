using SongPad.Messages;
using SongPad.Services;
using SongPad.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongPad.ViewModels
{
	public class ProjectViewModel : BaseViewModel
	{
		private IEventDispatcher _eventDispatcher;

		public string Title { get; set; }

		public ObservableCollection<CardViewModel> Cards { get; set; }

		public ProjectViewModel(IEventDispatcher eventDispatcher)
		{
			_eventDispatcher = eventDispatcher;

		}

		public override void Initialize()
		{
			Cards = new ObservableCollection<CardViewModel>();
			AddCard();

			base.Initialize();
		}

		public override void Cleanup()
		{
			base.Cleanup();

			foreach (var card in Cards)
				card.Cleanup();

			Cards.Clear();
		}

		protected override void Subscribe()
		{
			base.Subscribe();

			foreach (var card in Cards)
				card.RemoveEventHandler += OnRemoveCard;

			_eventDispatcher.Subscribe<AddCardEvent>(this, OnAddCard);
		}

		protected override void Unsubscribe()
		{
			base.Unsubscribe();

			foreach (var card in Cards)
				card.RemoveEventHandler -= OnRemoveCard;

			_eventDispatcher.Unsubscribe<AddCardEvent>(this);
		}

		private void AddCard()
		{
			var card = IoC.GetInstance<CardViewModel>();

			card.Initialize();
			card.RemoveEventHandler += OnRemoveCard;
			Cards.Add(card);
		}

		private void RemoveCard(CardViewModel card)
		{
			card.RemoveEventHandler -= OnRemoveCard;
			card.Cleanup();
			Cards.Remove(card);
		}

		private void OnAddCard(AddCardEvent message)
		{
			AddCard();
		}

		private void OnRemoveCard(object sender, EventArgs e)
		{
			RemoveCard((CardViewModel)sender);
		}
	}
}
