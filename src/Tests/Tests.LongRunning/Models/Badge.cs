using System;

namespace Tests.LongRunning.Models
{
	public class Badge
	{
		public BadgeClass Class { get; set; }
		public DateTimeOffset Date { get; set; }
		public string Name { get; set; }
	}

	public class BadgeMeta
	{
		public Badge Badge { get; set; }
		public int UserId { get; set; }
	}
}
