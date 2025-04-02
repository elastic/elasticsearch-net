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

namespace Elastic.Clients.Elasticsearch;

public sealed partial class OpenPointInTimeRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// Indicates whether the point in time tolerates unavailable shards or shard failures when initially creating the PIT.
	/// If <c>false</c>, creating a point in time request when a shard is missing or unavailable will throw an exception.
	/// If <c>true</c>, the point in time will contain all the shards that are available at the time of the request.
	/// </para>
	/// </summary>
	public bool? AllowPartialSearchResults { get => Q<bool?>("allow_partial_search_results"); set => Q("allow_partial_search_results", value); }

	/// <summary>
	/// <para>
	/// The type of index that wildcard patterns can match.
	/// If the request can target data streams, this argument determines whether wildcard expressions match hidden data streams.
	/// It supports comma-separated values, such as <c>open,hidden</c>. Valid values are: <c>all</c>, <c>open</c>, <c>closed</c>, <c>hidden</c>, <c>none</c>.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? ExpandWildcards { get => Q<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>?>("expand_wildcards"); set => Q("expand_wildcards", value); }

	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error if it targets a missing or closed index.
	/// </para>
	/// </summary>
	public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

	/// <summary>
	/// <para>
	/// Extend the length of time that the point in time persists.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? KeepAlive { get => Q<Elastic.Clients.Elasticsearch.Duration?>("keep_alive"); set => Q("keep_alive", value); }

	/// <summary>
	/// <para>
	/// The node or shard the operation should be performed on.
	/// By default, it is random.
	/// </para>
	/// </summary>
	public string? Preference { get => Q<string?>("preference"); set => Q("preference", value); }

	/// <summary>
	/// <para>
	/// A custom value that is used to route operations to a specific shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Routing? Routing { get => Q<Elastic.Clients.Elasticsearch.Routing?>("routing"); set => Q("routing", value); }
}

internal sealed partial class OpenPointInTimeRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.OpenPointInTimeRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropIndexFilter = System.Text.Json.JsonEncodedText.Encode("index_filter");

	public override Elastic.Clients.Elasticsearch.OpenPointInTimeRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.QueryDsl.Query?> propIndexFilter = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propIndexFilter.TryReadProperty(ref reader, options, PropIndexFilter, null))
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
		return new Elastic.Clients.Elasticsearch.OpenPointInTimeRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			IndexFilter = propIndexFilter.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.OpenPointInTimeRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropIndexFilter, value.IndexFilter, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Open a point in time.
/// </para>
/// <para>
/// A search request by default runs against the most recent visible data of the target indices,
/// which is called point in time. Elasticsearch pit (point in time) is a lightweight view into the
/// state of the data as it existed when initiated. In some cases, it’s preferred to perform multiple
/// search requests using the same point in time. For example, if refreshes happen between
/// <c>search_after</c> requests, then the results of those requests might not be consistent as changes happening
/// between searches are only visible to the more recent point in time.
/// </para>
/// <para>
/// A point in time must be opened explicitly before being used in search requests.
/// </para>
/// <para>
/// A subsequent search request with the <c>pit</c> parameter must not specify <c>index</c>, <c>routing</c>, or <c>preference</c> values as these parameters are copied from the point in time.
/// </para>
/// <para>
/// Just like regular searches, you can use <c>from</c> and <c>size</c> to page through point in time search results, up to the first 10,000 hits.
/// If you want to retrieve more hits, use PIT with <c>search_after</c>.
/// </para>
/// <para>
/// IMPORTANT: The open point in time request and each subsequent search request can return different identifiers; always use the most recently received ID for the next search request.
/// </para>
/// <para>
/// When a PIT that contains shard failures is used in a search request, the missing are always reported in the search response as a <c>NoShardAvailableActionException</c> exception.
/// To get rid of these exceptions, a new PIT needs to be created so that shards missing from the previous PIT can be handled, assuming they become available in the meantime.
/// </para>
/// <para>
/// <strong>Keeping point in time alive</strong>
/// </para>
/// <para>
/// The <c>keep_alive</c> parameter, which is passed to a open point in time request and search request, extends the time to live of the corresponding point in time.
/// The value does not need to be long enough to process all data — it just needs to be long enough for the next request.
/// </para>
/// <para>
/// Normally, the background merge process optimizes the index by merging together smaller segments to create new, bigger segments.
/// Once the smaller segments are no longer needed they are deleted.
/// However, open point-in-times prevent the old segments from being deleted since they are still in use.
/// </para>
/// <para>
/// TIP: Keeping older segments alive means that more disk space and file handles are needed.
/// Ensure that you have configured your nodes to have ample free file handles.
/// </para>
/// <para>
/// Additionally, if a segment contains deleted or updated documents then the point in time must keep track of whether each document in the segment was live at the time of the initial search request.
/// Ensure that your nodes have sufficient heap space if you have many open point-in-times on an index that is subject to ongoing deletes or updates.
/// Note that a point-in-time doesn't prevent its associated indices from being deleted.
/// You can check how many point-in-times (that is, search contexts) are open with the nodes stats API.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.OpenPointInTimeRequestConverter))]
public sealed partial class OpenPointInTimeRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.OpenPointInTimeRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public OpenPointInTimeRequest(Elastic.Clients.Elasticsearch.Indices indices) : base(r => r.Required("index", indices))
	{
	}
