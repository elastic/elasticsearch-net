// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

[JsonConverter(typeof(OpTypeConverter))]
public partial struct OpType : IStringable
{
	public static OpType Index = new("index");
	public static OpType Create = new("create");

	public OpType(string value) => Value = value;

	public string Value { get; }

	public static implicit operator OpType(string v) => new(v);

	public string GetString() => Value ?? string.Empty;
}

internal sealed class OpTypeConverter :
	JsonConverter<OpType>
{
	public override OpType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.String)
		{
			throw new JsonException("Unexpected token.");
		}

		var value = reader.GetString();

		return value switch
		{
			"index" => OpType.Index,
			"create" => OpType.Create,
			_ => throw new JsonException($"Unsupported value '{value}' for '{nameof(OpType)}' enum.")
		};
	}

	public override void Write(Utf8JsonWriter writer, OpType value, JsonSerializerOptions options) => writer.WriteStringValue(value.Value);
}
