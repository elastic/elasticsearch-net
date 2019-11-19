// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗  
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝  
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// -----------------------------------------------
//  
// This file is automatically generated 
// Please do not edit these files manually
// Run the following in the root of the repos:
//
// 		*NIX 		:	./build.sh codegen
// 		Windows 	:	build.bat codegen
//
// -----------------------------------------------
// ReSharper disable RedundantUsingDirective
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

// ReSharper disable once CheckNamespace
namespace Elasticsearch.Net.Specification.ClusterApi
{
	///<summary>Request options for AllocationExplain <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-allocation-explain.html</para></summary>
	public class ClusterAllocationExplainRequestParameters : RequestParameters<ClusterAllocationExplainRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.POST;
		///<summary>Return information about disk usage and shard sizes (default: false)</summary>
		public bool? IncludeDiskInfo
		{
			get => Q<bool? >("include_disk_info");
			set => Q("include_disk_info", value);
		}

		///<summary>Return 'YES' decisions in explanation (default: false)</summary>
		public bool? IncludeYesDecisions
		{
			get => Q<bool? >("include_yes_decisions");
			set => Q("include_yes_decisions", value);
		}
	}

	///<summary>Request options for GetSettings <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-update-settings.html</para></summary>
	public class ClusterGetSettingsRequestParameters : RequestParameters<ClusterGetSettingsRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.GET;
		///<summary>Return settings in flat format (default: false)</summary>
		public bool? FlatSettings
		{
			get => Q<bool? >("flat_settings");
			set => Q("flat_settings", value);
		}

		///<summary>Whether to return all default clusters setting.</summary>
		public bool? IncludeDefaults
		{
			get => Q<bool? >("include_defaults");
			set => Q("include_defaults", value);
		}

		///<summary>Explicit operation timeout for connection to master node</summary>
		public TimeSpan MasterTimeout
		{
			get => Q<TimeSpan>("master_timeout");
			set => Q("master_timeout", value);
		}

		///<summary>Explicit operation timeout</summary>
		public TimeSpan Timeout
		{
			get => Q<TimeSpan>("timeout");
			set => Q("timeout", value);
		}
	}

	///<summary>Request options for Health <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-health.html</para></summary>
	public class ClusterHealthRequestParameters : RequestParameters<ClusterHealthRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.GET;
		///<summary>Whether to expand wildcard expression to concrete indices that are open, closed or both.</summary>
		public ExpandWildcards? ExpandWildcards
		{
			get => Q<ExpandWildcards? >("expand_wildcards");
			set => Q("expand_wildcards", value);
		}

		///<summary>Specify the level of detail for returned information</summary>
		public Level? Level
		{
			get => Q<Level? >("level");
			set => Q("level", value);
		}

		///<summary>Return local information, do not retrieve the state from master node (default: false)</summary>
		public bool? Local
		{
			get => Q<bool? >("local");
			set => Q("local", value);
		}

		///<summary>Explicit operation timeout for connection to master node</summary>
		public TimeSpan MasterTimeout
		{
			get => Q<TimeSpan>("master_timeout");
			set => Q("master_timeout", value);
		}

		///<summary>Explicit operation timeout</summary>
		public TimeSpan Timeout
		{
			get => Q<TimeSpan>("timeout");
			set => Q("timeout", value);
		}

		///<summary>Wait until the specified number of shards is active</summary>
		public string WaitForActiveShards
		{
			get => Q<string>("wait_for_active_shards");
			set => Q("wait_for_active_shards", value);
		}

		///<summary>Wait until all currently queued events with the given priority are processed</summary>
		public WaitForEvents? WaitForEvents
		{
			get => Q<WaitForEvents? >("wait_for_events");
			set => Q("wait_for_events", value);
		}

		///<summary>Whether to wait until there are no initializing shards in the cluster</summary>
		public bool? WaitForNoInitializingShards
		{
			get => Q<bool? >("wait_for_no_initializing_shards");
			set => Q("wait_for_no_initializing_shards", value);
		}

		///<summary>Whether to wait until there are no relocating shards in the cluster</summary>
		public bool? WaitForNoRelocatingShards
		{
			get => Q<bool? >("wait_for_no_relocating_shards");
			set => Q("wait_for_no_relocating_shards", value);
		}

		///<summary>Wait until the specified number of nodes is available</summary>
		public string WaitForNodes
		{
			get => Q<string>("wait_for_nodes");
			set => Q("wait_for_nodes", value);
		}

		///<summary>Wait until cluster is in a specific state</summary>
		public WaitForStatus? WaitForStatus
		{
			get => Q<WaitForStatus? >("wait_for_status");
			set => Q("wait_for_status", value);
		}
	}

	///<summary>Request options for PendingTasks <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-pending.html</para></summary>
	public class ClusterPendingTasksRequestParameters : RequestParameters<ClusterPendingTasksRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.GET;
		///<summary>Return local information, do not retrieve the state from master node (default: false)</summary>
		public bool? Local
		{
			get => Q<bool? >("local");
			set => Q("local", value);
		}

		///<summary>Specify timeout for connection to master</summary>
		public TimeSpan MasterTimeout
		{
			get => Q<TimeSpan>("master_timeout");
			set => Q("master_timeout", value);
		}
	}

	///<summary>Request options for PutSettings <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-update-settings.html</para></summary>
	public class ClusterPutSettingsRequestParameters : RequestParameters<ClusterPutSettingsRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.PUT;
		///<summary>Return settings in flat format (default: false)</summary>
		public bool? FlatSettings
		{
			get => Q<bool? >("flat_settings");
			set => Q("flat_settings", value);
		}

		///<summary>Explicit operation timeout for connection to master node</summary>
		public TimeSpan MasterTimeout
		{
			get => Q<TimeSpan>("master_timeout");
			set => Q("master_timeout", value);
		}

		///<summary>Explicit operation timeout</summary>
		public TimeSpan Timeout
		{
			get => Q<TimeSpan>("timeout");
			set => Q("timeout", value);
		}
	}

	///<summary>Request options for RemoteInfo <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-remote-info.html</para></summary>
	public class RemoteInfoRequestParameters : RequestParameters<RemoteInfoRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.GET;
	}

	///<summary>Request options for Reroute <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-reroute.html</para></summary>
	public class ClusterRerouteRequestParameters : RequestParameters<ClusterRerouteRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.POST;
		///<summary>Simulate the operation only and return the resulting state</summary>
		public bool? DryRun
		{
			get => Q<bool? >("dry_run");
			set => Q("dry_run", value);
		}

		///<summary>Return an explanation of why the commands can or cannot be executed</summary>
		public bool? Explain
		{
			get => Q<bool? >("explain");
			set => Q("explain", value);
		}

		///<summary>Explicit operation timeout for connection to master node</summary>
		public TimeSpan MasterTimeout
		{
			get => Q<TimeSpan>("master_timeout");
			set => Q("master_timeout", value);
		}

		///<summary>Limit the information returned to the specified metrics. Defaults to all but metadata</summary>
		public string[] Metric
		{
			get => Q<string[]>("metric");
			set => Q("metric", value);
		}

		///<summary>Retries allocation of shards that are blocked due to too many subsequent allocation failures</summary>
		public bool? RetryFailed
		{
			get => Q<bool? >("retry_failed");
			set => Q("retry_failed", value);
		}

		///<summary>Explicit operation timeout</summary>
		public TimeSpan Timeout
		{
			get => Q<TimeSpan>("timeout");
			set => Q("timeout", value);
		}
	}

	///<summary>Request options for State <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-state.html</para></summary>
	public class ClusterStateRequestParameters : RequestParameters<ClusterStateRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.GET;
		///<summary>
		/// Whether to ignore if a wildcard indices expression resolves into no concrete indices. (This includes `_all` string or when no indices have
		/// been specified)
		///</summary>
		public bool? AllowNoIndices
		{
			get => Q<bool? >("allow_no_indices");
			set => Q("allow_no_indices", value);
		}

		///<summary>Whether to expand wildcard expression to concrete indices that are open, closed or both.</summary>
		public ExpandWildcards? ExpandWildcards
		{
			get => Q<ExpandWildcards? >("expand_wildcards");
			set => Q("expand_wildcards", value);
		}

		///<summary>Return settings in flat format (default: false)</summary>
		public bool? FlatSettings
		{
			get => Q<bool? >("flat_settings");
			set => Q("flat_settings", value);
		}

		///<summary>Whether specified concrete indices should be ignored when unavailable (missing or closed)</summary>
		public bool? IgnoreUnavailable
		{
			get => Q<bool? >("ignore_unavailable");
			set => Q("ignore_unavailable", value);
		}

		///<summary>Return local information, do not retrieve the state from master node (default: false)</summary>
		public bool? Local
		{
			get => Q<bool? >("local");
			set => Q("local", value);
		}

		///<summary>Specify timeout for connection to master</summary>
		public TimeSpan MasterTimeout
		{
			get => Q<TimeSpan>("master_timeout");
			set => Q("master_timeout", value);
		}

		///<summary>Wait for the metadata version to be equal or greater than the specified metadata version</summary>
		public long? WaitForMetadataVersion
		{
			get => Q<long? >("wait_for_metadata_version");
			set => Q("wait_for_metadata_version", value);
		}

		///<summary>The maximum time to wait for wait_for_metadata_version before timing out</summary>
		public TimeSpan WaitForTimeout
		{
			get => Q<TimeSpan>("wait_for_timeout");
			set => Q("wait_for_timeout", value);
		}
	}

	///<summary>Request options for Stats <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-stats.html</para></summary>
	public class ClusterStatsRequestParameters : RequestParameters<ClusterStatsRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.GET;
		///<summary>Return settings in flat format (default: false)</summary>
		public bool? FlatSettings
		{
			get => Q<bool? >("flat_settings");
			set => Q("flat_settings", value);
		}

		///<summary>Explicit operation timeout</summary>
		public TimeSpan Timeout
		{
			get => Q<TimeSpan>("timeout");
			set => Q("timeout", value);
		}
	}
}