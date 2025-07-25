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

namespace Elastic.Clients.Elasticsearch.Nodes;

internal sealed partial class BreakerConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Nodes.Breaker>
{
	private static readonly System.Text.Json.JsonEncodedText PropEstimatedSize = System.Text.Json.JsonEncodedText.Encode("estimated_size");
	private static readonly System.Text.Json.JsonEncodedText PropEstimatedSizeInBytes = System.Text.Json.JsonEncodedText.Encode("estimated_size_in_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropLimitSize = System.Text.Json.JsonEncodedText.Encode("limit_size");
	private static readonly System.Text.Json.JsonEncodedText PropLimitSizeInBytes = System.Text.Json.JsonEncodedText.Encode("limit_size_in_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropOverhead = System.Text.Json.JsonEncodedText.Encode("overhead");
	private static readonly System.Text.Json.JsonEncodedText PropTripped = System.Text.Json.JsonEncodedText.Encode("tripped");

	public override Elastic.Clients.Elasticsearch.Nodes.Breaker Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propEstimatedSize = default;
		LocalJsonValue<long?> propEstimatedSizeInBytes = default;
		LocalJsonValue<string?> propLimitSize = default;
		LocalJsonValue<long?> propLimitSizeInBytes = default;
		LocalJsonValue<float?> propOverhead = default;
		LocalJsonValue<float?> propTripped = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propEstimatedSize.TryReadProperty(ref reader, options, PropEstimatedSize, null))
			{
				continue;
			}

			if (propEstimatedSizeInBytes.TryReadProperty(ref reader, options, PropEstimatedSizeInBytes, static long? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<long>(o)))
			{
				continue;
			}

			if (propLimitSize.TryReadProperty(ref reader, options, PropLimitSize, null))
			{
				continue;
			}

			if (propLimitSizeInBytes.TryReadProperty(ref reader, options, PropLimitSizeInBytes, static long? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<long>(o)))
			{
				continue;
			}

			if (propOverhead.TryReadProperty(ref reader, options, PropOverhead, static float? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<float>(o)))
			{
				continue;
			}

			if (propTripped.TryReadProperty(ref reader, options, PropTripped, static float? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<float>(o)))
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
		return new Elastic.Clients.Elasticsearch.Nodes.Breaker(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			EstimatedSize = propEstimatedSize.Value,
			EstimatedSizeInBytes = propEstimatedSizeInBytes.Value,
			LimitSize = propLimitSize.Value,
			LimitSizeInBytes = propLimitSizeInBytes.Value,
			Overhead = propOverhead.Value,
			Tripped = propTripped.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Nodes.Breaker value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropEstimatedSize, value.EstimatedSize, null, null);
		writer.WriteProperty(options, PropEstimatedSizeInBytes, value.EstimatedSizeInBytes, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, long? v) => w.WriteNullableValue<long>(o, v));
		writer.WriteProperty(options, PropLimitSize, value.LimitSize, null, null);
		writer.WriteProperty(options, PropLimitSizeInBytes, value.LimitSizeInBytes, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, long? v) => w.WriteNullableValue<long>(o, v));
		writer.WriteProperty(options, PropOverhead, value.Overhead, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, float? v) => w.WriteNullableValue<float>(o, v));
		writer.WriteProperty(options, PropTripped, value.Tripped, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, float? v) => w.WriteNullableValue<float>(o, v));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Nodes.BreakerConverter))]
public sealed partial class Breaker
{
#if NET7_0_OR_GREATER
	public Breaker()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public Breaker()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal Breaker(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Estimated memory used for the operation.
	/// </para>
	/// </summary>
	public string? EstimatedSize { get; set; }

	/// <summary>
	/// <para>
	/// Estimated memory used, in bytes, for the operation.
	/// </para>
	/// </summary>
	public long? EstimatedSizeInBytes { get; set; }

	/// <summary>
	/// <para>
	/// Memory limit for the circuit breaker.
	/// </para>
	/// </summary>
	public string? LimitSize { get; set; }

	/// <summary>
	/// <para>
	/// Memory limit, in bytes, for the circuit breaker.
	/// </para>
	/// </summary>
	public long? LimitSizeInBytes { get; set; }

	/// <summary>
	/// <para>
	/// A constant that all estimates for the circuit breaker are multiplied with to calculate a final estimate.
	/// </para>
	/// </summary>
	public float? Overhead { get; set; }

	/// <summary>
	/// <para>
	/// Total number of times the circuit breaker has been triggered and prevented an out of memory error.
	/// </para>
	/// </summary>
	public float? Tripped { get; set; }
}