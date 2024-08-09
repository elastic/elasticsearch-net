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
using Elastic.Clients.Elasticsearch.Requests;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.MachineLearning;

public sealed partial class GetModelSnapshotUpgradeStatsRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// Specifies what to do when the request:
	/// </para>
	/// <list type="bullet">
	/// <item>
	/// <para>
	/// Contains wildcard expressions and there are no jobs that match.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Contains the _all string or no identifiers and there are no matches.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Contains wildcard expressions and there are only partial matches.
	/// </para>
	/// </item>
	/// </list>
	/// <para>
	/// The default value is true, which returns an empty jobs array when there are no matches and the subset of results
	/// when there are partial matches. If this parameter is false, the request returns a 404 status code when there are
	/// no matches or only partial matches.
	/// </para>
	/// </summary>
	public bool? AllowNoMatch { get => Q<bool?>("allow_no_match"); set => Q("allow_no_match", value); }
}

/// <summary>
/// <para>
/// Retrieves usage information for anomaly detection job model snapshot upgrades.
/// </para>
/// </summary>
public sealed partial class GetModelSnapshotUpgradeStatsRequest : PlainRequest<GetModelSnapshotUpgradeStatsRequestParameters>
{
	public GetModelSnapshotUpgradeStatsRequest(Elastic.Clients.Elasticsearch.Id jobId, Elastic.Clients.Elasticsearch.Id snapshotId) : base(r => r.Required("job_id", jobId).Required("snapshot_id", snapshotId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningGetModelSnapshotUpgradeStats;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ml.get_model_snapshot_upgrade_stats";

	/// <summary>
	/// <para>
	/// Specifies what to do when the request:
	/// </para>
	/// <list type="bullet">
	/// <item>
	/// <para>
	/// Contains wildcard expressions and there are no jobs that match.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Contains the _all string or no identifiers and there are no matches.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Contains wildcard expressions and there are only partial matches.
	/// </para>
	/// </item>
	/// </list>
	/// <para>
	/// The default value is true, which returns an empty jobs array when there are no matches and the subset of results
	/// when there are partial matches. If this parameter is false, the request returns a 404 status code when there are
	/// no matches or only partial matches.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? AllowNoMatch { get => Q<bool?>("allow_no_match"); set => Q("allow_no_match", value); }
}

/// <summary>
/// <para>
/// Retrieves usage information for anomaly detection job model snapshot upgrades.
/// </para>
/// </summary>
public sealed partial class GetModelSnapshotUpgradeStatsRequestDescriptor : RequestDescriptor<GetModelSnapshotUpgradeStatsRequestDescriptor, GetModelSnapshotUpgradeStatsRequestParameters>
{
	internal GetModelSnapshotUpgradeStatsRequestDescriptor(Action<GetModelSnapshotUpgradeStatsRequestDescriptor> configure) => configure.Invoke(this);

	public GetModelSnapshotUpgradeStatsRequestDescriptor(Elastic.Clients.Elasticsearch.Id jobId, Elastic.Clients.Elasticsearch.Id snapshotId) : base(r => r.Required("job_id", jobId).Required("snapshot_id", snapshotId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningGetModelSnapshotUpgradeStats;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ml.get_model_snapshot_upgrade_stats";

	public GetModelSnapshotUpgradeStatsRequestDescriptor AllowNoMatch(bool? allowNoMatch = true) => Qs("allow_no_match", allowNoMatch);

	public GetModelSnapshotUpgradeStatsRequestDescriptor JobId(Elastic.Clients.Elasticsearch.Id jobId)
	{
		RouteValues.Required("job_id", jobId);
		return Self;
	}

	public GetModelSnapshotUpgradeStatsRequestDescriptor SnapshotId(Elastic.Clients.Elasticsearch.Id snapshotId)
	{
		RouteValues.Required("snapshot_id", snapshotId);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}