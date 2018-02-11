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
		public string Title { get; set; }

		public ObservableCollection<CardViewModel> Cards { get; set; }

		public ProjectViewModel()
		{
			Cards = new ObservableCollection<CardViewModel>();
			Cards.Add(new CardViewModel());  
		}

		public void Initialize()
		{
			foreach (var card in Cards)
			{
				card.RemoveEventHandler += OnCardRemove;
			}
		}

		private void OnCardRemove(object sender, EventArgs e)
		{
			var card = (CardViewModel)sender;

			card.RemoveEventHandler -= OnCardRemove;
			Cards.Remove(card);
		}
	}
}
