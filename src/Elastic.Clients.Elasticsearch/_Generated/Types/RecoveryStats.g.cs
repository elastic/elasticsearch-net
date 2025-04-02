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

internal sealed partial class RecoveryStatsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.RecoveryStats>
{
	private static readonly System.Text.Json.JsonEncodedText PropCurrentAsSource = System.Text.Json.JsonEncodedText.Encode("current_as_source");
	private static readonly System.Text.Json.JsonEncodedText PropCurrentAsTarget = System.Text.Json.JsonEncodedText.Encode("current_as_target");
	private static readonly System.Text.Json.JsonEncodedText PropThrottleTime = System.Text.Json.JsonEncodedText.Encode("throttle_time");
	private static readonly System.Text.Json.JsonEncodedText PropThrottleTimeInMillis = System.Text.Json.JsonEncodedText.Encode("throttle_time_in_millis");

	public override Elastic.Clients.Elasticsearch.RecoveryStats Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<long> propCurrentAsSource = default;
		LocalJsonValue<long> propCurrentAsTarget = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propThrottleTime = default;
		LocalJsonValue<System.TimeSpan> propThrottleTimeInMillis = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCurrentAsSource.TryReadProperty(ref reader, options, PropCurrentAsSource, null))
			{
				continue;
			}

			if (propCurrentAsTarget.TryReadProperty(ref reader, options, PropCurrentAsTarget, null))
			{
				continue;
			}

			if (propThrottleTime.TryReadProperty(ref reader, options, PropThrottleTime, null))
			{
				continue;
			}

			if (propThrottleTimeInMillis.TryReadProperty(ref reader, options, PropThrottleTimeInMillis, static System.TimeSpan (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.TimeSpan>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker))))
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
		return new Elastic.Clients.Elasticsearch.RecoveryStats(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			CurrentAsSource = propCurrentAsSource.Value,
			CurrentAsTarget = propCurrentAsTarget.Value,
			ThrottleTime = propThrottleTime.Value,
			ThrottleTimeInMillis = propThrottleTimeInMillis.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.RecoveryStats value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCurrentAsSource, value.CurrentAsSource, null, null);
		writer.WriteProperty(options, PropCurrentAsTarget, value.CurrentAsTarget, null, null);
		writer.WriteProperty(options, PropThrottleTime, value.ThrottleTime, null, null);
		writer.WriteProperty(options, PropThrottleTimeInMillis, value.ThrottleTimeInMillis, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan v) => w.WriteValueEx<System.TimeSpan>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker)));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.RecoveryStatsConverter))]
public sealed partial class RecoveryStats
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RecoveryStats(long currentAsSource, long currentAsTarget, System.TimeSpan throttleTimeInMillis)
	{
		CurrentAsSource = currentAsSource;
		CurrentAsTarget = currentAsTarget;
		ThrottleTimeInMillis = throttleTimeInMillis;
	}
#if NET7_0_OR_GREATER
	public RecoveryStats()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public RecoveryStats()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal RecoveryStats(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	long CurrentAsSource { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long CurrentAsTarget { get; set; }
	public Elastic.Clients.Elasticsearch.Duration? ThrottleTime { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.TimeSpan ThrottleTimeInMillis { get; set; }
}