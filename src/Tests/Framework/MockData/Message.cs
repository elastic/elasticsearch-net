using System;
using System.Text;
using Bogus;

namespace Tests.Framework.MockData
{
	public class Message
	{
		public string Body { get; set; }

		public Guid Id { get; set; }

		public DateTime Timestamp { get; set; }

		private static readonly Random Random = new Random(15842);
		private static readonly string[] Words = { "molestie", "vel", "metus", "neque", "dui", "volutpat", "sollicitudin", "sociis", "ac", "imperdiet", "tristique", "et", "nascetur", "ad", "rhoncus", "viverra", "ornare", "consectetur", "ultrices", "orci", "parturient", "lorem", "massa", "quis", "platea", "aenean", "fermentum", "augue", "placerat", "auctor", "natoque", "habitasse", "pharetra", "ridiculus", "leo", "sit", "cras", "est", "venenatis", "aptent", "nibh", "magnis", "sodales", "malesuada", "praesent", "potenti", "lobortis", "justo", "quam", "cubilia", "pellentesque", "porttitor", "pretium", "adipiscing", "phasellus", "lectus", "vivamus", "id", "mi", "bibendum", "feugiat", "odio", "rutrum", "vestibulum", "posuere", "elementum", "suscipit", "purus", "accumsan", "egestas", "mus", "varius", "a", "arcu", "commodo", "dis", "lacinia", "tellus", "cursus", "aliquet", "interdum", "turpis", "maecenas", "dapibus", "cum", "fames", "montes", "iaculis", "erat", "euismod", "hac", "faucibus", "mauris", "tempus", "primis", "velit", "sem", "duis", "luctus", "penatibus", "sapien", "blandit", "eros", "suspendisse", "urna", "ipsum", "congue", "nulla", "taciti", "mollis", "facilisis", "at", "amet", "laoreet", "dignissim", "fringilla", "in", "nostra", "quisque", "donec", "enim", "eleifend", "nisl", "morbi", "felis", "torquent", "eget", "convallis", "etiam", "tincidunt", "facilisi", "pulvinar", "vulputate", "integre", "himenaeos", "netus", "senectus", "non", "litora", "per", "curae", "ultricies", "nec", "nam", "eu", "ante", "mattis", "vehicula", "sociosqu", "nunc", "semper", "lacus", "proin", "risus", "condimentum", "scelerisque", "conubia", "consequat", "dolor", "libero", "diam", "ut", "inceptos", "porta", "nullam", "dictumst", "magna", "tempor", "fusce", "vitae", "aliquam", "curabitur", "ligula", "habitant", "class", "hendrerit", "sagittis", "gravida", "nisi", "tortor", "ullamcorper", "dictum", "elit", "sed" };

		public static Faker<Message> Generator { get; } =
			new Faker<Message>()
				.RuleFor(m => m.Id, m => Guid.NewGuid())
				.RuleFor(m => m.Body, m => GetMessageText())
				.RuleFor(m => m.Timestamp, m => DateTime.UtcNow)
			;

		private static string GetMessageText()
		{
			var numWords = Random.Next(1, 20);

			var sb = new StringBuilder();

			for (var i = 0; i < numWords; i++)
			{
				sb.Append(Words[Random.Next(0, Words.Length)]).Append(" ");
			}

			return sb.ToString().TrimEnd();
		}
	}
}
