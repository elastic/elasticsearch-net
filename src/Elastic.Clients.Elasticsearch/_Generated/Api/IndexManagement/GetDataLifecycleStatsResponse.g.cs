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

internal sealed partial class GetDataLifecycleStatsResponseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleStatsResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropDataStreamCount = System.Text.Json.JsonEncodedText.Encode("data_stream_count");
	private static readonly System.Text.Json.JsonEncodedText PropDataStreams = System.Text.Json.JsonEncodedText.Encode("data_streams");
	private static readonly System.Text.Json.JsonEncodedText PropLastRunDurationInMillis = System.Text.Json.JsonEncodedText.Encode("last_run_duration_in_millis");
	private static readonly System.Text.Json.JsonEncodedText PropTimeBetweenStartsInMillis = System.Text.Json.JsonEncodedText.Encode("time_between_starts_in_millis");

	public override Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleStatsResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<int> propDataStreamCount = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.IndexManagement.DataStreamStats>> propDataStreams = default;
		LocalJsonValue<System.TimeSpan?> propLastRunDurationInMillis = default;
		LocalJsonValue<System.TimeSpan?> propTimeBetweenStartsInMillis = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDataStreamCount.TryReadProperty(ref reader, options, PropDataStreamCount, null))
			{
				continue;
			}

			if (propDataStreams.TryReadProperty(ref reader, options, PropDataStreams, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.IndexManagement.DataStreamStats> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.IndexManagement.DataStreamStats>(o, null)!))
			{
				continue;
			}

			if (propLastRunDurationInMillis.TryReadProperty(ref reader, options, PropLastRunDurationInMillis, static System.TimeSpan? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValueEx<System.TimeSpan>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker))))
			{
				continue;
			}

			if (propTimeBetweenStartsInMillis.TryReadProperty(ref reader, options, PropTimeBetweenStartsInMillis, static System.TimeSpan? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValueEx<System.TimeSpan>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker))))
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleStatsResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			DataStreamCount = propDataStreamCount.Value,
			DataStreams = propDataStreams.Value,
			LastRunDurationInMillis = propLastRunDurationInMillis.Value,
			TimeBetweenStartsInMillis = propTimeBetweenStartsInMillis.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleStatsResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDataStreamCount, value.DataStreamCount, null, null);
		writer.WriteProperty(options, PropDataStreams, value.DataStreams, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.IndexManagement.DataStreamStats> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.IndexManagement.DataStreamStats>(o, v, null));
		writer.WriteProperty(options, PropLastRunDurationInMillis, value.LastRunDurationInMillis, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan? v) => w.WriteNullableValueEx<System.TimeSpan>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker)));
		writer.WriteProperty(options, PropTimeBetweenStartsInMillis, value.TimeBetweenStartsInMillis, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan? v) => w.WriteNullableValueEx<System.TimeSpan>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker)));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleStatsResponseConverter))]
public sealed partial class GetDataLifecycleStatsResponse : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetDataLifecycleStatsResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetDataLifecycleStatsResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The count of data streams currently being managed by the data stream lifecycle.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int DataStreamCount { get; set; }

	/// <summary>
	/// <para>
	/// Information about the data streams that are managed by the data stream lifecycle.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.IndexManagement.DataStreamStats> DataStreams { get; set; }

	/// <summary>
	/// <para>
	/// The duration of the last data stream lifecycle execution.
	/// </para>
	/// </summary>
	public System.TimeSpan? LastRunDurationInMillis { get; set; }

	/// <summary>
	/// <para>
	/// The time that passed between the start of the last two data stream lifecycle executions.
	/// This value should amount approximately to <c>data_streams.lifecycle.poll_interval</c>.
	/// </para>
	/// </summary>
	public System.TimeSpan? TimeBetweenStartsInMillis { get; set; }
}