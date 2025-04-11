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

public sealed partial class GetSnapshotRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// An offset identifier to start pagination from as returned by the next field in the response body.
	/// </para>
	/// </summary>
	public string? After { get => Q<string?>("after"); set => Q("after", value); }

	/// <summary>
	/// <para>
	/// The value of the current sort column at which to start retrieval.
	/// It can be a string <c>snapshot-</c> or a repository name when sorting by snapshot or repository name.
	/// It can be a millisecond time value or a number when sorting by <c>index-</c> or shard count.
	/// </para>
	/// </summary>
	public string? FromSortValue { get => Q<string?>("from_sort_value"); set => Q("from_sort_value", value); }

	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error for any snapshots that are unavailable.
	/// </para>
	/// </summary>
	public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response includes the repository name in each snapshot.
	/// </para>
	/// </summary>
	public bool? IncludeRepository { get => Q<bool?>("include_repository"); set => Q("include_repository", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response includes additional information about each index in the snapshot comprising the number of shards in the index, the total size of the index in bytes, and the maximum number of segments per shard in the index.
	/// The default is <c>false</c>, meaning that this information is omitted.
	/// </para>
	/// </summary>
	public bool? IndexDetails { get => Q<bool?>("index_details"); set => Q("index_details", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response includes the name of each index in each snapshot.
	/// </para>
	/// </summary>
	public bool? IndexNames { get => Q<bool?>("index_names"); set => Q("index_names", value); }

	/// <summary>
	/// <para>
	/// The period to wait for a connection to the master node.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>
	/// Numeric offset to start pagination from based on the snapshots matching this request. Using a non-zero value for this parameter is mutually exclusive with using the after parameter. Defaults to 0.
	/// </para>
	/// </summary>
	public int? Offset { get => Q<int?>("offset"); set => Q("offset", value); }

	/// <summary>
	/// <para>
	/// The sort order.
	/// Valid values are <c>asc</c> for ascending and <c>desc</c> for descending order.
	/// The default behavior is ascending order.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.SortOrder? Order { get => Q<Elastic.Clients.Elasticsearch.SortOrder?>("order"); set => Q("order", value); }

	/// <summary>
	/// <para>
	/// The maximum number of snapshots to return.
	/// The default is 0, which means to return all that match the request without limit.
	/// </para>
	/// </summary>
	public int? Size { get => Q<int?>("size"); set => Q("size", value); }

	/// <summary>
	/// <para>
	/// Filter snapshots by a comma-separated list of snapshot lifecycle management (SLM) policy names that snapshots belong to.
	/// </para>
	/// <para>
	/// You can use wildcards (<c>*</c>) and combinations of wildcards followed by exclude patterns starting with <c>-</c>.
	/// For example, the pattern <c>*,-policy-a-\*</c> will return all snapshots except for those that were created by an SLM policy with a name starting with <c>policy-a-</c>.
	/// Note that the wildcard pattern <c>*</c> matches all snapshots created by an SLM policy but not those snapshots that were not created by an SLM policy.
	/// To include snapshots that were not created by an SLM policy, you can use the special pattern <c>_none</c> that will match all snapshots without an SLM policy.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Name? SlmPolicyFilter { get => Q<Elastic.Clients.Elasticsearch.Name?>("slm_policy_filter"); set => Q("slm_policy_filter", value); }

	/// <summary>
	/// <para>
	/// The sort order for the result.
	/// The default behavior is sorting by snapshot start time stamp.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.SnapshotSort? Sort { get => Q<Elastic.Clients.Elasticsearch.Snapshot.SnapshotSort?>("sort"); set => Q("sort", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, returns additional information about each snapshot such as the version of Elasticsearch which took the snapshot, the start and end times of the snapshot, and the number of shards snapshotted.
	/// </para>
	/// <para>
	/// NOTE: The parameters <c>size</c>, <c>order</c>, <c>after</c>, <c>from_sort_value</c>, <c>offset</c>, <c>slm_policy_filter</c>, and <c>sort</c> are not supported when you set <c>verbose=false</c> and the sort order for requests with <c>verbose=false</c> is undefined.
	/// </para>
	/// </summary>
	public bool? Verbose { get => Q<bool?>("verbose"); set => Q("verbose", value); }
}

internal sealed partial class GetSnapshotRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequest>
{
	public override Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get snapshot information.
/// </para>
/// <para>
/// NOTE: The <c>after</c> parameter and <c>next</c> field enable you to iterate through snapshots with some consistency guarantees regarding concurrent creation or deletion of snapshots.
/// It is guaranteed that any snapshot that exists at the beginning of the iteration and is not concurrently deleted will be seen during the iteration.
/// Snapshots concurrently created may be seen during an iteration.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequestConverter))]
public sealed partial class GetSnapshotRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetSnapshotRequest(Elastic.Clients.Elasticsearch.Name repository, Elastic.Clients.Elasticsearch.Names snapshot) : base(r => r.Required("repository", repository).Required("snapshot", snapshot))
	{
	}
#if NET7_0_OR_GREATER
	public GetSnapshotRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetSnapshotRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.SnapshotGet;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "snapshot.get";

	/// <summary>
	/// <para>
	/// A comma-separated list of snapshot repository names used to limit the request.
	/// Wildcard (<c>*</c>) expressions are supported.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Name Repository { get => P<Elastic.Clients.Elasticsearch.Name>("repository"); set => PR("repository", value); }

	/// <summary>
	/// <para>
	/// A comma-separated list of snapshot names to retrieve
	/// Wildcards (<c>*</c>) are supported.
	/// </para>
	/// <list type="bullet">
	/// <item>
	/// <para>
	/// To get information about all snapshots in a registered repository, use a wildcard (<c>*</c>) or <c>_all</c>.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// To get information about any snapshots that are currently running, use <c>_current</c>.
	/// </para>
	/// </item>
	/// </list>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Names Snapshot { get => P<Elastic.Clients.Elasticsearch.Names>("snapshot"); set => PR("snapshot", value); }

	/// <summary>
	/// <para>
	/// An offset identifier to start pagination from as returned by the next field in the response body.
	/// </para>
	/// </summary>
	public string? After { get => Q<string?>("after"); set => Q("after", value); }

	/// <summary>
	/// <para>
	/// The value of the current sort column at which to start retrieval.
	/// It can be a string <c>snapshot-</c> or a repository name when sorting by snapshot or repository name.
	/// It can be a millisecond time value or a number when sorting by <c>index-</c> or shard count.
	/// </para>
	/// </summary>
	public string? FromSortValue { get => Q<string?>("from_sort_value"); set => Q("from_sort_value", value); }

	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error for any snapshots that are unavailable.
	/// </para>
	/// </summary>
	public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response includes the repository name in each snapshot.
	/// </para>
	/// </summary>
	public bool? IncludeRepository { get => Q<bool?>("include_repository"); set => Q("include_repository", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response includes additional information about each index in the snapshot comprising the number of shards in the index, the total size of the index in bytes, and the maximum number of segments per shard in the index.
	/// The default is <c>false</c>, meaning that this information is omitted.
	/// </para>
	/// </summary>
	public bool? IndexDetails { get => Q<bool?>("index_details"); set => Q("index_details", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response includes the name of each index in each snapshot.
	/// </para>
	/// </summary>
	public bool? IndexNames { get => Q<bool?>("index_names"); set => Q("index_names", value); }

	/// <summary>
	/// <para>
	/// The period to wait for a connection to the master node.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>
	/// Numeric offset to start pagination from based on the snapshots matching this request. Using a non-zero value for this parameter is mutually exclusive with using the after parameter. Defaults to 0.
	/// </para>
	/// </summary>
	public int? Offset { get => Q<int?>("offset"); set => Q("offset", value); }

	/// <summary>
	/// <para>
	/// The sort order.
	/// Valid values are <c>asc</c> for ascending and <c>desc</c> for descending order.
	/// The default behavior is ascending order.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.SortOrder? Order { get => Q<Elastic.Clients.Elasticsearch.SortOrder?>("order"); set => Q("order", value); }

	/// <summary>
	/// <para>
	/// The maximum number of snapshots to return.
	/// The default is 0, which means to return all that match the request without limit.
	/// </para>
	/// </summary>
	public int? Size { get => Q<int?>("size"); set => Q("size", value); }

	/// <summary>
	/// <para>
	/// Filter snapshots by a comma-separated list of snapshot lifecycle management (SLM) policy names that snapshots belong to.
	/// </para>
	/// <para>
	/// You can use wildcards (<c>*</c>) and combinations of wildcards followed by exclude patterns starting with <c>-</c>.
	/// For example, the pattern <c>*,-policy-a-\*</c> will return all snapshots except for those that were created by an SLM policy with a name starting with <c>policy-a-</c>.
	/// Note that the wildcard pattern <c>*</c> matches all snapshots created by an SLM policy but not those snapshots that were not created by an SLM policy.
	/// To include snapshots that were not created by an SLM policy, you can use the special pattern <c>_none</c> that will match all snapshots without an SLM policy.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Name? SlmPolicyFilter { get => Q<Elastic.Clients.Elasticsearch.Name?>("slm_policy_filter"); set => Q("slm_policy_filter", value); }

	/// <summary>
	/// <para>
	/// The sort order for the result.
	/// The default behavior is sorting by snapshot start time stamp.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.SnapshotSort? Sort { get => Q<Elastic.Clients.Elasticsearch.Snapshot.SnapshotSort?>("sort"); set => Q("sort", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, returns additional information about each snapshot such as the version of Elasticsearch which took the snapshot, the start and end times of the snapshot, and the number of shards snapshotted.
	/// </para>
	/// <para>
	/// NOTE: The parameters <c>size</c>, <c>order</c>, <c>after</c>, <c>from_sort_value</c>, <c>offset</c>, <c>slm_policy_filter</c>, and <c>sort</c> are not supported when you set <c>verbose=false</c> and the sort order for requests with <c>verbose=false</c> is undefined.
	/// </para>
	/// </summary>
	public bool? Verbose { get => Q<bool?>("verbose"); set => Q("verbose", value); }
}

/// <summary>
/// <para>
/// Get snapshot information.
/// </para>
/// <para>
/// NOTE: The <c>after</c> parameter and <c>next</c> field enable you to iterate through snapshots with some consistency guarantees regarding concurrent creation or deletion of snapshots.
/// It is guaranteed that any snapshot that exists at the beginning of the iteration and is not concurrently deleted will be seen during the iteration.
/// Snapshots concurrently created may be seen during an iteration.
/// </para>
/// </summary>
public readonly partial struct GetSnapshotRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetSnapshotRequestDescriptor(Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequest instance)
	{
		Instance = instance;
	}

	public GetSnapshotRequestDescriptor(Elastic.Clients.Elasticsearch.Name repository, Elastic.Clients.Elasticsearch.Names snapshot)
	{
		Instance = new Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequest(repository, snapshot);
	}

	[System.Obsolete("The use of the parameterless constructor is not permitted for this type.")]
	public GetSnapshotRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequestDescriptor(Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequest instance) => new Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequest(Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A comma-separated list of snapshot repository names used to limit the request.
	/// Wildcard (<c>*</c>) expressions are supported.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequestDescriptor Repository(Elastic.Clients.Elasticsearch.Name value)
	{
		Instance.Repository = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of snapshot names to retrieve
	/// Wildcards (<c>*</c>) are supported.
	/// </para>
	/// <list type="bullet">
	/// <item>
	/// <para>
	/// To get information about all snapshots in a registered repository, use a wildcard (<c>*</c>) or <c>_all</c>.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// To get information about any snapshots that are currently running, use <c>_current</c>.
	/// </para>
	/// </item>
	/// </list>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequestDescriptor Snapshot(Elastic.Clients.Elasticsearch.Names value)
	{
		Instance.Snapshot = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// An offset identifier to start pagination from as returned by the next field in the response body.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequestDescriptor After(string? value)
	{
		Instance.After = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The value of the current sort column at which to start retrieval.
	/// It can be a string <c>snapshot-</c> or a repository name when sorting by snapshot or repository name.
	/// It can be a millisecond time value or a number when sorting by <c>index-</c> or shard count.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequestDescriptor FromSortValue(string? value)
	{
		Instance.FromSortValue = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error for any snapshots that are unavailable.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequestDescriptor IgnoreUnavailable(bool? value = true)
	{
		Instance.IgnoreUnavailable = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response includes the repository name in each snapshot.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequestDescriptor IncludeRepository(bool? value = true)
	{
		Instance.IncludeRepository = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response includes additional information about each index in the snapshot comprising the number of shards in the index, the total size of the index in bytes, and the maximum number of segments per shard in the index.
	/// The default is <c>false</c>, meaning that this information is omitted.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequestDescriptor IndexDetails(bool? value = true)
	{
		Instance.IndexDetails = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response includes the name of each index in each snapshot.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequestDescriptor IndexNames(bool? value = true)
	{
		Instance.IndexNames = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The period to wait for a connection to the master node.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.MasterTimeout = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Numeric offset to start pagination from based on the snapshots matching this request. Using a non-zero value for this parameter is mutually exclusive with using the after parameter. Defaults to 0.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequestDescriptor Offset(int? value)
	{
		Instance.Offset = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The sort order.
	/// Valid values are <c>asc</c> for ascending and <c>desc</c> for descending order.
	/// The default behavior is ascending order.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequestDescriptor Order(Elastic.Clients.Elasticsearch.SortOrder? value)
	{
		Instance.Order = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The maximum number of snapshots to return.
	/// The default is 0, which means to return all that match the request without limit.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequestDescriptor Size(int? value)
	{
		Instance.Size = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Filter snapshots by a comma-separated list of snapshot lifecycle management (SLM) policy names that snapshots belong to.
	/// </para>
	/// <para>
	/// You can use wildcards (<c>*</c>) and combinations of wildcards followed by exclude patterns starting with <c>-</c>.
	/// For example, the pattern <c>*,-policy-a-\*</c> will return all snapshots except for those that were created by an SLM policy with a name starting with <c>policy-a-</c>.
	/// Note that the wildcard pattern <c>*</c> matches all snapshots created by an SLM policy but not those snapshots that were not created by an SLM policy.
	/// To include snapshots that were not created by an SLM policy, you can use the special pattern <c>_none</c> that will match all snapshots without an SLM policy.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequestDescriptor SlmPolicyFilter(Elastic.Clients.Elasticsearch.Name? value)
	{
		Instance.SlmPolicyFilter = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The sort order for the result.
	/// The default behavior is sorting by snapshot start time stamp.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequestDescriptor Sort(Elastic.Clients.Elasticsearch.Snapshot.SnapshotSort? value)
	{
		Instance.Sort = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, returns additional information about each snapshot such as the version of Elasticsearch which took the snapshot, the start and end times of the snapshot, and the number of shards snapshotted.
	/// </para>
	/// <para>
	/// NOTE: The parameters <c>size</c>, <c>order</c>, <c>after</c>, <c>from_sort_value</c>, <c>offset</c>, <c>slm_policy_filter</c>, and <c>sort</c> are not supported when you set <c>verbose=false</c> and the sort order for requests with <c>verbose=false</c> is undefined.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequestDescriptor Verbose(bool? value = true)
	{
		Instance.Verbose = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequest Build(System.Action<Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequestDescriptor(new Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Snapshot.GetSnapshotRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}