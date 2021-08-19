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
namespace Nest.Cluster
{
	public class ClusterNamespace : NamespacedClientProxy
	{
		internal ClusterNamespace(ElasticClient client) : base(client)
		{
		}

		public AllocationExplainResponse AllocationExplain(IAllocationExplainRequest request)
		{
			return DoRequest<IAllocationExplainRequest, AllocationExplainResponse>(request, request.RequestParameters);
		}

		public Task<AllocationExplainResponse> AllocationExplainAsync(IAllocationExplainRequest request, CancellationToken cancellationToken = default)
		{
			return DoRequestAsync<IAllocationExplainRequest, AllocationExplainResponse>(request, request.RequestParameters, cancellationToken);
		}

		public AllocationExplainResponse AllocationExplain(Func<AllocationExplainDescriptor, IAllocationExplainRequest> selector = null)
		{
			return AllocationExplain(selector.InvokeOrDefault(new AllocationExplainDescriptor()));
		}

		public Task<AllocationExplainResponse> AllocationExplainAsync(Func<AllocationExplainDescriptor, IAllocationExplainRequest> selector = null, CancellationToken cancellationToken = default)
		{
			return AllocationExplainAsync(selector.InvokeOrDefault(new AllocationExplainDescriptor()), cancellationToken);
		}

		public DeleteComponentTemplateResponse DeleteComponentTemplate(IDeleteComponentTemplateRequest request)
		{
			return DoRequest<IDeleteComponentTemplateRequest, DeleteComponentTemplateResponse>(request, request.RequestParameters);
		}

		public Task<DeleteComponentTemplateResponse> DeleteComponentTemplateAsync(IDeleteComponentTemplateRequest request, CancellationToken cancellationToken = default)
		{
			return DoRequestAsync<IDeleteComponentTemplateRequest, DeleteComponentTemplateResponse>(request, request.RequestParameters, cancellationToken);
		}

		public DeleteComponentTemplateResponse DeleteComponentTemplate(Nest.Name name, Func<DeleteComponentTemplateDescriptor, IDeleteComponentTemplateRequest> selector = null)
		{
			return DeleteComponentTemplate(selector.InvokeOrDefault(new DeleteComponentTemplateDescriptor(name)));
		}

		public Task<DeleteComponentTemplateResponse> DeleteComponentTemplateAsync(Nest.Name name, Func<DeleteComponentTemplateDescriptor, IDeleteComponentTemplateRequest> selector = null, CancellationToken cancellationToken = default)
		{
			return DeleteComponentTemplateAsync(selector.InvokeOrDefault(new DeleteComponentTemplateDescriptor(name)), cancellationToken);
		}

		public DeleteVotingConfigExclusionsResponse DeleteVotingConfigExclusions(IDeleteVotingConfigExclusionsRequest request)
		{
			return DoRequest<IDeleteVotingConfigExclusionsRequest, DeleteVotingConfigExclusionsResponse>(request, request.RequestParameters);
		}

		public Task<DeleteVotingConfigExclusionsResponse> DeleteVotingConfigExclusionsAsync(IDeleteVotingConfigExclusionsRequest request, CancellationToken cancellationToken = default)
		{
			return DoRequestAsync<IDeleteVotingConfigExclusionsRequest, DeleteVotingConfigExclusionsResponse>(request, request.RequestParameters, cancellationToken);
		}

		public DeleteVotingConfigExclusionsResponse DeleteVotingConfigExclusions(Func<DeleteVotingConfigExclusionsDescriptor, IDeleteVotingConfigExclusionsRequest> selector = null)
		{
			return DeleteVotingConfigExclusions(selector.InvokeOrDefault(new DeleteVotingConfigExclusionsDescriptor()));
		}

