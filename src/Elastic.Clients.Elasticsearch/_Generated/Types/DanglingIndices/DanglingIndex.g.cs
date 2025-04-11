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

namespace Elastic.Clients.Elasticsearch.DanglingIndices;

internal sealed partial class DanglingIndexConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.DanglingIndices.DanglingIndex>
{
	private static readonly System.Text.Json.JsonEncodedText PropCreationDateMillis = System.Text.Json.JsonEncodedText.Encode("creation_date_millis");
	private static readonly System.Text.Json.JsonEncodedText PropIndexName = System.Text.Json.JsonEncodedText.Encode("index_name");
	private static readonly System.Text.Json.JsonEncodedText PropIndexUuid = System.Text.Json.JsonEncodedText.Encode("index_uuid");
	private static readonly System.Text.Json.JsonEncodedText PropNodeIds = System.Text.Json.JsonEncodedText.Encode("node_ids");

	public override Elastic.Clients.Elasticsearch.DanglingIndices.DanglingIndex Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.DateTimeOffset> propCreationDateMillis = default;
		LocalJsonValue<string> propIndexName = default;
		LocalJsonValue<string> propIndexUuid = default;
		LocalJsonValue<System.Collections.Generic.ICollection<string>> propNodeIds = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCreationDateMillis.TryReadProperty(ref reader, options, PropCreationDateMillis, static System.DateTimeOffset (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.DateTimeOffset>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker))))
			{
				continue;
			}

			if (propIndexName.TryReadProperty(ref reader, options, PropIndexName, null))
			{
				continue;
			}

			if (propIndexUuid.TryReadProperty(ref reader, options, PropIndexUuid, null))
			{
				continue;
			}

			if (propNodeIds.TryReadProperty(ref reader, options, PropNodeIds, static System.Collections.Generic.ICollection<string> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadSingleOrManyCollectionValue<string>(o, null)!))
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
		return new Elastic.Clients.Elasticsearch.DanglingIndices.DanglingIndex(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			CreationDateMillis = propCreationDateMillis.Value,
			IndexName = propIndexName.Value,
			IndexUuid = propIndexUuid.Value,
			NodeIds = propNodeIds.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.DanglingIndices.DanglingIndex value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCreationDateMillis, value.CreationDateMillis, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTimeOffset v) => w.WriteValueEx<System.DateTimeOffset>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker)));
		writer.WriteProperty(options, PropIndexName, value.IndexName, null, null);
		writer.WriteProperty(options, PropIndexUuid, value.IndexUuid, null, null);
		writer.WriteProperty(options, PropNodeIds, value.NodeIds, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<string> v) => w.WriteSingleOrManyCollectionValue<string>(o, v, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.DanglingIndices.DanglingIndexConverter))]
public sealed partial class DanglingIndex
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DanglingIndex(System.DateTimeOffset creationDateMillis, string indexName, string indexUuid, System.Collections.Generic.ICollection<string> nodeIds)
	{
		CreationDateMillis = creationDateMillis;
		IndexName = indexName;
		IndexUuid = indexUuid;
		NodeIds = nodeIds;
	}
#if NET7_0_OR_GREATER
	public DanglingIndex()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public DanglingIndex()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal DanglingIndex(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	System.DateTimeOffset CreationDateMillis { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string IndexName { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string IndexUuid { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.ICollection<string> NodeIds { get; set; }
}