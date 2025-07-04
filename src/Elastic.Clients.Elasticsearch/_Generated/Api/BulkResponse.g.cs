// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
//
// ------------------------------------------------

#nullable restore

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch;

internal sealed partial class BulkResponseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.BulkResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropErrors = System.Text.Json.JsonEncodedText.Encode("errors");
	private static readonly System.Text.Json.JsonEncodedText PropIngestTook = System.Text.Json.JsonEncodedText.Encode("ingest_took");
	private static readonly System.Text.Json.JsonEncodedText PropItems = System.Text.Json.JsonEncodedText.Encode("items");
	private static readonly System.Text.Json.JsonEncodedText PropTook = System.Text.Json.JsonEncodedText.Encode("took");

	public override Elastic.Clients.Elasticsearch.BulkResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool> propErrors = default;
		LocalJsonValue<long?> propIngestTook = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.Bulk.ResponseItem>> propItems = default;
		LocalJsonValue<long> propTook = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propErrors.TryReadProperty(ref reader, options, PropErrors, null))
			{
				continue;
			}

			if (propIngestTook.TryReadProperty(ref reader, options, PropIngestTook, static long? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<long>(o)))
			{
				continue;
			}

			if (propItems.TryReadProperty(ref reader, options, PropItems, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.Bulk.ResponseItem> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Core.Bulk.ResponseItem>(o, null)!))
			{
				continue;
			}

			if (propTook.TryReadProperty(ref reader, options, PropTook, null))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.BulkResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Errors = propErrors.Value,
			IngestTook = propIngestTook.Value,
			Items = propItems.Value,
			Took = propTook.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.BulkResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropErrors, value.Errors, null, null);
		writer.WriteProperty(options, PropIngestTook, value.IngestTook, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, long? v) => w.WriteNullableValue<long>(o, v));
		writer.WriteProperty(options, PropItems, value.Items, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.Bulk.ResponseItem> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Core.Bulk.ResponseItem>(o, v, null));
		writer.WriteProperty(options, PropTook, value.Took, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.BulkResponseConverter))]
public partial class BulkResponse : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public BulkResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal BulkResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, one or more of the operations in the bulk request did not complete successfully.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool Errors { get; set; }
	public long? IngestTook { get; set; }

	/// <summary>
	/// <para>
	/// The result of each operation in the bulk request, in the order they were submitted.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.Bulk.ResponseItem> Items { get; set; }

	/// <summary>
	/// <para>
	/// The length of time, in milliseconds, it took to process the bulk request.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	long Took { get; set; }
}