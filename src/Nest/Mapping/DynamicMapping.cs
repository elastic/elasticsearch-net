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

using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Internal;


namespace Nest
{
	[StringEnum]
	public enum DynamicMapping
	{
		/// <summary>
		/// If new unmapped fields are passed, the whole document will not be added/updated
		/// </summary>
		[EnumMember(Value = "strict")]
		Strict
	}

	internal class DynamicMappingFormatter : IJsonFormatter<Union<bool,DynamicMapping>>
	{
		private static readonly AutomataDictionary Values = new AutomataDictionary { { "true", 0 }, { "false", 1 }, { "strict", 2 } };

		public void Serialize(ref JsonWriter writer, Union<bool, DynamicMapping> value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			switch (value.Tag)
			{
				case 0:
					writer.WriteBoolean(value.Item1);
					break;
				case 1:
					writer.WriteString(value.Item2.GetStringValue());
					break;
			}
		}

		public Union<bool, DynamicMapping> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.ReadIsNull())
				return null;

			var token = reader.GetCurrentJsonToken();
			switch (token)
			{
				case JsonToken.True:
				case JsonToken.False:
					return new Union<bool, DynamicMapping>(reader.ReadBoolean());
				case JsonToken.String:
					var segment = reader.ReadStringSegmentUnsafe();
					if (Values.TryGetValue(segment, out var value))
					{
						switch (value)
						{
							case 0:
								return new Union<bool, DynamicMapping>(true);
							case 1:
								return new Union<bool, DynamicMapping>(false);
							case 2:
								return new Union<bool, DynamicMapping>(DynamicMapping.Strict);
						}
					}

					return null;
				default:
					throw new JsonParsingException($"Cannot parse Union<bool, DynamicMapping> from token '{token}'");
			}
		}
	}
}
