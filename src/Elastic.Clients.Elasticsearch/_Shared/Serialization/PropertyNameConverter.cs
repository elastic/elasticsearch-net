// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch.Serialization;

internal sealed class PropertyNameConverter : SettingsJsonConverter<PropertyName>
{
	public override void WriteAsPropertyName(Utf8JsonWriter writer, PropertyName value, JsonSerializerOptions options) =>
		writer.WritePropertyName(((IUrlParameter)value).GetString(GetSettings(options)));

	public override PropertyName ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
		reader.GetString();

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

		var propertyName = GetSettings(options).Inferrer.PropertyName(value);
		writer.WriteStringValue(propertyName);
	}
}
