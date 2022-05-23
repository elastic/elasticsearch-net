// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch
{
	[JsonConverter(typeof(StoredScriptIdConveter))]
	public partial class StoredScriptId
	{
		public StoredScriptId(string id) => Id = id;
		public StoredScriptId(Id id) => Id = id;
	}

	internal sealed class StoredScriptIdConveter : JsonConverter<StoredScriptId>
	{
		public override StoredScriptId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			StoredScriptId? storedScriptId = null;

			Dictionary<string, object>? parameters = null;
			while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
			{
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					if (reader.ValueTextEquals("id"))
					{
						reader.Read();
						var value = JsonSerializer.Deserialize<Id>(ref reader, options);
						if (value is not null)
						{
							storedScriptId = new StoredScriptId(value);
						}

						continue;
					}

					if (reader.ValueTextEquals("params"))
					{
						reader.Read();
						var value = JsonSerializer.Deserialize<Dictionary<string, object>>(ref reader, options);
						if (value is not null)
						{
							parameters = value;
						}

						continue;
					}
				}
			}

			if (storedScriptId is not null)
			{
				storedScriptId.Params = null;
			}

			return storedScriptId;
		}

		public override void Write(Utf8JsonWriter writer, StoredScriptId value, JsonSerializerOptions options)
		{
			if (value is null)
			{
				writer.WriteNullValue();
				return;
			}

			writer.WriteStartObject();
			writer.WritePropertyName("id");
			JsonSerializer.Serialize(writer, value.Id, options);

			if (value.Params is not null)
			{
				writer.WritePropertyName("params");
				JsonSerializer.Serialize(writer, value.Params, options);
			}

			writer.WriteEndObject();
		}
	}
}
