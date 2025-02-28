// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Core.Bulk;

internal sealed class BulkResponseItemConverter : JsonConverter<ResponseItem>
{
	public override ResponseItem Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		reader.ValidateToken(JsonTokenType.StartObject);

		ResponseItem? result = null;

		while (reader.Read() && reader.TokenType is JsonTokenType.PropertyName)
		{
			var operation = reader.ReadPropertyName<OperationType>(options);
			reader.Read();

			result = operation switch
			{
				OperationType.Update => reader.ReadValue<BulkUpdateResponseItem>(options),
				OperationType.Index => reader.ReadValue<BulkIndexResponseItem>(options),
				OperationType.Delete => reader.ReadValue<BulkDeleteResponseItem>(options),
				OperationType.Create => reader.ReadValue<CreateResponseItem>(options),
				_ => throw new InvalidOperationException()
			};
		}

		return result!;
	}

	public override void Write(Utf8JsonWriter writer, ResponseItem value, JsonSerializerOptions options)
	{
		writer.WriteStartObject(value.Operation);
		writer.WriteValue(options, value);
		writer.WriteEndObject();
	}
}