#if NET7_0_OR_GREATER
	public OpenPointInTimeRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal OpenPointInTimeRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.NoNamespaceOpenPointInTime;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "open_point_in_time";

	/// <summary>
	/// <para>
	/// A comma-separated list of index names to open point in time; use <c>_all</c> or empty string to perform the operation on all indices
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Indices Indices { get => P<Elastic.Clients.Elasticsearch.Indices>("index"); set => PR("index", value); }

	/// <summary>
	/// <para>
	/// Indicates whether the point in time tolerates unavailable shards or shard failures when initially creating the PIT.
	/// If <c>false</c>, creating a point in time request when a shard is missing or unavailable will throw an exception.
	/// If <c>true</c>, the point in time will contain all the shards that are available at the time of the request.
	/// </para>
	/// </summary>
	public bool? AllowPartialSearchResults { get => Q<bool?>("allow_partial_search_results"); set => Q("allow_partial_search_results", value); }

	/// <summary>
	/// <para>
	/// The type of index that wildcard patterns can match.
	/// If the request can target data streams, this argument determines whether wildcard expressions match hidden data streams.
	/// It supports comma-separated values, such as <c>open,hidden</c>. Valid values are: <c>all</c>, <c>open</c>, <c>closed</c>, <c>hidden</c>, <c>none</c>.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? ExpandWildcards { get => Q<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>?>("expand_wildcards"); set => Q("expand_wildcards", value); }

	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error if it targets a missing or closed index.
	/// </para>
	/// </summary>
	public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

	/// <summary>
	/// <para>
	/// Extend the length of time that the point in time persists.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? KeepAlive { get => Q<Elastic.Clients.Elasticsearch.Duration?>("keep_alive"); set => Q("keep_alive", value); }

	/// <summary>
	/// <para>
	/// The node or shard the operation should be performed on.
	/// By default, it is random.
	/// </para>
	/// </summary>
	public string? Preference { get => Q<string?>("preference"); set => Q("preference", value); }

	/// <summary>
	/// <para>
	/// A custom value that is used to route operations to a specific shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Routing? Routing { get => Q<Elastic.Clients.Elasticsearch.Routing?>("routing"); set => Q("routing", value); }

	/// <summary>
	/// <para>
	/// Filter indices if the provided query rewrites to <c>match_none</c> on every shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.Query? IndexFilter { get; set; }
}

