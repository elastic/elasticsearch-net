// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using System.Text;
using Bogus;
using Nest;
using Tests.Configuration;

namespace Tests.Domain
{
	public class Log
	{
		private static readonly Random Random = new Random(15842);

		public static readonly string[] Words =
		{
			"molestie", "vel", "metus", "neque", "dui", "volutpat", "sollicitudin", "sociis", "ac", "imperdiet", "tristique", "et",
			"nascetur", "ad", "rhoncus", "viverra", "ornare", "consectetur", "ultrices", "orci", "parturient", "lorem", "massa", "quis",
			"platea", "aenean", "fermentum", "augue", "placerat", "auctor", "natoque", "habitasse", "pharetra", "ridiculus", "leo", "sit",
			"cras", "est", "venenatis", "aptent", "nibh", "magnis", "sodales", "malesuada", "praesent", "potenti", "lobortis", "justo",
			"quam", "cubilia", "pellentesque", "porttitor", "pretium", "adipiscing", "phasellus", "lectus", "vivamus", "id", "mi",
			"bibendum", "feugiat", "odio", "rutrum", "vestibulum", "posuere", "elementum", "suscipit", "purus", "accumsan", "egestas",
			"mus", "varius", "a", "arcu", "commodo", "dis", "lacinia", "tellus", "cursus", "aliquet", "interdum", "turpis", "maecenas",
			"dapibus", "cum", "fames", "montes", "iaculis", "erat", "euismod", "hac", "faucibus", "mauris", "tempus", "primis", "velit",
			"sem", "duis", "luctus", "penatibus", "sapien", "blandit", "eros", "suspendisse", "urna", "ipsum", "congue", "nulla", "taciti",
			"mollis", "facilisis", "at", "amet", "laoreet", "dignissim", "fringilla", "in", "nostra", "quisque", "donec", "enim",
			"eleifend", "nisl", "morbi", "felis", "torquent", "eget", "convallis", "etiam", "tincidunt", "facilisi", "pulvinar",
			"vulputate", "integre", "himenaeos", "netus", "senectus", "non", "litora", "per", "curae", "ultricies", "nec", "nam", "eu",
			"ante", "mattis", "vehicula", "sociosqu", "nunc", "semper", "lacus", "proin", "risus", "condimentum", "scelerisque", "conubia",
			"consequat", "dolor", "libero", "diam", "ut", "inceptos", "porta", "nullam", "dictumst", "magna", "tempor", "fusce", "vitae",
			"aliquam", "curabitur", "ligula", "habitant", "class", "hendrerit", "sagittis", "gravida", "nisi", "tortor", "ullamcorper",
			"dictum", "elit", "sed"
		};

		public static readonly string[] Sections = Words.Where(w => w.Length > 3).Take(10).ToArray();


		public string Body { get; set; }

		public static Faker<Log> Generator { get; } =
			new Faker<Log>()
				.UseSeed(TestConfiguration.Instance.Seed)
				.RuleFor(m => m.Timestamp, m => m.Date.Between(DateTime.UtcNow.AddDays(-7), DateTime.UtcNow))
				.RuleFor(m => m.Body, m => GetMessageText())
				.RuleFor(m => m.Tag, m => m.Hacker.Abbreviation())
				.RuleFor(m => m.UserAgent, m => m.Internet.UserAgent())
				.RuleFor(m => m.User, m => m.Internet.UserName())
				.RuleFor(m => m.Url, m => m.Internet.UrlWithPath())
				.RuleFor(m => m.Temperature, m => m.Random.Number(-10, 45))
				.RuleFor(m => m.Voltage, m => m.Random.Double(0, 10.0))
				.RuleFor(m => m.Section, m => m.PickRandom(Sections))
				.RuleFor(m => m.Load, m => m.Random.Double(100, 500))
				.RuleFor(m => m.NetIn, m => m.Random.Double(1000, 10000))
				.RuleFor(m => m.NetOut, m => m.Random.Double(1000, 10000));

		public double Load { get; set; }
		public double NetIn { get; set; }
		public double NetOut { get; set; }
		[Keyword] public string Section { get; set; }
		[Keyword] public string Tag { get; set; }
		public long Temperature { get; set; }
		public DateTime Timestamp { get; set; }
		[Keyword] public string Url { get; set; }
		[Keyword] public string User { get; set; }
		[Keyword] public string UserAgent { get; set; }
		public double Voltage { get; set; }

		private static string GetMessageText()
		{
			var numWords = Random.Next(1, 20);

			var sb = new StringBuilder();

			for (var i = 0; i < numWords; i++) sb.Append(Words[Random.Next(0, Words.Length)]).Append(" ");

			return sb.ToString().TrimEnd();
		}
	}
}
