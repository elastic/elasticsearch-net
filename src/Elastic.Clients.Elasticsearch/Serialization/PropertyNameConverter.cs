// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch
{
	internal sealed class PropertyNameConverter : JsonConverter<PropertyName?>
	{
		private readonly IElasticsearchClientSettings _settings;

		public PropertyNameConverter(IElasticsearchClientSettings settings) => _settings = settings;

		public override PropertyName? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.String)
			{
				PropertyName propertyName = reader.GetString();
				return propertyName;
			}

			return null;
		}

		public override void Write(Utf8JsonWriter writer, PropertyName? value, JsonSerializerOptions options)
		{
			if (value is null)
			{
				writer.WriteNullValue();
				return;
			}

			var propertyName = _settings.Inferrer.PropertyName(value);
			writer.WriteStringValue(propertyName);
		}
	}
}
