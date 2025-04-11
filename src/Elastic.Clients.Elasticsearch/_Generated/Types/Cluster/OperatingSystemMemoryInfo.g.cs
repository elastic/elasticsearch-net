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

namespace Elastic.Clients.Elasticsearch.Cluster;

internal sealed partial class OperatingSystemMemoryInfoConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Cluster.OperatingSystemMemoryInfo>
{
	private static readonly System.Text.Json.JsonEncodedText PropAdjustedTotalInBytes = System.Text.Json.JsonEncodedText.Encode("adjusted_total_in_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropFreeInBytes = System.Text.Json.JsonEncodedText.Encode("free_in_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropFreePercent = System.Text.Json.JsonEncodedText.Encode("free_percent");
	private static readonly System.Text.Json.JsonEncodedText PropTotalInBytes = System.Text.Json.JsonEncodedText.Encode("total_in_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropUsedInBytes = System.Text.Json.JsonEncodedText.Encode("used_in_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropUsedPercent = System.Text.Json.JsonEncodedText.Encode("used_percent");

	public override Elastic.Clients.Elasticsearch.Cluster.OperatingSystemMemoryInfo Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<long?> propAdjustedTotalInBytes = default;
		LocalJsonValue<long> propFreeInBytes = default;
		LocalJsonValue<int> propFreePercent = default;
		LocalJsonValue<long> propTotalInBytes = default;
		LocalJsonValue<long> propUsedInBytes = default;
		LocalJsonValue<int> propUsedPercent = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAdjustedTotalInBytes.TryReadProperty(ref reader, options, PropAdjustedTotalInBytes, null))
			{
				continue;
			}

			if (propFreeInBytes.TryReadProperty(ref reader, options, PropFreeInBytes, null))
			{
				continue;
			}

			if (propFreePercent.TryReadProperty(ref reader, options, PropFreePercent, null))
			{
				continue;
			}

			if (propTotalInBytes.TryReadProperty(ref reader, options, PropTotalInBytes, null))
			{
				continue;
			}

			if (propUsedInBytes.TryReadProperty(ref reader, options, PropUsedInBytes, null))
			{
				continue;
			}

			if (propUsedPercent.TryReadProperty(ref reader, options, PropUsedPercent, null))
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
		return new Elastic.Clients.Elasticsearch.Cluster.OperatingSystemMemoryInfo(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			AdjustedTotalInBytes = propAdjustedTotalInBytes.Value,
			FreeInBytes = propFreeInBytes.Value,
			FreePercent = propFreePercent.Value,
			TotalInBytes = propTotalInBytes.Value,
			UsedInBytes = propUsedInBytes.Value,
			UsedPercent = propUsedPercent.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Cluster.OperatingSystemMemoryInfo value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAdjustedTotalInBytes, value.AdjustedTotalInBytes, null, null);
		writer.WriteProperty(options, PropFreeInBytes, value.FreeInBytes, null, null);
		writer.WriteProperty(options, PropFreePercent, value.FreePercent, null, null);
		writer.WriteProperty(options, PropTotalInBytes, value.TotalInBytes, null, null);
		writer.WriteProperty(options, PropUsedInBytes, value.UsedInBytes, null, null);
		writer.WriteProperty(options, PropUsedPercent, value.UsedPercent, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Cluster.OperatingSystemMemoryInfoConverter))]
public sealed partial class OperatingSystemMemoryInfo
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public OperatingSystemMemoryInfo(long freeInBytes, int freePercent, long totalInBytes, long usedInBytes, int usedPercent)
	{
		FreeInBytes = freeInBytes;
		FreePercent = freePercent;
		TotalInBytes = totalInBytes;
		UsedInBytes = usedInBytes;
		UsedPercent = usedPercent;
	}
#if NET7_0_OR_GREATER
	public OperatingSystemMemoryInfo()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public OperatingSystemMemoryInfo()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal OperatingSystemMemoryInfo(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Total amount, in bytes, of memory across all selected nodes, but using the value specified using the <c>es.total_memory_bytes</c> system property instead of measured total memory for those nodes where that system property was set.
	/// </para>
	/// </summary>
	public long? AdjustedTotalInBytes { get; set; }

	/// <summary>
	/// <para>
	/// Amount, in bytes, of free physical memory across all selected nodes.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	long FreeInBytes { get; set; }

	/// <summary>
	/// <para>
	/// Percentage of free physical memory across all selected nodes.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int FreePercent { get; set; }

	/// <summary>
	/// <para>
	/// Total amount, in bytes, of physical memory across all selected nodes.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	long TotalInBytes { get; set; }

	/// <summary>
	/// <para>
	/// Amount, in bytes, of physical memory in use across all selected nodes.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	long UsedInBytes { get; set; }

	/// <summary>
	/// <para>
	/// Percentage of physical memory in use across all selected nodes.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int UsedPercent { get; set; }
}