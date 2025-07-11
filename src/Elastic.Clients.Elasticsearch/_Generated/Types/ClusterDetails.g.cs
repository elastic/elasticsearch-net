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

internal sealed partial class ClusterDetailsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.ClusterDetails>
{
	private static readonly System.Text.Json.JsonEncodedText PropFailures = System.Text.Json.JsonEncodedText.Encode("failures");
	private static readonly System.Text.Json.JsonEncodedText PropIndices = System.Text.Json.JsonEncodedText.Encode("indices");
	private static readonly System.Text.Json.JsonEncodedText PropShards = System.Text.Json.JsonEncodedText.Encode("_shards");
	private static readonly System.Text.Json.JsonEncodedText PropStatus = System.Text.Json.JsonEncodedText.Encode("status");
	private static readonly System.Text.Json.JsonEncodedText PropTimedOut = System.Text.Json.JsonEncodedText.Encode("timed_out");
	private static readonly System.Text.Json.JsonEncodedText PropTook = System.Text.Json.JsonEncodedText.Encode("took");

	public override Elastic.Clients.Elasticsearch.ClusterDetails Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.ShardFailure>?> propFailures = default;
		LocalJsonValue<string> propIndices = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ShardStatistics?> propShards = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ClusterSearchStatus> propStatus = default;
		LocalJsonValue<bool> propTimedOut = default;
		LocalJsonValue<System.TimeSpan?> propTook = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propFailures.TryReadProperty(ref reader, options, PropFailures, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.ShardFailure>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.ShardFailure>(o, null)))
			{
				continue;
			}

			if (propIndices.TryReadProperty(ref reader, options, PropIndices, null))
			{
				continue;
			}

			if (propShards.TryReadProperty(ref reader, options, PropShards, null))
			{
				continue;
			}

			if (propStatus.TryReadProperty(ref reader, options, PropStatus, null))
			{
				continue;
			}

			if (propTimedOut.TryReadProperty(ref reader, options, PropTimedOut, null))
			{
				continue;
			}

			if (propTook.TryReadProperty(ref reader, options, PropTook, static System.TimeSpan? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValueEx<System.TimeSpan>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker))))
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
		return new Elastic.Clients.Elasticsearch.ClusterDetails(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Failures = propFailures.Value,
			Indices = propIndices.Value,
			Shards = propShards.Value,
			Status = propStatus.Value,
			TimedOut = propTimedOut.Value,
			Took = propTook.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.ClusterDetails value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropFailures, value.Failures, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.ShardFailure>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.ShardFailure>(o, v, null));
		writer.WriteProperty(options, PropIndices, value.Indices, null, null);
		writer.WriteProperty(options, PropShards, value.Shards, null, null);
		writer.WriteProperty(options, PropStatus, value.Status, null, null);
		writer.WriteProperty(options, PropTimedOut, value.TimedOut, null, null);
		writer.WriteProperty(options, PropTook, value.Took, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan? v) => w.WriteNullableValueEx<System.TimeSpan>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker)));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.ClusterDetailsConverter))]
public sealed partial class ClusterDetails
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ClusterDetails(string indices, Elastic.Clients.Elasticsearch.ClusterSearchStatus status, bool timedOut)
	{
		Indices = indices;
		Status = status;
		TimedOut = timedOut;
	}
#if NET7_0_OR_GREATER
	public ClusterDetails()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public ClusterDetails()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ClusterDetails(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.ShardFailure>? Failures { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Indices { get; set; }
	public Elastic.Clients.Elasticsearch.ShardStatistics? Shards { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.ClusterSearchStatus Status { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool TimedOut { get; set; }
	public System.TimeSpan? Took { get; set; }
}