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

using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Internal;


namespace Nest
{
	internal class FieldFormatter : IJsonFormatter<Field>, IObjectPropertyNameFormatter<Field>
	{
		private static readonly AutomataDictionary Fields = new AutomataDictionary
		{
			{ "field", 0 },
			{ "boost", 1 },
			{ "format", 2 },
		};

		public Field Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			switch (token)
			{
				case JsonToken.Null:
					reader.ReadNext();
					return null;
				case JsonToken.String:
					return new Field(reader.ReadString());
				case JsonToken.BeginObject:
					var count = 0;
					string field = null;
					double? boost = null;
					string format = null;

					while (reader.ReadIsInObject(ref count))
					{
						var property = reader.ReadPropertyNameSegmentRaw();
						if (Fields.TryGetValue(property, out var value))
						{
							switch (value)
							{
								case 0:
									field = reader.ReadString();
									break;
								case 1:
									boost = reader.ReadDouble();
									break;
								case 2:
									format = reader.ReadString();
									break;
							}
						}
						else
							reader.ReadNextBlock();
					}

					return new Field(field, boost, format);
				default:
					throw new JsonParsingException($"Cannot deserialize {typeof(Field).FullName} from {token}");
			}
		}

		public void Serialize(ref JsonWriter writer, Field value, IJsonFormatterResolver formatterResolver) =>
			Serialize(ref writer, value, formatterResolver, false);

		private static void Serialize(ref JsonWriter writer, Field value, IJsonFormatterResolver formatterResolver, bool serializeAsString)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var settings = formatterResolver.GetConnectionSettings();
			var fieldName = settings.Inferrer.Field(value);
			if (serializeAsString || string.IsNullOrEmpty(value.Format))
				writer.WriteString(fieldName);
			else
			{
				writer.WriteBeginObject();
				writer.WritePropertyName("field");
				writer.WriteString(fieldName);
				writer.WriteValueSeparator();
				writer.WritePropertyName("format");
				writer.WriteString(value.Format);
				writer.WriteEndObject();
			}
		}

		public void SerializeToPropertyName(ref JsonWriter writer, Field value, IJsonFormatterResolver formatterResolver) =>
			Serialize(ref writer, value, formatterResolver, true);

		public Field DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			Deserialize(ref reader, formatterResolver);
	}
}
