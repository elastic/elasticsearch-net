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

public sealed partial class ForcemergeRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// Whether to ignore if a wildcard indices expression resolves into no concrete indices. (This includes <c>_all</c> string or when no indices have been specified)
	/// </para>
	/// </summary>
	public bool? AllowNoIndices { get => Q<bool?>("allow_no_indices"); set => Q("allow_no_indices", value); }

	/// <summary>
	/// <para>
	/// Whether to expand wildcard expression to concrete indices that are open, closed or both.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? ExpandWildcards { get => Q<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>?>("expand_wildcards"); set => Q("expand_wildcards", value); }

	/// <summary>
	/// <para>
	/// Specify whether the index should be flushed after performing the operation (default: true)
	/// </para>
	/// </summary>
	public bool? Flush { get => Q<bool?>("flush"); set => Q("flush", value); }

	/// <summary>
	/// <para>
	/// Whether specified concrete indices should be ignored when unavailable (missing or closed)
	/// </para>
	/// </summary>
	public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

	/// <summary>
	/// <para>
	/// The number of segments the index should be merged into (default: dynamic)
	/// </para>
	/// </summary>
	public long? MaxNumSegments { get => Q<long?>("max_num_segments"); set => Q("max_num_segments", value); }

	/// <summary>
	/// <para>
	/// Specify whether the operation should only expunge deleted documents
	/// </para>
	/// </summary>
	public bool? OnlyExpungeDeletes { get => Q<bool?>("only_expunge_deletes"); set => Q("only_expunge_deletes", value); }

	/// <summary>
	/// <para>
	/// Should the request wait until the force merge is completed.
	/// </para>
	/// </summary>
	public bool? WaitForCompletion { get => Q<bool?>("wait_for_completion"); set => Q("wait_for_completion", value); }
}

internal sealed partial class ForcemergeRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequest>
{
	public override Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Force a merge.
/// Perform the force merge operation on the shards of one or more indices.
/// For data streams, the API forces a merge on the shards of the stream's backing indices.
/// </para>
/// <para>
/// Merging reduces the number of segments in each shard by merging some of them together and also frees up the space used by deleted documents.
/// Merging normally happens automatically, but sometimes it is useful to trigger a merge manually.
/// </para>
/// <para>
/// WARNING: We recommend force merging only a read-only index (meaning the index is no longer receiving writes).
/// When documents are updated or deleted, the old version is not immediately removed but instead soft-deleted and marked with a "tombstone".
/// These soft-deleted documents are automatically cleaned up during regular segment merges.
/// But force merge can cause very large (greater than 5 GB) segments to be produced, which are not eligible for regular merges.
/// So the number of soft-deleted documents can then grow rapidly, resulting in higher disk usage and worse search performance.
/// If you regularly force merge an index receiving writes, this can also make snapshots more expensive, since the new documents can't be backed up incrementally.
/// </para>
/// <para>
/// <strong>Blocks during a force merge</strong>
/// </para>
/// <para>
/// Calls to this API block until the merge is complete (unless request contains <c>wait_for_completion=false</c>).
/// If the client connection is lost before completion then the force merge process will continue in the background.
/// Any new requests to force merge the same indices will also block until the ongoing force merge is complete.
/// </para>
/// <para>
/// <strong>Running force merge asynchronously</strong>
/// </para>
/// <para>
/// If the request contains <c>wait_for_completion=false</c>, Elasticsearch performs some preflight checks, launches the request, and returns a task you can use to get the status of the task.
/// However, you can not cancel this task as the force merge task is not cancelable.
/// Elasticsearch creates a record of this task as a document at <c>_tasks/&lt;task_id></c>.
/// When you are done with a task, you should delete the task document so Elasticsearch can reclaim the space.
/// </para>
/// <para>
/// <strong>Force merging multiple indices</strong>
/// </para>
/// <para>
/// You can force merge multiple indices with a single request by targeting:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// One or more data streams that contain multiple backing indices
/// </para>
/// </item>
/// <item>
/// <para>
/// Multiple indices
/// </para>
/// </item>
/// <item>
/// <para>
/// One or more aliases
/// </para>
/// </item>
/// <item>
/// <para>
/// All data streams and indices in a cluster
/// </para>
/// </item>
/// </list>
/// <para>
/// Each targeted shard is force-merged separately using the force_merge threadpool.
/// By default each node only has a single <c>force_merge</c> thread which means that the shards on that node are force-merged one at a time.
/// If you expand the <c>force_merge</c> threadpool on a node then it will force merge its shards in parallel
/// </para>
/// <para>
/// Force merge makes the storage for the shard being merged temporarily increase, as it may require free space up to triple its size in case <c>max_num_segments parameter</c> is set to <c>1</c>, to rewrite all segments into a new one.
/// </para>
/// <para>
/// <strong>Data streams and time-based indices</strong>
/// </para>
/// <para>
/// Force-merging is useful for managing a data stream's older backing indices and other time-based indices, particularly after a rollover.
/// In these cases, each index only receives indexing traffic for a certain period of time.
/// Once an index receive no more writes, its shards can be force-merged to a single segment.
/// This can be a good idea because single-segment shards can sometimes use simpler and more efficient data structures to perform searches.
/// For example:
/// </para>
/// <code>
/// POST /.ds-my-data-stream-2099.03.07-000001/_forcemerge?max_num_segments=1
/// </code>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestConverter))]
public sealed partial class ForcemergeRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestParameters>
{
	public ForcemergeRequest(Elastic.Clients.Elasticsearch.Indices? indices) : base(r => r.Optional("index", indices))
	{
	}
#if NET7_0_OR_GREATER
	public ForcemergeRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public ForcemergeRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ForcemergeRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.IndexManagementForcemerge;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.POST;

