// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json.Serialization;
using System.Text.Json;
using Elastic.Transport;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Json;

public sealed class JoinFieldConverter : JsonConverter<JoinField>
{
	private static readonly JsonEncodedText PropName = JsonEncodedText.Encode("name");
	private static readonly JsonEncodedText PropParent = JsonEncodedText.Encode("parent");

	public override JoinField? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType == JsonTokenType.String)
		{
			var parent = reader.GetString()!;
			return new JoinField(new JoinField.Parent(parent));
		}

		reader.ValidateToken(JsonTokenType.StartObject);

		Id? parentId = null;
		string? name = null;

		while (reader.Read() && reader.TokenType is JsonTokenType.PropertyName)
		{
			if (reader.TryReadProperty(options, PropName, ref name, null))
			{
				continue;
			}

			if (reader.TryReadProperty(options, PropParent, ref parentId, null))
			{
				continue;
			}

			throw new JsonException($"Read an unexpected property name {reader.GetString()!}.");
		}

		reader.ValidateToken(JsonTokenType.EndObject);

		if (name is null)
		{
			throw new JsonException("Missing required property 'name'.");
		}

		return parentId != null
			? new JoinField(new JoinField.Child(name, parentId))
			: new JoinField(new JoinField.Parent(name));
	}

	public override void Write(Utf8JsonWriter writer, JoinField value, JsonSerializerOptions options)
	{
		if (value is null)
		{
			throw new ArgumentNullException(nameof(value));
		}

		switch (value.Tag)
		{
			case 0:
				writer.WriteValue(options, value.ParentOption.Name);
				break;

			case 1:
				writer.WriteStartObject();
				writer.WriteProperty(options, PropName, value.ChildOption.Name);
				writer.WriteProperty(options, PropParent, (value.ChildOption.ParentId as IUrlParameter)?.GetString(options.GetContext<IElasticsearchClientSettings>()));
				writer.WriteEndObject();
				break;
		}
	}
}
