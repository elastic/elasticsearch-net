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
	internal class FieldsFormatter : IJsonFormatter<Fields>
	{
		private static readonly FieldFormatter FieldFormatter = new FieldFormatter();

		public Fields Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginArray)
			{
				reader.ReadNextBlock();
				return null;
			}

			var count = 0;
			var fields = new List<Field>();
			while (reader.ReadIsInArray(ref count))
			{
				var field = FieldFormatter.Deserialize(ref reader, formatterResolver);
				if (field != null)
					fields.Add(field);
			}
			return new Fields(fields);
		}

		public void Serialize(ref JsonWriter writer, Fields value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var fields = value.ListOfFields;
			writer.WriteBeginArray();
			for (var i = 0; i < fields.Count; i++)
			{
				if (i > 0)
					writer.WriteValueSeparator();
				FieldFormatter.Serialize(ref writer, fields[i], formatterResolver);
			}
			writer.WriteEndArray();
		}
	}
}
