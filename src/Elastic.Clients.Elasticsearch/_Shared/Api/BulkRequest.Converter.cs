// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

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
/// <remarks>
/// When the body arrives as a <see cref="Stream"/> (the <see cref="INdjsonStreamReadable"/> path), the nested
/// <c>StreamAssembly</c> drives <see cref="NdjsonValueReader"/> one top-level value at a time, so the whole body is
/// never buffered. Both paths share the same per-operation parsing (<c>ReadActionHeaderObject</c> for the header,
/// <c>CompleteOperation</c> for the source/body), with the span-based <c>ReadActionHeader</c> / <c>CompleteOperation</c>
/// entry points isolating one value for the streaming reader.
/// </remarks>
public sealed class BulkRequestConverter : JsonConverter<BulkRequest>, INdjsonStreamReadable
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

	object INdjsonStreamReadable.Read(Stream stream, JsonSerializerOptions options)
	{
		var assembly = new StreamAssembly(options);
		NdjsonValueReader.DriveStream(stream, NdjsonValueReader.BuildReaderOptions(options), assembly.Visit);
		return assembly.Build();
	}

	async ValueTask<object> INdjsonStreamReadable.ReadAsync(Stream stream, JsonSerializerOptions options, CancellationToken cancellationToken)
	{
		var assembly = new StreamAssembly(options);
		await NdjsonValueReader.DriveStreamAsync(stream, NdjsonValueReader.BuildReaderOptions(options), assembly.Visit, cancellationToken).ConfigureAwait(false);
		return assembly.Build();
	}

	// Builds the request by pairing each streamed value with the next: a 'delete' header completes on its own; every
	// other action header pends until its source value arrives. The header is materialized the moment it is seen, so no
	// byte slice is held across a buffer refill.
	private sealed class StreamAssembly(JsonSerializerOptions options)
	{
		private readonly BulkOperationsCollection _operations = new();
		private BulkActionHeader? _pending;

		public void Visit(ReadOnlySequence<byte> value, int index)
		{
			if (_pending is null)
			{
				var header = ReadHeader(value, options);

				if (header.OperationType is "delete")
					_operations.Add(CompleteFrom(in header, default, options));
				else
					_pending = header;

				return;
			}

			var pending = _pending.Value;
			_operations.Add(CompleteFrom(in pending, value, options));
			_pending = null;
		}

		public BulkRequest Build()
		{
			if (_pending is not null)
				throw new JsonException($"Expected a source line following the '{_pending.Value.OperationType}' bulk action header.");

			return new BulkRequest(JsonConstructorSentinel.Instance) { Operations = _operations };
		}

		private static BulkActionHeader ReadHeader(in ReadOnlySequence<byte> value, JsonSerializerOptions options)
		{
			if (value.IsSingleSegment)
				return ReadActionHeader(value.First.Span, options);

			var length = (int)value.Length;
			var rented = ArrayPool<byte>.Shared.Rent(length);
			try
			{
				value.CopyTo(rented);
				return ReadActionHeader(rented.AsSpan(0, length), options);
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(rented);
			}
		}

		private static IBulkOperation CompleteFrom(in BulkActionHeader header, in ReadOnlySequence<byte> source, JsonSerializerOptions options)
		{
			if (source.IsSingleSegment)
				return CompleteOperation(in header, source.First.Span, options);

			var length = (int)source.Length;
			var rented = ArrayPool<byte>.Shared.Rent(length);
			try
			{
				source.CopyTo(rented);
				return CompleteOperation(in header, rented.AsSpan(0, length), options);
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(rented);
			}
		}
	}

	private static IBulkOperation ReadOperation(ref Utf8JsonReader reader, JsonSerializerOptions options)
	{
		var header = ReadActionHeaderObject(ref reader, options);

		// reader is on the action-header EndObject. index/create/update consume the following source value; delete does not.
		if (header.OperationType is not "delete")
			reader.Read();

		return CompleteOperation(in header, ref reader, options);
	}

	/// <summary>
	/// Reads a single bulk action-header value (<c>{ "index": { …meta… } }</c>) into a contiguous span. Used by the
	/// streaming reader, which isolates one top-level value at a time.
	/// </summary>
	private static BulkActionHeader ReadActionHeader(ReadOnlySpan<byte> headerSpan, JsonSerializerOptions options)
	{
		var reader = new Utf8JsonReader(headerSpan, new JsonReaderOptions { MaxDepth = options.MaxDepth });
		reader.Read(); // action-header StartObject
		return ReadActionHeaderObject(ref reader, options);
	}

	/// <summary>
	/// Builds the operation from a parsed header and its source value held in a contiguous span (empty for delete).
	/// Used by the streaming reader.
	/// </summary>
	private static IBulkOperation CompleteOperation(in BulkActionHeader header, ReadOnlySpan<byte> sourceSpan, JsonSerializerOptions options)
	{
		var sourceReader = new Utf8JsonReader(sourceSpan, new JsonReaderOptions { MaxDepth = options.MaxDepth });
		if (header.OperationType is not "delete")
			sourceReader.Read();

		return CompleteOperation(in header, ref sourceReader, options);
	}

	// Reads the action-header object. The reader must be positioned on its StartObject; on return it sits on the
	// action-header EndObject. Only materialized values are captured, so the result can outlive the reader's buffer.
	private static BulkActionHeader ReadActionHeaderObject(ref Utf8JsonReader reader, JsonSerializerOptions options)
	{
		reader.Read(); // PropertyName (operation type)
		var header = new BulkActionHeader { OperationType = reader.GetString()! };
		reader.Read(); // metadata StartObject

		while (reader.Read() && reader.TokenType is JsonTokenType.PropertyName)
		{
			if (reader.ValueTextEquals("_id"u8)) { reader.Read(); header.Id = reader.ReadValue<Id>(options); }
			else if (reader.ValueTextEquals("_index"u8)) { reader.Read(); header.Index = reader.ReadValue<IndexName>(options); }
			else if (reader.ValueTextEquals("routing"u8)) { reader.Read(); header.Routing = reader.ReadValue<Routing>(options); }
			else if (reader.ValueTextEquals("version"u8)) { reader.Read(); header.Version = reader.ReadNullableValue<long>(options); }
			else if (reader.ValueTextEquals("version_type"u8)) { reader.Read(); header.VersionType = reader.ReadNullableValue<VersionType>(options); }
			else if (reader.ValueTextEquals("if_seq_no"u8)) { reader.Read(); header.IfSeqNo = reader.ReadNullableValue<long>(options); }
			else if (reader.ValueTextEquals("if_primary_term"u8)) { reader.Read(); header.IfPrimaryTerm = reader.ReadNullableValue<long>(options); }
			else if (reader.ValueTextEquals("require_alias"u8)) { reader.Read(); header.RequireAlias = reader.ReadNullableValue<bool>(options); }
			else if (reader.ValueTextEquals("pipeline"u8)) { reader.Read(); header.Pipeline = reader.GetString(); }
			else if (reader.ValueTextEquals("dynamic_templates"u8)) { reader.Read(); header.DynamicTemplates = reader.ReadDictionaryValue<string, string>(options, null, null); }
			else if (reader.ValueTextEquals("retry_on_conflict"u8)) { reader.Read(); header.RetryOnConflict = reader.ReadNullableValue<int>(options); }
			else { reader.Read(); reader.Skip(); }
		}

		// reader is now on the metadata EndObject; step out to the action-header EndObject.
		reader.Read();
		return header;
	}

	// Builds the operation from a parsed header and a source reader positioned on the source value's first token
	// (unused for delete).
	private static IBulkOperation CompleteOperation(in BulkActionHeader header, ref Utf8JsonReader sourceReader, JsonSerializerOptions options)
	{
		switch (header.OperationType)
		{
			case "index":
			{
				var op = new BulkIndexOperation<object>(sourceReader.ReadValue<JsonElement>(options)) { Pipeline = header.Pipeline, DynamicTemplates = header.DynamicTemplates };
				ApplyMetadata(op, in header);
				return op;
			}

			case "create":
			{
				var op = new BulkCreateOperation<object>(sourceReader.ReadValue<JsonElement>(options)) { Pipeline = header.Pipeline, DynamicTemplates = header.DynamicTemplates };
				ApplyMetadata(op, in header);
				return op;
			}

			case "update":
			{
				var op = new BulkUpdateOperation<object, object>(header.Id!) { RetryOnConflict = header.RetryOnConflict };
				ApplyMetadata(op, in header);
				ReadUpdateBody(ref sourceReader, options, op);
				return op;
			}

			case "delete":
			{
				var op = new BulkDeleteOperation(header.Id!);
				ApplyMetadata(op, in header);
				return op;
			}

			default:
				throw new JsonException($"Unknown bulk operation type '{header.OperationType}'.");
		}
	}

	// The base-class metadata shared by every operation. 'Id' is passed to the constructor for update/delete (they
	// have no parameterless constructor), so it is emitted in the initializer only for index/create.
	private static void ApplyMetadata(BulkOperation op, in BulkActionHeader header)
	{
		op.Id = header.Id;
		op.Index = header.Index;
		op.Routing = header.Routing;
		op.Version = header.Version;
		op.VersionType = header.VersionType;
		op.IfSequenceNumber = header.IfSeqNo;
		op.IfPrimaryTerm = header.IfPrimaryTerm;
		op.RequireAlias = header.RequireAlias;
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

/// <summary>
/// The parsed contents of a bulk action header (<c>{ "index": { …metadata… } }</c>): the operation type plus the
/// metadata preceding the operation's optional source line. Holds only materialized values, so the streaming reader can
/// carry it from the header value to the source value across a buffer refill.
/// </summary>
internal struct BulkActionHeader
{
	public string OperationType;
	public Id? Id;
	public IndexName? Index;
	public Routing? Routing;
	public long? Version;
	public VersionType? VersionType;
	public long? IfSeqNo;
	public long? IfPrimaryTerm;
	public bool? RequireAlias;
	public string? Pipeline;
	public IDictionary<string, string>? DynamicTemplates;
	public int? RetryOnConflict;
}
