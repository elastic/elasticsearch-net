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

internal sealed partial class JvmMemoryStatsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Nodes.JvmMemoryStats>
{
	private static readonly System.Text.Json.JsonEncodedText PropHeapCommittedInBytes = System.Text.Json.JsonEncodedText.Encode("heap_committed_in_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropHeapMaxInBytes = System.Text.Json.JsonEncodedText.Encode("heap_max_in_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropHeapUsedInBytes = System.Text.Json.JsonEncodedText.Encode("heap_used_in_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropHeapUsedPercent = System.Text.Json.JsonEncodedText.Encode("heap_used_percent");
	private static readonly System.Text.Json.JsonEncodedText PropNonHeapCommittedInBytes = System.Text.Json.JsonEncodedText.Encode("non_heap_committed_in_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropNonHeapUsedInBytes = System.Text.Json.JsonEncodedText.Encode("non_heap_used_in_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropPools = System.Text.Json.JsonEncodedText.Encode("pools");

	public override Elastic.Clients.Elasticsearch.Nodes.JvmMemoryStats Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<long?> propHeapCommittedInBytes = default;
		LocalJsonValue<long?> propHeapMaxInBytes = default;
		LocalJsonValue<long?> propHeapUsedInBytes = default;
		LocalJsonValue<long?> propHeapUsedPercent = default;
		LocalJsonValue<long?> propNonHeapCommittedInBytes = default;
		LocalJsonValue<long?> propNonHeapUsedInBytes = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Nodes.Pool>?> propPools = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propHeapCommittedInBytes.TryReadProperty(ref reader, options, PropHeapCommittedInBytes, null))
			{
				continue;
			}

			if (propHeapMaxInBytes.TryReadProperty(ref reader, options, PropHeapMaxInBytes, null))
			{
				continue;
			}

			if (propHeapUsedInBytes.TryReadProperty(ref reader, options, PropHeapUsedInBytes, null))
			{
				continue;
			}

			if (propHeapUsedPercent.TryReadProperty(ref reader, options, PropHeapUsedPercent, null))
			{
				continue;
			}

			if (propNonHeapCommittedInBytes.TryReadProperty(ref reader, options, PropNonHeapCommittedInBytes, null))
			{
				continue;
			}

			if (propNonHeapUsedInBytes.TryReadProperty(ref reader, options, PropNonHeapUsedInBytes, null))
			{
				continue;
			}

			if (propPools.TryReadProperty(ref reader, options, PropPools, static System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Nodes.Pool>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, Elastic.Clients.Elasticsearch.Nodes.Pool>(o, null, null)))
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
		return new Elastic.Clients.Elasticsearch.Nodes.JvmMemoryStats(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			HeapCommittedInBytes = propHeapCommittedInBytes.Value,
			HeapMaxInBytes = propHeapMaxInBytes.Value,
			HeapUsedInBytes = propHeapUsedInBytes.Value,
			HeapUsedPercent = propHeapUsedPercent.Value,
			NonHeapCommittedInBytes = propNonHeapCommittedInBytes.Value,
			NonHeapUsedInBytes = propNonHeapUsedInBytes.Value,
			Pools = propPools.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Nodes.JvmMemoryStats value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropHeapCommittedInBytes, value.HeapCommittedInBytes, null, null);
		writer.WriteProperty(options, PropHeapMaxInBytes, value.HeapMaxInBytes, null, null);
		writer.WriteProperty(options, PropHeapUsedInBytes, value.HeapUsedInBytes, null, null);
		writer.WriteProperty(options, PropHeapUsedPercent, value.HeapUsedPercent, null, null);
		writer.WriteProperty(options, PropNonHeapCommittedInBytes, value.NonHeapCommittedInBytes, null, null);
		writer.WriteProperty(options, PropNonHeapUsedInBytes, value.NonHeapUsedInBytes, null, null);
		writer.WriteProperty(options, PropPools, value.Pools, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Nodes.Pool>? v) => w.WriteDictionaryValue<string, Elastic.Clients.Elasticsearch.Nodes.Pool>(o, v, null, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Nodes.JvmMemoryStatsConverter))]
public sealed partial class JvmMemoryStats
{
#if NET7_0_OR_GREATER
	public JvmMemoryStats()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public JvmMemoryStats()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal JvmMemoryStats(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Amount of memory, in bytes, available for use by the heap.
	/// </para>
	/// </summary>
	public long? HeapCommittedInBytes { get; set; }

	/// <summary>
	/// <para>
	/// Maximum amount of memory, in bytes, available for use by the heap.
	/// </para>
	/// </summary>
	public long? HeapMaxInBytes { get; set; }

	/// <summary>
	/// <para>
	/// Memory, in bytes, currently in use by the heap.
	/// </para>
	/// </summary>
	public long? HeapUsedInBytes { get; set; }

	/// <summary>
	/// <para>
	/// Percentage of memory currently in use by the heap.
	/// </para>
	/// </summary>
	public long? HeapUsedPercent { get; set; }

	/// <summary>
	/// <para>
	/// Amount of non-heap memory available, in bytes.
	/// </para>
	/// </summary>
	public long? NonHeapCommittedInBytes { get; set; }

	/// <summary>
	/// <para>
	/// Non-heap memory used, in bytes.
	/// </para>
	/// </summary>
	public long? NonHeapUsedInBytes { get; set; }

	/// <summary>
	/// <para>
	/// Contains statistics about heap memory usage for the node.
	/// </para>
	/// </summary>
	public System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Nodes.Pool>? Pools { get; set; }
}