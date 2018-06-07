using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	internal class AggregateDictionaryConverter : VerbatimDictionaryKeysJsonConverter<string, IAggregate>
	{
		private static readonly AggregateJsonConverter OldSchoolHeuristicsParser = new AggregateJsonConverter();

		public override bool CanRead => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var dictionary = new Dictionary<string, IAggregate>();
			if (reader.TokenType != JsonToken.StartObject)
			{
				reader.Skip();
				return new AggregateDictionary(dictionary);
			}

			var depth = reader.Depth;
			while (reader.Depth >= depth)
			{
				reader.Read();
				var typedProperty = reader.Value as string;
				if (typedProperty.IsNullOrEmpty()) break;
				var tokens = AggregateDictionary.TypedKeyTokens(typedProperty);
				if (tokens.Length == 1)
					ParseAggregate(reader, serializer, tokens[0], dictionary);
				else
					ReadAggregate(reader, serializer, tokens, dictionary);
			}

			return new AggregateDictionary(dictionary);
		}

		private static void ReadAggregate(JsonReader r, JsonSerializer s, string[] tokens, Dictionary<string, IAggregate> d)
		{
			var name = tokens[1];
			var type = tokens[0];
			switch (type)
			{
				case "geo_centroid":
					ReadAggregate<GeoCentroidAggregate>(r, s, d, name);
					break;
				default:
					//still fall back to heuristics based parsed in case we do not know the key
					ParseAggregate(r, s, name, d);
					break;
			}
		}

		private static void ReadAggregate<TAggregate>(JsonReader reader, JsonSerializer serializer, Dictionary<string, IAggregate> dictionary, string name)
			where TAggregate : IAggregate
		{
			reader.Read();
			var aggregate = serializer.Deserialize<TAggregate>(reader);
			dictionary.Add(name, aggregate);
		}

		private static void ParseAggregate(JsonReader reader, JsonSerializer serializer, string name, Dictionary<string, IAggregate> dictionary)
		{
			reader.Read();
			var aggregate = OldSchoolHeuristicsParser.ReadJson(reader, typeof(IAggregate), null, serializer) as IAggregate;
			dictionary.Add(name, aggregate);
		}
	}
}