	internal override bool SupportsBody => false;

	internal override string OperationName => "indices.forcemerge";

	/// <summary>
	/// <para>
	/// A comma-separated list of index names; use <c>_all</c> or empty string to perform the operation on all indices
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Indices? Indices { get => P<Elastic.Clients.Elasticsearch.Indices?>("index"); set => PO("index", value); }

	/// <summary>
	/// <para>
	/// Whether to ignore if a wildcard indices expression resolves into no concrete indices. (This includes <c>_all</c> string or when no indices have been specified)
	/// </para>
	/// </summary>
	public bool? AllowNoIndices { get => Q<bool?>("allow_no_indices"); set => Q("allow_no_indices", value); }

	/// <summary>
	/// <para>
	/// Whether to expand wildcard expression to concrete indices that are open, closed or both.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? ExpandWildcards { get => Q<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>?>("expand_wildcards"); set => Q("expand_wildcards", value); }

	/// <summary>
	/// <para>
	/// Specify whether the index should be flushed after performing the operation (default: true)
	/// </para>
	/// </summary>
	public bool? Flush { get => Q<bool?>("flush"); set => Q("flush", value); }

	/// <summary>
	/// <para>
	/// Whether specified concrete indices should be ignored when unavailable (missing or closed)
	/// </para>
	/// </summary>
	public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

	/// <summary>
	/// <para>
	/// The number of segments the index should be merged into (default: dynamic)
	/// </para>
	/// </summary>
	public long? MaxNumSegments { get => Q<long?>("max_num_segments"); set => Q("max_num_segments", value); }

	/// <summary>
	/// <para>
	/// Specify whether the operation should only expunge deleted documents
	/// </para>
	/// </summary>
	public bool? OnlyExpungeDeletes { get => Q<bool?>("only_expunge_deletes"); set => Q("only_expunge_deletes", value); }

