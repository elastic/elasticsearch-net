// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch.Core.Bulk;
using Elastic.Clients.Elasticsearch.Core.Search;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch;

/// <summary>
/// Reads the NDJSON body of a bulk request back into a <see cref="BulkRequest"/>. The body is a sequence of
/// operations, each an action-header line (<c>{ "index": { …meta… } }</c>) optionally followed by a source/body
/// line (index/create/update; delete is header-only). Accepts either a JSON array of line values or genuine
/// multi-top-level-value NDJSON (the enclosing reader is created with <c>AllowMultipleValues</c>). Documents are read
/// as raw <see cref="JsonElement"/> (stored through the <c>object</c> operation generic). Writing flows through the
/// <see cref="IStreamSerializable"/> path, so <see cref="Write"/> is not used on the normal request path.
/// </summary>
public sealed class BulkRequestConverter : JsonConverter<BulkRequest>
{
	public override BulkRequest Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var operations = new BulkOperationsCollection();

		var isArray = reader.TokenType is JsonTokenType.StartArray;
		if (isArray)
			reader.Read();

		while (reader.TokenType is not JsonTokenType.EndArray)
		{
			operations.Add(ReadOperation(ref reader, options));

			// Advance to the next action-header (array element or top-level value); stop at the end of the stream.
			if (!reader.Read())
				break;
		}

