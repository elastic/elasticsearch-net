using System;
using System.Collections.Generic;

namespace Tests.LongRunning.Models
{
	public class User
	{
		public string AboutMe { get; set; }
		public int AccountId { get; set; }
		public int? Age { get; set; }
		public List<Badge> Badges { get; set; }
		public DateTimeOffset CreationDate { get; set; }
		public string DisplayName { get; set; }
		public int DownVotes { get; set; }
		public int Id { get; set; }
		public DateTimeOffset LastAccessDate { get; set; }
		public string Location { get; set; }
		public string ProfileImageUrl { get; set; }
		public int Reputation { get; set; }
		public int UpVotes { get; set; }
		public int Views { get; set; }
		public string WebsiteUrl { get; set; }
	}
}
