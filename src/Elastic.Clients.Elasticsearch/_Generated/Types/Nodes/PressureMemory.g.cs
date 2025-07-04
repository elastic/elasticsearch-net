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

internal sealed partial class PressureMemoryConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Nodes.PressureMemory>
{
	private static readonly System.Text.Json.JsonEncodedText PropAll = System.Text.Json.JsonEncodedText.Encode("all");
	private static readonly System.Text.Json.JsonEncodedText PropAllInBytes = System.Text.Json.JsonEncodedText.Encode("all_in_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropCombinedCoordinatingAndPrimary = System.Text.Json.JsonEncodedText.Encode("combined_coordinating_and_primary");
	private static readonly System.Text.Json.JsonEncodedText PropCombinedCoordinatingAndPrimaryInBytes = System.Text.Json.JsonEncodedText.Encode("combined_coordinating_and_primary_in_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropCoordinating = System.Text.Json.JsonEncodedText.Encode("coordinating");
	private static readonly System.Text.Json.JsonEncodedText PropCoordinatingInBytes = System.Text.Json.JsonEncodedText.Encode("coordinating_in_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropCoordinatingRejections = System.Text.Json.JsonEncodedText.Encode("coordinating_rejections");
	private static readonly System.Text.Json.JsonEncodedText PropPrimary = System.Text.Json.JsonEncodedText.Encode("primary");
	private static readonly System.Text.Json.JsonEncodedText PropPrimaryInBytes = System.Text.Json.JsonEncodedText.Encode("primary_in_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropPrimaryRejections = System.Text.Json.JsonEncodedText.Encode("primary_rejections");
	private static readonly System.Text.Json.JsonEncodedText PropReplica = System.Text.Json.JsonEncodedText.Encode("replica");
	private static readonly System.Text.Json.JsonEncodedText PropReplicaInBytes = System.Text.Json.JsonEncodedText.Encode("replica_in_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropReplicaRejections = System.Text.Json.JsonEncodedText.Encode("replica_rejections");

	public override Elastic.Clients.Elasticsearch.Nodes.PressureMemory Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propAll = default;
		LocalJsonValue<long?> propAllInBytes = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propCombinedCoordinatingAndPrimary = default;
		LocalJsonValue<long?> propCombinedCoordinatingAndPrimaryInBytes = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propCoordinating = default;
		LocalJsonValue<long?> propCoordinatingInBytes = default;
		LocalJsonValue<long?> propCoordinatingRejections = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propPrimary = default;
		LocalJsonValue<long?> propPrimaryInBytes = default;
		LocalJsonValue<long?> propPrimaryRejections = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propReplica = default;
		LocalJsonValue<long?> propReplicaInBytes = default;
		LocalJsonValue<long?> propReplicaRejections = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAll.TryReadProperty(ref reader, options, PropAll, null))
			{
				continue;
			}

			if (propAllInBytes.TryReadProperty(ref reader, options, PropAllInBytes, static long? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<long>(o)))
			{
				continue;
			}

			if (propCombinedCoordinatingAndPrimary.TryReadProperty(ref reader, options, PropCombinedCoordinatingAndPrimary, null))
			{
				continue;
			}

			if (propCombinedCoordinatingAndPrimaryInBytes.TryReadProperty(ref reader, options, PropCombinedCoordinatingAndPrimaryInBytes, static long? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<long>(o)))
			{
				continue;
			}

			if (propCoordinating.TryReadProperty(ref reader, options, PropCoordinating, null))
			{
				continue;
			}

			if (propCoordinatingInBytes.TryReadProperty(ref reader, options, PropCoordinatingInBytes, static long? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<long>(o)))
			{
				continue;
			}

			if (propCoordinatingRejections.TryReadProperty(ref reader, options, PropCoordinatingRejections, static long? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<long>(o)))
			{
				continue;
			}

			if (propPrimary.TryReadProperty(ref reader, options, PropPrimary, null))
			{
				continue;
			}

			if (propPrimaryInBytes.TryReadProperty(ref reader, options, PropPrimaryInBytes, static long? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<long>(o)))
			{
				continue;
			}

			if (propPrimaryRejections.TryReadProperty(ref reader, options, PropPrimaryRejections, static long? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<long>(o)))
			{
				continue;
			}

			if (propReplica.TryReadProperty(ref reader, options, PropReplica, null))
			{
				continue;
			}

			if (propReplicaInBytes.TryReadProperty(ref reader, options, PropReplicaInBytes, static long? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<long>(o)))
			{
				continue;
			}

			if (propReplicaRejections.TryReadProperty(ref reader, options, PropReplicaRejections, static long? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<long>(o)))
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
		return new Elastic.Clients.Elasticsearch.Nodes.PressureMemory(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			All = propAll.Value,
			AllInBytes = propAllInBytes.Value,
			CombinedCoordinatingAndPrimary = propCombinedCoordinatingAndPrimary.Value,
			CombinedCoordinatingAndPrimaryInBytes = propCombinedCoordinatingAndPrimaryInBytes.Value,
			Coordinating = propCoordinating.Value,
			CoordinatingInBytes = propCoordinatingInBytes.Value,
			CoordinatingRejections = propCoordinatingRejections.Value,
			Primary = propPrimary.Value,
			PrimaryInBytes = propPrimaryInBytes.Value,
			PrimaryRejections = propPrimaryRejections.Value,
			Replica = propReplica.Value,
			ReplicaInBytes = propReplicaInBytes.Value,
			ReplicaRejections = propReplicaRejections.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Nodes.PressureMemory value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAll, value.All, null, null);
		writer.WriteProperty(options, PropAllInBytes, value.AllInBytes, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, long? v) => w.WriteNullableValue<long>(o, v));
		writer.WriteProperty(options, PropCombinedCoordinatingAndPrimary, value.CombinedCoordinatingAndPrimary, null, null);
		writer.WriteProperty(options, PropCombinedCoordinatingAndPrimaryInBytes, value.CombinedCoordinatingAndPrimaryInBytes, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, long? v) => w.WriteNullableValue<long>(o, v));
		writer.WriteProperty(options, PropCoordinating, value.Coordinating, null, null);
		writer.WriteProperty(options, PropCoordinatingInBytes, value.CoordinatingInBytes, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, long? v) => w.WriteNullableValue<long>(o, v));
		writer.WriteProperty(options, PropCoordinatingRejections, value.CoordinatingRejections, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, long? v) => w.WriteNullableValue<long>(o, v));
		writer.WriteProperty(options, PropPrimary, value.Primary, null, null);
		writer.WriteProperty(options, PropPrimaryInBytes, value.PrimaryInBytes, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, long? v) => w.WriteNullableValue<long>(o, v));
		writer.WriteProperty(options, PropPrimaryRejections, value.PrimaryRejections, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, long? v) => w.WriteNullableValue<long>(o, v));
		writer.WriteProperty(options, PropReplica, value.Replica, null, null);
		writer.WriteProperty(options, PropReplicaInBytes, value.ReplicaInBytes, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, long? v) => w.WriteNullableValue<long>(o, v));
		writer.WriteProperty(options, PropReplicaRejections, value.ReplicaRejections, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, long? v) => w.WriteNullableValue<long>(o, v));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Nodes.PressureMemoryConverter))]
