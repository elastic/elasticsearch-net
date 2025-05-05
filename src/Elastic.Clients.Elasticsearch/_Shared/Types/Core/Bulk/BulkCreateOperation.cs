// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Elastic.Transport.Extensions;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Core.Bulk;

/// <summary>
/// Represents a bulk operation to create a document.
/// </summary>
/// <typeparam name="T">The type representing the document being created.</typeparam>
public sealed class BulkCreateOperation<T> : BulkOperation
{
	/// <summary>
	/// Creates an instance of <see cref="BulkCreateOperation{T}"/> with the provided <typeparamref name="T"/> document serialized
	/// as source data.
	/// </summary>
	/// <param name="document">The <typeparamref name="T"/> document to index.</param>
	public BulkCreateOperation(T document) => Document = document;

	/// <summary>
	/// Creates an instance of <see cref="BulkCreateOperation{T}"/> with the provided <typeparamref name="T"/> document serialized
	/// as source data.
	/// </summary>
	/// <remarks>When an <see cref="IndexName"/> is provided, even if the value is <c>null</c>, no further inference will occur.
	/// <para>Setting null using this overload is considered explicit intent not to set an index for the operation. In this case
	/// it is expected that the bulk request will have defined an index to operate on.</para>
	/// </remarks>
	/// <param name="document">The <typeparamref name="T"/> document to index.</param>
	/// <param name="index">The <see cref="IndexName"/> which can represent an index, alias or data stream.</param>
	public BulkCreateOperation(T document, IndexName index) : this(document) => Index = index;

	/// <summary>
	/// ID of the pipeline to use to preprocess incoming documents.
	/// </summary>
	[JsonPropertyName("pipeline")]
	public string? Pipeline { get; set; }

	/// <summary>
	/// A dictionary from the full name of fields to the name of dynamic templates.
	/// If a name matches a dynamic template, then that template will be applied regardless of other match predicates
	/// defined in the template. And if a field is already defined in the mapping, then this parameter won’t be used.
	/// </summary>
	[JsonPropertyName("dynamic_templates")]
	public Dictionary<string, string>? DynamicTemplates { get; set; }

	/// <inheritdoc />
	protected override string Operation => "create";

	/// <summary>
	/// The source document.
	/// </summary>
	[JsonIgnore]
	public T Document { get; set; }

	protected override Type ClrType => typeof(T);

	/// <inheritdoc />
	protected override void Serialize(Stream stream, IElasticsearchClientSettings settings)
	{
		SetValues(settings);
		using var writer = new Utf8JsonWriter(stream);
		SerializeOperationAction(settings, writer);
		writer.Flush();
		stream.WriteByte(SerializationConstants.Newline);
		settings.SourceSerializer.Serialize(Document, stream);
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
			stream.WriteByte(SerializationConstants.Newline);
			await settings.SourceSerializer.SerializeAsync(Document, stream).ConfigureAwait(false);
		}
	}

	private void SetValues(IElasticsearchClientSettings settings)
	{
		// This allocates but avoids serialising "routing":null etc. into the operation action
		// Unfortunately, the alternative is we always set to new Routing(Document) which is then
		// never null even if the inferrer will be unable to return a value for the Routing during serialization

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

		if (settings.ExperimentalEnableSerializeNullInferredValues)
		{
			Id ??= new Id(Document);
		}
		else if (Id is null)
		{
			var id = new Id(Document);
			if (!string.IsNullOrEmpty(id.GetString(settings)))
				Id = id;
		}
	}

	private void SerializeOperationAction(IElasticsearchClientSettings settings, Utf8JsonWriter writer)
	{
		var requestResponseSerializer = settings.RequestResponseSerializer;
		writer.WriteStartObject();
		writer.WritePropertyName(Operation);
		requestResponseSerializer.Serialize(this, writer, settings.MemoryStreamFactory);
		writer.WriteEndObject();
	}
}
