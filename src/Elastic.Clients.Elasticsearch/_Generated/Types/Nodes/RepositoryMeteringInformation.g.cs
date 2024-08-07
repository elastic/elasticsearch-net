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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Nodes;

public sealed partial class RepositoryMeteringInformation
{
	/// <summary>
	/// <para>
	/// A flag that tells whether or not this object has been archived. When a repository is closed or updated the
	/// repository metering information is archived and kept for a certain period of time. This allows retrieving the
	/// repository metering information of previous repository instantiations.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("archived")]
	public bool Archived { get; init; }

	/// <summary>
	/// <para>
	/// The cluster state version when this object was archived, this field can be used as a logical timestamp to delete
	/// all the archived metrics up to an observed version. This field is only present for archived repository metering
	/// information objects. The main purpose of this field is to avoid possible race conditions during repository metering
	/// information deletions, i.e. deleting archived repositories metering information that we haven’t observed yet.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("cluster_version")]
	public long? ClusterVersion { get; init; }

	/// <summary>
	/// <para>
	/// An identifier that changes every time the repository is updated.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("repository_ephemeral_id")]
	public string RepositoryEphemeralId { get; init; }

	/// <summary>
	/// <para>
	/// Represents an unique location within the repository.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("repository_location")]
	public Elastic.Clients.Elasticsearch.Nodes.RepositoryLocation RepositoryLocation { get; init; }

	/// <summary>
	/// <para>
	/// Repository name.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("repository_name")]
	public string RepositoryName { get; init; }

	/// <summary>
	/// <para>
	/// Time the repository was created or updated. Recorded in milliseconds since the Unix Epoch.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("repository_started_at")]
	public long RepositoryStartedAt { get; init; }

	/// <summary>
	/// <para>
	/// Time the repository was deleted or updated. Recorded in milliseconds since the Unix Epoch.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("repository_stopped_at")]
	public long? RepositoryStoppedAt { get; init; }

	/// <summary>
	/// <para>
	/// Repository type.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("repository_type")]
	public string RepositoryType { get; init; }

	/// <summary>
	/// <para>
	/// An object with the number of request performed against the repository grouped by request type.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("request_counts")]
	public Elastic.Clients.Elasticsearch.Nodes.RequestCounts RequestCounts { get; init; }
}