		public Task<DeleteVotingConfigExclusionsResponse> DeleteVotingConfigExclusionsAsync(Func<DeleteVotingConfigExclusionsDescriptor, IDeleteVotingConfigExclusionsRequest> selector = null, CancellationToken cancellationToken = default)
		{
			return DeleteVotingConfigExclusionsAsync(selector.InvokeOrDefault(new DeleteVotingConfigExclusionsDescriptor()), cancellationToken);
		}

		public GetComponentTemplateResponse GetComponentTemplate(IGetComponentTemplateRequest request)
		{
			return DoRequest<IGetComponentTemplateRequest, GetComponentTemplateResponse>(request, request.RequestParameters);
		}

		public Task<GetComponentTemplateResponse> GetComponentTemplateAsync(IGetComponentTemplateRequest request, CancellationToken cancellationToken = default)
		{
			return DoRequestAsync<IGetComponentTemplateRequest, GetComponentTemplateResponse>(request, request.RequestParameters, cancellationToken);
		}

		public GetComponentTemplateResponse GetComponentTemplate(Func<GetComponentTemplateDescriptor, IGetComponentTemplateRequest> selector = null)
		{
			return GetComponentTemplate(selector.InvokeOrDefault(new GetComponentTemplateDescriptor()));
		}

		public Task<GetComponentTemplateResponse> GetComponentTemplateAsync(Func<GetComponentTemplateDescriptor, IGetComponentTemplateRequest> selector = null, CancellationToken cancellationToken = default)
		{
			return GetComponentTemplateAsync(selector.InvokeOrDefault(new GetComponentTemplateDescriptor()), cancellationToken);
		}

		public GetSettingsResponse GetSettings(IGetSettingsRequest request)
		{
			return DoRequest<IGetSettingsRequest, GetSettingsResponse>(request, request.RequestParameters);
		}

		public Task<GetSettingsResponse> GetSettingsAsync(IGetSettingsRequest request, CancellationToken cancellationToken = default)
		{
			return DoRequestAsync<IGetSettingsRequest, GetSettingsResponse>(request, request.RequestParameters, cancellationToken);
		}

		public GetSettingsResponse GetSettings(Func<GetSettingsDescriptor, IGetSettingsRequest> selector = null)
		{
			return GetSettings(selector.InvokeOrDefault(new GetSettingsDescriptor()));
		}

		public Task<GetSettingsResponse> GetSettingsAsync(Func<GetSettingsDescriptor, IGetSettingsRequest> selector = null, CancellationToken cancellationToken = default)
		{
			return GetSettingsAsync(selector.InvokeOrDefault(new GetSettingsDescriptor()), cancellationToken);
		}

		public HealthResponse Health(IHealthRequest request)
		{
			return DoRequest<IHealthRequest, HealthResponse>(request, request.RequestParameters);
		}

		public Task<HealthResponse> HealthAsync(IHealthRequest request, CancellationToken cancellationToken = default)
		{
			return DoRequestAsync<IHealthRequest, HealthResponse>(request, request.RequestParameters, cancellationToken);
		}

		public HealthResponse Health(Func<HealthDescriptor, IHealthRequest> selector = null)
		{
			return Health(selector.InvokeOrDefault(new HealthDescriptor()));
		}

		public Task<HealthResponse> HealthAsync(Func<HealthDescriptor, IHealthRequest> selector = null, CancellationToken cancellationToken = default)
		{
			return HealthAsync(selector.InvokeOrDefault(new HealthDescriptor()), cancellationToken);
		}

		public PendingTasksResponse PendingTasks(IPendingTasksRequest request)
		{
			return DoRequest<IPendingTasksRequest, PendingTasksResponse>(request, request.RequestParameters);
		}

		public Task<PendingTasksResponse> PendingTasksAsync(IPendingTasksRequest request, CancellationToken cancellationToken = default)
		{
			return DoRequestAsync<IPendingTasksRequest, PendingTasksResponse>(request, request.RequestParameters, cancellationToken);
		}