		return new BulkRequest(JsonConstructorSentinel.Instance) { Operations = operations };
	}

	public override void Write(Utf8JsonWriter writer, BulkRequest value, JsonSerializerOptions options) =>
		throw new NotSupportedException("'BulkRequest' is written as NDJSON through 'IStreamSerializable', not via this converter.");

	private static IBulkOperation ReadOperation(ref Utf8JsonReader reader, JsonSerializerOptions options)
	{
		// Action-header line: a single-property object whose name is the operation type and whose value is the metadata.
		reader.Read(); // PropertyName (operation type)
		var operationType = reader.GetString()!;
		reader.Read(); // metadata StartObject

		Id? id = null;
		IndexName? index = null;
		Routing? routing = null;
		long? version = null;
		VersionType? versionType = null;
		long? ifSeqNo = null;
		long? ifPrimaryTerm = null;
		bool? requireAlias = null;
		string? pipeline = null;
		IDictionary<string, string>? dynamicTemplates = null;
		int? retryOnConflict = null;

		while (reader.Read() && reader.TokenType is JsonTokenType.PropertyName)
		{
			if (reader.ValueTextEquals("_id"u8)) { reader.Read(); id = reader.ReadValue<Id>(options); }
			else if (reader.ValueTextEquals("_index"u8)) { reader.Read(); index = reader.ReadValue<IndexName>(options); }
			else if (reader.ValueTextEquals("routing"u8)) { reader.Read(); routing = reader.ReadValue<Routing>(options); }
			else if (reader.ValueTextEquals("version"u8)) { reader.Read(); version = reader.ReadNullableValue<long>(options); }
			else if (reader.ValueTextEquals("version_type"u8)) { reader.Read(); versionType = reader.ReadNullableValue<VersionType>(options); }
			else if (reader.ValueTextEquals("if_seq_no"u8)) { reader.Read(); ifSeqNo = reader.ReadNullableValue<long>(options); }
			else if (reader.ValueTextEquals("if_primary_term"u8)) { reader.Read(); ifPrimaryTerm = reader.ReadNullableValue<long>(options); }
			else if (reader.ValueTextEquals("require_alias"u8)) { reader.Read(); requireAlias = reader.ReadNullableValue<bool>(options); }
			else if (reader.ValueTextEquals("pipeline"u8)) { reader.Read(); pipeline = reader.GetString(); }
			else if (reader.ValueTextEquals("dynamic_templates"u8)) { reader.Read(); dynamicTemplates = reader.ReadDictionaryValue<string, string>(options, null, null); }
			else if (reader.ValueTextEquals("retry_on_conflict"u8)) { reader.Read(); retryOnConflict = reader.ReadNullableValue<int>(options); }
			else { reader.Read(); reader.Skip(); }
		}

		// reader is now on the metadata EndObject; step out to the action-header EndObject.
		reader.Read();

		switch (operationType)
		{
			case "index":
			{
				reader.Read(); // source line
				var op = new BulkIndexOperation<object>(reader.ReadValue<JsonElement>(options)) { Pipeline = pipeline, DynamicTemplates = dynamicTemplates };
				ApplyMetadata(op, id, index, routing, version, versionType, ifSeqNo, ifPrimaryTerm, requireAlias);
				return op;
			}

			case "create":
			{
				reader.Read(); // source line
				var op = new BulkCreateOperation<object>(reader.ReadValue<JsonElement>(options)) { Pipeline = pipeline, DynamicTemplates = dynamicTemplates };
				ApplyMetadata(op, id, index, routing, version, versionType, ifSeqNo, ifPrimaryTerm, requireAlias);
				return op;
			}

			case "update":
			{
				reader.Read(); // update body line
				var op = new BulkUpdateOperation<object, object>(id!) { RetryOnConflict = retryOnConflict };
				ApplyMetadata(op, id, index, routing, version, versionType, ifSeqNo, ifPrimaryTerm, requireAlias);
				ReadUpdateBody(ref reader, options, op);
				return op;
			}

			case "delete":
			{
				var op = new BulkDeleteOperation(id!);
				ApplyMetadata(op, id, index, routing, version, versionType, ifSeqNo, ifPrimaryTerm, requireAlias);
				return op;
			}

			default:
				throw new JsonException($"Unknown bulk operation type '{operationType}'.");
		}
	}

	private static void ApplyMetadata(BulkOperation op, Id? id, IndexName? index, Routing? routing, long? version,
		VersionType? versionType, long? ifSeqNo, long? ifPrimaryTerm, bool? requireAlias)
	{
		op.Id = id;
		op.Index = index;
		op.Routing = routing;
		op.Version = version;
		op.VersionType = versionType;
		op.IfSequenceNumber = ifSeqNo;
		op.IfPrimaryTerm = ifPrimaryTerm;
		op.RequireAlias = requireAlias;
	}

	private static void ReadUpdateBody(ref Utf8JsonReader reader, JsonSerializerOptions options, BulkUpdateOperation<object, object> op)
	{
		// reader is on the update body StartObject.
		while (reader.Read() && reader.TokenType is JsonTokenType.PropertyName)
		{
			if (reader.ValueTextEquals("doc"u8))
			{
				reader.Read();
				var doc = reader.ReadValue<JsonElement>(options);
				if (doc.ValueKind is not JsonValueKind.Undefined)
					op.Doc = doc;
			}
			else if (reader.ValueTextEquals("upsert"u8))
			{
				reader.Read();
				var upsert = reader.ReadValue<JsonElement>(options);
				if (upsert.ValueKind is not JsonValueKind.Undefined)
					op.Upsert = upsert;
			}
			else if (reader.ValueTextEquals("script"u8)) { reader.Read(); op.Script = reader.ReadValue<Script>(options); }
			else if (reader.ValueTextEquals("doc_as_upsert"u8)) { reader.Read(); op.DocAsUpsert = reader.ReadNullableValue<bool>(options); }
			else if (reader.ValueTextEquals("scripted_upsert"u8)) { reader.Read(); op.ScriptedUpsert = reader.ReadNullableValue<bool>(options); }
			else if (reader.ValueTextEquals("_source"u8))
			{
				reader.Read();
				// An update body '_source' is bool | SourceFilter (the client's Union<bool, SourceFilter>). Read by
				// token: the union's converter does not read the bool arm, so a bare 'true'/'false' must be taken
				// directly. Other shapes (e.g. a field-list array) are not representable by this union.
				if (reader.TokenType is JsonTokenType.True or JsonTokenType.False)
					op.Source = reader.GetBoolean();
				else
					op.Source = reader.ReadValue<SourceFilter>(options);
			}
			else if (reader.ValueTextEquals("if_seq_no"u8)) { reader.Read(); op.IfSequenceNumber = reader.ReadNullableValue<long>(options); }
			else if (reader.ValueTextEquals("if_primary_term"u8)) { reader.Read(); op.IfPrimaryTerm = reader.ReadNullableValue<long>(options); }
			else { reader.Read(); reader.Skip(); }
		}
		// reader is on the update body EndObject.
	}
}
