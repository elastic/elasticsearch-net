using System;
using System.Collections.Generic;
using Utf8Json;
using Utf8Json.Internal;

namespace Nest
{
	internal static class HitFields
	{
		public static readonly AutomataDictionary AutomataDictionary = new AutomataDictionary
		{
			{ "_explanation",		0 },
			{ "fields",				1 },
			{ "_id",				2 },
			{ "_index",				3 },
			{ "inner_hits",			4 },
			{ "matched_queries",	5 },
			{ "_nested",			6 },
			{ "_parent",			7 },
			{ "_routing",			8 },
			{ "_score",				9 },
			{ "sort",				10 },
			{ "_source",			11 },
			{ "_type",				12 },
			{ "_version",			13 },
			{ "highlight",			14 },
		};
	}

	internal class HitFormatter<T> : IJsonFormatter<Hit<T>> where T : class
	{
		private readonly SourceFormatter<T> _sourceFormatter;

		public HitFormatter() => _sourceFormatter = new SourceFormatter<T>();

		public Hit<T> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.ReadIsNull())
				return null;

			var count = 0;
			var hit = new Hit<T>();
			while (reader.ReadIsInObject(ref count))
			{
				var propertyName = reader.ReadPropertyNameSegmentRaw();
				if (!HitFields.AutomataDictionary.TryGetValue(propertyName, out var value))
					continue;

				switch (value)
				{
					case 0:
						hit.Explanation = formatterResolver.GetFormatter<Explanation>().Deserialize(ref reader, formatterResolver);
						break;
					case 1:
						hit.Fields = formatterResolver.GetFormatter<FieldValues>().Deserialize(ref reader, formatterResolver);
						break;
					case 2:
						hit.Id = reader.ReadString();
						break;
					case 3:
						hit.Index = reader.ReadString();
						break;
					case 4:
						hit.InnerHits = formatterResolver.GetFormatter<IReadOnlyDictionary<string, InnerHitsResult>>()
							.Deserialize(ref reader, formatterResolver);
						break;
					case 5:
						hit.MatchedQueries = formatterResolver.GetFormatter<IReadOnlyCollection<string>>()
							.Deserialize(ref reader, formatterResolver);
						break;
					case 6:
						hit.Nested = formatterResolver.GetFormatter<NestedIdentity>().Deserialize(ref reader, formatterResolver);
						break;
					case 7:
#pragma warning disable 618
						hit.Parent = reader.ReadString();
#pragma warning restore 618
						break;
					case 8:
						hit.Routing = reader.ReadString();
						break;
					case 9:
						hit.Score = reader.ReadNullableDouble();
						break;
					case 10:
						hit.Sorts = formatterResolver.GetFormatter<IReadOnlyCollection<object>>().Deserialize(ref reader, formatterResolver);
						break;
					case 11:
						hit.Source = _sourceFormatter.Deserialize(ref reader, formatterResolver);
						break;
					case 12:
						hit.Type = reader.ReadString();
						break;
					case 13:
						hit.Version = reader.ReadNullableLong();
						break;
					case 14:
						hit._Highlight = formatterResolver.GetFormatter<Dictionary<string, List<string>>>()
							.Deserialize(ref reader, formatterResolver);
						break;
				}
			}

			return hit;
		}

		public void Serialize(ref JsonWriter writer, Hit<T> value, IJsonFormatterResolver formatterResolver) => throw new NotSupportedException();
	}
}
