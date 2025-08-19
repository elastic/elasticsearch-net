// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Core.Bulk.Json;

public sealed class BulkResponseItemConverter : JsonConverter<ResponseItem>
{
	private static readonly JsonEncodedText PropError = JsonEncodedText.Encode("error");
	private static readonly JsonEncodedText PropForcedRefresh = JsonEncodedText.Encode("forced_refresh");
	private static readonly JsonEncodedText PropGet = JsonEncodedText.Encode("get");
	private static readonly JsonEncodedText PropId = JsonEncodedText.Encode("_id");
	private static readonly JsonEncodedText PropIndex = JsonEncodedText.Encode("_index");
	private static readonly JsonEncodedText PropPrimaryTerm = JsonEncodedText.Encode("_primary_term");
	private static readonly JsonEncodedText PropResult = JsonEncodedText.Encode("result");
	private static readonly JsonEncodedText PropSeqNo = JsonEncodedText.Encode("_seq_no");
	private static readonly JsonEncodedText PropShards = JsonEncodedText.Encode("_shards");
	private static readonly JsonEncodedText PropStatus = JsonEncodedText.Encode("status");
	private static readonly JsonEncodedText PropVersion = JsonEncodedText.Encode("_version");

	public override ResponseItem Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		reader.ValidateToken(JsonTokenType.StartObject);

		reader.Read();

		reader.ValidateToken(JsonTokenType.PropertyName);

		var operation = reader.ReadPropertyName<OperationType>(options);
		reader.Read();

		reader.ValidateToken(JsonTokenType.StartObject);

		LocalJsonValue<ErrorCause?> propError = default;
		LocalJsonValue<bool?> propForcedRefresh = default;
		LocalJsonValue<InlineGet<System.Collections.Generic.IReadOnlyDictionary<string, object>>?> propGet = default;
		LocalJsonValue<string?> propId = default;
		LocalJsonValue<string> propIndex = default;
		LocalJsonValue<long?> propPrimaryTerm = default;
		LocalJsonValue<string?> propResult = default;
		LocalJsonValue<long?> propSeqNo = default;
		LocalJsonValue<ShardStatistics?> propShards = default;
		LocalJsonValue<int> propStatus = default;
		LocalJsonValue<long?> propVersion = default;

		while (reader.Read() && reader.TokenType is JsonTokenType.PropertyName)
		{
			if (propError.TryReadProperty(ref reader, options, PropError, null))
			{
				continue;
			}

			if (propForcedRefresh.TryReadProperty(ref reader, options, PropForcedRefresh, null))
			{
				continue;
			}

			if (propGet.TryReadProperty(ref reader, options, PropGet, null))
			{
				continue;
			}

			if (propId.TryReadProperty(ref reader, options, PropId, null))
			{
				continue;
			}

			if (propIndex.TryReadProperty(ref reader, options, PropIndex, null))
			{
				continue;
			}

			if (propPrimaryTerm.TryReadProperty(ref reader, options, PropPrimaryTerm, null))
			{
				continue;
			}

			if (propResult.TryReadProperty(ref reader, options, PropResult, null))
			{
				continue;
			}

			if (propSeqNo.TryReadProperty(ref reader, options, PropSeqNo, null))
			{
				continue;
			}

			if (propShards.TryReadProperty(ref reader, options, PropShards, null))
			{
				continue;
			}

			if (propStatus.TryReadProperty(ref reader, options, PropStatus, null))
			{
				continue;
			}

			if (propVersion.TryReadProperty(ref reader, options, PropVersion, null))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
			}

			throw new JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(JsonTokenType.EndObject);

		reader.Read();

		reader.ValidateToken(JsonTokenType.EndObject);

		ResponseItem result = operation switch
		{
			OperationType.Update => new BulkUpdateResponseItem(JsonConstructorSentinel.Instance),
			OperationType.Index => new BulkIndexResponseItem(JsonConstructorSentinel.Instance),
			OperationType.Delete => new BulkDeleteResponseItem(JsonConstructorSentinel.Instance),
			OperationType.Create => new CreateResponseItem(JsonConstructorSentinel.Instance),
			_ => throw new InvalidOperationException()
		};

		result.Error = propError.Value;
		result.ForcedRefresh = propForcedRefresh.Value;
		result.Get = propGet.Value;
		result.Id = propId.Value;
		result.Index = propIndex.Value;
		result.PrimaryTerm = propPrimaryTerm.Value;
		result.Result = propResult.Value;
		result.SeqNo = propSeqNo.Value;
		result.Shards = propShards.Value;
		result.Status = propStatus.Value;
		result.Version = propVersion.Value;

		return result;
	}

	public override void Write(Utf8JsonWriter writer, ResponseItem value, JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WritePropertyName(value.Operation);
		writer.WriteStartObject();
		writer.WriteProperty(options, PropError, value.Error, null, null);
		writer.WriteProperty(options, PropForcedRefresh, value.ForcedRefresh, null, null);
		writer.WriteProperty(options, PropGet, value.Get, null, null);
		writer.WriteProperty(options, PropId, value.Id, null, null);
		writer.WriteProperty(options, PropIndex, value.Index, null, null);
		writer.WriteProperty(options, PropPrimaryTerm, value.PrimaryTerm, null, null);
		writer.WriteProperty(options, PropResult, value.Result, null, null);
		writer.WriteProperty(options, PropSeqNo, value.SeqNo, null, null);
		writer.WriteProperty(options, PropShards, value.Shards, null, null);
		writer.WriteProperty(options, PropStatus, value.Status, null, null);
		writer.WriteProperty(options, PropVersion, value.Version, null, null);
		writer.WriteEndObject();
		writer.WriteEndObject();
	}
}
