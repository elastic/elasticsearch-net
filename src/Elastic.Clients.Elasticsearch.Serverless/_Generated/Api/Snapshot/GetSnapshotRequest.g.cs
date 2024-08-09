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

using Elastic.Clients.Elasticsearch.Serverless.Fluent;
using Elastic.Clients.Elasticsearch.Serverless.Requests;
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using Elastic.Transport;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.Snapshot;

public sealed partial class GetSnapshotRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// Offset identifier to start pagination from as returned by the next field in the response body.
	/// </para>
	/// </summary>
	public string? After { get => Q<string?>("after"); set => Q("after", value); }

	/// <summary>
	/// <para>
	/// Value of the current sort column at which to start retrieval. Can either be a string snapshot- or repository name when sorting by snapshot or repository name, a millisecond time value or a number when sorting by index- or shard count.
	/// </para>
	/// </summary>
	public string? FromSortValue { get => Q<string?>("from_sort_value"); set => Q("from_sort_value", value); }

	/// <summary>
	/// <para>
	/// If false, the request returns an error for any snapshots that are unavailable.
	/// </para>
	/// </summary>
	public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

	/// <summary>
	/// <para>
	/// If true, returns the repository name in each snapshot.
	/// </para>
	/// </summary>
	public bool? IncludeRepository { get => Q<bool?>("include_repository"); set => Q("include_repository", value); }

	/// <summary>
	/// <para>
	/// If true, returns additional information about each index in the snapshot comprising the number of shards in the index, the total size of the index in bytes, and the maximum number of segments per shard in the index. Defaults to false, meaning that this information is omitted.
	/// </para>
	/// </summary>
	public bool? IndexDetails { get => Q<bool?>("index_details"); set => Q("index_details", value); }

	/// <summary>
	/// <para>
	/// If true, returns the name of each index in each snapshot.
	/// </para>
	/// </summary>
	public bool? IndexNames { get => Q<bool?>("index_names"); set => Q("index_names", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Serverless.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>
	/// Numeric offset to start pagination from based on the snapshots matching this request. Using a non-zero value for this parameter is mutually exclusive with using the after parameter. Defaults to 0.
	/// </para>
	/// </summary>
	public int? Offset { get => Q<int?>("offset"); set => Q("offset", value); }

	/// <summary>
	/// <para>
	/// Sort order. Valid values are asc for ascending and desc for descending order. Defaults to asc, meaning ascending order.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.SortOrder? Order { get => Q<Elastic.Clients.Elasticsearch.Serverless.SortOrder?>("order"); set => Q("order", value); }

	/// <summary>
	/// <para>
	/// Maximum number of snapshots to return. Defaults to 0 which means return all that match the request without limit.
	/// </para>
	/// </summary>
	public int? Size { get => Q<int?>("size"); set => Q("size", value); }

	/// <summary>
	/// <para>
	/// Filter snapshots by a comma-separated list of SLM policy names that snapshots belong to. Also accepts wildcards (*) and combinations of wildcards followed by exclude patterns starting with -. To include snapshots not created by an SLM policy you can use the special pattern _none that will match all snapshots without an SLM policy.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.Name? SlmPolicyFilter { get => Q<Elastic.Clients.Elasticsearch.Serverless.Name?>("slm_policy_filter"); set => Q("slm_policy_filter", value); }

	/// <summary>
	/// <para>
	/// Allows setting a sort order for the result. Defaults to start_time, i.e. sorting by snapshot start time stamp.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.Snapshot.SnapshotSort? Sort { get => Q<Elastic.Clients.Elasticsearch.Serverless.Snapshot.SnapshotSort?>("sort"); set => Q("sort", value); }

	/// <summary>
	/// <para>
	/// If true, returns additional information about each snapshot such as the version of Elasticsearch which took the snapshot, the start and end times of the snapshot, and the number of shards snapshotted.
	/// </para>
	/// </summary>
	public bool? Verbose { get => Q<bool?>("verbose"); set => Q("verbose", value); }
}

/// <summary>
/// <para>
/// Returns information about a snapshot.
/// </para>
/// </summary>
public sealed partial class GetSnapshotRequest : PlainRequest<GetSnapshotRequestParameters>
{
	public GetSnapshotRequest(Elastic.Clients.Elasticsearch.Serverless.Name repository, Elastic.Clients.Elasticsearch.Serverless.Names snapshot) : base(r => r.Required("repository", repository).Required("snapshot", snapshot))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SnapshotGet;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "snapshot.get";

	/// <summary>
	/// <para>
	/// Offset identifier to start pagination from as returned by the next field in the response body.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public string? After { get => Q<string?>("after"); set => Q("after", value); }

	/// <summary>
	/// <para>
	/// Value of the current sort column at which to start retrieval. Can either be a string snapshot- or repository name when sorting by snapshot or repository name, a millisecond time value or a number when sorting by index- or shard count.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public string? FromSortValue { get => Q<string?>("from_sort_value"); set => Q("from_sort_value", value); }

	/// <summary>
	/// <para>
	/// If false, the request returns an error for any snapshots that are unavailable.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

	/// <summary>
	/// <para>
	/// If true, returns the repository name in each snapshot.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? IncludeRepository { get => Q<bool?>("include_repository"); set => Q("include_repository", value); }

	/// <summary>
	/// <para>
	/// If true, returns additional information about each index in the snapshot comprising the number of shards in the index, the total size of the index in bytes, and the maximum number of segments per shard in the index. Defaults to false, meaning that this information is omitted.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? IndexDetails { get => Q<bool?>("index_details"); set => Q("index_details", value); }

	/// <summary>
	/// <para>
	/// If true, returns the name of each index in each snapshot.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? IndexNames { get => Q<bool?>("index_names"); set => Q("index_names", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Serverless.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>
	/// Numeric offset to start pagination from based on the snapshots matching this request. Using a non-zero value for this parameter is mutually exclusive with using the after parameter. Defaults to 0.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public int? Offset { get => Q<int?>("offset"); set => Q("offset", value); }

	/// <summary>
	/// <para>
	/// Sort order. Valid values are asc for ascending and desc for descending order. Defaults to asc, meaning ascending order.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.SortOrder? Order { get => Q<Elastic.Clients.Elasticsearch.Serverless.SortOrder?>("order"); set => Q("order", value); }

	/// <summary>
	/// <para>
	/// Maximum number of snapshots to return. Defaults to 0 which means return all that match the request without limit.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public int? Size { get => Q<int?>("size"); set => Q("size", value); }

	/// <summary>
	/// <para>
	/// Filter snapshots by a comma-separated list of SLM policy names that snapshots belong to. Also accepts wildcards (*) and combinations of wildcards followed by exclude patterns starting with -. To include snapshots not created by an SLM policy you can use the special pattern _none that will match all snapshots without an SLM policy.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.Name? SlmPolicyFilter { get => Q<Elastic.Clients.Elasticsearch.Serverless.Name?>("slm_policy_filter"); set => Q("slm_policy_filter", value); }

	/// <summary>
	/// <para>
	/// Allows setting a sort order for the result. Defaults to start_time, i.e. sorting by snapshot start time stamp.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.Snapshot.SnapshotSort? Sort { get => Q<Elastic.Clients.Elasticsearch.Serverless.Snapshot.SnapshotSort?>("sort"); set => Q("sort", value); }

	/// <summary>
	/// <para>
	/// If true, returns additional information about each snapshot such as the version of Elasticsearch which took the snapshot, the start and end times of the snapshot, and the number of shards snapshotted.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? Verbose { get => Q<bool?>("verbose"); set => Q("verbose", value); }
}

/// <summary>
/// <para>
/// Returns information about a snapshot.
/// </para>
/// </summary>
public sealed partial class GetSnapshotRequestDescriptor : RequestDescriptor<GetSnapshotRequestDescriptor, GetSnapshotRequestParameters>
{
	internal GetSnapshotRequestDescriptor(Action<GetSnapshotRequestDescriptor> configure) => configure.Invoke(this);

	public GetSnapshotRequestDescriptor(Elastic.Clients.Elasticsearch.Serverless.Name repository, Elastic.Clients.Elasticsearch.Serverless.Names snapshot) : base(r => r.Required("repository", repository).Required("snapshot", snapshot))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SnapshotGet;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "snapshot.get";

	public GetSnapshotRequestDescriptor After(string? after) => Qs("after", after);
	public GetSnapshotRequestDescriptor FromSortValue(string? fromSortValue) => Qs("from_sort_value", fromSortValue);
	public GetSnapshotRequestDescriptor IgnoreUnavailable(bool? ignoreUnavailable = true) => Qs("ignore_unavailable", ignoreUnavailable);
	public GetSnapshotRequestDescriptor IncludeRepository(bool? includeRepository = true) => Qs("include_repository", includeRepository);
	public GetSnapshotRequestDescriptor IndexDetails(bool? indexDetails = true) => Qs("index_details", indexDetails);
	public GetSnapshotRequestDescriptor IndexNames(bool? indexNames = true) => Qs("index_names", indexNames);
	public GetSnapshotRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Serverless.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);
	public GetSnapshotRequestDescriptor Offset(int? offset) => Qs("offset", offset);
	public GetSnapshotRequestDescriptor Order(Elastic.Clients.Elasticsearch.Serverless.SortOrder? order) => Qs("order", order);
	public GetSnapshotRequestDescriptor Size(int? size) => Qs("size", size);
	public GetSnapshotRequestDescriptor SlmPolicyFilter(Elastic.Clients.Elasticsearch.Serverless.Name? slmPolicyFilter) => Qs("slm_policy_filter", slmPolicyFilter);
	public GetSnapshotRequestDescriptor Sort(Elastic.Clients.Elasticsearch.Serverless.Snapshot.SnapshotSort? sort) => Qs("sort", sort);
	public GetSnapshotRequestDescriptor Verbose(bool? verbose = true) => Qs("verbose", verbose);

	public GetSnapshotRequestDescriptor Repository(Elastic.Clients.Elasticsearch.Serverless.Name repository)
	{
		RouteValues.Required("repository", repository);
		return Self;
	}

	public GetSnapshotRequestDescriptor Snapshot(Elastic.Clients.Elasticsearch.Serverless.Names snapshot)
	{
		RouteValues.Required("snapshot", snapshot);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}