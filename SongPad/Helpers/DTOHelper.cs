using SongPad.DTO;
using SongPad.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SongPad.Helpers
{
	public static class DTOHelper
	{
		public static ProjectDTO ToDTO(this ProjectViewModel project)
		{
			var dto = new ProjectDTO();

			dto.FilePath = project.FilePath;
			dto.Title = project.Title.TrimEnd('*');
			dto.Cards = project.Cards.Select(c => c.ToDTO()).ToArray();

			return dto;
		}

		public static CardDTO ToDTO(this CardViewModel card)
		{
			var dto = new CardDTO();

			dto.Title = card.Title;
			dto.Lines = card.Lines.Select(l => l.Line).ToArray();

			return dto;
		}
	}
}