	/// <summary>
	/// <para>
	/// Should the request wait until the force merge is completed.
	/// </para>
	/// </summary>
	public bool? WaitForCompletion { get => Q<bool?>("wait_for_completion"); set => Q("wait_for_completion", value); }
}

/// <summary>
/// <para>
/// Force a merge.
/// Perform the force merge operation on the shards of one or more indices.
/// For data streams, the API forces a merge on the shards of the stream's backing indices.
/// </para>
/// <para>
/// Merging reduces the number of segments in each shard by merging some of them together and also frees up the space used by deleted documents.
/// Merging normally happens automatically, but sometimes it is useful to trigger a merge manually.
/// </para>
/// <para>
/// WARNING: We recommend force merging only a read-only index (meaning the index is no longer receiving writes).
/// When documents are updated or deleted, the old version is not immediately removed but instead soft-deleted and marked with a "tombstone".
/// These soft-deleted documents are automatically cleaned up during regular segment merges.
/// But force merge can cause very large (greater than 5 GB) segments to be produced, which are not eligible for regular merges.
/// So the number of soft-deleted documents can then grow rapidly, resulting in higher disk usage and worse search performance.
/// If you regularly force merge an index receiving writes, this can also make snapshots more expensive, since the new documents can't be backed up incrementally.
/// </para>
/// <para>
/// <strong>Blocks during a force merge</strong>
/// </para>
/// <para>
/// Calls to this API block until the merge is complete (unless request contains <c>wait_for_completion=false</c>).
/// If the client connection is lost before completion then the force merge process will continue in the background.
/// Any new requests to force merge the same indices will also block until the ongoing force merge is complete.
/// </para>
/// <para>
/// <strong>Running force merge asynchronously</strong>
/// </para>
/// <para>
/// If the request contains <c>wait_for_completion=false</c>, Elasticsearch performs some preflight checks, launches the request, and returns a task you can use to get the status of the task.
/// However, you can not cancel this task as the force merge task is not cancelable.
/// Elasticsearch creates a record of this task as a document at <c>_tasks/&lt;task_id></c>.
/// When you are done with a task, you should delete the task document so Elasticsearch can reclaim the space.
/// </para>
/// <para>
/// <strong>Force merging multiple indices</strong>
/// </para>
/// <para>
/// You can force merge multiple indices with a single request by targeting:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// One or more data streams that contain multiple backing indices
/// </para>
/// </item>
/// <item>
/// <para>
/// Multiple indices
/// </para>
/// </item>
/// <item>
/// <para>
/// One or more aliases
/// </para>
/// </item>
/// <item>
/// <para>
/// All data streams and indices in a cluster
/// </para>
/// </item>
/// </list>
/// <para>
/// Each targeted shard is force-merged separately using the force_merge threadpool.
/// By default each node only has a single <c>force_merge</c> thread which means that the shards on that node are force-merged one at a time.
/// If you expand the <c>force_merge</c> threadpool on a node then it will force merge its shards in parallel
/// </para>
/// <para>
/// Force merge makes the storage for the shard being merged temporarily increase, as it may require free space up to triple its size in case <c>max_num_segments parameter</c> is set to <c>1</c>, to rewrite all segments into a new one.
/// </para>
/// <para>
/// <strong>Data streams and time-based indices</strong>
/// </para>
/// <para>
/// Force-merging is useful for managing a data stream's older backing indices and other time-based indices, particularly after a rollover.
/// In these cases, each index only receives indexing traffic for a certain period of time.
/// Once an index receive no more writes, its shards can be force-merged to a single segment.
/// This can be a good idea because single-segment shards can sometimes use simpler and more efficient data structures to perform searches.
/// For example:
/// </para>
/// <code>
/// POST /.ds-my-data-stream-2099.03.07-000001/_forcemerge?max_num_segments=1
/// </code>
/// </summary>
public readonly partial struct ForcemergeRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ForcemergeRequestDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequest instance)
	{
		Instance = instance;
	}

	public ForcemergeRequestDescriptor(Elastic.Clients.Elasticsearch.Indices? indices)
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequest(indices);
	}

	public ForcemergeRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequest instance) => new Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequest(Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A comma-separated list of index names; use <c>_all</c> or empty string to perform the operation on all indices
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor Indices(Elastic.Clients.Elasticsearch.Indices? value)
	{
		Instance.Indices = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Whether to ignore if a wildcard indices expression resolves into no concrete indices. (This includes <c>_all</c> string or when no indices have been specified)
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor AllowNoIndices(bool? value = true)
	{
		Instance.AllowNoIndices = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Whether to expand wildcard expression to concrete indices that are open, closed or both.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor ExpandWildcards(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? value)
	{
		Instance.ExpandWildcards = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Whether to expand wildcard expression to concrete indices that are open, closed or both.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor ExpandWildcards(params Elastic.Clients.Elasticsearch.ExpandWildcard[] values)
	{
		Instance.ExpandWildcards = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Specify whether the index should be flushed after performing the operation (default: true)
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor Flush(bool? value = true)
	{
		Instance.Flush = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Whether specified concrete indices should be ignored when unavailable (missing or closed)
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor IgnoreUnavailable(bool? value = true)
	{
		Instance.IgnoreUnavailable = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The number of segments the index should be merged into (default: dynamic)
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor MaxNumSegments(long? value)
	{
		Instance.MaxNumSegments = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specify whether the operation should only expunge deleted documents
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor OnlyExpungeDeletes(bool? value = true)
	{
		Instance.OnlyExpungeDeletes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Should the request wait until the force merge is completed.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor WaitForCompletion(bool? value = true)
	{
		Instance.WaitForCompletion = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequest Build(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor(new Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}

/// <summary>
/// <para>
/// Force a merge.
/// Perform the force merge operation on the shards of one or more indices.
/// For data streams, the API forces a merge on the shards of the stream's backing indices.
/// </para>
/// <para>
/// Merging reduces the number of segments in each shard by merging some of them together and also frees up the space used by deleted documents.
/// Merging normally happens automatically, but sometimes it is useful to trigger a merge manually.
/// </para>
/// <para>
/// WARNING: We recommend force merging only a read-only index (meaning the index is no longer receiving writes).
/// When documents are updated or deleted, the old version is not immediately removed but instead soft-deleted and marked with a "tombstone".
/// These soft-deleted documents are automatically cleaned up during regular segment merges.
/// But force merge can cause very large (greater than 5 GB) segments to be produced, which are not eligible for regular merges.
/// So the number of soft-deleted documents can then grow rapidly, resulting in higher disk usage and worse search performance.
/// If you regularly force merge an index receiving writes, this can also make snapshots more expensive, since the new documents can't be backed up incrementally.
/// </para>
/// <para>
/// <strong>Blocks during a force merge</strong>
/// </para>
/// <para>
/// Calls to this API block until the merge is complete (unless request contains <c>wait_for_completion=false</c>).
/// If the client connection is lost before completion then the force merge process will continue in the background.
/// Any new requests to force merge the same indices will also block until the ongoing force merge is complete.
/// </para>
/// <para>
/// <strong>Running force merge asynchronously</strong>
/// </para>
/// <para>
/// If the request contains <c>wait_for_completion=false</c>, Elasticsearch performs some preflight checks, launches the request, and returns a task you can use to get the status of the task.
/// However, you can not cancel this task as the force merge task is not cancelable.
/// Elasticsearch creates a record of this task as a document at <c>_tasks/&lt;task_id></c>.
/// When you are done with a task, you should delete the task document so Elasticsearch can reclaim the space.
/// </para>
/// <para>
/// <strong>Force merging multiple indices</strong>
/// </para>
/// <para>
/// You can force merge multiple indices with a single request by targeting:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// One or more data streams that contain multiple backing indices
/// </para>
/// </item>
/// <item>
/// <para>
/// Multiple indices
/// </para>
/// </item>
/// <item>
/// <para>
/// One or more aliases
/// </para>
/// </item>
/// <item>
/// <para>
/// All data streams and indices in a cluster
/// </para>
/// </item>
/// </list>
/// <para>
/// Each targeted shard is force-merged separately using the force_merge threadpool.
/// By default each node only has a single <c>force_merge</c> thread which means that the shards on that node are force-merged one at a time.
/// If you expand the <c>force_merge</c> threadpool on a node then it will force merge its shards in parallel
/// </para>
/// <para>
/// Force merge makes the storage for the shard being merged temporarily increase, as it may require free space up to triple its size in case <c>max_num_segments parameter</c> is set to <c>1</c>, to rewrite all segments into a new one.
/// </para>
/// <para>
/// <strong>Data streams and time-based indices</strong>
/// </para>
/// <para>
/// Force-merging is useful for managing a data stream's older backing indices and other time-based indices, particularly after a rollover.
/// In these cases, each index only receives indexing traffic for a certain period of time.
/// Once an index receive no more writes, its shards can be force-merged to a single segment.
/// This can be a good idea because single-segment shards can sometimes use simpler and more efficient data structures to perform searches.
/// For example:
/// </para>
/// <code>
/// POST /.ds-my-data-stream-2099.03.07-000001/_forcemerge?max_num_segments=1
/// </code>
/// </summary>
public readonly partial struct ForcemergeRequestDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ForcemergeRequestDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequest instance)
	{
		Instance = instance;
	}

	public ForcemergeRequestDescriptor(Elastic.Clients.Elasticsearch.Indices? indices)
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequest(indices);
	}

	public ForcemergeRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor<TDocument>(Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequest instance) => new Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequest(Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A comma-separated list of index names; use <c>_all</c> or empty string to perform the operation on all indices
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor<TDocument> Indices(Elastic.Clients.Elasticsearch.Indices? value)
	{
		Instance.Indices = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Whether to ignore if a wildcard indices expression resolves into no concrete indices. (This includes <c>_all</c> string or when no indices have been specified)
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor<TDocument> AllowNoIndices(bool? value = true)
	{
		Instance.AllowNoIndices = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Whether to expand wildcard expression to concrete indices that are open, closed or both.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor<TDocument> ExpandWildcards(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? value)
	{
		Instance.ExpandWildcards = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Whether to expand wildcard expression to concrete indices that are open, closed or both.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor<TDocument> ExpandWildcards(params Elastic.Clients.Elasticsearch.ExpandWildcard[] values)
	{
		Instance.ExpandWildcards = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Specify whether the index should be flushed after performing the operation (default: true)
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor<TDocument> Flush(bool? value = true)
	{
		Instance.Flush = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Whether specified concrete indices should be ignored when unavailable (missing or closed)
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor<TDocument> IgnoreUnavailable(bool? value = true)
	{
		Instance.IgnoreUnavailable = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The number of segments the index should be merged into (default: dynamic)
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor<TDocument> MaxNumSegments(long? value)
	{
		Instance.MaxNumSegments = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specify whether the operation should only expunge deleted documents
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor<TDocument> OnlyExpungeDeletes(bool? value = true)
	{
		Instance.OnlyExpungeDeletes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Should the request wait until the force merge is completed.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor<TDocument> WaitForCompletion(bool? value = true)
	{
		Instance.WaitForCompletion = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequest Build(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor<TDocument>>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor<TDocument> ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor<TDocument> FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor<TDocument> Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor<TDocument> Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor<TDocument> SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor<TDocument> RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ForcemergeRequestDescriptor<TDocument> RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}