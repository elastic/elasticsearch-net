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

namespace Elastic.Clients.Elasticsearch.MachineLearning;

internal sealed partial class MemStatsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.MemStats>
{
	private static readonly System.Text.Json.JsonEncodedText PropAdjustedTotal = System.Text.Json.JsonEncodedText.Encode("adjusted_total");
	private static readonly System.Text.Json.JsonEncodedText PropAdjustedTotalInBytes = System.Text.Json.JsonEncodedText.Encode("adjusted_total_in_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropMl = System.Text.Json.JsonEncodedText.Encode("ml");
	private static readonly System.Text.Json.JsonEncodedText PropTotal = System.Text.Json.JsonEncodedText.Encode("total");
	private static readonly System.Text.Json.JsonEncodedText PropTotalInBytes = System.Text.Json.JsonEncodedText.Encode("total_in_bytes");

	public override Elastic.Clients.Elasticsearch.MachineLearning.MemStats Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propAdjustedTotal = default;
		LocalJsonValue<int> propAdjustedTotalInBytes = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.MemMlStats> propMl = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propTotal = default;
		LocalJsonValue<int> propTotalInBytes = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAdjustedTotal.TryReadProperty(ref reader, options, PropAdjustedTotal, null))
			{
				continue;
			}

			if (propAdjustedTotalInBytes.TryReadProperty(ref reader, options, PropAdjustedTotalInBytes, null))
			{
				continue;
			}

			if (propMl.TryReadProperty(ref reader, options, PropMl, null))
			{
				continue;
			}

			if (propTotal.TryReadProperty(ref reader, options, PropTotal, null))
			{
				continue;
			}

			if (propTotalInBytes.TryReadProperty(ref reader, options, PropTotalInBytes, null))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.MemStats(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			AdjustedTotal = propAdjustedTotal.Value,
			AdjustedTotalInBytes = propAdjustedTotalInBytes.Value,
			Ml = propMl.Value,
			Total = propTotal.Value,
			TotalInBytes = propTotalInBytes.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.MemStats value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAdjustedTotal, value.AdjustedTotal, null, null);
		writer.WriteProperty(options, PropAdjustedTotalInBytes, value.AdjustedTotalInBytes, null, null);
		writer.WriteProperty(options, PropMl, value.Ml, null, null);
		writer.WriteProperty(options, PropTotal, value.Total, null, null);
		writer.WriteProperty(options, PropTotalInBytes, value.TotalInBytes, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.MemStatsConverter))]
public sealed partial class MemStats
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public MemStats(int adjustedTotalInBytes, Elastic.Clients.Elasticsearch.MachineLearning.MemMlStats ml, int totalInBytes)
	{
		AdjustedTotalInBytes = adjustedTotalInBytes;
		Ml = ml;
		TotalInBytes = totalInBytes;
	}
#if NET7_0_OR_GREATER
	public MemStats()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public MemStats()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal MemStats(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// If the amount of physical memory has been overridden using the es.total_memory_bytes system property
	/// then this reports the overridden value. Otherwise it reports the same value as total.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ByteSize? AdjustedTotal { get; set; }

	/// <summary>
	/// <para>
	/// If the amount of physical memory has been overridden using the <c>es.total_memory_bytes</c> system property
	/// then this reports the overridden value in bytes. Otherwise it reports the same value as <c>total_in_bytes</c>.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int AdjustedTotalInBytes { get; set; }

	/// <summary>
	/// <para>
	/// Contains statistics about machine learning use of native memory on the node.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.MachineLearning.MemMlStats Ml { get; set; }

	/// <summary>
	/// <para>
	/// Total amount of physical memory.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ByteSize? Total { get; set; }

	/// <summary>
	/// <para>
	/// Total amount of physical memory in bytes.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int TotalInBytes { get; set; }
}