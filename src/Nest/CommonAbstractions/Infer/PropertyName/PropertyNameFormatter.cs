// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	internal class PropertyNameFormatter : IJsonFormatter<PropertyName>, IObjectPropertyNameFormatter<PropertyName>
	{
		public PropertyName Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.String)
			{
				reader.ReadNextBlock();
				return null;
			}

			PropertyName propertyName = reader.ReadString();
			return propertyName;
		}

		public void Serialize(ref JsonWriter writer, PropertyName value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var infer = formatterResolver.GetConnectionSettings().Inferrer;
			writer.WriteString(infer.PropertyName(value));
		}

		public void SerializeToPropertyName(ref JsonWriter writer, PropertyName value, IJsonFormatterResolver formatterResolver) =>
			Serialize(ref writer, value, formatterResolver);

		public PropertyName DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			Deserialize(ref reader, formatterResolver);
	}
}
