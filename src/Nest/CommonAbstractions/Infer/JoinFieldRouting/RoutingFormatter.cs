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
	internal class RoutingFormatter : IJsonFormatter<Routing>
	{
		public Routing Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			reader.GetCurrentJsonToken() == JsonToken.Number
				? new Routing(reader.ReadInt64())
				: new Routing(reader.ReadString());

		public void Serialize(ref JsonWriter writer, Routing value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			if (value.Document != null)
			{
				var settings = formatterResolver.GetConnectionSettings();
				var documentId = settings.Inferrer.Routing(value.Document.GetType(), value.Document);
				writer.WriteString(documentId);
			}
			else if (value.DocumentGetter != null)
			{
				var settings = formatterResolver.GetConnectionSettings();
				var doc = value.DocumentGetter();
				var documentId = settings.Inferrer.Routing(doc.GetType(), doc);
				writer.WriteString(documentId);
			}
			else if (value.LongValue != null) writer.WriteInt64(value.LongValue.Value);
			else writer.WriteString(value.StringValue);
		}
	}
}
