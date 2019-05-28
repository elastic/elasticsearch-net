using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net.Specification.WatcherApi;

// ReSharper disable once CheckNamespace
namespace Nest.Specification.WatcherApi
{
	///<summary>
	/// Logically groups all Watcher API's together so that they may be discovered more naturally.
	/// <para>Not intended to be instantiated directly please defer to the <see cref = "IElasticClient.Watcher"/> property
	/// on <see cref = "IElasticClient"/>.
	///</para>
	///</summary>
	public class WatcherNamespace : NamespacedClientProxy
	{
		internal WatcherNamespace(ElasticClient client): base(client)
		{
		}

		///<inheritdoc cref = "IAcknowledgeWatchRequest"/>
		public AcknowledgeWatchResponse Acknowledge(Id watchId, Func<AcknowledgeWatchDescriptor, IAcknowledgeWatchRequest> selector = null) => Acknowledge(selector.InvokeOrDefault(new AcknowledgeWatchDescriptor(watchId: watchId)));
		///<inheritdoc cref = "IAcknowledgeWatchRequest"/>
		public Task<AcknowledgeWatchResponse> AcknowledgeAsync(Id watchId, Func<AcknowledgeWatchDescriptor, IAcknowledgeWatchRequest> selector = null, CancellationToken ct = default) => AcknowledgeAsync(selector.InvokeOrDefault(new AcknowledgeWatchDescriptor(watchId: watchId)), ct);
		///<inheritdoc cref = "IAcknowledgeWatchRequest"/>
		public AcknowledgeWatchResponse Acknowledge(IAcknowledgeWatchRequest request) => DoRequest<IAcknowledgeWatchRequest, AcknowledgeWatchResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IAcknowledgeWatchRequest"/>
		public Task<AcknowledgeWatchResponse> AcknowledgeAsync(IAcknowledgeWatchRequest request, CancellationToken ct = default) => DoRequestAsync<IAcknowledgeWatchRequest, AcknowledgeWatchResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IActivateWatchRequest"/>
		public ActivateWatchResponse Activate(Id watchId, Func<ActivateWatchDescriptor, IActivateWatchRequest> selector = null) => Activate(selector.InvokeOrDefault(new ActivateWatchDescriptor(watchId: watchId)));
		///<inheritdoc cref = "IActivateWatchRequest"/>
		public Task<ActivateWatchResponse> ActivateAsync(Id watchId, Func<ActivateWatchDescriptor, IActivateWatchRequest> selector = null, CancellationToken ct = default) => ActivateAsync(selector.InvokeOrDefault(new ActivateWatchDescriptor(watchId: watchId)), ct);
		///<inheritdoc cref = "IActivateWatchRequest"/>
		public ActivateWatchResponse Activate(IActivateWatchRequest request) => DoRequest<IActivateWatchRequest, ActivateWatchResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IActivateWatchRequest"/>
		public Task<ActivateWatchResponse> ActivateAsync(IActivateWatchRequest request, CancellationToken ct = default) => DoRequestAsync<IActivateWatchRequest, ActivateWatchResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IDeactivateWatchRequest"/>
		public DeactivateWatchResponse Deactivate(Id watchId, Func<DeactivateWatchDescriptor, IDeactivateWatchRequest> selector = null) => Deactivate(selector.InvokeOrDefault(new DeactivateWatchDescriptor(watchId: watchId)));
		///<inheritdoc cref = "IDeactivateWatchRequest"/>
		public Task<DeactivateWatchResponse> DeactivateAsync(Id watchId, Func<DeactivateWatchDescriptor, IDeactivateWatchRequest> selector = null, CancellationToken ct = default) => DeactivateAsync(selector.InvokeOrDefault(new DeactivateWatchDescriptor(watchId: watchId)), ct);
		///<inheritdoc cref = "IDeactivateWatchRequest"/>
		public DeactivateWatchResponse Deactivate(IDeactivateWatchRequest request) => DoRequest<IDeactivateWatchRequest, DeactivateWatchResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IDeactivateWatchRequest"/>
		public Task<DeactivateWatchResponse> DeactivateAsync(IDeactivateWatchRequest request, CancellationToken ct = default) => DoRequestAsync<IDeactivateWatchRequest, DeactivateWatchResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IDeleteWatchRequest"/>
		public DeleteWatchResponse Delete(Id id, Func<DeleteWatchDescriptor, IDeleteWatchRequest> selector = null) => Delete(selector.InvokeOrDefault(new DeleteWatchDescriptor(id: id)));
		///<inheritdoc cref = "IDeleteWatchRequest"/>
		public Task<DeleteWatchResponse> DeleteAsync(Id id, Func<DeleteWatchDescriptor, IDeleteWatchRequest> selector = null, CancellationToken ct = default) => DeleteAsync(selector.InvokeOrDefault(new DeleteWatchDescriptor(id: id)), ct);
		///<inheritdoc cref = "IDeleteWatchRequest"/>
		public DeleteWatchResponse Delete(IDeleteWatchRequest request) => DoRequest<IDeleteWatchRequest, DeleteWatchResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IDeleteWatchRequest"/>
		public Task<DeleteWatchResponse> DeleteAsync(IDeleteWatchRequest request, CancellationToken ct = default) => DoRequestAsync<IDeleteWatchRequest, DeleteWatchResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IExecuteWatchRequest"/>
		public ExecuteWatchResponse Execute(Func<ExecuteWatchDescriptor, IExecuteWatchRequest> selector = null) => Execute(selector.InvokeOrDefault(new ExecuteWatchDescriptor()));
		///<inheritdoc cref = "IExecuteWatchRequest"/>
		public Task<ExecuteWatchResponse> ExecuteAsync(Func<ExecuteWatchDescriptor, IExecuteWatchRequest> selector = null, CancellationToken ct = default) => ExecuteAsync(selector.InvokeOrDefault(new ExecuteWatchDescriptor()), ct);
		///<inheritdoc cref = "IExecuteWatchRequest"/>
		public ExecuteWatchResponse Execute(IExecuteWatchRequest request) => DoRequest<IExecuteWatchRequest, ExecuteWatchResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IExecuteWatchRequest"/>
		public Task<ExecuteWatchResponse> ExecuteAsync(IExecuteWatchRequest request, CancellationToken ct = default) => DoRequestAsync<IExecuteWatchRequest, ExecuteWatchResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IGetWatchRequest"/>
		public GetWatchResponse Get(Id id, Func<GetWatchDescriptor, IGetWatchRequest> selector = null) => Get(selector.InvokeOrDefault(new GetWatchDescriptor(id: id)));
		///<inheritdoc cref = "IGetWatchRequest"/>
		public Task<GetWatchResponse> GetAsync(Id id, Func<GetWatchDescriptor, IGetWatchRequest> selector = null, CancellationToken ct = default) => GetAsync(selector.InvokeOrDefault(new GetWatchDescriptor(id: id)), ct);
		///<inheritdoc cref = "IGetWatchRequest"/>
		public GetWatchResponse Get(IGetWatchRequest request) => DoRequest<IGetWatchRequest, GetWatchResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IGetWatchRequest"/>
		public Task<GetWatchResponse> GetAsync(IGetWatchRequest request, CancellationToken ct = default) => DoRequestAsync<IGetWatchRequest, GetWatchResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IPutWatchRequest"/>
		public PutWatchResponse Put(Id id, Func<PutWatchDescriptor, IPutWatchRequest> selector = null) => Put(selector.InvokeOrDefault(new PutWatchDescriptor(id: id)));
		///<inheritdoc cref = "IPutWatchRequest"/>
		public Task<PutWatchResponse> PutAsync(Id id, Func<PutWatchDescriptor, IPutWatchRequest> selector = null, CancellationToken ct = default) => PutAsync(selector.InvokeOrDefault(new PutWatchDescriptor(id: id)), ct);
		///<inheritdoc cref = "IPutWatchRequest"/>
		public PutWatchResponse Put(IPutWatchRequest request) => DoRequest<IPutWatchRequest, PutWatchResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IPutWatchRequest"/>
		public Task<PutWatchResponse> PutAsync(IPutWatchRequest request, CancellationToken ct = default) => DoRequestAsync<IPutWatchRequest, PutWatchResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IStartWatcherRequest"/>
		public StartWatcherResponse Start(Func<StartWatcherDescriptor, IStartWatcherRequest> selector = null) => Start(selector.InvokeOrDefault(new StartWatcherDescriptor()));
		///<inheritdoc cref = "IStartWatcherRequest"/>
		public Task<StartWatcherResponse> StartAsync(Func<StartWatcherDescriptor, IStartWatcherRequest> selector = null, CancellationToken ct = default) => StartAsync(selector.InvokeOrDefault(new StartWatcherDescriptor()), ct);
		///<inheritdoc cref = "IStartWatcherRequest"/>
		public StartWatcherResponse Start(IStartWatcherRequest request) => DoRequest<IStartWatcherRequest, StartWatcherResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IStartWatcherRequest"/>
		public Task<StartWatcherResponse> StartAsync(IStartWatcherRequest request, CancellationToken ct = default) => DoRequestAsync<IStartWatcherRequest, StartWatcherResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IWatcherStatsRequest"/>
		public WatcherStatsResponse Stats(Func<WatcherStatsDescriptor, IWatcherStatsRequest> selector = null) => Stats(selector.InvokeOrDefault(new WatcherStatsDescriptor()));
		///<inheritdoc cref = "IWatcherStatsRequest"/>
		public Task<WatcherStatsResponse> StatsAsync(Func<WatcherStatsDescriptor, IWatcherStatsRequest> selector = null, CancellationToken ct = default) => StatsAsync(selector.InvokeOrDefault(new WatcherStatsDescriptor()), ct);
		///<inheritdoc cref = "IWatcherStatsRequest"/>
		public WatcherStatsResponse Stats(IWatcherStatsRequest request) => DoRequest<IWatcherStatsRequest, WatcherStatsResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IWatcherStatsRequest"/>
		public Task<WatcherStatsResponse> StatsAsync(IWatcherStatsRequest request, CancellationToken ct = default) => DoRequestAsync<IWatcherStatsRequest, WatcherStatsResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IStopWatcherRequest"/>
		public StopWatcherResponse Stop(Func<StopWatcherDescriptor, IStopWatcherRequest> selector = null) => Stop(selector.InvokeOrDefault(new StopWatcherDescriptor()));
		///<inheritdoc cref = "IStopWatcherRequest"/>
		public Task<StopWatcherResponse> StopAsync(Func<StopWatcherDescriptor, IStopWatcherRequest> selector = null, CancellationToken ct = default) => StopAsync(selector.InvokeOrDefault(new StopWatcherDescriptor()), ct);
		///<inheritdoc cref = "IStopWatcherRequest"/>
		public StopWatcherResponse Stop(IStopWatcherRequest request) => DoRequest<IStopWatcherRequest, StopWatcherResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IStopWatcherRequest"/>
		public Task<StopWatcherResponse> StopAsync(IStopWatcherRequest request, CancellationToken ct = default) => DoRequestAsync<IStopWatcherRequest, StopWatcherResponse>(request, request.RequestParameters, ct);
	}
}