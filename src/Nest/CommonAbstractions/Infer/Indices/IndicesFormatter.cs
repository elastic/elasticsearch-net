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
