// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	internal class IndicesFormatter : IJsonFormatter<Indices>
	{
		public Indices Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			switch (reader.GetCurrentJsonToken())
			{
				case JsonToken.BeginArray:
				{
					var indices = new List<IndexName>();
					var count = 0;
					while (reader.ReadIsInArray(ref count))
					{
						var index = reader.ReadString();
						indices.Add(index);
					}
					return new Indices(indices);
				}
				case JsonToken.String:
				{
					Indices indices = reader.ReadString();
					return indices;
				}
				default:
					reader.ReadNextBlock();
					return null;
			}
		}

		public void Serialize(ref JsonWriter writer, Indices value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			switch (value.Tag)
			{
				case 0:
					writer.WriteBeginArray();
					writer.WriteString("_all");
					writer.WriteEndArray();
					break;
				case 1:
					var settings = formatterResolver.GetConnectionSettings();
					writer.WriteBeginArray();
					for (var index = 0; index < value.Item2.Indices.Count; index++)
					{
						if (index > 0)
							writer.WriteValueSeparator();

						var indexName = value.Item2.Indices[index];
						writer.WriteString(indexName.GetString(settings));
					}
					writer.WriteEndArray();
					break;
			}
		}
	}
}
