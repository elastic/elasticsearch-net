// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json.Serialization;
using System.Text.Json;
using Elastic.Transport;
using Elastic.Clients.Elasticsearch.Serialization;
using System.Runtime;

namespace Elastic.Clients.Elasticsearch;

internal sealed class JoinFieldConverter : JsonConverter<JoinField>
{
	private IElasticsearchClientSettings _settings;

	public override JoinField? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType == JsonTokenType.String)
		{
			var parent = reader.GetString();
			return new JoinField(new JoinField.Parent(parent));
		}

		Id parentId = null;
		string name = null;

		while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
		{
			if (reader.TokenType != JsonTokenType.PropertyName)
				throw new JsonException($"Unexpected token. Expected {JsonTokenType.PropertyName}, but read {reader.TokenType}.");

			var propertyName = reader.GetString();
			reader.Read();

			switch (propertyName)
			{
				case "parent":
					parentId = JsonSerializer.Deserialize<Id>(ref reader, options);
					break;
				case "name":
					name = reader.GetString();
					break;
				default:
					throw new JsonException($"Read an unexpected property name {propertyName}.");
			}
		}

		return parentId != null
			? new JoinField(new JoinField.Child(name, parentId))
			: new JoinField(new JoinField.Parent(name));
	}

	public override void Write(Utf8JsonWriter writer, JoinField value, JsonSerializerOptions options)
	{
		if (value is null)
		{
			writer.WriteNullValue();
			return;
		}

		switch (value.Tag)
		{
			case 0:
				JsonSerializer.Serialize(writer, value.ParentOption.Name, options);
				break;

			case 1:
				InitializeSettings(options);
				writer.WriteStartObject();
				writer.WritePropertyName("name");
				JsonSerializer.Serialize(writer, value.ChildOption.Name, options);
				writer.WritePropertyName("parent");
				var id = (value.ChildOption.ParentId as IUrlParameter)?.GetString(_settings);
				writer.WriteStringValue(id);
				writer.WriteEndObject();
				break;
		}
	}

	private void InitializeSettings(JsonSerializerOptions options)
	{
		if (_settings is null)
		{
			if (!options.TryGetClientSettings(out var settings))
				ThrowHelper.ThrowJsonExceptionForMissingSettings();

			_settings = settings;
		}
	}
}
