// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Elastic.Transport.Extensions;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Core.Bulk;

[JsonConverter(typeof(JsonIncompatibleConverter))]
public class BulkDeleteOperation :
	BulkOperation
{
	private static readonly System.Text.Json.JsonEncodedText PropId = System.Text.Json.JsonEncodedText.Encode("_id");
	private static readonly System.Text.Json.JsonEncodedText PropIfPrimaryTerm = System.Text.Json.JsonEncodedText.Encode("if_primary_term");
	private static readonly System.Text.Json.JsonEncodedText PropIfSeqNo = System.Text.Json.JsonEncodedText.Encode("if_seq_no");
	private static readonly System.Text.Json.JsonEncodedText PropIndex = System.Text.Json.JsonEncodedText.Encode("_index");
	private static readonly System.Text.Json.JsonEncodedText PropRouting = System.Text.Json.JsonEncodedText.Encode("routing");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("version");
	private static readonly System.Text.Json.JsonEncodedText PropVersionType = System.Text.Json.JsonEncodedText.Encode("version_type");

	public BulkDeleteOperation(Id id) => Id = id;

	/// <inheritdoc />
	protected override void Serialize(Stream stream, IElasticsearchClientSettings settings)
	{
		SetValues(settings);
		using var writer = new Utf8JsonWriter(stream);
		SerializeOperationAction(settings, writer);
		writer.Flush();
	}

	/// <inheritdoc />
	protected override async Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings)
	{
		SetValues(settings);
		var writer = new Utf8JsonWriter(stream);
		await using (writer.ConfigureAwait(false))
		{
			SerializeOperationAction(settings, writer);
			await writer.FlushAsync().ConfigureAwait(false);
		}
	}

	private void SerializeOperationAction(IElasticsearchClientSettings settings, Utf8JsonWriter writer)
	{
		if (!settings.RequestResponseSerializer.TryGetJsonSerializerOptions(out var options))
		{
			throw new InvalidOperationException("unreachable");
		}

		writer.WriteStartObject();
		writer.WritePropertyName(Operation);
		writer.WriteStartObject();
		writer.WriteProperty(options, PropId, Id, null, null);
		writer.WriteProperty(options, PropIfPrimaryTerm, IfPrimaryTerm, null, null);
		writer.WriteProperty(options, PropIfSeqNo, IfSequenceNumber, null, null);
		writer.WriteProperty(options, PropIndex, Index, null, null);
		writer.WriteProperty(options, PropRouting, Routing, null, null);
		writer.WriteProperty(options, PropVersion, Version, null, null);
		writer.WriteProperty(options, PropVersionType, VersionType, null, null);
		writer.WriteEndObject();
		writer.WriteEndObject();
	}

	protected virtual void SetValues(IElasticsearchClientSettings settings)
	{
	}

	protected override string Operation => "delete";

	protected override Type ClrType => null;
}

public sealed class BulkDeleteOperation<T> :
	BulkDeleteOperation
{
	public BulkDeleteOperation(T document) : base(new Id(document))
		=> Document = document;

	public BulkDeleteOperation(Id id) : base(id)
	{
	}

	[JsonIgnore]
	public T Document { get; set; }

	protected override void SetValues(IElasticsearchClientSettings settings)
	{
		if (settings.ExperimentalEnableSerializeNullInferredValues)
		{
			Routing ??= new Routing(Document);
		}
		else if (Routing is null)
		{
			var routing = new Routing(Document);
			if (!string.IsNullOrEmpty(routing.GetString(settings)))
				Routing = routing;
		}

		Index ??= typeof(T);
	}

	protected override Type ClrType => typeof(T);
}
