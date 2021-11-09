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

using System;
using System.Threading;
using System.Threading.Tasks;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Cluster
{
	public class ClusterNamespace : NamespacedClientProxy
	{
		internal ClusterNamespace(ElasticClient client) : base(client)
		{
		}

		public ClusterAllocationExplainResponse AllocationExplain(ClusterAllocationExplainRequest request) => DoRequest<ClusterAllocationExplainRequest, ClusterAllocationExplainResponse>(request);
		public Task<ClusterAllocationExplainResponse> AllocationExplainAsync(ClusterAllocationExplainRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<ClusterAllocationExplainRequest, ClusterAllocationExplainResponse>(request, cancellationToken);
		public ClusterAllocationExplainResponse AllocationExplain(Action<ClusterAllocationExplainRequestDescriptor> configureRequest = null)
		{
			var descriptor = new ClusterAllocationExplainRequestDescriptor();
			configureRequest?.Invoke(descriptor);
			return DoRequest<ClusterAllocationExplainRequestDescriptor, ClusterAllocationExplainResponse>(descriptor);
		}

		public Task<ClusterAllocationExplainResponse> AllocationExplainAsync(Action<ClusterAllocationExplainRequestDescriptor> configureRequest = null, CancellationToken cancellationToken = default)
		{
			var descriptor = new ClusterAllocationExplainRequestDescriptor();
			configureRequest?.Invoke(descriptor);
			return DoRequestAsync<ClusterAllocationExplainRequestDescriptor, ClusterAllocationExplainResponse>(descriptor);
		}

		public ClusterDeleteComponentTemplateResponse DeleteComponentTemplate(ClusterDeleteComponentTemplateRequest request) => DoRequest<ClusterDeleteComponentTemplateRequest, ClusterDeleteComponentTemplateResponse>(request);
		public Task<ClusterDeleteComponentTemplateResponse> DeleteComponentTemplateAsync(ClusterDeleteComponentTemplateRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<ClusterDeleteComponentTemplateRequest, ClusterDeleteComponentTemplateResponse>(request, cancellationToken);
		public ClusterDeleteComponentTemplateResponse DeleteComponentTemplate(Elastic.Clients.Elasticsearch.Name name, Action<ClusterDeleteComponentTemplateRequestDescriptor> configureRequest = null)
		{
			var descriptor = new ClusterDeleteComponentTemplateRequestDescriptor(name);
			configureRequest?.Invoke(descriptor);
			return DoRequest<ClusterDeleteComponentTemplateRequestDescriptor, ClusterDeleteComponentTemplateResponse>(descriptor);
		}

		public Task<ClusterDeleteComponentTemplateResponse> DeleteComponentTemplateAsync(Elastic.Clients.Elasticsearch.Name name, Action<ClusterDeleteComponentTemplateRequestDescriptor> configureRequest = null, CancellationToken cancellationToken = default)
		{
			var descriptor = new ClusterDeleteComponentTemplateRequestDescriptor(name);
			configureRequest?.Invoke(descriptor);
			return DoRequestAsync<ClusterDeleteComponentTemplateRequestDescriptor, ClusterDeleteComponentTemplateResponse>(descriptor);
		}

		public ClusterDeleteVotingConfigExclusionsResponse DeleteVotingConfigExclusions(ClusterDeleteVotingConfigExclusionsRequest request) => DoRequest<ClusterDeleteVotingConfigExclusionsRequest, ClusterDeleteVotingConfigExclusionsResponse>(request);
		public Task<ClusterDeleteVotingConfigExclusionsResponse> DeleteVotingConfigExclusionsAsync(ClusterDeleteVotingConfigExclusionsRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<ClusterDeleteVotingConfigExclusionsRequest, ClusterDeleteVotingConfigExclusionsResponse>(request, cancellationToken);
		public ClusterDeleteVotingConfigExclusionsResponse DeleteVotingConfigExclusions(Action<ClusterDeleteVotingConfigExclusionsRequestDescriptor> configureRequest = null)
		{
			var descriptor = new ClusterDeleteVotingConfigExclusionsRequestDescriptor();
			configureRequest?.Invoke(descriptor);
			return DoRequest<ClusterDeleteVotingConfigExclusionsRequestDescriptor, ClusterDeleteVotingConfigExclusionsResponse>(descriptor);
		}

		public Task<ClusterDeleteVotingConfigExclusionsResponse> DeleteVotingConfigExclusionsAsync(Action<ClusterDeleteVotingConfigExclusionsRequestDescriptor> configureRequest = null, CancellationToken cancellationToken = default)
		{
			var descriptor = new ClusterDeleteVotingConfigExclusionsRequestDescriptor();
			configureRequest?.Invoke(descriptor);
			return DoRequestAsync<ClusterDeleteVotingConfigExclusionsRequestDescriptor, ClusterDeleteVotingConfigExclusionsResponse>(descriptor);
		}

		public ClusterExistsComponentTemplateResponse ExistsComponentTemplate(ClusterExistsComponentTemplateRequest request) => DoRequest<ClusterExistsComponentTemplateRequest, ClusterExistsComponentTemplateResponse>(request);
		public Task<ClusterExistsComponentTemplateResponse> ExistsComponentTemplateAsync(ClusterExistsComponentTemplateRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<ClusterExistsComponentTemplateRequest, ClusterExistsComponentTemplateResponse>(request, cancellationToken);
		public ClusterExistsComponentTemplateResponse ExistsComponentTemplate(Elastic.Clients.Elasticsearch.Names name, Action<ClusterExistsComponentTemplateRequestDescriptor> configureRequest = null)
		{
			var descriptor = new ClusterExistsComponentTemplateRequestDescriptor(name);
			configureRequest?.Invoke(descriptor);
			return DoRequest<ClusterExistsComponentTemplateRequestDescriptor, ClusterExistsComponentTemplateResponse>(descriptor);
		}

		public Task<ClusterExistsComponentTemplateResponse> ExistsComponentTemplateAsync(Elastic.Clients.Elasticsearch.Names name, Action<ClusterExistsComponentTemplateRequestDescriptor> configureRequest = null, CancellationToken cancellationToken = default)
		{
			var descriptor = new ClusterExistsComponentTemplateRequestDescriptor(name);
			configureRequest?.Invoke(descriptor);
			return DoRequestAsync<ClusterExistsComponentTemplateRequestDescriptor, ClusterExistsComponentTemplateResponse>(descriptor);
		}

		public ClusterGetComponentTemplateResponse GetComponentTemplate(ClusterGetComponentTemplateRequest request) => DoRequest<ClusterGetComponentTemplateRequest, ClusterGetComponentTemplateResponse>(request);
		public Task<ClusterGetComponentTemplateResponse> GetComponentTemplateAsync(ClusterGetComponentTemplateRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<ClusterGetComponentTemplateRequest, ClusterGetComponentTemplateResponse>(request, cancellationToken);
		public ClusterGetComponentTemplateResponse GetComponentTemplate(Action<ClusterGetComponentTemplateRequestDescriptor> configureRequest = null)
		{
			var descriptor = new ClusterGetComponentTemplateRequestDescriptor();
			configureRequest?.Invoke(descriptor);
			return DoRequest<ClusterGetComponentTemplateRequestDescriptor, ClusterGetComponentTemplateResponse>(descriptor);
		}

		public Task<ClusterGetComponentTemplateResponse> GetComponentTemplateAsync(Action<ClusterGetComponentTemplateRequestDescriptor> configureRequest = null, CancellationToken cancellationToken = default)
		{
			var descriptor = new ClusterGetComponentTemplateRequestDescriptor();
			configureRequest?.Invoke(descriptor);
			return DoRequestAsync<ClusterGetComponentTemplateRequestDescriptor, ClusterGetComponentTemplateResponse>(descriptor);
		}

		public ClusterGetSettingsResponse GetSettings(ClusterGetSettingsRequest request) => DoRequest<ClusterGetSettingsRequest, ClusterGetSettingsResponse>(request);
		public Task<ClusterGetSettingsResponse> GetSettingsAsync(ClusterGetSettingsRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<ClusterGetSettingsRequest, ClusterGetSettingsResponse>(request, cancellationToken);
		public ClusterGetSettingsResponse GetSettings(Action<ClusterGetSettingsRequestDescriptor> configureRequest = null)
		{
			var descriptor = new ClusterGetSettingsRequestDescriptor();
			configureRequest?.Invoke(descriptor);
			return DoRequest<ClusterGetSettingsRequestDescriptor, ClusterGetSettingsResponse>(descriptor);
		}

		public Task<ClusterGetSettingsResponse> GetSettingsAsync(Action<ClusterGetSettingsRequestDescriptor> configureRequest = null, CancellationToken cancellationToken = default)
		{
			var descriptor = new ClusterGetSettingsRequestDescriptor();
			configureRequest?.Invoke(descriptor);
			return DoRequestAsync<ClusterGetSettingsRequestDescriptor, ClusterGetSettingsResponse>(descriptor);
		}

		public ClusterHealthResponse Health(ClusterHealthRequest request) => DoRequest<ClusterHealthRequest, ClusterHealthResponse>(request);
		public Task<ClusterHealthResponse> HealthAsync(ClusterHealthRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<ClusterHealthRequest, ClusterHealthResponse>(request, cancellationToken);
		public ClusterHealthResponse Health(Action<ClusterHealthRequestDescriptor> configureRequest = null)
		{
			var descriptor = new ClusterHealthRequestDescriptor();
			configureRequest?.Invoke(descriptor);
			return DoRequest<ClusterHealthRequestDescriptor, ClusterHealthResponse>(descriptor);
		}

		public Task<ClusterHealthResponse> HealthAsync(Action<ClusterHealthRequestDescriptor> configureRequest = null, CancellationToken cancellationToken = default)
		{
			var descriptor = new ClusterHealthRequestDescriptor();
			configureRequest?.Invoke(descriptor);
			return DoRequestAsync<ClusterHealthRequestDescriptor, ClusterHealthResponse>(descriptor);
		}

		public ClusterPendingTasksResponse PendingTasks(ClusterPendingTasksRequest request) => DoRequest<ClusterPendingTasksRequest, ClusterPendingTasksResponse>(request);
		public Task<ClusterPendingTasksResponse> PendingTasksAsync(ClusterPendingTasksRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<ClusterPendingTasksRequest, ClusterPendingTasksResponse>(request, cancellationToken);
		public ClusterPendingTasksResponse PendingTasks(Action<ClusterPendingTasksRequestDescriptor> configureRequest = null)
		{
			var descriptor = new ClusterPendingTasksRequestDescriptor();
			configureRequest?.Invoke(descriptor);
			return DoRequest<ClusterPendingTasksRequestDescriptor, ClusterPendingTasksResponse>(descriptor);
		}

		public Task<ClusterPendingTasksResponse> PendingTasksAsync(Action<ClusterPendingTasksRequestDescriptor> configureRequest = null, CancellationToken cancellationToken = default)
		{
			var descriptor = new ClusterPendingTasksRequestDescriptor();
			configureRequest?.Invoke(descriptor);
			return DoRequestAsync<ClusterPendingTasksRequestDescriptor, ClusterPendingTasksResponse>(descriptor);
		}

		public ClusterPostVotingConfigExclusionsResponse PostVotingConfigExclusions(ClusterPostVotingConfigExclusionsRequest request) => DoRequest<ClusterPostVotingConfigExclusionsRequest, ClusterPostVotingConfigExclusionsResponse>(request);
		public Task<ClusterPostVotingConfigExclusionsResponse> PostVotingConfigExclusionsAsync(ClusterPostVotingConfigExclusionsRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<ClusterPostVotingConfigExclusionsRequest, ClusterPostVotingConfigExclusionsResponse>(request, cancellationToken);
		public ClusterPostVotingConfigExclusionsResponse PostVotingConfigExclusions(Action<ClusterPostVotingConfigExclusionsRequestDescriptor> configureRequest = null)
		{
			var descriptor = new ClusterPostVotingConfigExclusionsRequestDescriptor();
			configureRequest?.Invoke(descriptor);
			return DoRequest<ClusterPostVotingConfigExclusionsRequestDescriptor, ClusterPostVotingConfigExclusionsResponse>(descriptor);
		}

		public Task<ClusterPostVotingConfigExclusionsResponse> PostVotingConfigExclusionsAsync(Action<ClusterPostVotingConfigExclusionsRequestDescriptor> configureRequest = null, CancellationToken cancellationToken = default)
		{
			var descriptor = new ClusterPostVotingConfigExclusionsRequestDescriptor();
			configureRequest?.Invoke(descriptor);
			return DoRequestAsync<ClusterPostVotingConfigExclusionsRequestDescriptor, ClusterPostVotingConfigExclusionsResponse>(descriptor);
		}

		public ClusterPutComponentTemplateResponse PutComponentTemplate(ClusterPutComponentTemplateRequest request) => DoRequest<ClusterPutComponentTemplateRequest, ClusterPutComponentTemplateResponse>(request);
		public Task<ClusterPutComponentTemplateResponse> PutComponentTemplateAsync(ClusterPutComponentTemplateRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<ClusterPutComponentTemplateRequest, ClusterPutComponentTemplateResponse>(request, cancellationToken);
		public ClusterPutComponentTemplateResponse PutComponentTemplate(Elastic.Clients.Elasticsearch.Name name, Action<ClusterPutComponentTemplateRequestDescriptor> configureRequest = null)
		{
			var descriptor = new ClusterPutComponentTemplateRequestDescriptor(name);
			configureRequest?.Invoke(descriptor);
			return DoRequest<ClusterPutComponentTemplateRequestDescriptor, ClusterPutComponentTemplateResponse>(descriptor);
		}

		public Task<ClusterPutComponentTemplateResponse> PutComponentTemplateAsync(Elastic.Clients.Elasticsearch.Name name, Action<ClusterPutComponentTemplateRequestDescriptor> configureRequest = null, CancellationToken cancellationToken = default)
		{
			var descriptor = new ClusterPutComponentTemplateRequestDescriptor(name);
			configureRequest?.Invoke(descriptor);
			return DoRequestAsync<ClusterPutComponentTemplateRequestDescriptor, ClusterPutComponentTemplateResponse>(descriptor);
		}

		public ClusterPutSettingsResponse PutSettings(ClusterPutSettingsRequest request) => DoRequest<ClusterPutSettingsRequest, ClusterPutSettingsResponse>(request);
		public Task<ClusterPutSettingsResponse> PutSettingsAsync(ClusterPutSettingsRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<ClusterPutSettingsRequest, ClusterPutSettingsResponse>(request, cancellationToken);
		public ClusterPutSettingsResponse PutSettings(Action<ClusterPutSettingsRequestDescriptor> configureRequest = null)
		{
			var descriptor = new ClusterPutSettingsRequestDescriptor();
			configureRequest?.Invoke(descriptor);
			return DoRequest<ClusterPutSettingsRequestDescriptor, ClusterPutSettingsResponse>(descriptor);
		}

		public Task<ClusterPutSettingsResponse> PutSettingsAsync(Action<ClusterPutSettingsRequestDescriptor> configureRequest = null, CancellationToken cancellationToken = default)
		{
			var descriptor = new ClusterPutSettingsRequestDescriptor();
			configureRequest?.Invoke(descriptor);
			return DoRequestAsync<ClusterPutSettingsRequestDescriptor, ClusterPutSettingsResponse>(descriptor);
		}

		public ClusterRemoteInfoResponse RemoteInfo(ClusterRemoteInfoRequest request) => DoRequest<ClusterRemoteInfoRequest, ClusterRemoteInfoResponse>(request);
		public Task<ClusterRemoteInfoResponse> RemoteInfoAsync(ClusterRemoteInfoRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<ClusterRemoteInfoRequest, ClusterRemoteInfoResponse>(request, cancellationToken);
		public ClusterRemoteInfoResponse RemoteInfo(Action<ClusterRemoteInfoRequestDescriptor> configureRequest = null)
		{
			var descriptor = new ClusterRemoteInfoRequestDescriptor();
			configureRequest?.Invoke(descriptor);
			return DoRequest<ClusterRemoteInfoRequestDescriptor, ClusterRemoteInfoResponse>(descriptor);
		}

		public Task<ClusterRemoteInfoResponse> RemoteInfoAsync(Action<ClusterRemoteInfoRequestDescriptor> configureRequest = null, CancellationToken cancellationToken = default)
		{
			var descriptor = new ClusterRemoteInfoRequestDescriptor();
			configureRequest?.Invoke(descriptor);
			return DoRequestAsync<ClusterRemoteInfoRequestDescriptor, ClusterRemoteInfoResponse>(descriptor);
		}

		public ClusterRerouteResponse Reroute(ClusterRerouteRequest request) => DoRequest<ClusterRerouteRequest, ClusterRerouteResponse>(request);
		public Task<ClusterRerouteResponse> RerouteAsync(ClusterRerouteRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<ClusterRerouteRequest, ClusterRerouteResponse>(request, cancellationToken);
		public ClusterRerouteResponse Reroute(Action<ClusterRerouteRequestDescriptor> configureRequest = null)
		{
			var descriptor = new ClusterRerouteRequestDescriptor();
			configureRequest?.Invoke(descriptor);
			return DoRequest<ClusterRerouteRequestDescriptor, ClusterRerouteResponse>(descriptor);
		}

		public Task<ClusterRerouteResponse> RerouteAsync(Action<ClusterRerouteRequestDescriptor> configureRequest = null, CancellationToken cancellationToken = default)
		{
			var descriptor = new ClusterRerouteRequestDescriptor();
			configureRequest?.Invoke(descriptor);
			return DoRequestAsync<ClusterRerouteRequestDescriptor, ClusterRerouteResponse>(descriptor);
		}

		public ClusterStateResponse State(ClusterStateRequest request) => DoRequest<ClusterStateRequest, ClusterStateResponse>(request);
		public Task<ClusterStateResponse> StateAsync(ClusterStateRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<ClusterStateRequest, ClusterStateResponse>(request, cancellationToken);
		public ClusterStateResponse State(Action<ClusterStateRequestDescriptor> configureRequest = null)
		{
			var descriptor = new ClusterStateRequestDescriptor();
			configureRequest?.Invoke(descriptor);
			return DoRequest<ClusterStateRequestDescriptor, ClusterStateResponse>(descriptor);
		}

		public Task<ClusterStateResponse> StateAsync(Action<ClusterStateRequestDescriptor> configureRequest = null, CancellationToken cancellationToken = default)
		{
			var descriptor = new ClusterStateRequestDescriptor();
			configureRequest?.Invoke(descriptor);
			return DoRequestAsync<ClusterStateRequestDescriptor, ClusterStateResponse>(descriptor);
		}

		public ClusterStatsResponse Stats(ClusterStatsRequest request) => DoRequest<ClusterStatsRequest, ClusterStatsResponse>(request);
		public Task<ClusterStatsResponse> StatsAsync(ClusterStatsRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<ClusterStatsRequest, ClusterStatsResponse>(request, cancellationToken);
		public ClusterStatsResponse Stats(Action<ClusterStatsRequestDescriptor> configureRequest = null)
		{
			var descriptor = new ClusterStatsRequestDescriptor();
			configureRequest?.Invoke(descriptor);
			return DoRequest<ClusterStatsRequestDescriptor, ClusterStatsResponse>(descriptor);
		}

		public Task<ClusterStatsResponse> StatsAsync(Action<ClusterStatsRequestDescriptor> configureRequest = null, CancellationToken cancellationToken = default)
		{
			var descriptor = new ClusterStatsRequestDescriptor();
			configureRequest?.Invoke(descriptor);
			return DoRequestAsync<ClusterStatsRequestDescriptor, ClusterStatsResponse>(descriptor);
		}
	}
}