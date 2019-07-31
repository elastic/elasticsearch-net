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
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Specification.ClusterApi;

// ReSharper disable RedundantBaseConstructorCall
// ReSharper disable UnusedTypeParameter
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier
namespace Nest
{
	///<summary>descriptor for AllocationExplain <para>http://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-allocation-explain.html</para></summary>
	public partial class ClusterAllocationExplainDescriptor : RequestDescriptorBase<ClusterAllocationExplainDescriptor, ClusterAllocationExplainRequestParameters, IClusterAllocationExplainRequest>, IClusterAllocationExplainRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.ClusterAllocationExplain;
		// values part of the url path
		// Request parameters
		///<summary>Return information about disk usage and shard sizes (default: false)</summary>
		public ClusterAllocationExplainDescriptor IncludeDiskInfo(bool? includediskinfo = true) => Qs("include_disk_info", includediskinfo);
		///<summary>Return 'YES' decisions in explanation (default: false)</summary>
		public ClusterAllocationExplainDescriptor IncludeYesDecisions(bool? includeyesdecisions = true) => Qs("include_yes_decisions", includeyesdecisions);
	}

	///<summary>descriptor for GetSettings <para>http://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-update-settings.html</para></summary>
	public partial class ClusterGetSettingsDescriptor : RequestDescriptorBase<ClusterGetSettingsDescriptor, ClusterGetSettingsRequestParameters, IClusterGetSettingsRequest>, IClusterGetSettingsRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.ClusterGetSettings;
		// values part of the url path
		// Request parameters
		///<summary>Return settings in flat format (default: false)</summary>
		public ClusterGetSettingsDescriptor FlatSettings(bool? flatsettings = true) => Qs("flat_settings", flatsettings);
		///<summary>Whether to return all default clusters setting.</summary>
		public ClusterGetSettingsDescriptor IncludeDefaults(bool? includedefaults = true) => Qs("include_defaults", includedefaults);
		///<summary>Explicit operation timeout for connection to master node</summary>
		public ClusterGetSettingsDescriptor MasterTimeout(Time mastertimeout) => Qs("master_timeout", mastertimeout);
		///<summary>Explicit operation timeout</summary>
		public ClusterGetSettingsDescriptor Timeout(Time timeout) => Qs("timeout", timeout);
	}

	///<summary>descriptor for Health <para>http://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-health.html</para></summary>
	public partial class ClusterHealthDescriptor : RequestDescriptorBase<ClusterHealthDescriptor, ClusterHealthRequestParameters, IClusterHealthRequest>, IClusterHealthRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.ClusterHealth;
		///<summary>/_cluster/health</summary>
		public ClusterHealthDescriptor(): base()
		{
		}

		///<summary>/_cluster/health/{index}</summary>
		///<param name = "index">Optional, accepts null</param>
		public ClusterHealthDescriptor(Indices index): base(r => r.Optional("index", index))
		{
		}

		// values part of the url path
		Indices IClusterHealthRequest.Index => Self.RouteValues.Get<Indices>("index");
		///<summary>Limit the information returned to a specific index</summary>
		public ClusterHealthDescriptor Index(Indices index) => Assign(index, (a, v) => a.RouteValues.Optional("index", v));
		///<summary>a shortcut into calling Index(typeof(TOther))</summary>
		public ClusterHealthDescriptor Index<TOther>()
			where TOther : class => Assign(typeof(TOther), (a, v) => a.RouteValues.Optional("index", (Indices)v));
		///<summary>A shortcut into calling Index(Indices.All)</summary>
		public ClusterHealthDescriptor AllIndices() => Index(Indices.All);
		// Request parameters
		///<summary>Specify the level of detail for returned information</summary>
		public ClusterHealthDescriptor Level(Level? level) => Qs("level", level);
		///<summary>Return local information, do not retrieve the state from master node (default: false)</summary>
		public ClusterHealthDescriptor Local(bool? local = true) => Qs("local", local);
		///<summary>Explicit operation timeout for connection to master node</summary>
		public ClusterHealthDescriptor MasterTimeout(Time mastertimeout) => Qs("master_timeout", mastertimeout);
		///<summary>Explicit operation timeout</summary>
		public ClusterHealthDescriptor Timeout(Time timeout) => Qs("timeout", timeout);
		///<summary>Wait until the specified number of shards is active</summary>
		public ClusterHealthDescriptor WaitForActiveShards(string waitforactiveshards) => Qs("wait_for_active_shards", waitforactiveshards);
		///<summary>Wait until all currently queued events with the given priority are processed</summary>
		public ClusterHealthDescriptor WaitForEvents(WaitForEvents? waitforevents) => Qs("wait_for_events", waitforevents);
		///<summary>Whether to wait until there are no initializing shards in the cluster</summary>
		public ClusterHealthDescriptor WaitForNoInitializingShards(bool? waitfornoinitializingshards = true) => Qs("wait_for_no_initializing_shards", waitfornoinitializingshards);
		///<summary>Whether to wait until there are no relocating shards in the cluster</summary>
		public ClusterHealthDescriptor WaitForNoRelocatingShards(bool? waitfornorelocatingshards = true) => Qs("wait_for_no_relocating_shards", waitfornorelocatingshards);
		///<summary>Wait until the specified number of nodes is available</summary>
		public ClusterHealthDescriptor WaitForNodes(string waitfornodes) => Qs("wait_for_nodes", waitfornodes);
		///<summary>Wait until cluster is in a specific state</summary>
		public ClusterHealthDescriptor WaitForStatus(WaitForStatus? waitforstatus) => Qs("wait_for_status", waitforstatus);
	}

	///<summary>descriptor for PendingTasks <para>http://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-pending.html</para></summary>
	public partial class ClusterPendingTasksDescriptor : RequestDescriptorBase<ClusterPendingTasksDescriptor, ClusterPendingTasksRequestParameters, IClusterPendingTasksRequest>, IClusterPendingTasksRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.ClusterPendingTasks;
		// values part of the url path
		// Request parameters
		///<summary>Return local information, do not retrieve the state from master node (default: false)</summary>
		public ClusterPendingTasksDescriptor Local(bool? local = true) => Qs("local", local);
		///<summary>Specify timeout for connection to master</summary>
		public ClusterPendingTasksDescriptor MasterTimeout(Time mastertimeout) => Qs("master_timeout", mastertimeout);
	}

	///<summary>descriptor for PutSettings <para>http://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-update-settings.html</para></summary>
	public partial class ClusterPutSettingsDescriptor : RequestDescriptorBase<ClusterPutSettingsDescriptor, ClusterPutSettingsRequestParameters, IClusterPutSettingsRequest>, IClusterPutSettingsRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.ClusterPutSettings;
		// values part of the url path
		// Request parameters
		///<summary>Return settings in flat format (default: false)</summary>
		public ClusterPutSettingsDescriptor FlatSettings(bool? flatsettings = true) => Qs("flat_settings", flatsettings);
		///<summary>Explicit operation timeout for connection to master node</summary>
		public ClusterPutSettingsDescriptor MasterTimeout(Time mastertimeout) => Qs("master_timeout", mastertimeout);
		///<summary>Explicit operation timeout</summary>
		public ClusterPutSettingsDescriptor Timeout(Time timeout) => Qs("timeout", timeout);
	}

	///<summary>descriptor for RemoteInfo <para>http://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-remote-info.html</para></summary>
	public partial class RemoteInfoDescriptor : RequestDescriptorBase<RemoteInfoDescriptor, RemoteInfoRequestParameters, IRemoteInfoRequest>, IRemoteInfoRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.ClusterRemoteInfo;
	// values part of the url path
	// Request parameters
	}

	///<summary>descriptor for Reroute <para>http://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-reroute.html</para></summary>
	public partial class ClusterRerouteDescriptor : RequestDescriptorBase<ClusterRerouteDescriptor, ClusterRerouteRequestParameters, IClusterRerouteRequest>, IClusterRerouteRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.ClusterReroute;
		// values part of the url path
		// Request parameters
		///<summary>Simulate the operation only and return the resulting state</summary>
		public ClusterRerouteDescriptor DryRun(bool? dryrun = true) => Qs("dry_run", dryrun);
		///<summary>Return an explanation of why the commands can or cannot be executed</summary>
		public ClusterRerouteDescriptor Explain(bool? explain = true) => Qs("explain", explain);
		///<summary>Explicit operation timeout for connection to master node</summary>
		public ClusterRerouteDescriptor MasterTimeout(Time mastertimeout) => Qs("master_timeout", mastertimeout);
		///<summary>Limit the information returned to the specified metrics. Defaults to all but metadata</summary>
		public ClusterRerouteDescriptor Metric(params string[] metric) => Qs("metric", metric);
		///<summary>Retries allocation of shards that are blocked due to too many subsequent allocation failures</summary>
		public ClusterRerouteDescriptor RetryFailed(bool? retryfailed = true) => Qs("retry_failed", retryfailed);
		///<summary>Explicit operation timeout</summary>
		public ClusterRerouteDescriptor Timeout(Time timeout) => Qs("timeout", timeout);
	}

	///<summary>descriptor for State <para>http://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-state.html</para></summary>
	public partial class ClusterStateDescriptor : RequestDescriptorBase<ClusterStateDescriptor, ClusterStateRequestParameters, IClusterStateRequest>, IClusterStateRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.ClusterState;
		///<summary>/_cluster/state</summary>
		public ClusterStateDescriptor(): base()
		{
		}

		///<summary>/_cluster/state/{metric}</summary>
		///<param name = "metric">Optional, accepts null</param>
		public ClusterStateDescriptor(Metrics metric): base(r => r.Optional("metric", metric))
		{
		}

		///<summary>/_cluster/state/{metric}/{index}</summary>
		///<param name = "metric">Optional, accepts null</param>
		///<param name = "index">Optional, accepts null</param>
		public ClusterStateDescriptor(Metrics metric, Indices index): base(r => r.Optional("metric", metric).Optional("index", index))
		{
		}

		// values part of the url path
		Metrics IClusterStateRequest.Metric => Self.RouteValues.Get<Metrics>("metric");
		Indices IClusterStateRequest.Index => Self.RouteValues.Get<Indices>("index");
		///<summary>Limit the information returned to the specified metrics</summary>
		public ClusterStateDescriptor Metric(Metrics metric) => Assign(metric, (a, v) => a.RouteValues.Optional("metric", v));
		///<summary>A comma-separated list of index names; use the special string `_all` or Indices.All to perform the operation on all indices</summary>
		public ClusterStateDescriptor Index(Indices index) => Assign(index, (a, v) => a.RouteValues.Optional("index", v));
		///<summary>a shortcut into calling Index(typeof(TOther))</summary>
		public ClusterStateDescriptor Index<TOther>()
			where TOther : class => Assign(typeof(TOther), (a, v) => a.RouteValues.Optional("index", (Indices)v));
		///<summary>A shortcut into calling Index(Indices.All)</summary>
		public ClusterStateDescriptor AllIndices() => Index(Indices.All);
		// Request parameters
		///<summary>Whether to ignore if a wildcard indices expression resolves into no concrete indices. (This includes `_all` string or when no indices have been specified)</summary>
		public ClusterStateDescriptor AllowNoIndices(bool? allownoindices = true) => Qs("allow_no_indices", allownoindices);
		///<summary>Whether to expand wildcard expression to concrete indices that are open, closed or both.</summary>
		public ClusterStateDescriptor ExpandWildcards(ExpandWildcards? expandwildcards) => Qs("expand_wildcards", expandwildcards);
		///<summary>Return settings in flat format (default: false)</summary>
		public ClusterStateDescriptor FlatSettings(bool? flatsettings = true) => Qs("flat_settings", flatsettings);
		///<summary>Whether specified concrete indices should be ignored when unavailable (missing or closed)</summary>
		public ClusterStateDescriptor IgnoreUnavailable(bool? ignoreunavailable = true) => Qs("ignore_unavailable", ignoreunavailable);
		///<summary>Return local information, do not retrieve the state from master node (default: false)</summary>
		public ClusterStateDescriptor Local(bool? local = true) => Qs("local", local);
		///<summary>Specify timeout for connection to master</summary>
		public ClusterStateDescriptor MasterTimeout(Time mastertimeout) => Qs("master_timeout", mastertimeout);
		///<summary>Wait for the metadata version to be equal or greater than the specified metadata version</summary>
		public ClusterStateDescriptor WaitForMetadataVersion(long? waitformetadataversion) => Qs("wait_for_metadata_version", waitformetadataversion);
		///<summary>The maximum time to wait for wait_for_metadata_version before timing out</summary>
		public ClusterStateDescriptor WaitForTimeout(Time waitfortimeout) => Qs("wait_for_timeout", waitfortimeout);
	}

	///<summary>descriptor for Stats <para>http://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-stats.html</para></summary>
	public partial class ClusterStatsDescriptor : RequestDescriptorBase<ClusterStatsDescriptor, ClusterStatsRequestParameters, IClusterStatsRequest>, IClusterStatsRequest
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.ClusterStats;
		///<summary>/_cluster/stats</summary>
		public ClusterStatsDescriptor(): base()
		{
		}

		///<summary>/_cluster/stats/nodes/{node_id}</summary>
		///<param name = "nodeId">Optional, accepts null</param>
		public ClusterStatsDescriptor(NodeIds nodeId): base(r => r.Optional("node_id", nodeId))
		{
		}

		// values part of the url path
		NodeIds IClusterStatsRequest.NodeId => Self.RouteValues.Get<NodeIds>("node_id");
		///<summary>A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you're connecting to, leave empty to get information from all nodes</summary>
		public ClusterStatsDescriptor NodeId(NodeIds nodeId) => Assign(nodeId, (a, v) => a.RouteValues.Optional("node_id", v));
		// Request parameters
		///<summary>Return settings in flat format (default: false)</summary>
		public ClusterStatsDescriptor FlatSettings(bool? flatsettings = true) => Qs("flat_settings", flatsettings);
		///<summary>Explicit operation timeout</summary>
		public ClusterStatsDescriptor Timeout(Time timeout) => Qs("timeout", timeout);
	}
}