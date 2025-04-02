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

internal sealed partial class RecoveryBytesConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.RecoveryBytes>
{
	private static readonly System.Text.Json.JsonEncodedText PropPercent = System.Text.Json.JsonEncodedText.Encode("percent");
	private static readonly System.Text.Json.JsonEncodedText PropRecovered = System.Text.Json.JsonEncodedText.Encode("recovered");
	private static readonly System.Text.Json.JsonEncodedText PropRecoveredFromSnapshot = System.Text.Json.JsonEncodedText.Encode("recovered_from_snapshot");
	private static readonly System.Text.Json.JsonEncodedText PropRecoveredFromSnapshotInBytes = System.Text.Json.JsonEncodedText.Encode("recovered_from_snapshot_in_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropRecoveredInBytes = System.Text.Json.JsonEncodedText.Encode("recovered_in_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropReused = System.Text.Json.JsonEncodedText.Encode("reused");
	private static readonly System.Text.Json.JsonEncodedText PropReusedInBytes = System.Text.Json.JsonEncodedText.Encode("reused_in_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropTotal = System.Text.Json.JsonEncodedText.Encode("total");
	private static readonly System.Text.Json.JsonEncodedText PropTotalInBytes = System.Text.Json.JsonEncodedText.Encode("total_in_bytes");

	public override Elastic.Clients.Elasticsearch.IndexManagement.RecoveryBytes Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Percentage> propPercent = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propRecovered = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propRecoveredFromSnapshot = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propRecoveredFromSnapshotInBytes = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize> propRecoveredInBytes = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propReused = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize> propReusedInBytes = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propTotal = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize> propTotalInBytes = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propPercent.TryReadProperty(ref reader, options, PropPercent, null))
			{
				continue;
			}

			if (propRecovered.TryReadProperty(ref reader, options, PropRecovered, null))
			{
				continue;
			}

			if (propRecoveredFromSnapshot.TryReadProperty(ref reader, options, PropRecoveredFromSnapshot, null))
			{
				continue;
			}

			if (propRecoveredFromSnapshotInBytes.TryReadProperty(ref reader, options, PropRecoveredFromSnapshotInBytes, null))
			{
				continue;
			}

			if (propRecoveredInBytes.TryReadProperty(ref reader, options, PropRecoveredInBytes, null))
			{
				continue;
			}

			if (propReused.TryReadProperty(ref reader, options, PropReused, null))
			{
				continue;
			}

			if (propReusedInBytes.TryReadProperty(ref reader, options, PropReusedInBytes, null))
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.RecoveryBytes(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Percent = propPercent.Value,
			Recovered = propRecovered.Value,
			RecoveredFromSnapshot = propRecoveredFromSnapshot.Value,
			RecoveredFromSnapshotInBytes = propRecoveredFromSnapshotInBytes.Value,
			RecoveredInBytes = propRecoveredInBytes.Value,
			Reused = propReused.Value,
			ReusedInBytes = propReusedInBytes.Value,
			Total = propTotal.Value,
			TotalInBytes = propTotalInBytes.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.RecoveryBytes value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropPercent, value.Percent, null, null);
		writer.WriteProperty(options, PropRecovered, value.Recovered, null, null);
		writer.WriteProperty(options, PropRecoveredFromSnapshot, value.RecoveredFromSnapshot, null, null);
		writer.WriteProperty(options, PropRecoveredFromSnapshotInBytes, value.RecoveredFromSnapshotInBytes, null, null);
		writer.WriteProperty(options, PropRecoveredInBytes, value.RecoveredInBytes, null, null);
		writer.WriteProperty(options, PropReused, value.Reused, null, null);
		writer.WriteProperty(options, PropReusedInBytes, value.ReusedInBytes, null, null);
		writer.WriteProperty(options, PropTotal, value.Total, null, null);
		writer.WriteProperty(options, PropTotalInBytes, value.TotalInBytes, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.RecoveryBytesConverter))]
public sealed partial class RecoveryBytes
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RecoveryBytes(Elastic.Clients.Elasticsearch.Percentage percent, Elastic.Clients.Elasticsearch.ByteSize recoveredInBytes, Elastic.Clients.Elasticsearch.ByteSize reusedInBytes, Elastic.Clients.Elasticsearch.ByteSize totalInBytes)
	{
		Percent = percent;
		RecoveredInBytes = recoveredInBytes;
		ReusedInBytes = reusedInBytes;
		TotalInBytes = totalInBytes;
	}
#if NET7_0_OR_GREATER
	public RecoveryBytes()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public RecoveryBytes()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal RecoveryBytes(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Percentage Percent { get; set; }
	public Elastic.Clients.Elasticsearch.ByteSize? Recovered { get; set; }
	public Elastic.Clients.Elasticsearch.ByteSize? RecoveredFromSnapshot { get; set; }
	public Elastic.Clients.Elasticsearch.ByteSize? RecoveredFromSnapshotInBytes { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.ByteSize RecoveredInBytes { get; set; }
	public Elastic.Clients.Elasticsearch.ByteSize? Reused { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.ByteSize ReusedInBytes { get; set; }
	public Elastic.Clients.Elasticsearch.ByteSize? Total { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.ByteSize TotalInBytes { get; set; }
}