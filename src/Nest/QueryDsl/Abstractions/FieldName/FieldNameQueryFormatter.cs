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

using Nest.Utf8Json;
namespace Nest
{
	internal class FieldNameQueryFormatter<T, TInterface> : ReadAsFormatter<T, TInterface>
		where T : class, TInterface, IFieldNameQuery, new()
		where TInterface : class, IFieldNameQuery
	{
		public override void Serialize(ref JsonWriter writer, TInterface value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var fieldName = value.Field;
			if (fieldName == null)
				return;

			var settings = formatterResolver.GetConnectionSettings();
			var field = settings.Inferrer.Field(fieldName);
			if (field.IsNullOrEmpty())
				return;

			writer.WriteBeginObject();
			writer.WritePropertyName(field);

			base.Serialize(ref writer, value, formatterResolver);

			writer.WriteEndObject();
		}

		public override TInterface Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			reader.ReadIsBeginObjectWithVerify();

			if (reader.ReadIsEndObject())
				return default;

			TInterface query = null;
			var fieldName = reader.ReadPropertyName();
			var token = reader.GetCurrentJsonToken();
			switch (token)
			{
				case JsonToken.BeginObject:
					query = base.Deserialize(ref reader, formatterResolver);
					reader.ReadIsEndObjectWithVerify();
					break;
				case JsonToken.Null:
					reader.ReadNext();
					break;
				default:
					query = new T();
					switch (query)
					{
						case ITermQuery termQuery:
							switch (token)
							{
								case JsonToken.String:
									termQuery.Value = reader.ReadString();
									break;
								case JsonToken.Number:
									var segment = reader.ReadNumberSegment();
									if (segment.IsLong())
										termQuery.Value = NumberConverter.ReadInt64(segment.Array, segment.Offset, out _);
									else
										termQuery.Value = NumberConverter.ReadDouble(segment.Array, segment.Offset, out _);
									break;
								case JsonToken.True:
								case JsonToken.False:
									termQuery.Value = reader.ReadBoolean();
									break;
							}
							reader.ReadIsEndObjectWithVerify();
							break;
						case IMatchQuery matchQuery:
							matchQuery.Query = reader.ReadString();
							reader.ReadIsEndObjectWithVerify();
							break;
						case IMatchPhraseQuery matchPhraseQuery:
							matchPhraseQuery.Query = reader.ReadString();
							reader.ReadIsEndObjectWithVerify();
							break;
						case IMatchPhrasePrefixQuery matchPhrasePrefixQuery:
							matchPhrasePrefixQuery.Query = reader.ReadString();
							reader.ReadIsEndObjectWithVerify();
							break;
						case IMatchBoolPrefixQuery matchBoolPrefixQuery:
							matchBoolPrefixQuery.Query = reader.ReadString();
							reader.ReadIsEndObjectWithVerify();
							break;
					}
					break;
			}

			if (query == null)
				return null;

			query.Field = fieldName;
			return query;
		}
	}
}
