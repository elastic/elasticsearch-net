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

namespace Elastic.Clients.Elasticsearch.IndexManagement;

internal sealed partial class ShardStoresResponseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropIndices = System.Text.Json.JsonEncodedText.Encode("indices");

	public override Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.IndexManagement.IndicesShardStores>> propIndices = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propIndices.TryReadProperty(ref reader, options, PropIndices, static System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.IndexManagement.IndicesShardStores> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, Elastic.Clients.Elasticsearch.IndexManagement.IndicesShardStores>(o, null, null)!))
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Indices = propIndices.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropIndices, value.Indices, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.IndexManagement.IndicesShardStores> v) => w.WriteDictionaryValue<string, Elastic.Clients.Elasticsearch.IndexManagement.IndicesShardStores>(o, v, null, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresResponseConverter))]
public sealed partial class ShardStoresResponse : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ShardStoresResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ShardStoresResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
		required
#endif
		System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.IndexManagement.IndicesShardStores> Indices { get; set; }
}