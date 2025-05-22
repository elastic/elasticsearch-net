// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport.Extensions;

namespace Elastic.Clients.Elasticsearch.Core.Bulk;

public abstract class BulkUpdateOperation :
	BulkOperation
{
	private static readonly System.Text.Json.JsonEncodedText PropId = System.Text.Json.JsonEncodedText.Encode("_id");
	private static readonly System.Text.Json.JsonEncodedText PropIfPrimaryTerm = System.Text.Json.JsonEncodedText.Encode("if_primary_term");
	private static readonly System.Text.Json.JsonEncodedText PropIfSeqNo = System.Text.Json.JsonEncodedText.Encode("if_seq_no");
	private static readonly System.Text.Json.JsonEncodedText PropIndex = System.Text.Json.JsonEncodedText.Encode("_index");
	private static readonly System.Text.Json.JsonEncodedText PropRequireAlias = System.Text.Json.JsonEncodedText.Encode("require_alias");
	private static readonly System.Text.Json.JsonEncodedText PropRetryOnConflict = System.Text.Json.JsonEncodedText.Encode("retry_on_conflict");
	private static readonly System.Text.Json.JsonEncodedText PropRouting = System.Text.Json.JsonEncodedText.Encode("routing");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("version");
	private static readonly System.Text.Json.JsonEncodedText PropVersionType = System.Text.Json.JsonEncodedText.Encode("version_type");

	protected internal BulkUpdateOperation() : base()
	{
	}

	public int? RetryOnConflict { get; set; }

	protected override string Operation => "update";

	protected abstract void BeforeSerialize(IElasticsearchClientSettings settings);

	protected override void Serialize(Stream stream, IElasticsearchClientSettings settings)
	{
		BeforeSerialize(settings);

		if (!settings.RequestResponseSerializer.TryGetJsonSerializerOptions(out var options))
		{
			throw new InvalidOperationException("unreachable");
		}

		using (var writer = new Utf8JsonWriter(stream))
		{
			SerializeOperationAction(writer, options);
			writer.Flush();
		}

		stream.WriteByte(SerializationConstants.Newline);
		var body = GetBody();

		using (var writer = new Utf8JsonWriter(stream))
		{
			body.Serialize(writer, options, settings);
			writer.Flush();
		}

		stream.Flush();
	}

	protected override async Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings)
	{
		BeforeSerialize(settings);

		if (!settings.RequestResponseSerializer.TryGetJsonSerializerOptions(out var options))
		{
			throw new InvalidOperationException("unreachable");
		}

		var writer = new Utf8JsonWriter(stream);
		await using (writer.ConfigureAwait(false))
		{
			SerializeOperationAction(writer, options);
			await writer.FlushAsync().ConfigureAwait(false);
		}

		stream.WriteByte(SerializationConstants.Newline);
		var body = GetBody();

		writer = new Utf8JsonWriter(stream);
		await using (writer.ConfigureAwait(false))
		{
			body.Serialize(writer, options, settings);
			await writer.FlushAsync().ConfigureAwait(false);
		}

		await stream.FlushAsync().ConfigureAwait(false);
	}

	private void SerializeOperationAction(Utf8JsonWriter writer, JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WritePropertyName(Operation);
		writer.WriteStartObject();
		writer.WriteProperty(options, PropId, Id, null, null);
		writer.WriteProperty(options, PropIfPrimaryTerm, IfPrimaryTerm, null, null);
		writer.WriteProperty(options, PropIfSeqNo, IfSequenceNumber, null, null);
		writer.WriteProperty(options, PropIndex, Index, null, null);
		writer.WriteProperty(options, PropRequireAlias, RequireAlias, null, null);
		writer.WriteProperty(options, PropRetryOnConflict, RetryOnConflict, null, null);
		writer.WriteProperty(options, PropRouting, Routing, null, null);
		writer.WriteProperty(options, PropVersion, Version, null, null);
		writer.WriteProperty(options, PropVersionType, VersionType, null, null);
		writer.WriteEndObject();
		writer.WriteEndObject();
	}

	private protected abstract BulkUpdateBody GetBody();
}

public abstract class BulkUpdateOperationDescriptorBase<TSource> :
	BulkOperationDescriptor<BulkUpdateOperationDescriptorBase<TSource>>
{
	protected BulkUpdateOperationDescriptorBase(BulkOperation instance) :
		base(instance)
	{
	}
}
