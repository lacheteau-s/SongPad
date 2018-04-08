﻿using SongPad.Messages;
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
using Microsoft.Win32;
using System.IO;
using SongPad.Helpers;
using SongPad.DTO;
using System.Xml.Serialization;
using System.Collections.Specialized;

namespace SongPad.ViewModels
{
	public class ProjectViewModel : BaseViewModel, IActive
	{
		private IEventDispatcher _eventDispatcher;
		private IDialogService _dialogService;
		private ISerializer _sgpSerializer;

		private readonly string PROJECT_FORMAT_FILTER = "SongPad file (*.sgp)|*.sgp";

		public bool IsActive { get; private set; }

		public string FilePath { get; set; }

		public bool IsSaved => !string.IsNullOrEmpty(FilePath);

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

		public ProjectViewModel(IEventDispatcher eventDispatcher, IDialogService dialogService)
		{
			_eventDispatcher = eventDispatcher;
			_dialogService = dialogService;
			_sgpSerializer = IoC.GetInstance<SgpSerializer>();
		}

		#region Lifecycle

		public void Initialize(ProjectDTO dto)
		{
			FilePath = dto.FilePath;
			Title = dto.Title;
			Cards = new ObservableCollection<CardViewModel>(dto.Cards.Select(c =>
			{
				var vm = IoC.GetInstance<CardViewModel>();

				vm.Title = c.Title;
				vm.Initialize(c);
				vm.RemoveEventHandler += OnRemoveCard;

				return vm;

			}).ToList());
			IsActive = true;

			base.Initialize();
		}

		public override void Initialize()
		{
			Cards = new ObservableCollection<CardViewModel>();
			AddCard();

			base.Initialize();

			_eventDispatcher.Invoke(new ProjectLoadedEvent(Cards));
			IsActive = true;
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

			Cards.CollectionChanged += OnCardsCollectionChanged;

			_eventDispatcher.Subscribe<AddCardEvent>(this, OnAddCard);
			_eventDispatcher.Subscribe<ProjectChangedEvent>(this, OnChange);
		}

		protected override void Unsubscribe()
		{
			base.Unsubscribe();

			foreach (var card in Cards)
				card.RemoveEventHandler -= OnRemoveCard;

			Cards.CollectionChanged -= OnCardsCollectionChanged;

			_eventDispatcher.Unsubscribe<AddCardEvent>(this);
			_eventDispatcher.Unsubscribe<ProjectChangedEvent>(this);
		}

		#endregion

		public void SetActive(bool isActive)
		{
			if (isActive && !IsActive)
			{
				Subscribe();
				_eventDispatcher.Invoke(new ProjectLoadedEvent(Cards)); // TODO: perhaps rename event
			}
			else if (!isActive)
				Unsubscribe();

			IsActive = isActive;
		}

		public void Save()
		{
			// TODO : async + IsSaving to prevent saving again if the previous save isn't complete
			if (!HasChanges && IsSaved)
				return;

			if (!IsSaved)
			{
				var result = (FileDialogResult)_dialogService.SaveFileDialog(PROJECT_FORMAT_FILTER, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

				if (!result.IsOk) // Select destination popup
					return;

				FilePath = result.FilePath; // Fill out project path
			}

			Title = Path.GetFileName(FilePath); // Edit title to match file
			_sgpSerializer.Export(FilePath, this.ToDTO()); // async + try/catch

			HasChanges = false;
		}

		private void AddCard()
		{
			var card = IoC.GetInstance<CardViewModel>();

			card.Initialize();
			Cards.Add(card);
		}

		private void OnAddCard(AddCardEvent message)
		{
			AddCard();
		}

		private void OnRemoveCard(object sender, EventArgs e)
		{
			Cards.Remove((CardViewModel)sender);
		}

		private void OnChange(ProjectChangedEvent evt)
		{
			HasChanges = true;
		}

		private void OnCardsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (e.OldItems != null)
				foreach (CardViewModel card in e.OldItems)
				{
					card.RemoveEventHandler -= OnRemoveCard;
					card.Cleanup();
				}
			else if (e.NewItems != null)
				foreach (CardViewModel card in e.NewItems)
					card.RemoveEventHandler += OnRemoveCard;

			HasChanges = true;
		}

		protected override void RegisterDependencies()
		{
			RegisterDependency(nameof(HasChanges), nameof(Title));
		}
	}
}
