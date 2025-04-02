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

namespace Elastic.Clients.Elasticsearch.Snapshot;

internal sealed partial class ShardsStatsSummaryConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Snapshot.ShardsStatsSummary>
{
	private static readonly System.Text.Json.JsonEncodedText PropIncremental = System.Text.Json.JsonEncodedText.Encode("incremental");
	private static readonly System.Text.Json.JsonEncodedText PropStartTimeInMillis = System.Text.Json.JsonEncodedText.Encode("start_time_in_millis");
	private static readonly System.Text.Json.JsonEncodedText PropTime = System.Text.Json.JsonEncodedText.Encode("time");
	private static readonly System.Text.Json.JsonEncodedText PropTimeInMillis = System.Text.Json.JsonEncodedText.Encode("time_in_millis");
	private static readonly System.Text.Json.JsonEncodedText PropTotal = System.Text.Json.JsonEncodedText.Encode("total");

	public override Elastic.Clients.Elasticsearch.Snapshot.ShardsStatsSummary Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Snapshot.ShardsStatsSummaryItem> propIncremental = default;
		LocalJsonValue<System.DateTime> propStartTimeInMillis = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propTime = default;
		LocalJsonValue<System.TimeSpan> propTimeInMillis = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Snapshot.ShardsStatsSummaryItem> propTotal = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propIncremental.TryReadProperty(ref reader, options, PropIncremental, null))
			{
				continue;
			}

			if (propStartTimeInMillis.TryReadProperty(ref reader, options, PropStartTimeInMillis, static System.DateTime (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.DateTime>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker))))
			{
				continue;
			}

			if (propTime.TryReadProperty(ref reader, options, PropTime, null))
			{
				continue;
			}

			if (propTimeInMillis.TryReadProperty(ref reader, options, PropTimeInMillis, static System.TimeSpan (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.TimeSpan>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker))))
			{
				continue;
			}

			if (propTotal.TryReadProperty(ref reader, options, PropTotal, null))
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
		return new Elastic.Clients.Elasticsearch.Snapshot.ShardsStatsSummary(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Incremental = propIncremental.Value,
			StartTimeInMillis = propStartTimeInMillis.Value,
			Time = propTime.Value,
			TimeInMillis = propTimeInMillis.Value,
			Total = propTotal.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Snapshot.ShardsStatsSummary value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropIncremental, value.Incremental, null, null);
		writer.WriteProperty(options, PropStartTimeInMillis, value.StartTimeInMillis, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTime v) => w.WriteValueEx<System.DateTime>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker)));
		writer.WriteProperty(options, PropTime, value.Time, null, null);
		writer.WriteProperty(options, PropTimeInMillis, value.TimeInMillis, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan v) => w.WriteValueEx<System.TimeSpan>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker)));
		writer.WriteProperty(options, PropTotal, value.Total, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Snapshot.ShardsStatsSummaryConverter))]
public sealed partial class ShardsStatsSummary
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ShardsStatsSummary(Elastic.Clients.Elasticsearch.Snapshot.ShardsStatsSummaryItem incremental, System.DateTime startTimeInMillis, System.TimeSpan timeInMillis, Elastic.Clients.Elasticsearch.Snapshot.ShardsStatsSummaryItem total)
	{
		Incremental = incremental;
		StartTimeInMillis = startTimeInMillis;
		TimeInMillis = timeInMillis;
		Total = total;
	}
#if NET7_0_OR_GREATER
	public ShardsStatsSummary()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public ShardsStatsSummary()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ShardsStatsSummary(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Snapshot.ShardsStatsSummaryItem Incremental { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.DateTime StartTimeInMillis { get; set; }
	public Elastic.Clients.Elasticsearch.Duration? Time { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.TimeSpan TimeInMillis { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Snapshot.ShardsStatsSummaryItem Total { get; set; }
}