// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using Elastic.Transport.Extensions;

namespace Elastic.Clients.Elasticsearch.Core.Bulk;

public abstract class BulkUpdateOperation : BulkOperation
{
	protected internal BulkUpdateOperation() : base() { }

	private static byte _newline => (byte)'\n';

	[JsonPropertyName("retry_on_conflict")]
	public int? RetryOnConflict { get; set; }

	protected override string Operation => "update";

	protected abstract void BeforeSerialize(IElasticsearchClientSettings settings);

	/// <summary>
	/// Serialise the operation action line for the NDJSON stream.
	/// </summary>
	/// <param name="writer"></param>
	/// <param name="options"></param>
	protected abstract void WriteOperation(Utf8JsonWriter writer, JsonSerializerOptions options = null);

	protected override void Serialize(Stream stream, IElasticsearchClientSettings settings)
	{
		BeforeSerialize(settings);

		var requestResponseSerializer = settings.RequestResponseSerializer;

		var internalWriter = new Utf8JsonWriter(stream);

		internalWriter.WriteStartObject();
		internalWriter.WritePropertyName(Operation);
		requestResponseSerializer.TryGetJsonSerializerOptions(out var options);
		WriteOperation(internalWriter, options);
		internalWriter.WriteEndObject();
		internalWriter.Flush();

		stream.WriteByte(_newline);

		var body = GetBody();
		settings.RequestResponseSerializer.Serialize(body, stream);
		stream.Flush();
	}

	protected override async Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings)
	{
		BeforeSerialize(settings);

		var internalWriter = new Utf8JsonWriter(stream);
		SerializeOperationAction(settings, internalWriter);
		internalWriter.Flush();

		stream.WriteByte(_newline);

		var body = GetBody();
		await settings.RequestResponseSerializer.SerializeAsync(body, stream).ConfigureAwait(false);
		await stream.FlushAsync().ConfigureAwait(false);
	}

	private void SerializeOperationAction(IElasticsearchClientSettings settings, Utf8JsonWriter writer)
	{
		var requestResponseSerializer = settings.RequestResponseSerializer;
		writer.WriteStartObject();
		writer.WritePropertyName(Operation);
		requestResponseSerializer.TryGetJsonSerializerOptions(out var options);
		WriteOperation(writer, options);
		writer.WriteEndObject();
	}

	protected abstract object GetBody();
}

public abstract class BulkUpdateOperationDescriptorBase<TSource> : BulkOperationDescriptor<BulkUpdateOperationDescriptorBase<TSource>>
{
	private static byte _newline => (byte)'\n';

	protected override string Operation => "update";

	protected abstract void BeforeSerialize(IElasticsearchClientSettings settings);

	protected abstract void WriteOperation(Utf8JsonWriter writer, JsonSerializerOptions options = null);

	protected override void Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None)
	{
		BeforeSerialize(settings);

		var requestResponseSerializer = settings.RequestResponseSerializer;

		var internalWriter = new Utf8JsonWriter(stream);

		internalWriter.WriteStartObject();
		internalWriter.WritePropertyName(Operation);
		requestResponseSerializer.TryGetJsonSerializerOptions(out var options);
		WriteOperation(internalWriter, options);
		internalWriter.WriteEndObject();
		internalWriter.Flush();

		stream.WriteByte(_newline);
		var body = GetBody();
		settings.RequestResponseSerializer.Serialize(body, stream, formatting);
	}

	protected override async Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting, CancellationToken cancellationToken = default)
	{
		BeforeSerialize(settings);

		var requestResponseSerializer = settings.RequestResponseSerializer;

		var internalWriter = new Utf8JsonWriter(stream);

		internalWriter.WriteStartObject();
		internalWriter.WritePropertyName(Operation);

		requestResponseSerializer.TryGetJsonSerializerOptions(out var options);
		WriteOperation(internalWriter, options);

		internalWriter.WriteEndObject();
		internalWriter.Flush();

		stream.WriteByte(_newline);

		var body = GetBody();

		await settings.SourceSerializer.SerializeAsync(body, stream, formatting).ConfigureAwait(false);
	}
}
