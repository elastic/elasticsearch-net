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

internal sealed partial class StatusConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Snapshot.Status>
{
	private static readonly System.Text.Json.JsonEncodedText PropIncludeGlobalState = System.Text.Json.JsonEncodedText.Encode("include_global_state");
	private static readonly System.Text.Json.JsonEncodedText PropIndices = System.Text.Json.JsonEncodedText.Encode("indices");
	private static readonly System.Text.Json.JsonEncodedText PropRepository = System.Text.Json.JsonEncodedText.Encode("repository");
	private static readonly System.Text.Json.JsonEncodedText PropShardsStats = System.Text.Json.JsonEncodedText.Encode("shards_stats");
	private static readonly System.Text.Json.JsonEncodedText PropSnapshot = System.Text.Json.JsonEncodedText.Encode("snapshot");
	private static readonly System.Text.Json.JsonEncodedText PropState = System.Text.Json.JsonEncodedText.Encode("state");
	private static readonly System.Text.Json.JsonEncodedText PropStats = System.Text.Json.JsonEncodedText.Encode("stats");
	private static readonly System.Text.Json.JsonEncodedText PropUuid = System.Text.Json.JsonEncodedText.Encode("uuid");

	public override Elastic.Clients.Elasticsearch.Snapshot.Status Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool> propIncludeGlobalState = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Snapshot.SnapshotIndexStats>> propIndices = default;
		LocalJsonValue<string> propRepository = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Snapshot.ShardsStats> propShardsStats = default;
		LocalJsonValue<string> propSnapshot = default;
		LocalJsonValue<string> propState = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Snapshot.SnapshotStats> propStats = default;
		LocalJsonValue<string> propUuid = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propIncludeGlobalState.TryReadProperty(ref reader, options, PropIncludeGlobalState, null))
			{
				continue;
			}

			if (propIndices.TryReadProperty(ref reader, options, PropIndices, static System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Snapshot.SnapshotIndexStats> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, Elastic.Clients.Elasticsearch.Snapshot.SnapshotIndexStats>(o, null, null)!))
			{
				continue;
			}

			if (propRepository.TryReadProperty(ref reader, options, PropRepository, null))
			{
				continue;
			}

			if (propShardsStats.TryReadProperty(ref reader, options, PropShardsStats, null))
			{
				continue;
			}

			if (propSnapshot.TryReadProperty(ref reader, options, PropSnapshot, null))
			{
				continue;
			}

			if (propState.TryReadProperty(ref reader, options, PropState, null))
			{
				continue;
			}

			if (propStats.TryReadProperty(ref reader, options, PropStats, null))
			{
				continue;
			}

			if (propUuid.TryReadProperty(ref reader, options, PropUuid, null))
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
		return new Elastic.Clients.Elasticsearch.Snapshot.Status(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			IncludeGlobalState = propIncludeGlobalState.Value,
			Indices = propIndices.Value,
			Repository = propRepository.Value,
			ShardsStats = propShardsStats.Value,
			Snapshot = propSnapshot.Value,
			State = propState.Value,
			Stats = propStats.Value,
			Uuid = propUuid.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Snapshot.Status value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropIncludeGlobalState, value.IncludeGlobalState, null, null);
		writer.WriteProperty(options, PropIndices, value.Indices, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Snapshot.SnapshotIndexStats> v) => w.WriteDictionaryValue<string, Elastic.Clients.Elasticsearch.Snapshot.SnapshotIndexStats>(o, v, null, null));
		writer.WriteProperty(options, PropRepository, value.Repository, null, null);
		writer.WriteProperty(options, PropShardsStats, value.ShardsStats, null, null);
		writer.WriteProperty(options, PropSnapshot, value.Snapshot, null, null);
		writer.WriteProperty(options, PropState, value.State, null, null);
		writer.WriteProperty(options, PropStats, value.Stats, null, null);
		writer.WriteProperty(options, PropUuid, value.Uuid, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Snapshot.StatusConverter))]
public sealed partial class Status
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public Status(bool includeGlobalState, System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Snapshot.SnapshotIndexStats> indices, string repository, Elastic.Clients.Elasticsearch.Snapshot.ShardsStats shardsStats, string snapshot, string state, Elastic.Clients.Elasticsearch.Snapshot.SnapshotStats stats, string uuid)
	{
		IncludeGlobalState = includeGlobalState;
		Indices = indices;
		Repository = repository;
		ShardsStats = shardsStats;
		Snapshot = snapshot;
		State = state;
		Stats = stats;
		Uuid = uuid;
	}
#if NET7_0_OR_GREATER
	public Status()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public Status()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal Status(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Indicates whether the current cluster state is included in the snapshot.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool IncludeGlobalState { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Snapshot.SnapshotIndexStats> Indices { get; set; }

	/// <summary>
	/// <para>
	/// The name of the repository that includes the snapshot.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Repository { get; set; }

	/// <summary>
	/// <para>
	/// Statistics for the shards in the snapshot.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Snapshot.ShardsStats ShardsStats { get; set; }

	/// <summary>
	/// <para>
	/// The name of the snapshot.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Snapshot { get; set; }

	/// <summary>
	/// <para>
	/// The current snapshot state:
	/// </para>
	/// <list type="bullet">
	/// <item>
	/// <para>
	/// <c>FAILED</c>: The snapshot finished with an error and failed to store any data.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>STARTED</c>: The snapshot is currently running.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>SUCCESS</c>: The snapshot completed.
	/// </para>
	/// </item>
	/// </list>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string State { get; set; }

	/// <summary>
	/// <para>
	/// Details about the number (<c>file_count</c>) and size (<c>size_in_bytes</c>) of files included in the snapshot.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Snapshot.SnapshotStats Stats { get; set; }

	/// <summary>
	/// <para>
	/// The universally unique identifier (UUID) for the snapshot.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Uuid { get; set; }
}