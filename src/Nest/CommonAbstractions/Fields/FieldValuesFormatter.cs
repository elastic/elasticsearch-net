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
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	internal class FieldValuesFormatter : IJsonFormatter<FieldValues>
	{
		private static readonly LazyDocumentFormatter LazyDocumentFormatter = new LazyDocumentFormatter();

		public FieldValues Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
			{
				reader.ReadNextBlock();
				return null;
			}

			var count = 0;
			var fields = new Dictionary<string, LazyDocument>();
			while (reader.ReadIsInObject(ref count))
			{
				var propertyName = reader.ReadPropertyName();
				var lazyDocument = LazyDocumentFormatter.Deserialize(ref reader, formatterResolver);
				fields[propertyName] = lazyDocument;
			}

			return new FieldValues(formatterResolver.GetConnectionSettings().Inferrer, fields);
		}

		public void Serialize(ref JsonWriter writer, FieldValues value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteBeginObject();
			var count = 0;
			foreach (var fieldValue in value)
			{
				if (count > 0)
					writer.WriteValueSeparator();

				writer.WritePropertyName(fieldValue.Key);
				writer.WriteRaw(fieldValue.Value.Bytes);
				count++;
			}
			writer.WriteEndObject();
		}
	}
}
