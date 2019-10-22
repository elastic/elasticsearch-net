using System;
using Nest;

namespace Tests.LongRunning.Models
{
	public class Question : Post
	{
		public int? AcceptedAnswerId { get; set; }
		public int AnswerCount { get; set; }
		public DateTimeOffset? CommunityOwnedDate { get; set; }
		public int FavoriteCount { get; set; }
		public string LastEditorDisplayName { get; set; }
		public string Title { get; set; }
		public CompletionField TitleSuggest { get; set; }
		public override string Type => nameof(Question);
		public int ViewCount { get; set; }
	}
}
