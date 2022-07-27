// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch;

public sealed partial class InlineScript : ISelfTwoWaySerializable
{
	// This type is ISelfTwoWaySerializable because it potentially uses the source serialiser for params serialisation

	public InlineScript(string source) => Source = source;

	public InlineScript() { }

	void ISelfTwoWaySerializable.Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();

		if (Language is not null)
		{
			writer.WritePropertyName("lang");
			JsonSerializer.Serialize(writer, Language, options);
		}

		if (Options is not null)
		{
			writer.WritePropertyName("options");
			JsonSerializer.Serialize(writer, Options, options);
		}

		writer.WritePropertyName("source");
		writer.WriteStringValue(Source);

		if (Params is not null)
		{
			writer.WritePropertyName("params");
			SourceSerialisation.SerializeParams(Params, writer, settings);
		}

		writer.WriteEndObject();
	}

	void ISelfTwoWaySerializable.Deserialize(ref Utf8JsonReader reader, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
		{
			if (reader.TokenType == JsonTokenType.PropertyName)
			{
				if (reader.ValueTextEquals("lang"))
				{
					reader.Read();
					var value = reader.GetString();
					if (value is not null)
					{
						Language = value;
					}

					continue;
				}

				if (reader.ValueTextEquals("options"))
				{
					var value = JsonSerializer.Deserialize<Dictionary<string, string>>(ref reader, options);
					if (value is not null)
					{
						Options = value;
					}

					continue;
				}

				if (reader.ValueTextEquals("source"))
				{
					reader.Read();
					var value = reader.GetString();
					if (value is not null)
					{
						Source = value;
					}

					continue;
				}

				if (reader.ValueTextEquals("params"))
				{
					var value = SourceSerialisation.DeserializeParams<Dictionary<string, object>>(ref reader, settings);
					if (value is not null)
					{
						Params = value;
					}

					continue;
				}
			}
		}
	}
}
