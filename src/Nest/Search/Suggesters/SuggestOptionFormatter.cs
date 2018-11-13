using System;
using System.Collections.Generic;
using Utf8Json;
using Utf8Json.Internal;

namespace Nest
{
	internal static class SuggestOptionFields
	{
		public static readonly AutomataDictionary AutomataDictionary = new AutomataDictionary
		{
			{ "collate_match",		0 },
			{ "contexts",			1 },
			{ "freq",				2 },
			{ "highlighted",		3 },
			{ "_id",				4 },
			{ "_index",				5 },
			{ "_source",			6 },
			{ "text",				7 },
			{ "_type",				8 },
			{ "_score",				9 },
			{ "score",				10 },
		};
	}

	internal class SuggestOptionFormatter<T> : IJsonFormatter<SuggestOption<T>> where T : class
	{
		private readonly SourceFormatter<T> _sourceFormatter;

		public SuggestOptionFormatter() => _sourceFormatter = new SourceFormatter<T>();

		public SuggestOption<T> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.ReadIsNull())
				return null;

			var count = 0;
			var option = new SuggestOption<T>();
			while (reader.ReadIsInObject(ref count))
			{
				var propertyName = reader.ReadPropertyNameSegmentRaw();
				if (!SuggestOptionFields.AutomataDictionary.TryGetValue(propertyName, out var value))
					continue;

				switch (value)
				{
					case 0:
						option.CollateMatch = reader.ReadBoolean();
						break;
					case 1:
						option.Contexts = formatterResolver.GetFormatter<IDictionary<string, IEnumerable<Context>>>()
							.Deserialize(ref reader, formatterResolver);
						break;
					case 2:
						option.Frequency = reader.ReadInt64();
						break;
					case 3:
						option.Index = reader.ReadString();
						break;
					case 4:
						option.Id = formatterResolver.GetFormatter<Id>().Deserialize(ref reader, formatterResolver);
						break;
					case 5:
						option.Index = formatterResolver.GetFormatter<IndexName>().Deserialize(ref reader, formatterResolver);
						break;
					case 6:
						option.Source = _sourceFormatter.Deserialize(ref reader, formatterResolver);
						break;
					case 7:
						option.Text = reader.ReadString();
						break;
					case 8:
						option.Type = formatterResolver.GetFormatter<TypeName>().Deserialize(ref reader, formatterResolver);
						break;
					case 9:
						option.DocumentScore = reader.ReadNullableDouble();
						break;
					case 10:
						option.SuggestScore = reader.ReadNullableDouble();
						break;
				}
			}

			return option;
		}

		public void Serialize(ref JsonWriter writer, SuggestOption<T> value, IJsonFormatterResolver formatterResolver) =>
			throw new NotImplementedException();
	}
}