		public PendingTasksResponse PendingTasks(Func<PendingTasksDescriptor, IPendingTasksRequest> selector = null)
		{
			return PendingTasks(selector.InvokeOrDefault(new PendingTasksDescriptor()));
		}

		public Task<PendingTasksResponse> PendingTasksAsync(Func<PendingTasksDescriptor, IPendingTasksRequest> selector = null, CancellationToken cancellationToken = default)
		{
			return PendingTasksAsync(selector.InvokeOrDefault(new PendingTasksDescriptor()), cancellationToken);
		}

		public PostVotingConfigExclusionsResponse PostVotingConfigExclusions(IPostVotingConfigExclusionsRequest request)
		{
			return DoRequest<IPostVotingConfigExclusionsRequest, PostVotingConfigExclusionsResponse>(request, request.RequestParameters);
		}

		public Task<PostVotingConfigExclusionsResponse> PostVotingConfigExclusionsAsync(IPostVotingConfigExclusionsRequest request, CancellationToken cancellationToken = default)
		{
			return DoRequestAsync<IPostVotingConfigExclusionsRequest, PostVotingConfigExclusionsResponse>(request, request.RequestParameters, cancellationToken);
		}

		public PostVotingConfigExclusionsResponse PostVotingConfigExclusions(Func<PostVotingConfigExclusionsDescriptor, IPostVotingConfigExclusionsRequest> selector = null)
		{
			return PostVotingConfigExclusions(selector.InvokeOrDefault(new PostVotingConfigExclusionsDescriptor()));
		}

		public Task<PostVotingConfigExclusionsResponse> PostVotingConfigExclusionsAsync(Func<PostVotingConfigExclusionsDescriptor, IPostVotingConfigExclusionsRequest> selector = null, CancellationToken cancellationToken = default)
		{
			return PostVotingConfigExclusionsAsync(selector.InvokeOrDefault(new PostVotingConfigExclusionsDescriptor()), cancellationToken);
		}

		public PutComponentTemplateResponse PutComponentTemplate(IPutComponentTemplateRequest request)
		{
			return DoRequest<IPutComponentTemplateRequest, PutComponentTemplateResponse>(request, request.RequestParameters);
		}

		public Task<PutComponentTemplateResponse> PutComponentTemplateAsync(IPutComponentTemplateRequest request, CancellationToken cancellationToken = default)
		{
			return DoRequestAsync<IPutComponentTemplateRequest, PutComponentTemplateResponse>(request, request.RequestParameters, cancellationToken);
		}

		public PutComponentTemplateResponse PutComponentTemplate(Nest.Name name, Func<PutComponentTemplateDescriptor, IPutComponentTemplateRequest> selector = null)
		{
			return PutComponentTemplate(selector.InvokeOrDefault(new PutComponentTemplateDescriptor(name)));
		}

		public Task<PutComponentTemplateResponse> PutComponentTemplateAsync(Nest.Name name, Func<PutComponentTemplateDescriptor, IPutComponentTemplateRequest> selector = null, CancellationToken cancellationToken = default)
		{
			return PutComponentTemplateAsync(selector.InvokeOrDefault(new PutComponentTemplateDescriptor(name)), cancellationToken);
		}

		public PutSettingsResponse PutSettings(IPutSettingsRequest request)
		{
			return DoRequest<IPutSettingsRequest, PutSettingsResponse>(request, request.RequestParameters);
		}

		public Task<PutSettingsResponse> PutSettingsAsync(IPutSettingsRequest request, CancellationToken cancellationToken = default)
		{
			return DoRequestAsync<IPutSettingsRequest, PutSettingsResponse>(request, request.RequestParameters, cancellationToken);
		}

		public PutSettingsResponse PutSettings(Func<PutSettingsDescriptor, IPutSettingsRequest> selector = null)
		{
			return PutSettings(selector.InvokeOrDefault(new PutSettingsDescriptor()));
		}

