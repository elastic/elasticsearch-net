/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System.Collections.Generic;
using Nest.Utf8Json;

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
