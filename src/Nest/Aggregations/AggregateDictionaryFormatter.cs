// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Collections.Generic;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	internal class AggregateDictionaryFormatter : IJsonFormatter<AggregateDictionary>
	{
		private static readonly AggregateFormatter Formatter = new AggregateFormatter();

		public AggregateDictionary Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var dictionary = new Dictionary<string, IAggregate>();
			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
			{
				reader.ReadNextBlock();
				return new AggregateDictionary(dictionary);
			}

			var count = 0;
			while (reader.ReadIsInObject(ref count))
			{
				var typedProperty = reader.ReadPropertyName();

				if (typedProperty.IsNullOrEmpty())
				{
					reader.ReadNextBlock();
					continue;
				}

				var tokens = AggregateDictionary.TypedKeyTokens(typedProperty);
				if (tokens.Length == 1)
					ParseAggregate(ref reader, formatterResolver, tokens[0], dictionary);
				else
					ReadAggregate(ref reader, formatterResolver, tokens, dictionary);
			}

			return new AggregateDictionary(dictionary);
		}

		public void Serialize(ref JsonWriter writer, AggregateDictionary value, IJsonFormatterResolver formatterResolver) =>
			throw new NotSupportedException();

		private static void ReadAggregate(ref JsonReader reader, IJsonFormatterResolver formatterResolver, string[] tokens,
			Dictionary<string, IAggregate> dictionary
		)
		{
			var name = tokens[1];
			var type = tokens[0];
			switch (type)
			{
				case "geo_centroid":
					ReadAggregate<GeoCentroidAggregate>(ref reader, formatterResolver, name, dictionary);
					break;
				default:
					//still fall back to heuristics based parsed in case we do not know the key
					ParseAggregate(ref reader, formatterResolver, name, dictionary);
					break;
			}
		}

		private static void ReadAggregate<TAggregate>(ref JsonReader reader, IJsonFormatterResolver formatterResolver,
			string name,
			Dictionary<string, IAggregate> dictionary
		)
			where TAggregate : IAggregate
		{
			var aggregate = formatterResolver.GetFormatter<TAggregate>().Deserialize(ref reader, formatterResolver);
			dictionary.Add(name, aggregate);
		}

		private static void ParseAggregate(ref JsonReader reader, IJsonFormatterResolver formatterResolver, string name,
			Dictionary<string, IAggregate> dictionary
		)
		{
			var aggregate = Formatter.Deserialize(ref reader, formatterResolver);
			dictionary.Add(name, aggregate);
		}
	}
}