		public Task<PutSettingsResponse> PutSettingsAsync(Func<PutSettingsDescriptor, IPutSettingsRequest> selector = null, CancellationToken cancellationToken = default)
		{
			return PutSettingsAsync(selector.InvokeOrDefault(new PutSettingsDescriptor()), cancellationToken);
		}

		public RemoteInfoResponse RemoteInfo(IRemoteInfoRequest request)
		{
			return DoRequest<IRemoteInfoRequest, RemoteInfoResponse>(request, request.RequestParameters);
		}

		public Task<RemoteInfoResponse> RemoteInfoAsync(IRemoteInfoRequest request, CancellationToken cancellationToken = default)
		{
			return DoRequestAsync<IRemoteInfoRequest, RemoteInfoResponse>(request, request.RequestParameters, cancellationToken);
		}

		public RemoteInfoResponse RemoteInfo(Func<RemoteInfoDescriptor, IRemoteInfoRequest> selector = null)
		{
			return RemoteInfo(selector.InvokeOrDefault(new RemoteInfoDescriptor()));
		}

		public Task<RemoteInfoResponse> RemoteInfoAsync(Func<RemoteInfoDescriptor, IRemoteInfoRequest> selector = null, CancellationToken cancellationToken = default)
		{
			return RemoteInfoAsync(selector.InvokeOrDefault(new RemoteInfoDescriptor()), cancellationToken);
		}

		public RerouteResponse Reroute(IRerouteRequest request)
		{
			return DoRequest<IRerouteRequest, RerouteResponse>(request, request.RequestParameters);
		}

		public Task<RerouteResponse> RerouteAsync(IRerouteRequest request, CancellationToken cancellationToken = default)
		{
			return DoRequestAsync<IRerouteRequest, RerouteResponse>(request, request.RequestParameters, cancellationToken);
		}

		public RerouteResponse Reroute(Func<RerouteDescriptor, IRerouteRequest> selector = null)
		{
			return Reroute(selector.InvokeOrDefault(new RerouteDescriptor()));
		}

		public Task<RerouteResponse> RerouteAsync(Func<RerouteDescriptor, IRerouteRequest> selector = null, CancellationToken cancellationToken = default)
		{
			return RerouteAsync(selector.InvokeOrDefault(new RerouteDescriptor()), cancellationToken);
		}

		public StateResponse State(IStateRequest request)
		{
			return DoRequest<IStateRequest, StateResponse>(request, request.RequestParameters);
		}

		public Task<StateResponse> StateAsync(IStateRequest request, CancellationToken cancellationToken = default)
		{
			return DoRequestAsync<IStateRequest, StateResponse>(request, request.RequestParameters, cancellationToken);
		}

		public StateResponse State(Func<StateDescriptor, IStateRequest> selector = null)
		{
			return State(selector.InvokeOrDefault(new StateDescriptor()));
		}

		public Task<StateResponse> StateAsync(Func<StateDescriptor, IStateRequest> selector = null, CancellationToken cancellationToken = default)
		{
			return StateAsync(selector.InvokeOrDefault(new StateDescriptor()), cancellationToken);
		}

		public StatsResponse Stats(IStatsRequest request)
		{
			return DoRequest<IStatsRequest, StatsResponse>(request, request.RequestParameters);
		}

		public Task<StatsResponse> StatsAsync(IStatsRequest request, CancellationToken cancellationToken = default)
		{
			return DoRequestAsync<IStatsRequest, StatsResponse>(request, request.RequestParameters, cancellationToken);
		}

		public StatsResponse Stats(Func<StatsDescriptor, IStatsRequest> selector = null)
		{
			return Stats(selector.InvokeOrDefault(new StatsDescriptor()));
		}

		public Task<StatsResponse> StatsAsync(Func<StatsDescriptor, IStatsRequest> selector = null, CancellationToken cancellationToken = default)
		{
			return StatsAsync(selector.InvokeOrDefault(new StatsDescriptor()), cancellationToken);
		}
	}
}