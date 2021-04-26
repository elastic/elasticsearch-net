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

namespace Elasticsearch.Net
{
	internal class NullableStringIntFormatter : IJsonFormatter<int?>
	{
		public int? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			switch (token)
			{
				case JsonToken.Null:
					reader.ReadNext();
					return null;
				case JsonToken.String:
					var s = reader.ReadString();
					if (!int.TryParse(s, out var i))
						throw new JsonParsingException($"Cannot parse {typeof(int).FullName} from: {s}");

					return i;
				case JsonToken.Number:
					return reader.ReadInt32();
				default:
					throw new JsonParsingException($"Cannot parse {typeof(int).FullName} from: {token}");
			}
		}

		public void Serialize(ref JsonWriter writer, int? value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteInt32(value.Value);
		}
	}
}
