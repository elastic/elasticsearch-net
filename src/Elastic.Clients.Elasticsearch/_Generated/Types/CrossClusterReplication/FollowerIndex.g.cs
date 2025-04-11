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

internal sealed partial class FollowerIndexConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowerIndex>
{
	private static readonly System.Text.Json.JsonEncodedText PropFollowerIndexValue = System.Text.Json.JsonEncodedText.Encode("follower_index");
	private static readonly System.Text.Json.JsonEncodedText PropLeaderIndex = System.Text.Json.JsonEncodedText.Encode("leader_index");
	private static readonly System.Text.Json.JsonEncodedText PropParameters = System.Text.Json.JsonEncodedText.Encode("parameters");
	private static readonly System.Text.Json.JsonEncodedText PropRemoteCluster = System.Text.Json.JsonEncodedText.Encode("remote_cluster");
	private static readonly System.Text.Json.JsonEncodedText PropStatus = System.Text.Json.JsonEncodedText.Encode("status");

	public override Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowerIndex Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string> propFollowerIndexValue = default;
		LocalJsonValue<string> propLeaderIndex = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowerIndexParameters?> propParameters = default;
		LocalJsonValue<string> propRemoteCluster = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowerIndexStatus> propStatus = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propFollowerIndexValue.TryReadProperty(ref reader, options, PropFollowerIndexValue, null))
			{
				continue;
			}

			if (propLeaderIndex.TryReadProperty(ref reader, options, PropLeaderIndex, null))
			{
				continue;
			}

			if (propParameters.TryReadProperty(ref reader, options, PropParameters, null))
			{
				continue;
			}

			if (propRemoteCluster.TryReadProperty(ref reader, options, PropRemoteCluster, null))
			{
				continue;
			}

			if (propStatus.TryReadProperty(ref reader, options, PropStatus, null))
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
		return new Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowerIndex(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			FollowerIndexValue = propFollowerIndexValue.Value,
			LeaderIndex = propLeaderIndex.Value,
			Parameters = propParameters.Value,
			RemoteCluster = propRemoteCluster.Value,
			Status = propStatus.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowerIndex value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropFollowerIndexValue, value.FollowerIndexValue, null, null);
		writer.WriteProperty(options, PropLeaderIndex, value.LeaderIndex, null, null);
		writer.WriteProperty(options, PropParameters, value.Parameters, null, null);
		writer.WriteProperty(options, PropRemoteCluster, value.RemoteCluster, null, null);
		writer.WriteProperty(options, PropStatus, value.Status, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowerIndexConverter))]
public sealed partial class FollowerIndex
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public FollowerIndex(string followerIndexValue, string leaderIndex, string remoteCluster, Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowerIndexStatus status)
	{
		FollowerIndexValue = followerIndexValue;
		LeaderIndex = leaderIndex;
		RemoteCluster = remoteCluster;
		Status = status;
	}
#if NET7_0_OR_GREATER
	public FollowerIndex()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public FollowerIndex()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal FollowerIndex(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The name of the follower index.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string FollowerIndexValue { get; set; }

	/// <summary>
	/// <para>
	/// The name of the index in the leader cluster that is followed.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string LeaderIndex { get; set; }

	/// <summary>
	/// <para>
	/// An object that encapsulates cross-cluster replication parameters. If the follower index's status is paused, this object is omitted.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowerIndexParameters? Parameters { get; set; }

	/// <summary>
	/// <para>
	/// The remote cluster that contains the leader index.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string RemoteCluster { get; set; }

	/// <summary>
	/// <para>
	/// The status of the index following: <c>active</c> or <c>paused</c>.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowerIndexStatus Status { get; set; }
}