using System;
using System.Collections.Generic;
using System.Text;

namespace Benchmarking
{
	/// <summary>
	/// Generates random messages
	/// </summary>
	internal class MessageGenerator
	{
		private static readonly Random Random = new Random(15842);
		private static readonly string[] Words = { "molestie", "vel", "metus", "neque", "dui", "volutpat", "sollicitudin", "sociis", "ac", "imperdiet", "tristique", "et", "nascetur", "ad", "rhoncus", "viverra", "ornare", "consectetur", "ultrices", "orci", "parturient", "lorem", "massa", "quis", "platea", "aenean", "fermentum", "augue", "placerat", "auctor", "natoque", "habitasse", "pharetra", "ridiculus", "leo", "sit", "cras", "est", "venenatis", "aptent", "nibh", "magnis", "sodales", "malesuada", "praesent", "potenti", "lobortis", "justo", "quam", "cubilia", "pellentesque", "porttitor", "pretium", "adipiscing", "phasellus", "lectus", "vivamus", "id", "mi", "bibendum", "feugiat", "odio", "rutrum", "vestibulum", "posuere", "elementum", "suscipit", "purus", "accumsan", "egestas", "mus", "varius", "a", "arcu", "commodo", "dis", "lacinia", "tellus", "cursus", "aliquet", "interdum", "turpis", "maecenas", "dapibus", "cum", "fames", "montes", "iaculis", "erat", "euismod", "hac", "faucibus", "mauris", "tempus", "primis", "velit", "sem", "duis", "luctus", "penatibus", "sapien", "blandit", "eros", "suspendisse", "urna", "ipsum", "congue", "nulla", "taciti", "mollis", "facilisis", "at", "amet", "laoreet", "dignissim", "fringilla", "in", "nostra", "quisque", "donec", "enim", "eleifend", "nisl", "morbi", "felis", "torquent", "eget", "convallis", "etiam", "tincidunt", "facilisi", "pulvinar", "vulputate", "integre", "himenaeos", "netus", "senectus", "non", "litora", "per", "curae", "ultricies", "nec", "nam", "eu", "ante", "mattis", "vehicula", "sociosqu", "nunc", "semper", "lacus", "proin", "risus", "condimentum", "scelerisque", "conubia", "consequat", "dolor", "libero", "diam", "ut", "inceptos", "porta", "nullam", "dictumst", "magna", "tempor", "fusce", "vitae", "aliquam", "curabitur", "ligula", "habitant", "class", "hendrerit", "sagittis", "gravida", "nisi", "tortor", "ullamcorper", "dictum", "elit", "sed" };

		public IEnumerable<Message> Generate(int numToGenerate)
		{
			for (int i = 0; i < numToGenerate; i++)
			{
				yield return new Message()
				{
					Id = Guid.NewGuid(),
					Timestamp = DateTime.UtcNow,
					Body = GetMessageText()
				};
			}
		}

		private static string GetMessageText()
		{
			int numWords = Random.Next(1, 20);

			var sb = new StringBuilder();

			for (int i = 0; i < numWords; i++)
			{
				sb.Append(Words[Random.Next(0, Words.Length)]).Append(" ");
			}

			return sb.ToString().TrimEnd();
		}
	}
}