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

namespace Elastic.Clients.Elasticsearch.Graph;

internal sealed partial class ExploreResponseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Graph.ExploreResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropConnections = System.Text.Json.JsonEncodedText.Encode("connections");
	private static readonly System.Text.Json.JsonEncodedText PropFailures = System.Text.Json.JsonEncodedText.Encode("failures");
	private static readonly System.Text.Json.JsonEncodedText PropTimedOut = System.Text.Json.JsonEncodedText.Encode("timed_out");
	private static readonly System.Text.Json.JsonEncodedText PropTook = System.Text.Json.JsonEncodedText.Encode("took");
	private static readonly System.Text.Json.JsonEncodedText PropVertices = System.Text.Json.JsonEncodedText.Encode("vertices");

	public override Elastic.Clients.Elasticsearch.Graph.ExploreResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Graph.Connection>> propConnections = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.ShardFailure>> propFailures = default;
		LocalJsonValue<bool> propTimedOut = default;
		LocalJsonValue<long> propTook = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Graph.Vertex>> propVertices = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propConnections.TryReadProperty(ref reader, options, PropConnections, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Graph.Connection> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Graph.Connection>(o, null)!))
			{
				continue;
			}

			if (propFailures.TryReadProperty(ref reader, options, PropFailures, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.ShardFailure> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.ShardFailure>(o, null)!))
			{
				continue;
			}

			if (propTimedOut.TryReadProperty(ref reader, options, PropTimedOut, null))
			{
				continue;
			}

			if (propTook.TryReadProperty(ref reader, options, PropTook, null))
			{
				continue;
			}

			if (propVertices.TryReadProperty(ref reader, options, PropVertices, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Graph.Vertex> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Graph.Vertex>(o, null)!))
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
		return new Elastic.Clients.Elasticsearch.Graph.ExploreResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Connections = propConnections.Value,
			Failures = propFailures.Value,
			TimedOut = propTimedOut.Value,
			Took = propTook.Value,
			Vertices = propVertices.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Graph.ExploreResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropConnections, value.Connections, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Graph.Connection> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Graph.Connection>(o, v, null));
		writer.WriteProperty(options, PropFailures, value.Failures, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.ShardFailure> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.ShardFailure>(o, v, null));
		writer.WriteProperty(options, PropTimedOut, value.TimedOut, null, null);
		writer.WriteProperty(options, PropTook, value.Took, null, null);
		writer.WriteProperty(options, PropVertices, value.Vertices, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Graph.Vertex> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Graph.Vertex>(o, v, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Graph.ExploreResponseConverter))]
public sealed partial class ExploreResponse : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ExploreResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ExploreResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
		required
#endif
		System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Graph.Connection> Connections { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.ShardFailure> Failures { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		bool TimedOut { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		long Took { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Graph.Vertex> Vertices { get; set; }
}