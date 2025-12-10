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
[JsonConverter(typeof(JsonIncompatibleConverter))]
public sealed class BulkCreateOperation<T> : BulkOperation
{
	private static readonly System.Text.Json.JsonEncodedText PropDynamicTemplates = System.Text.Json.JsonEncodedText.Encode("dynamic_templates"u8);
	private static readonly System.Text.Json.JsonEncodedText PropId = System.Text.Json.JsonEncodedText.Encode("_id"u8);
	private static readonly System.Text.Json.JsonEncodedText PropIfPrimaryTerm = System.Text.Json.JsonEncodedText.Encode("if_primary_term"u8);
	private static readonly System.Text.Json.JsonEncodedText PropIfSeqNo = System.Text.Json.JsonEncodedText.Encode("if_seq_no"u8);
	private static readonly System.Text.Json.JsonEncodedText PropIndex = System.Text.Json.JsonEncodedText.Encode("_index"u8);
	private static readonly System.Text.Json.JsonEncodedText PropPipeline = System.Text.Json.JsonEncodedText.Encode("pipeline"u8);
	private static readonly System.Text.Json.JsonEncodedText PropRequireAlias = System.Text.Json.JsonEncodedText.Encode("require_alias"u8);
	private static readonly System.Text.Json.JsonEncodedText PropRouting = System.Text.Json.JsonEncodedText.Encode("routing"u8);
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("version"u8);
	private static readonly System.Text.Json.JsonEncodedText PropVersionType = System.Text.Json.JsonEncodedText.Encode("version_type"u8);

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
	public IDictionary<string, string>? DynamicTemplates { get; set; }

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

	private void SerializeOperationAction(IElasticsearchClientSettings settings, Utf8JsonWriter writer)
	{
		if (!settings.RequestResponseSerializer.TryGetJsonSerializerOptions(out var options))
		{
			throw new InvalidOperationException("unreachable");
		}

		writer.WriteStartObject();
		writer.WritePropertyName(Operation);
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDynamicTemplates, DynamicTemplates, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<string, string>? v) => w.WriteDictionaryValue<string, string>(o, v, null, null));
		writer.WriteProperty(options, PropId, Id, null, null);
		writer.WriteProperty(options, PropIfPrimaryTerm, IfPrimaryTerm, null, null);
		writer.WriteProperty(options, PropIfSeqNo, IfSequenceNumber, null, null);
		writer.WriteProperty(options, PropIndex, Index, null, null);
		writer.WriteProperty(options, PropPipeline, Pipeline, null, null);
		writer.WriteProperty(options, PropRequireAlias, RequireAlias, null, null);
		writer.WriteProperty(options, PropRouting, Routing, null, null);
		writer.WriteProperty(options, PropVersion, Version, null, null);
		writer.WriteProperty(options, PropVersionType, VersionType, null, null);
		writer.WriteEndObject();
		writer.WriteEndObject();
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
}
