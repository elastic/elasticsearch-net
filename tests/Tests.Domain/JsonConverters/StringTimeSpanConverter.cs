// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Reflection;
using Newtonsoft.Json;

namespace Tests.Domain.JsonConverters
{
	internal class StringTimeSpanConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
				writer.WriteNull();
			else
			{
				var timeSpan = (TimeSpan)value;
				writer.WriteValue(timeSpan.ToString());
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
			{
				if (!objectType.IsGenericType || objectType.GetGenericTypeDefinition() != typeof(Nullable<>))
					throw new JsonSerializationException($"Cannot convert null value to {objectType}.");

				return null;
			}
			if (reader.TokenType == JsonToken.String) return TimeSpan.Parse((string)reader.Value);

			throw new JsonSerializationException($"Cannot convert token of type {reader.TokenType} to {objectType}.");
		}

		public override bool CanConvert(Type objectType) => objectType == typeof(TimeSpan) || objectType == typeof(TimeSpan?);
	}
}
