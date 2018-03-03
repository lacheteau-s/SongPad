using SongPad.Messages;
using SongPad.Services;
using SongPad.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace SongPad.ViewModels
{
	public class ProjectViewModel : BaseViewModel
	{
		private IEventDispatcher _eventDispatcher;

		private string _filePath;

		public bool IsSaved => !string.IsNullOrEmpty(_filePath);

		private bool _hasChanges;

		public bool HasChanges
		{
			get { return _hasChanges; }
			set { SetProperty(ref _hasChanges, value); }
		}

		private string _title;

		public string Title
		{
			get { return $"{_title}{(_hasChanges ? "*" : string.Empty)}"; }
			set { SetProperty(ref _title, value); }
		}

		public ObservableCollection<CardViewModel> Cards { get; set; }

		public ProjectViewModel(IEventDispatcher eventDispatcher)
		{
			_eventDispatcher = eventDispatcher;
		}

		#region Lifecycle

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

		#endregion

		public void Save()
		{
			// TODO : async
			if (!HasChanges)
				return;

			if (!IsSaved)
			{
				// Select destination popup
				// Fill out project path
				// Create file here ?
			}

			// In case of error: first, make a duplicate of the file
			// Serialize project
			// Write to file
			// In case of error: delete file if new or rewrite old file

			HasChanges = false;
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
			HasChanges = true;
		}

		private void OnRemoveCard(object sender, EventArgs e)
		{
			RemoveCard((CardViewModel)sender);
			HasChanges = true;
		}

		protected override void RegisterDependencies()
		{
			RegisterDependency(nameof(HasChanges), nameof(Title));
		}
	}
}
