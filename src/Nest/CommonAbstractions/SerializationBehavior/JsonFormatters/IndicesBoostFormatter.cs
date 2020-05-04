// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	internal class IndicesBoostFormatter : IJsonFormatter<IDictionary<IndexName, double>>
	{
		public void Serialize(ref JsonWriter writer, IDictionary<IndexName, double> value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var settings = formatterResolver.GetConnectionSettings();
			writer.WriteBeginArray();
			var count = 0;
			foreach (var entry in value)
			{
				if (count > 0)
					writer.WriteValueSeparator();
				writer.WriteBeginObject();
				var indexName = settings.Inferrer.IndexName(entry.Key);
				writer.WritePropertyName(indexName);
				writer.WriteDouble(entry.Value);
				writer.WriteEndObject();
				count++;
			}
			writer.WriteEndArray();
		}

		public IDictionary<IndexName, double> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			switch (token)
			{
				case JsonToken.BeginObject:
					return formatterResolver.GetFormatter<Dictionary<IndexName, double>>()
						.Deserialize(ref reader, formatterResolver);
				case JsonToken.BeginArray:
					var dictionary = new Dictionary<IndexName, double>();
					var count = 0;
					var indexNameFormatter = formatterResolver.GetFormatter<IndexName>();
					while (reader.ReadIsInArray(ref count))
					{
						reader.ReadIsBeginObjectWithVerify();
						var indexName = indexNameFormatter.Deserialize(ref reader, formatterResolver);
						reader.ReadIsNameSeparatorWithVerify();
						dictionary.Add(indexName, reader.ReadDouble());
						reader.ReadIsEndObjectWithVerify();
					}
					return dictionary;
				default:
					reader.ReadNextBlock();
					return null;
			}
		}
	}
}
