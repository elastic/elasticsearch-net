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
// Run the following in the root of the repository:
//
// TODO - RUN INSTRUCTIONS
//
// ------------------------------------------------

using System.Threading;
using System.Threading.Tasks;

#nullable restore
namespace Nest
{
	public class ClusterNamespace : NamespacedClientProxy
	{
		internal ClusterNamespace(ElasticClient client) : base(client)
		{
		}

		public ClusterAllocationExplainResponse AllocationExplain(IClusterAllocationExplainRequest request)
		{
			return DoRequest<IClusterAllocationExplainRequest, ClusterAllocationExplainResponse>(request, request.RequestParameters);
		}

		public Task<ClusterAllocationExplainResponse> AllocationExplainAsync(IClusterAllocationExplainRequest request, CancellationToken cancellationToken = default)
		{
			return DoRequestAsync<IClusterAllocationExplainRequest, ClusterAllocationExplainResponse>(request, request.RequestParameters, cancellationToken);
		}

		public ClusterDeleteComponentTemplateResponse DeleteComponentTemplate(IClusterDeleteComponentTemplateRequest request)
		{
			return DoRequest<IClusterDeleteComponentTemplateRequest, ClusterDeleteComponentTemplateResponse>(request, request.RequestParameters);
		}

		public Task<ClusterDeleteComponentTemplateResponse> DeleteComponentTemplateAsync(IClusterDeleteComponentTemplateRequest request, CancellationToken cancellationToken = default)
		{
			return DoRequestAsync<IClusterDeleteComponentTemplateRequest, ClusterDeleteComponentTemplateResponse>(request, request.RequestParameters, cancellationToken);
		}

		public ClusterDeleteVotingConfigExclusionsResponse DeleteVotingConfigExclusions(IClusterDeleteVotingConfigExclusionsRequest request)
		{
			return DoRequest<IClusterDeleteVotingConfigExclusionsRequest, ClusterDeleteVotingConfigExclusionsResponse>(request, request.RequestParameters);
		}

		public Task<ClusterDeleteVotingConfigExclusionsResponse> DeleteVotingConfigExclusionsAsync(IClusterDeleteVotingConfigExclusionsRequest request, CancellationToken cancellationToken = default)
		{
			return DoRequestAsync<IClusterDeleteVotingConfigExclusionsRequest, ClusterDeleteVotingConfigExclusionsResponse>(request, request.RequestParameters, cancellationToken);
		}

		public ClusterComponentTemplateExistsResponse ComponentTemplateExists(IClusterComponentTemplateExistsRequest request)
		{
			return DoRequest<IClusterComponentTemplateExistsRequest, ClusterComponentTemplateExistsResponse>(request, request.RequestParameters);
		}

		public Task<ClusterComponentTemplateExistsResponse> ComponentTemplateExistsAsync(IClusterComponentTemplateExistsRequest request, CancellationToken cancellationToken = default)
		{
			return DoRequestAsync<IClusterComponentTemplateExistsRequest, ClusterComponentTemplateExistsResponse>(request, request.RequestParameters, cancellationToken);
		}

		public ClusterGetComponentTemplateResponse GetComponentTemplate(IClusterGetComponentTemplateRequest request)
		{
			return DoRequest<IClusterGetComponentTemplateRequest, ClusterGetComponentTemplateResponse>(request, request.RequestParameters);
		}

		public Task<ClusterGetComponentTemplateResponse> GetComponentTemplateAsync(IClusterGetComponentTemplateRequest request, CancellationToken cancellationToken = default)
		{
			return DoRequestAsync<IClusterGetComponentTemplateRequest, ClusterGetComponentTemplateResponse>(request, request.RequestParameters, cancellationToken);
		}

		public ClusterGetSettingsResponse GetSettings(IClusterGetSettingsRequest request)
		{
			return DoRequest<IClusterGetSettingsRequest, ClusterGetSettingsResponse>(request, request.RequestParameters);
		}

		public Task<ClusterGetSettingsResponse> GetSettingsAsync(IClusterGetSettingsRequest request, CancellationToken cancellationToken = default)
		{
			return DoRequestAsync<IClusterGetSettingsRequest, ClusterGetSettingsResponse>(request, request.RequestParameters, cancellationToken);
		}

		public ClusterHealthResponse Health(IClusterHealthRequest request)
		{
			return DoRequest<IClusterHealthRequest, ClusterHealthResponse>(request, request.RequestParameters);
		}

