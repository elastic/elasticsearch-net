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

namespace Nest
{
	internal class IdFormatter : IJsonFormatter<Id>
	{
		public Id Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();

			return token == JsonToken.Number
				? new Id(reader.ReadInt64())
				: new Id(reader.ReadString());
		}

		public void Serialize(ref JsonWriter writer, Id value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			if (value.Document != null)
			{
				var settings = formatterResolver.GetConnectionSettings();
				var documentId = settings.Inferrer.Id(value.Document.GetType(), value.Document);
				writer.WriteString(documentId);
			}
			else if (value.LongValue != null)
				writer.WriteInt64(value.LongValue.Value);
			else
				writer.WriteString(value.StringValue);
		}
	}

	internal class IdStringFormatter : IJsonFormatter<string>
	{
		public string Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();

			return token == JsonToken.Number
				? reader.ReadInt64().ToString()
				: reader.ReadString();
		}

		public void Serialize(ref JsonWriter writer, string value, IJsonFormatterResolver formatterResolver) =>
			writer.WriteString(value);
	}
}
