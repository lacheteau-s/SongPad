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

		public ObservableCollection<CardViewModel> Cards { get; set; }

		public bool HasItems => Cards?.Count > 0;

		public CardsToolboxViewModel(IEventDispatcher eventDispatcher)
		{
			_eventDispatcher = eventDispatcher;
		}

		public override void Initialize()
		{
			Cards = new ObservableCollection<CardViewModel>();

			base.Initialize();
		}

		protected override void Subscribe()
		{
			base.Subscribe();

			_eventDispatcher.Subscribe<CardsCollectionChangedEvent>(this, OnCardsCollectionChanged);
		}

		protected override void Unsubscribe()
		{
			base.Unsubscribe();

			_eventDispatcher.Unsubscribe<CardsCollectionChangedEvent>(this);
		}

		private void OnCardsCollectionChanged(CardsCollectionChangedEvent evt)
		{
			var dict = new Dictionary<CardsCollectionChangedEvent.InstructionType, Action<CardViewModel>>
			{
				{ CardsCollectionChangedEvent.InstructionType.Added, (x) => Cards.Add(x) },
				{ CardsCollectionChangedEvent.InstructionType.Removed, (x) => Cards.Remove(x) }
			};

			foreach (var item in evt.Items)
				dict[evt.Instruction]((CardViewModel)item);
		}
	}
}