		public Task<ClusterHealthResponse> HealthAsync(IClusterHealthRequest request, CancellationToken cancellationToken = default)
		{
			return DoRequestAsync<IClusterHealthRequest, ClusterHealthResponse>(request, request.RequestParameters, cancellationToken);
		}

		public ClusterPendingTasksResponse PendingTasks(IClusterPendingTasksRequest request)
		{
			return DoRequest<IClusterPendingTasksRequest, ClusterPendingTasksResponse>(request, request.RequestParameters);
		}

		public Task<ClusterPendingTasksResponse> PendingTasksAsync(IClusterPendingTasksRequest request, CancellationToken cancellationToken = default)
		{
			return DoRequestAsync<IClusterPendingTasksRequest, ClusterPendingTasksResponse>(request, request.RequestParameters, cancellationToken);
		}

		public ClusterPostVotingConfigExclusionsResponse PostVotingConfigExclusions(IClusterPostVotingConfigExclusionsRequest request)
		{
			return DoRequest<IClusterPostVotingConfigExclusionsRequest, ClusterPostVotingConfigExclusionsResponse>(request, request.RequestParameters);
		}

		public Task<ClusterPostVotingConfigExclusionsResponse> PostVotingConfigExclusionsAsync(IClusterPostVotingConfigExclusionsRequest request, CancellationToken cancellationToken = default)
		{
			return DoRequestAsync<IClusterPostVotingConfigExclusionsRequest, ClusterPostVotingConfigExclusionsResponse>(request, request.RequestParameters, cancellationToken);
		}

		public ClusterPutComponentTemplateResponse PutComponentTemplate(IClusterPutComponentTemplateRequest request)
		{
			return DoRequest<IClusterPutComponentTemplateRequest, ClusterPutComponentTemplateResponse>(request, request.RequestParameters);
		}

		public Task<ClusterPutComponentTemplateResponse> PutComponentTemplateAsync(IClusterPutComponentTemplateRequest request, CancellationToken cancellationToken = default)
		{
			return DoRequestAsync<IClusterPutComponentTemplateRequest, ClusterPutComponentTemplateResponse>(request, request.RequestParameters, cancellationToken);
		}

		public ClusterPutSettingsResponse PutSettings(IClusterPutSettingsRequest request)
		{
			return DoRequest<IClusterPutSettingsRequest, ClusterPutSettingsResponse>(request, request.RequestParameters);
		}

		public Task<ClusterPutSettingsResponse> PutSettingsAsync(IClusterPutSettingsRequest request, CancellationToken cancellationToken = default)
		{
			return DoRequestAsync<IClusterPutSettingsRequest, ClusterPutSettingsResponse>(request, request.RequestParameters, cancellationToken);
		}

		public RemoteInfoResponse RemoteInfo(IRemoteInfoRequest request)
		{
			return DoRequest<IRemoteInfoRequest, RemoteInfoResponse>(request, request.RequestParameters);
		}

		public Task<RemoteInfoResponse> RemoteInfoAsync(IRemoteInfoRequest request, CancellationToken cancellationToken = default)
		{
			return DoRequestAsync<IRemoteInfoRequest, RemoteInfoResponse>(request, request.RequestParameters, cancellationToken);
		}

		public ClusterRerouteResponse Reroute(IClusterRerouteRequest request)
		{
			return DoRequest<IClusterRerouteRequest, ClusterRerouteResponse>(request, request.RequestParameters);
		}

		public Task<ClusterRerouteResponse> RerouteAsync(IClusterRerouteRequest request, CancellationToken cancellationToken = default)
		{
			return DoRequestAsync<IClusterRerouteRequest, ClusterRerouteResponse>(request, request.RequestParameters, cancellationToken);
		}

		public ClusterStateResponse State(IClusterStateRequest request)
		{
			return DoRequest<IClusterStateRequest, ClusterStateResponse>(request, request.RequestParameters);
		}

		public Task<ClusterStateResponse> StateAsync(IClusterStateRequest request, CancellationToken cancellationToken = default)
		{
			return DoRequestAsync<IClusterStateRequest, ClusterStateResponse>(request, request.RequestParameters, cancellationToken);
		}

		public ClusterStatsResponse Stats(IClusterStatsRequest request)
		{
			return DoRequest<IClusterStatsRequest, ClusterStatsResponse>(request, request.RequestParameters);
		}

		public Task<ClusterStatsResponse> StatsAsync(IClusterStatsRequest request, CancellationToken cancellationToken = default)
		{
			return DoRequestAsync<IClusterStatsRequest, ClusterStatsResponse>(request, request.RequestParameters, cancellationToken);
		}
	}
}