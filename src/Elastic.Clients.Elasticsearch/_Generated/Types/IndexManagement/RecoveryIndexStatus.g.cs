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

internal sealed partial class RecoveryIndexStatusConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.RecoveryIndexStatus>
{
	private static readonly System.Text.Json.JsonEncodedText PropBytes = System.Text.Json.JsonEncodedText.Encode("bytes");
	private static readonly System.Text.Json.JsonEncodedText PropFiles = System.Text.Json.JsonEncodedText.Encode("files");
	private static readonly System.Text.Json.JsonEncodedText PropSize = System.Text.Json.JsonEncodedText.Encode("size");
	private static readonly System.Text.Json.JsonEncodedText PropSourceThrottleTime = System.Text.Json.JsonEncodedText.Encode("source_throttle_time");
	private static readonly System.Text.Json.JsonEncodedText PropSourceThrottleTimeInMillis = System.Text.Json.JsonEncodedText.Encode("source_throttle_time_in_millis");
	private static readonly System.Text.Json.JsonEncodedText PropTargetThrottleTime = System.Text.Json.JsonEncodedText.Encode("target_throttle_time");
	private static readonly System.Text.Json.JsonEncodedText PropTargetThrottleTimeInMillis = System.Text.Json.JsonEncodedText.Encode("target_throttle_time_in_millis");
	private static readonly System.Text.Json.JsonEncodedText PropTotalTime = System.Text.Json.JsonEncodedText.Encode("total_time");
	private static readonly System.Text.Json.JsonEncodedText PropTotalTimeInMillis = System.Text.Json.JsonEncodedText.Encode("total_time_in_millis");

	public override Elastic.Clients.Elasticsearch.IndexManagement.RecoveryIndexStatus Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.RecoveryBytes?> propBytes = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.RecoveryFiles> propFiles = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.RecoveryBytes> propSize = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propSourceThrottleTime = default;
		LocalJsonValue<System.TimeSpan> propSourceThrottleTimeInMillis = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propTargetThrottleTime = default;
		LocalJsonValue<System.TimeSpan> propTargetThrottleTimeInMillis = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propTotalTime = default;
		LocalJsonValue<System.TimeSpan> propTotalTimeInMillis = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBytes.TryReadProperty(ref reader, options, PropBytes, null))
			{
				continue;
			}

			if (propFiles.TryReadProperty(ref reader, options, PropFiles, null))
			{
				continue;
			}

			if (propSize.TryReadProperty(ref reader, options, PropSize, null))
			{
				continue;
			}

			if (propSourceThrottleTime.TryReadProperty(ref reader, options, PropSourceThrottleTime, null))
			{
				continue;
			}

			if (propSourceThrottleTimeInMillis.TryReadProperty(ref reader, options, PropSourceThrottleTimeInMillis, static System.TimeSpan (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.TimeSpan>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker))))
			{
				continue;
			}

			if (propTargetThrottleTime.TryReadProperty(ref reader, options, PropTargetThrottleTime, null))
			{
				continue;
			}

			if (propTargetThrottleTimeInMillis.TryReadProperty(ref reader, options, PropTargetThrottleTimeInMillis, static System.TimeSpan (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.TimeSpan>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker))))
			{
				continue;
			}

			if (propTotalTime.TryReadProperty(ref reader, options, PropTotalTime, null))
			{
				continue;
			}

			if (propTotalTimeInMillis.TryReadProperty(ref reader, options, PropTotalTimeInMillis, static System.TimeSpan (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.TimeSpan>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker))))
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.RecoveryIndexStatus(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Bytes = propBytes.Value,
			Files = propFiles.Value,
			Size = propSize.Value,
			SourceThrottleTime = propSourceThrottleTime.Value,
			SourceThrottleTimeInMillis = propSourceThrottleTimeInMillis.Value,
			TargetThrottleTime = propTargetThrottleTime.Value,
			TargetThrottleTimeInMillis = propTargetThrottleTimeInMillis.Value,
			TotalTime = propTotalTime.Value,
			TotalTimeInMillis = propTotalTimeInMillis.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.RecoveryIndexStatus value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBytes, value.Bytes, null, null);
		writer.WriteProperty(options, PropFiles, value.Files, null, null);
		writer.WriteProperty(options, PropSize, value.Size, null, null);
		writer.WriteProperty(options, PropSourceThrottleTime, value.SourceThrottleTime, null, null);
		writer.WriteProperty(options, PropSourceThrottleTimeInMillis, value.SourceThrottleTimeInMillis, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan v) => w.WriteValueEx<System.TimeSpan>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker)));
		writer.WriteProperty(options, PropTargetThrottleTime, value.TargetThrottleTime, null, null);
		writer.WriteProperty(options, PropTargetThrottleTimeInMillis, value.TargetThrottleTimeInMillis, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan v) => w.WriteValueEx<System.TimeSpan>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker)));
		writer.WriteProperty(options, PropTotalTime, value.TotalTime, null, null);
		writer.WriteProperty(options, PropTotalTimeInMillis, value.TotalTimeInMillis, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan v) => w.WriteValueEx<System.TimeSpan>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker)));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.RecoveryIndexStatusConverter))]
public sealed partial class RecoveryIndexStatus
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RecoveryIndexStatus(Elastic.Clients.Elasticsearch.IndexManagement.RecoveryFiles files, Elastic.Clients.Elasticsearch.IndexManagement.RecoveryBytes size, System.TimeSpan sourceThrottleTimeInMillis, System.TimeSpan targetThrottleTimeInMillis, System.TimeSpan totalTimeInMillis)
	{
		Files = files;
		Size = size;
		SourceThrottleTimeInMillis = sourceThrottleTimeInMillis;
		TargetThrottleTimeInMillis = targetThrottleTimeInMillis;
		TotalTimeInMillis = totalTimeInMillis;
	}
#if NET7_0_OR_GREATER
	public RecoveryIndexStatus()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public RecoveryIndexStatus()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal RecoveryIndexStatus(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.RecoveryBytes? Bytes { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.IndexManagement.RecoveryFiles Files { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.IndexManagement.RecoveryBytes Size { get; set; }
	public Elastic.Clients.Elasticsearch.Duration? SourceThrottleTime { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.TimeSpan SourceThrottleTimeInMillis { get; set; }
	public Elastic.Clients.Elasticsearch.Duration? TargetThrottleTime { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.TimeSpan TargetThrottleTimeInMillis { get; set; }
	public Elastic.Clients.Elasticsearch.Duration? TotalTime { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.TimeSpan TotalTimeInMillis { get; set; }
}