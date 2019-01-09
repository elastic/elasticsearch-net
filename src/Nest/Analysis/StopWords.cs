using System;
using System.Collections.Generic;
using Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(StopWordsFormatter))]
	public class StopWords : Union<string, IEnumerable<string>>
	{
		public StopWords(string item) : base(item) { }

		public StopWords(IEnumerable<string> item) : base(item) { }

		public static implicit operator StopWords(string first) => new StopWords(first);

		public static implicit operator StopWords(List<string> second) => new StopWords(second);

		public static implicit operator StopWords(string[] second) => new StopWords(second);
	}

	internal class StopWordsFormatter : IJsonFormatter<StopWords>
	{
		public StopWords Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			if (token == JsonToken.BeginArray)
			{
				var stopwords = formatterResolver.GetFormatter<IEnumerable<string>>()
					.Deserialize(ref reader, formatterResolver);
				return new StopWords(stopwords);
			}

			var stopword = reader.ReadString();
			return new StopWords(stopword);
		}

		public void Serialize(ref JsonWriter writer, StopWords value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			switch (value._tag)
			{
				case 0:
					writer.WriteString(value.Item1);
					break;
				case 1:
					var formatter = formatterResolver.GetFormatter<IEnumerable<string>>();
					formatter.Serialize(ref writer, value.Item2, formatterResolver);
					break;
			}
		}
	}
}
