// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
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
