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

internal sealed partial class NodeBufferPoolConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Nodes.NodeBufferPool>
{
	private static readonly System.Text.Json.JsonEncodedText PropCount = System.Text.Json.JsonEncodedText.Encode("count");
	private static readonly System.Text.Json.JsonEncodedText PropTotalCapacity = System.Text.Json.JsonEncodedText.Encode("total_capacity");
	private static readonly System.Text.Json.JsonEncodedText PropTotalCapacityInBytes = System.Text.Json.JsonEncodedText.Encode("total_capacity_in_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropUsed = System.Text.Json.JsonEncodedText.Encode("used");
	private static readonly System.Text.Json.JsonEncodedText PropUsedInBytes = System.Text.Json.JsonEncodedText.Encode("used_in_bytes");

	public override Elastic.Clients.Elasticsearch.Nodes.NodeBufferPool Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<long?> propCount = default;
		LocalJsonValue<string?> propTotalCapacity = default;
		LocalJsonValue<long?> propTotalCapacityInBytes = default;
		LocalJsonValue<string?> propUsed = default;
		LocalJsonValue<long?> propUsedInBytes = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCount.TryReadProperty(ref reader, options, PropCount, static long? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<long>(o)))
			{
				continue;
			}

			if (propTotalCapacity.TryReadProperty(ref reader, options, PropTotalCapacity, null))
			{
				continue;
			}

			if (propTotalCapacityInBytes.TryReadProperty(ref reader, options, PropTotalCapacityInBytes, static long? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<long>(o)))
			{
				continue;
			}

			if (propUsed.TryReadProperty(ref reader, options, PropUsed, null))
			{
				continue;
			}

			if (propUsedInBytes.TryReadProperty(ref reader, options, PropUsedInBytes, static long? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<long>(o)))
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
		return new Elastic.Clients.Elasticsearch.Nodes.NodeBufferPool(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Count = propCount.Value,
			TotalCapacity = propTotalCapacity.Value,
			TotalCapacityInBytes = propTotalCapacityInBytes.Value,
			Used = propUsed.Value,
			UsedInBytes = propUsedInBytes.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Nodes.NodeBufferPool value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCount, value.Count, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, long? v) => w.WriteNullableValue<long>(o, v));
		writer.WriteProperty(options, PropTotalCapacity, value.TotalCapacity, null, null);
		writer.WriteProperty(options, PropTotalCapacityInBytes, value.TotalCapacityInBytes, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, long? v) => w.WriteNullableValue<long>(o, v));
		writer.WriteProperty(options, PropUsed, value.Used, null, null);
		writer.WriteProperty(options, PropUsedInBytes, value.UsedInBytes, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, long? v) => w.WriteNullableValue<long>(o, v));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Nodes.NodeBufferPoolConverter))]
public sealed partial class NodeBufferPool
{
#if NET7_0_OR_GREATER
	public NodeBufferPool()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public NodeBufferPool()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal NodeBufferPool(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Number of buffer pools.
	/// </para>
	/// </summary>
	public long? Count { get; set; }

	/// <summary>
	/// <para>
	/// Total capacity of buffer pools.
	/// </para>
	/// </summary>
	public string? TotalCapacity { get; set; }

	/// <summary>
	/// <para>
	/// Total capacity of buffer pools in bytes.
	/// </para>
	/// </summary>
	public long? TotalCapacityInBytes { get; set; }

	/// <summary>
	/// <para>
	/// Size of buffer pools.
	/// </para>
	/// </summary>
	public string? Used { get; set; }

	/// <summary>
	/// <para>
	/// Size of buffer pools in bytes.
	/// </para>
	/// </summary>
	public long? UsedInBytes { get; set; }
}