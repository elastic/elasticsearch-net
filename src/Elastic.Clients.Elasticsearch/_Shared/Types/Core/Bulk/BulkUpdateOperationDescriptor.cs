// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;
using Elastic.Transport.Extensions;
using Elastic.Clients.Elasticsearch.Core.Search;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Core.Bulk;

public sealed class BulkUpdateOperationDescriptor<TDocument, TPartialDocument> : BulkOperationDescriptor<BulkUpdateOperationDescriptor<TDocument, TPartialDocument>>
{
	private static byte _newline => (byte)'\n';

	public BulkUpdateOperationDescriptor() { }

	public BulkUpdateOperationDescriptor(Id id) => Id(id);

	private TPartialDocument _document;
	private TDocument _idFrom;
	private TDocument _upsert;
	private bool? _docAsUpsert;
	private bool? _scriptedUpsert;
	private int? _retriesOnConflict;
	private Script _script;
	private Union<bool, SourceFilter> _source;

	private Action<ScriptDescriptor> _scriptAction;

	protected override string Operation => "update";

	protected override Type ClrType => typeof(TDocument);

	public BulkUpdateOperationDescriptor<TDocument, TPartialDocument> Doc(TPartialDocument document) => Assign(document, (a, v) => a._document = v);

	public BulkUpdateOperationDescriptor<TDocument, TPartialDocument> DocAsUpsert(bool? partialDocumentAsUpsert = true) =>
		Assign(partialDocumentAsUpsert, (a, v) => a._docAsUpsert = v);

	public BulkUpdateOperationDescriptor<TDocument, TPartialDocument> IdFrom(TDocument idFrom, bool useAsUpsert = false)
	{
		Assign(idFrom, (a, v) => a._idFrom = v);
		return useAsUpsert ? Upsert(idFrom) : this;
	}

	public BulkUpdateOperationDescriptor<TDocument, TPartialDocument> RetriesOnConflict(int? retriesOnConflict) =>
		Assign(retriesOnConflict, (a, v) => a._retriesOnConflict = v);

	public BulkUpdateOperationDescriptor<TDocument, TPartialDocument> Script(Action<ScriptDescriptor> configure)
	{
		_script = null;

		return Assign(configure, (a, v) => a._scriptAction = v);
	}

	public BulkUpdateOperationDescriptor<TDocument, TPartialDocument> Script(Script script)
	{
		_scriptAction = null;

		return Assign(script, (a, v) => a._script = v);
	}

	public BulkUpdateOperationDescriptor<TDocument, TPartialDocument> ScriptedUpsert(bool? scriptedUpsert = true) =>
		Assign(scriptedUpsert, (a, v) => a._scriptedUpsert = v);

	public BulkUpdateOperationDescriptor<TDocument, TPartialDocument> Source(Union<bool, SourceFilter> source) =>
		Assign(source, (a, v) => a._source = v);

	public BulkUpdateOperationDescriptor<TDocument, TPartialDocument> Upsert(TDocument document) => Assign(document, (a, v) => a._upsert = v);

	protected override object GetBody()
	{
		if (_scriptAction is not null)
			return null;

		return new BulkUpdateBody<TDocument, TPartialDocument>
		{
			PartialUpdate = _document,
			Script = _script,
			Upsert = _upsert,
			DocAsUpsert = _docAsUpsert,
			ScriptedUpsert = _scriptedUpsert,
			IfPrimaryTerm = IfPrimaryTermValue,
			IfSequenceNumber = IfSequenceNoValue,
			Source = _source
		};
	}

	protected override void Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting)
	{
		var requestResponseSerializer = settings.RequestResponseSerializer;

		var writer = new Utf8JsonWriter(stream);

		writer.WriteStartObject();
		writer.WritePropertyName(Operation);

		requestResponseSerializer.TryGetJsonSerializerOptions(out var options);
		JsonSerializer.Serialize<BulkUpdateOperationDescriptor<TDocument, TPartialDocument>>(writer, this, options);

		writer.WriteEndObject();
		writer.Flush();

		stream.WriteByte(_newline);

		var body = GetBody();

		// In the simple path, we have a BulkUpdateBody we can simply serialise.
		// Only if we have some deferred delegates to configure descriptors so we need to manually serialise the data.
		if (body is not null)
		{
			settings.RequestResponseSerializer.Serialize(body, stream, formatting);
		}
		else
		{
			writer = new Utf8JsonWriter(stream);
			writer.WriteStartObject();

			WriteBody(settings, writer, options);

			writer.WriteEndObject();
			writer.Flush();
		}
	}

	private void WriteBody(IElasticsearchClientSettings settings, Utf8JsonWriter writer, JsonSerializerOptions options)
	{
		if (_scriptAction is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, new ScriptDescriptor(_scriptAction), options);
		}

		if (_document is not null)
		{
			writer.WritePropertyName("doc");
			settings.SourceSerializer.Serialize(_document, writer, null);
		}

		if (_scriptedUpsert.HasValue)
		{
			writer.WritePropertyName("scripted_upsert");
			JsonSerializer.Serialize(writer, _scriptedUpsert.Value, options);
		}

		if (_docAsUpsert.HasValue)
		{
			writer.WritePropertyName("doc_as_upsert");
			JsonSerializer.Serialize(writer, _docAsUpsert.Value, options);
		}

		if (_upsert is not null)
		{
			writer.WritePropertyName("upsert");
			settings.SourceSerializer.Serialize(_upsert, writer, null);
		}

		if (_source is not null)
		{
			writer.WritePropertyName("_source");
			JsonSerializer.Serialize(writer, Source, options);
		}
	}

	protected override async Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting, CancellationToken cancellationToken = default)
	{
		var requestResponseSerializer = settings.RequestResponseSerializer;

		var writer = new Utf8JsonWriter(stream);

		writer.WriteStartObject();
		writer.WritePropertyName(Operation);

		requestResponseSerializer.TryGetJsonSerializerOptions(out var options);
		JsonSerializer.Serialize<BulkUpdateOperationDescriptor<TDocument, TPartialDocument>>(writer, this, options);

		writer.WriteEndObject();
		await writer.FlushAsync(cancellationToken).ConfigureAwait(false);

		stream.WriteByte(_newline);

		var body = GetBody();

		// In the simple path, we have a BulkUpdateBody we can simply serialise.
		// Only if we have some deferred delegates to configure descriptors so we need to manually serialise the data.
		if (body is not null)
		{
			await settings.RequestResponseSerializer.SerializeAsync(body, stream, formatting, cancellationToken).ConfigureAwait(false);
		}
		else
		{
			writer = new Utf8JsonWriter(stream);
			writer.WriteStartObject();

			WriteBody(settings, writer, options);

			writer.WriteEndObject();
			await writer.FlushAsync(cancellationToken).ConfigureAwait(false);
		}
	}

	protected override void SerializeInternal(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		if (_retriesOnConflict.HasValue)
		{
			writer.WritePropertyName("retry_on_conflict");
			writer.WriteNumberValue(_retriesOnConflict.Value);
		}
	}

	protected override Id GetIdForOperation(Inferrer inferrer) =>
		IdValue ?? new Id(new object[] { _idFrom, _upsert }.FirstOrDefault(o => o != null));

	protected override Routing GetRoutingForOperation(Inferrer inferrer)
	{
		if (RoutingValue is not null)
			return RoutingValue;

		if (_idFrom != null)
			return new Routing(_idFrom);

		if (_upsert != null)
			return new Routing(_upsert);

		return null;
	}
}
