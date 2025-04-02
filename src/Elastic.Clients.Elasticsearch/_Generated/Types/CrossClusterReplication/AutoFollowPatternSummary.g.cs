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

namespace Elastic.Clients.Elasticsearch.CrossClusterReplication;

internal sealed partial class AutoFollowPatternSummaryConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.CrossClusterReplication.AutoFollowPatternSummary>
{
	private static readonly System.Text.Json.JsonEncodedText PropActive = System.Text.Json.JsonEncodedText.Encode("active");
	private static readonly System.Text.Json.JsonEncodedText PropFollowIndexPattern = System.Text.Json.JsonEncodedText.Encode("follow_index_pattern");
	private static readonly System.Text.Json.JsonEncodedText PropLeaderIndexExclusionPatterns = System.Text.Json.JsonEncodedText.Encode("leader_index_exclusion_patterns");
	private static readonly System.Text.Json.JsonEncodedText PropLeaderIndexPatterns = System.Text.Json.JsonEncodedText.Encode("leader_index_patterns");
	private static readonly System.Text.Json.JsonEncodedText PropMaxOutstandingReadRequests = System.Text.Json.JsonEncodedText.Encode("max_outstanding_read_requests");
	private static readonly System.Text.Json.JsonEncodedText PropRemoteCluster = System.Text.Json.JsonEncodedText.Encode("remote_cluster");

	public override Elastic.Clients.Elasticsearch.CrossClusterReplication.AutoFollowPatternSummary Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool> propActive = default;
		LocalJsonValue<string?> propFollowIndexPattern = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<string>> propLeaderIndexExclusionPatterns = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<string>> propLeaderIndexPatterns = default;
		LocalJsonValue<int> propMaxOutstandingReadRequests = default;
		LocalJsonValue<string> propRemoteCluster = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propActive.TryReadProperty(ref reader, options, PropActive, null))
			{
				continue;
			}

			if (propFollowIndexPattern.TryReadProperty(ref reader, options, PropFollowIndexPattern, null))
			{
				continue;
			}

			if (propLeaderIndexExclusionPatterns.TryReadProperty(ref reader, options, PropLeaderIndexExclusionPatterns, static System.Collections.Generic.IReadOnlyCollection<string> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)!))
			{
				continue;
			}

			if (propLeaderIndexPatterns.TryReadProperty(ref reader, options, PropLeaderIndexPatterns, static System.Collections.Generic.IReadOnlyCollection<string> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)!))
			{
				continue;
			}

			if (propMaxOutstandingReadRequests.TryReadProperty(ref reader, options, PropMaxOutstandingReadRequests, null))
			{
				continue;
			}

			if (propRemoteCluster.TryReadProperty(ref reader, options, PropRemoteCluster, null))
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
		return new Elastic.Clients.Elasticsearch.CrossClusterReplication.AutoFollowPatternSummary(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Active = propActive.Value,
			FollowIndexPattern = propFollowIndexPattern.Value,
			LeaderIndexExclusionPatterns = propLeaderIndexExclusionPatterns.Value,
			LeaderIndexPatterns = propLeaderIndexPatterns.Value,
			MaxOutstandingReadRequests = propMaxOutstandingReadRequests.Value,
			RemoteCluster = propRemoteCluster.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.CrossClusterReplication.AutoFollowPatternSummary value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropActive, value.Active, null, null);
		writer.WriteProperty(options, PropFollowIndexPattern, value.FollowIndexPattern, null, null);
		writer.WriteProperty(options, PropLeaderIndexExclusionPatterns, value.LeaderIndexExclusionPatterns, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<string> v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropLeaderIndexPatterns, value.LeaderIndexPatterns, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<string> v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropMaxOutstandingReadRequests, value.MaxOutstandingReadRequests, null, null);
		writer.WriteProperty(options, PropRemoteCluster, value.RemoteCluster, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.CrossClusterReplication.AutoFollowPatternSummaryConverter))]
public sealed partial class AutoFollowPatternSummary
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AutoFollowPatternSummary(bool active, System.Collections.Generic.IReadOnlyCollection<string> leaderIndexExclusionPatterns, System.Collections.Generic.IReadOnlyCollection<string> leaderIndexPatterns, int maxOutstandingReadRequests, string remoteCluster)
	{
		Active = active;
		LeaderIndexExclusionPatterns = leaderIndexExclusionPatterns;
		LeaderIndexPatterns = leaderIndexPatterns;
		MaxOutstandingReadRequests = maxOutstandingReadRequests;
		RemoteCluster = remoteCluster;
	}
#if NET7_0_OR_GREATER
	public AutoFollowPatternSummary()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public AutoFollowPatternSummary()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal AutoFollowPatternSummary(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	bool Active { get; set; }

	/// <summary>
	/// <para>
	/// The name of follower index.
	/// </para>
	/// </summary>
	public string? FollowIndexPattern { get; set; }

	/// <summary>
	/// <para>
	/// An array of simple index patterns that can be used to exclude indices from being auto-followed.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyCollection<string> LeaderIndexExclusionPatterns { get; set; }

	/// <summary>
	/// <para>
	/// An array of simple index patterns to match against indices in the remote cluster specified by the remote_cluster field.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyCollection<string> LeaderIndexPatterns { get; set; }

	/// <summary>
	/// <para>
	/// The maximum number of outstanding reads requests from the remote cluster.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int MaxOutstandingReadRequests { get; set; }

	/// <summary>
	/// <para>
	/// The remote cluster containing the leader indices to match against.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string RemoteCluster { get; set; }
}