/// <summary>
/// <para>
/// Open a point in time.
/// </para>
/// <para>
/// A search request by default runs against the most recent visible data of the target indices,
/// which is called point in time. Elasticsearch pit (point in time) is a lightweight view into the
/// state of the data as it existed when initiated. In some cases, it’s preferred to perform multiple
/// search requests using the same point in time. For example, if refreshes happen between
/// <c>search_after</c> requests, then the results of those requests might not be consistent as changes happening
/// between searches are only visible to the more recent point in time.
/// </para>
/// <para>
/// A point in time must be opened explicitly before being used in search requests.
/// </para>
/// <para>
/// A subsequent search request with the <c>pit</c> parameter must not specify <c>index</c>, <c>routing</c>, or <c>preference</c> values as these parameters are copied from the point in time.
/// </para>
/// <para>
/// Just like regular searches, you can use <c>from</c> and <c>size</c> to page through point in time search results, up to the first 10,000 hits.
/// If you want to retrieve more hits, use PIT with <c>search_after</c>.
/// </para>
/// <para>
/// IMPORTANT: The open point in time request and each subsequent search request can return different identifiers; always use the most recently received ID for the next search request.
/// </para>
/// <para>
/// When a PIT that contains shard failures is used in a search request, the missing are always reported in the search response as a <c>NoShardAvailableActionException</c> exception.
/// To get rid of these exceptions, a new PIT needs to be created so that shards missing from the previous PIT can be handled, assuming they become available in the meantime.
/// </para>
/// <para>
/// <strong>Keeping point in time alive</strong>
/// </para>
/// <para>
/// The <c>keep_alive</c> parameter, which is passed to a open point in time request and search request, extends the time to live of the corresponding point in time.
/// The value does not need to be long enough to process all data — it just needs to be long enough for the next request.
/// </para>
/// <para>
/// Normally, the background merge process optimizes the index by merging together smaller segments to create new, bigger segments.
/// Once the smaller segments are no longer needed they are deleted.
/// However, open point-in-times prevent the old segments from being deleted since they are still in use.
/// </para>
/// <para>
/// TIP: Keeping older segments alive means that more disk space and file handles are needed.
/// Ensure that you have configured your nodes to have ample free file handles.
/// </para>
/// <para>
/// Additionally, if a segment contains deleted or updated documents then the point in time must keep track of whether each document in the segment was live at the time of the initial search request.
/// Ensure that your nodes have sufficient heap space if you have many open point-in-times on an index that is subject to ongoing deletes or updates.
/// Note that a point-in-time doesn't prevent its associated indices from being deleted.
/// You can check how many point-in-times (that is, search contexts) are open with the nodes stats API.
/// </para>
/// </summary>
public readonly partial struct OpenPointInTimeRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.OpenPointInTimeRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public OpenPointInTimeRequestDescriptor(Elastic.Clients.Elasticsearch.OpenPointInTimeRequest instance)
	{
		Instance = instance;
	}

	public OpenPointInTimeRequestDescriptor(Elastic.Clients.Elasticsearch.Indices indices)
	{
		Instance = new Elastic.Clients.Elasticsearch.OpenPointInTimeRequest(indices);
	}

	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public OpenPointInTimeRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor(Elastic.Clients.Elasticsearch.OpenPointInTimeRequest instance) => new Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.OpenPointInTimeRequest(Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A comma-separated list of index names to open point in time; use <c>_all</c> or empty string to perform the operation on all indices
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor Indices(Elastic.Clients.Elasticsearch.Indices value)
	{
		Instance.Indices = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Indicates whether the point in time tolerates unavailable shards or shard failures when initially creating the PIT.
	/// If <c>false</c>, creating a point in time request when a shard is missing or unavailable will throw an exception.
	/// If <c>true</c>, the point in time will contain all the shards that are available at the time of the request.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor AllowPartialSearchResults(bool? value = true)
	{
		Instance.AllowPartialSearchResults = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The type of index that wildcard patterns can match.
	/// If the request can target data streams, this argument determines whether wildcard expressions match hidden data streams.
	/// It supports comma-separated values, such as <c>open,hidden</c>. Valid values are: <c>all</c>, <c>open</c>, <c>closed</c>, <c>hidden</c>, <c>none</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor ExpandWildcards(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? value)
	{
		Instance.ExpandWildcards = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The type of index that wildcard patterns can match.
	/// If the request can target data streams, this argument determines whether wildcard expressions match hidden data streams.
	/// It supports comma-separated values, such as <c>open,hidden</c>. Valid values are: <c>all</c>, <c>open</c>, <c>closed</c>, <c>hidden</c>, <c>none</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor ExpandWildcards()
	{
		Instance.ExpandWildcards = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfExpandWildcard.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// The type of index that wildcard patterns can match.
	/// If the request can target data streams, this argument determines whether wildcard expressions match hidden data streams.
	/// It supports comma-separated values, such as <c>open,hidden</c>. Valid values are: <c>all</c>, <c>open</c>, <c>closed</c>, <c>hidden</c>, <c>none</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor ExpandWildcards(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfExpandWildcard>? action)
	{
		Instance.ExpandWildcards = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfExpandWildcard.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The type of index that wildcard patterns can match.
	/// If the request can target data streams, this argument determines whether wildcard expressions match hidden data streams.
	/// It supports comma-separated values, such as <c>open,hidden</c>. Valid values are: <c>all</c>, <c>open</c>, <c>closed</c>, <c>hidden</c>, <c>none</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor ExpandWildcards(params Elastic.Clients.Elasticsearch.ExpandWildcard[] values)
	{
		Instance.ExpandWildcards = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error if it targets a missing or closed index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor IgnoreUnavailable(bool? value = true)
	{
		Instance.IgnoreUnavailable = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Extend the length of time that the point in time persists.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor KeepAlive(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.KeepAlive = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The node or shard the operation should be performed on.
	/// By default, it is random.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor Preference(string? value)
	{
		Instance.Preference = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A custom value that is used to route operations to a specific shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor Routing(Elastic.Clients.Elasticsearch.Routing? value)
	{
		Instance.Routing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Filter indices if the provided query rewrites to <c>match_none</c> on every shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor IndexFilter(Elastic.Clients.Elasticsearch.QueryDsl.Query? value)
	{
		Instance.IndexFilter = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Filter indices if the provided query rewrites to <c>match_none</c> on every shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor IndexFilter(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor> action)
	{
		Instance.IndexFilter = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Filter indices if the provided query rewrites to <c>match_none</c> on every shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor IndexFilter<T>(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>> action)
	{
		Instance.IndexFilter = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.OpenPointInTimeRequest Build(System.Action<Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor(new Elastic.Clients.Elasticsearch.OpenPointInTimeRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}

/// <summary>
/// <para>
/// Open a point in time.
/// </para>
/// <para>
/// A search request by default runs against the most recent visible data of the target indices,
/// which is called point in time. Elasticsearch pit (point in time) is a lightweight view into the
/// state of the data as it existed when initiated. In some cases, it’s preferred to perform multiple
/// search requests using the same point in time. For example, if refreshes happen between
/// <c>search_after</c> requests, then the results of those requests might not be consistent as changes happening
/// between searches are only visible to the more recent point in time.
/// </para>
/// <para>
/// A point in time must be opened explicitly before being used in search requests.
/// </para>
/// <para>
/// A subsequent search request with the <c>pit</c> parameter must not specify <c>index</c>, <c>routing</c>, or <c>preference</c> values as these parameters are copied from the point in time.
/// </para>
/// <para>
/// Just like regular searches, you can use <c>from</c> and <c>size</c> to page through point in time search results, up to the first 10,000 hits.
/// If you want to retrieve more hits, use PIT with <c>search_after</c>.
/// </para>
/// <para>
/// IMPORTANT: The open point in time request and each subsequent search request can return different identifiers; always use the most recently received ID for the next search request.
/// </para>
/// <para>
/// When a PIT that contains shard failures is used in a search request, the missing are always reported in the search response as a <c>NoShardAvailableActionException</c> exception.
/// To get rid of these exceptions, a new PIT needs to be created so that shards missing from the previous PIT can be handled, assuming they become available in the meantime.
/// </para>
/// <para>
/// <strong>Keeping point in time alive</strong>
/// </para>
/// <para>
/// The <c>keep_alive</c> parameter, which is passed to a open point in time request and search request, extends the time to live of the corresponding point in time.
/// The value does not need to be long enough to process all data — it just needs to be long enough for the next request.
/// </para>
/// <para>
/// Normally, the background merge process optimizes the index by merging together smaller segments to create new, bigger segments.
/// Once the smaller segments are no longer needed they are deleted.
/// However, open point-in-times prevent the old segments from being deleted since they are still in use.
/// </para>
/// <para>
/// TIP: Keeping older segments alive means that more disk space and file handles are needed.
/// Ensure that you have configured your nodes to have ample free file handles.
/// </para>
/// <para>
/// Additionally, if a segment contains deleted or updated documents then the point in time must keep track of whether each document in the segment was live at the time of the initial search request.
/// Ensure that your nodes have sufficient heap space if you have many open point-in-times on an index that is subject to ongoing deletes or updates.
/// Note that a point-in-time doesn't prevent its associated indices from being deleted.
/// You can check how many point-in-times (that is, search contexts) are open with the nodes stats API.
/// </para>
/// </summary>
public readonly partial struct OpenPointInTimeRequestDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.OpenPointInTimeRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public OpenPointInTimeRequestDescriptor(Elastic.Clients.Elasticsearch.OpenPointInTimeRequest instance)
	{
		Instance = instance;
	}

	public OpenPointInTimeRequestDescriptor(Elastic.Clients.Elasticsearch.Indices indices)
	{
		Instance = new Elastic.Clients.Elasticsearch.OpenPointInTimeRequest(indices);
	}

	public OpenPointInTimeRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.OpenPointInTimeRequest(typeof(TDocument));
	}

	public static explicit operator Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor<TDocument>(Elastic.Clients.Elasticsearch.OpenPointInTimeRequest instance) => new Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.OpenPointInTimeRequest(Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A comma-separated list of index names to open point in time; use <c>_all</c> or empty string to perform the operation on all indices
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor<TDocument> Indices(Elastic.Clients.Elasticsearch.Indices value)
	{
		Instance.Indices = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Indicates whether the point in time tolerates unavailable shards or shard failures when initially creating the PIT.
	/// If <c>false</c>, creating a point in time request when a shard is missing or unavailable will throw an exception.
	/// If <c>true</c>, the point in time will contain all the shards that are available at the time of the request.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor<TDocument> AllowPartialSearchResults(bool? value = true)
	{
		Instance.AllowPartialSearchResults = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The type of index that wildcard patterns can match.
	/// If the request can target data streams, this argument determines whether wildcard expressions match hidden data streams.
	/// It supports comma-separated values, such as <c>open,hidden</c>. Valid values are: <c>all</c>, <c>open</c>, <c>closed</c>, <c>hidden</c>, <c>none</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor<TDocument> ExpandWildcards(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? value)
	{
		Instance.ExpandWildcards = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The type of index that wildcard patterns can match.
	/// If the request can target data streams, this argument determines whether wildcard expressions match hidden data streams.
	/// It supports comma-separated values, such as <c>open,hidden</c>. Valid values are: <c>all</c>, <c>open</c>, <c>closed</c>, <c>hidden</c>, <c>none</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor<TDocument> ExpandWildcards()
	{
		Instance.ExpandWildcards = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfExpandWildcard.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// The type of index that wildcard patterns can match.
	/// If the request can target data streams, this argument determines whether wildcard expressions match hidden data streams.
	/// It supports comma-separated values, such as <c>open,hidden</c>. Valid values are: <c>all</c>, <c>open</c>, <c>closed</c>, <c>hidden</c>, <c>none</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor<TDocument> ExpandWildcards(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfExpandWildcard>? action)
	{
		Instance.ExpandWildcards = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfExpandWildcard.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The type of index that wildcard patterns can match.
	/// If the request can target data streams, this argument determines whether wildcard expressions match hidden data streams.
	/// It supports comma-separated values, such as <c>open,hidden</c>. Valid values are: <c>all</c>, <c>open</c>, <c>closed</c>, <c>hidden</c>, <c>none</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor<TDocument> ExpandWildcards(params Elastic.Clients.Elasticsearch.ExpandWildcard[] values)
	{
		Instance.ExpandWildcards = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error if it targets a missing or closed index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor<TDocument> IgnoreUnavailable(bool? value = true)
	{
		Instance.IgnoreUnavailable = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Extend the length of time that the point in time persists.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor<TDocument> KeepAlive(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.KeepAlive = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The node or shard the operation should be performed on.
	/// By default, it is random.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor<TDocument> Preference(string? value)
	{
		Instance.Preference = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A custom value that is used to route operations to a specific shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor<TDocument> Routing(Elastic.Clients.Elasticsearch.Routing? value)
	{
		Instance.Routing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Filter indices if the provided query rewrites to <c>match_none</c> on every shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor<TDocument> IndexFilter(Elastic.Clients.Elasticsearch.QueryDsl.Query? value)
	{
		Instance.IndexFilter = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Filter indices if the provided query rewrites to <c>match_none</c> on every shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor<TDocument> IndexFilter(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>> action)
	{
		Instance.IndexFilter = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.OpenPointInTimeRequest Build(System.Action<Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.OpenPointInTimeRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor<TDocument> ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor<TDocument> FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor<TDocument> Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor<TDocument> Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor<TDocument> SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor<TDocument> RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.OpenPointInTimeRequestDescriptor<TDocument> RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}