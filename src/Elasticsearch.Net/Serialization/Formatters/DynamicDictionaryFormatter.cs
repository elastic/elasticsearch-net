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
using System.Globalization;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Formatters;

namespace Elasticsearch.Net
{
	internal class DynamicDictionaryFormatter : IJsonFormatter<DynamicDictionary>
	{
		protected static readonly DictionaryFormatter<string, object> DictionaryFormatter =
			new DictionaryFormatter<string, object>();

		protected static readonly ArrayFormatter<object> ArrayFormatter = new ArrayFormatter<object>();

		public void Serialize(ref JsonWriter writer, DynamicDictionary value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteBeginObject();
			var formatter = formatterResolver.GetFormatter<object>();
			var count = 0;
			foreach (var kv in (IDictionary<string, DynamicValue>)value)
			{
				if (count > 0)
					writer.WriteValueSeparator();
				writer.WritePropertyName(kv.Key);
				formatter.Serialize(ref writer, kv.Value?.Value, formatterResolver);
				count++;
			}
			writer.WriteEndObject();
		}

		public DynamicDictionary Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() == JsonToken.BeginArray)
			{
				var array = ArrayFormatter.Deserialize(ref reader, formatterResolver);
				var arrayDict = new Dictionary<string, object>();
				for (var i = 0; i < array.Length; i++)
					arrayDict[i.ToString(CultureInfo.InvariantCulture)] = new DynamicValue(array[i]);
				return DynamicDictionary.Create(arrayDict);

			}
			var dictionary = DictionaryFormatter.Deserialize(ref reader, formatterResolver);
			return DynamicDictionary.Create(dictionary);
		}
	}
}
