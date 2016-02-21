using System;

namespace Tests.Framework.MockData
{
	public class Attachment
	{
		public string File { get; set; }

		public string Author { get; set; }

		public long? ContentLength { get; set; }

		public string ContentType { get; set; }

		public DateTime? Date { get; set; }

		public string Keywords { get; set; }

		public string Language { get; set; }

		public string Name { get; set; }

		public string Title { get; set; }
	}
}