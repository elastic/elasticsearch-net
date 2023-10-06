// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch;

[JsonConverter(typeof(ScriptConverter))]
public partial class Script
{
}

internal sealed class ScriptConverter : JsonConverter<Script>
{
	public override Script? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType == JsonTokenType.String)
		{
			var source = reader.GetString();
			return new Script(new InlineScript(source));
		}

		var readAheadCopy = reader;

		readAheadCopy.Read(); // {

		if (readAheadCopy.TokenType != JsonTokenType.PropertyName)
			throw new JsonException("Unexpected token type");

		if (readAheadCopy.ValueTextEquals("params"))
		{
			while (readAheadCopy.Read() && readAheadCopy.TokenType != JsonTokenType.EndObject)
			{
			}

			readAheadCopy.Read();
		}

		if (readAheadCopy.ValueTextEquals("id"))
		{
			var storedScript = JsonSerializer.Deserialize<StoredScriptId>(ref reader, options);
			return new Script(storedScript);
		}

		var inlineScript = JsonSerializer.Deserialize<InlineScript>(ref reader, options);
		return new Script(inlineScript);
	}

	public override void Write(Utf8JsonWriter writer, Script value, JsonSerializerOptions options)
	{
		if (value is null)
		{
			writer.WriteNullValue();
			return;
		}

		if (value.Item1 is not null)
		{
			JsonSerializer.Serialize(writer, value.Item1, value.Item1.GetType(), options);
			return;
		}

		if (value.Item2 is not null)
		{
			JsonSerializer.Serialize(writer, value.Item2, value.Item2.GetType(), options);
			return;
		}
	}
}