public sealed partial class PressureMemory
{
#if NET7_0_OR_GREATER
	public PressureMemory()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public PressureMemory()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal PressureMemory(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Memory consumed by indexing requests in the coordinating, primary, or replica stage.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ByteSize? All { get; set; }

	/// <summary>
	/// <para>
	/// Memory consumed, in bytes, by indexing requests in the coordinating, primary, or replica stage.
	/// </para>
	/// </summary>
	public long? AllInBytes { get; set; }

	/// <summary>
	/// <para>
	/// Memory consumed by indexing requests in the coordinating or primary stage.
	/// This value is not the sum of coordinating and primary as a node can reuse the coordinating memory if the primary stage is executed locally.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ByteSize? CombinedCoordinatingAndPrimary { get; set; }

	/// <summary>
	/// <para>
	/// Memory consumed, in bytes, by indexing requests in the coordinating or primary stage.
	/// This value is not the sum of coordinating and primary as a node can reuse the coordinating memory if the primary stage is executed locally.
	/// </para>
	/// </summary>
	public long? CombinedCoordinatingAndPrimaryInBytes { get; set; }

	/// <summary>
	/// <para>
	/// Memory consumed by indexing requests in the coordinating stage.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ByteSize? Coordinating { get; set; }

	/// <summary>
	/// <para>
	/// Memory consumed, in bytes, by indexing requests in the coordinating stage.
	/// </para>
	/// </summary>
	public long? CoordinatingInBytes { get; set; }

	/// <summary>
	/// <para>
	/// Number of indexing requests rejected in the coordinating stage.
	/// </para>
	/// </summary>
	public long? CoordinatingRejections { get; set; }

	/// <summary>
	/// <para>
	/// Memory consumed by indexing requests in the primary stage.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ByteSize? Primary { get; set; }

	/// <summary>
	/// <para>
	/// Memory consumed, in bytes, by indexing requests in the primary stage.
	/// </para>
	/// </summary>
	public long? PrimaryInBytes { get; set; }

	/// <summary>
	/// <para>
	/// Number of indexing requests rejected in the primary stage.
	/// </para>
	/// </summary>
	public long? PrimaryRejections { get; set; }

	/// <summary>
	/// <para>
	/// Memory consumed by indexing requests in the replica stage.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ByteSize? Replica { get; set; }

	/// <summary>
	/// <para>
	/// Memory consumed, in bytes, by indexing requests in the replica stage.
	/// </para>
	/// </summary>
	public long? ReplicaInBytes { get; set; }

	/// <summary>
	/// <para>
	/// Number of indexing requests rejected in the replica stage.
	/// </para>
	/// </summary>
	public long? ReplicaRejections { get; set; }
}