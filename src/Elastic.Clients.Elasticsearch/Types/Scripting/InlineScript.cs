// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Text.Json;

namespace Elastic.Clients.Elasticsearch;

public partial class InlineScript : ISelfTwoWaySerializable
{
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
					var value = JsonSerializer.Deserialize<Dictionary<string, object>>(ref reader, options);
					if (value is not null)
					{
						Params = value;
					}

					continue;
				}
			}
		}
	}

	public InlineScript(string source) => Source = source;

	internal InlineScript() { }
}
