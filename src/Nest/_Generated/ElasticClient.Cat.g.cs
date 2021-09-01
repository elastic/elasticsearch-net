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
namespace Nest.Cat
{
	public class CatNamespace : NamespacedClientProxy
	{
		internal CatNamespace(ElasticClient client) : base(client)
		{
		}

		public AliasesResponse Aliases(IAliasesRequest request) => DoRequest<IAliasesRequest, AliasesResponse>(request, request.RequestParameters);
		public Task<AliasesResponse> AliasesAsync(IAliasesRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<IAliasesRequest, AliasesResponse>(request, request.RequestParameters, cancellationToken);
		public AliasesResponse Aliases(Func<AliasesDescriptor, IAliasesRequest> selector = null) => Aliases(selector.InvokeOrDefault(new AliasesDescriptor()));
		public Task<AliasesResponse> AliasesAsync(Func<AliasesDescriptor, IAliasesRequest> selector = null, CancellationToken cancellationToken = default) => AliasesAsync(selector.InvokeOrDefault(new AliasesDescriptor()), cancellationToken);
		public AllocationResponse Allocation(IAllocationRequest request) => DoRequest<IAllocationRequest, AllocationResponse>(request, request.RequestParameters);
		public Task<AllocationResponse> AllocationAsync(IAllocationRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<IAllocationRequest, AllocationResponse>(request, request.RequestParameters, cancellationToken);
		public AllocationResponse Allocation(Func<AllocationDescriptor, IAllocationRequest> selector = null) => Allocation(selector.InvokeOrDefault(new AllocationDescriptor()));
		public Task<AllocationResponse> AllocationAsync(Func<AllocationDescriptor, IAllocationRequest> selector = null, CancellationToken cancellationToken = default) => AllocationAsync(selector.InvokeOrDefault(new AllocationDescriptor()), cancellationToken);
		public CountResponse Count(ICountRequest request) => DoRequest<ICountRequest, CountResponse>(request, request.RequestParameters);
		public Task<CountResponse> CountAsync(ICountRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<ICountRequest, CountResponse>(request, request.RequestParameters, cancellationToken);
		public CountResponse Count(Func<CountDescriptor, ICountRequest> selector = null) => Count(selector.InvokeOrDefault(new CountDescriptor()));
		public Task<CountResponse> CountAsync(Func<CountDescriptor, ICountRequest> selector = null, CancellationToken cancellationToken = default) => CountAsync(selector.InvokeOrDefault(new CountDescriptor()), cancellationToken);
		public FielddataResponse Fielddata(IFielddataRequest request) => DoRequest<IFielddataRequest, FielddataResponse>(request, request.RequestParameters);
		public Task<FielddataResponse> FielddataAsync(IFielddataRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<IFielddataRequest, FielddataResponse>(request, request.RequestParameters, cancellationToken);
		public FielddataResponse Fielddata(Func<FielddataDescriptor, IFielddataRequest> selector = null) => Fielddata(selector.InvokeOrDefault(new FielddataDescriptor()));
		public Task<FielddataResponse> FielddataAsync(Func<FielddataDescriptor, IFielddataRequest> selector = null, CancellationToken cancellationToken = default) => FielddataAsync(selector.InvokeOrDefault(new FielddataDescriptor()), cancellationToken);
		public HealthResponse Health(IHealthRequest request) => DoRequest<IHealthRequest, HealthResponse>(request, request.RequestParameters);
		public Task<HealthResponse> HealthAsync(IHealthRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<IHealthRequest, HealthResponse>(request, request.RequestParameters, cancellationToken);
		public HealthResponse Health(Func<HealthDescriptor, IHealthRequest> selector = null) => Health(selector.InvokeOrDefault(new HealthDescriptor()));
		public Task<HealthResponse> HealthAsync(Func<HealthDescriptor, IHealthRequest> selector = null, CancellationToken cancellationToken = default) => HealthAsync(selector.InvokeOrDefault(new HealthDescriptor()), cancellationToken);
		public HelpResponse Help(IHelpRequest request) => DoRequest<IHelpRequest, HelpResponse>(request, request.RequestParameters);
		public Task<HelpResponse> HelpAsync(IHelpRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<IHelpRequest, HelpResponse>(request, request.RequestParameters, cancellationToken);
		public HelpResponse Help(Func<HelpDescriptor, IHelpRequest> selector = null) => Help(selector.InvokeOrDefault(new HelpDescriptor()));
		public Task<HelpResponse> HelpAsync(Func<HelpDescriptor, IHelpRequest> selector = null, CancellationToken cancellationToken = default) => HelpAsync(selector.InvokeOrDefault(new HelpDescriptor()), cancellationToken);
		public IndicesResponse Indices(IIndicesRequest request) => DoRequest<IIndicesRequest, IndicesResponse>(request, request.RequestParameters);
		public Task<IndicesResponse> IndicesAsync(IIndicesRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<IIndicesRequest, IndicesResponse>(request, request.RequestParameters, cancellationToken);
		public IndicesResponse Indices(Func<IndicesDescriptor, IIndicesRequest> selector = null) => Indices(selector.InvokeOrDefault(new IndicesDescriptor()));
		public Task<IndicesResponse> IndicesAsync(Func<IndicesDescriptor, IIndicesRequest> selector = null, CancellationToken cancellationToken = default) => IndicesAsync(selector.InvokeOrDefault(new IndicesDescriptor()), cancellationToken);
		public MasterResponse Master(IMasterRequest request) => DoRequest<IMasterRequest, MasterResponse>(request, request.RequestParameters);
		public Task<MasterResponse> MasterAsync(IMasterRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<IMasterRequest, MasterResponse>(request, request.RequestParameters, cancellationToken);
		public MasterResponse Master(Func<MasterDescriptor, IMasterRequest> selector = null) => Master(selector.InvokeOrDefault(new MasterDescriptor()));
		public Task<MasterResponse> MasterAsync(Func<MasterDescriptor, IMasterRequest> selector = null, CancellationToken cancellationToken = default) => MasterAsync(selector.InvokeOrDefault(new MasterDescriptor()), cancellationToken);
		public MlDatafeedsResponse MlDatafeeds(IMlDatafeedsRequest request) => DoRequest<IMlDatafeedsRequest, MlDatafeedsResponse>(request, request.RequestParameters);
		public Task<MlDatafeedsResponse> MlDatafeedsAsync(IMlDatafeedsRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<IMlDatafeedsRequest, MlDatafeedsResponse>(request, request.RequestParameters, cancellationToken);
		public MlDatafeedsResponse MlDatafeeds(Func<MlDatafeedsDescriptor, IMlDatafeedsRequest> selector = null) => MlDatafeeds(selector.InvokeOrDefault(new MlDatafeedsDescriptor()));
		public Task<MlDatafeedsResponse> MlDatafeedsAsync(Func<MlDatafeedsDescriptor, IMlDatafeedsRequest> selector = null, CancellationToken cancellationToken = default) => MlDatafeedsAsync(selector.InvokeOrDefault(new MlDatafeedsDescriptor()), cancellationToken);
		public MlDataFrameAnalyticsResponse MlDataFrameAnalytics(IMlDataFrameAnalyticsRequest request) => DoRequest<IMlDataFrameAnalyticsRequest, MlDataFrameAnalyticsResponse>(request, request.RequestParameters);
		public Task<MlDataFrameAnalyticsResponse> MlDataFrameAnalyticsAsync(IMlDataFrameAnalyticsRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<IMlDataFrameAnalyticsRequest, MlDataFrameAnalyticsResponse>(request, request.RequestParameters, cancellationToken);
		public MlDataFrameAnalyticsResponse MlDataFrameAnalytics(Func<MlDataFrameAnalyticsDescriptor, IMlDataFrameAnalyticsRequest> selector = null) => MlDataFrameAnalytics(selector.InvokeOrDefault(new MlDataFrameAnalyticsDescriptor()));
		public Task<MlDataFrameAnalyticsResponse> MlDataFrameAnalyticsAsync(Func<MlDataFrameAnalyticsDescriptor, IMlDataFrameAnalyticsRequest> selector = null, CancellationToken cancellationToken = default) => MlDataFrameAnalyticsAsync(selector.InvokeOrDefault(new MlDataFrameAnalyticsDescriptor()), cancellationToken);
		public MlJobsResponse MlJobs(IMlJobsRequest request) => DoRequest<IMlJobsRequest, MlJobsResponse>(request, request.RequestParameters);
		public Task<MlJobsResponse> MlJobsAsync(IMlJobsRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<IMlJobsRequest, MlJobsResponse>(request, request.RequestParameters, cancellationToken);
		public MlJobsResponse MlJobs(Func<MlJobsDescriptor, IMlJobsRequest> selector = null) => MlJobs(selector.InvokeOrDefault(new MlJobsDescriptor()));
		public Task<MlJobsResponse> MlJobsAsync(Func<MlJobsDescriptor, IMlJobsRequest> selector = null, CancellationToken cancellationToken = default) => MlJobsAsync(selector.InvokeOrDefault(new MlJobsDescriptor()), cancellationToken);
		public MlTrainedModelsResponse MlTrainedModels(IMlTrainedModelsRequest request) => DoRequest<IMlTrainedModelsRequest, MlTrainedModelsResponse>(request, request.RequestParameters);
		public Task<MlTrainedModelsResponse> MlTrainedModelsAsync(IMlTrainedModelsRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<IMlTrainedModelsRequest, MlTrainedModelsResponse>(request, request.RequestParameters, cancellationToken);
		public MlTrainedModelsResponse MlTrainedModels(Func<MlTrainedModelsDescriptor, IMlTrainedModelsRequest> selector = null) => MlTrainedModels(selector.InvokeOrDefault(new MlTrainedModelsDescriptor()));
		public Task<MlTrainedModelsResponse> MlTrainedModelsAsync(Func<MlTrainedModelsDescriptor, IMlTrainedModelsRequest> selector = null, CancellationToken cancellationToken = default) => MlTrainedModelsAsync(selector.InvokeOrDefault(new MlTrainedModelsDescriptor()), cancellationToken);
		public NodeattrsResponse Nodeattrs(INodeattrsRequest request) => DoRequest<INodeattrsRequest, NodeattrsResponse>(request, request.RequestParameters);
		public Task<NodeattrsResponse> NodeattrsAsync(INodeattrsRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<INodeattrsRequest, NodeattrsResponse>(request, request.RequestParameters, cancellationToken);
		public NodeattrsResponse Nodeattrs(Func<NodeattrsDescriptor, INodeattrsRequest> selector = null) => Nodeattrs(selector.InvokeOrDefault(new NodeattrsDescriptor()));
		public Task<NodeattrsResponse> NodeattrsAsync(Func<NodeattrsDescriptor, INodeattrsRequest> selector = null, CancellationToken cancellationToken = default) => NodeattrsAsync(selector.InvokeOrDefault(new NodeattrsDescriptor()), cancellationToken);
		public NodesResponse Nodes(INodesRequest request) => DoRequest<INodesRequest, NodesResponse>(request, request.RequestParameters);
		public Task<NodesResponse> NodesAsync(INodesRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<INodesRequest, NodesResponse>(request, request.RequestParameters, cancellationToken);
		public NodesResponse Nodes(Func<NodesDescriptor, INodesRequest> selector = null) => Nodes(selector.InvokeOrDefault(new NodesDescriptor()));
		public Task<NodesResponse> NodesAsync(Func<NodesDescriptor, INodesRequest> selector = null, CancellationToken cancellationToken = default) => NodesAsync(selector.InvokeOrDefault(new NodesDescriptor()), cancellationToken);
		public PendingTasksResponse PendingTasks(IPendingTasksRequest request) => DoRequest<IPendingTasksRequest, PendingTasksResponse>(request, request.RequestParameters);
		public Task<PendingTasksResponse> PendingTasksAsync(IPendingTasksRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<IPendingTasksRequest, PendingTasksResponse>(request, request.RequestParameters, cancellationToken);
		public PendingTasksResponse PendingTasks(Func<PendingTasksDescriptor, IPendingTasksRequest> selector = null) => PendingTasks(selector.InvokeOrDefault(new PendingTasksDescriptor()));
		public Task<PendingTasksResponse> PendingTasksAsync(Func<PendingTasksDescriptor, IPendingTasksRequest> selector = null, CancellationToken cancellationToken = default) => PendingTasksAsync(selector.InvokeOrDefault(new PendingTasksDescriptor()), cancellationToken);
		public PluginsResponse Plugins(IPluginsRequest request) => DoRequest<IPluginsRequest, PluginsResponse>(request, request.RequestParameters);
		public Task<PluginsResponse> PluginsAsync(IPluginsRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<IPluginsRequest, PluginsResponse>(request, request.RequestParameters, cancellationToken);
		public PluginsResponse Plugins(Func<PluginsDescriptor, IPluginsRequest> selector = null) => Plugins(selector.InvokeOrDefault(new PluginsDescriptor()));
		public Task<PluginsResponse> PluginsAsync(Func<PluginsDescriptor, IPluginsRequest> selector = null, CancellationToken cancellationToken = default) => PluginsAsync(selector.InvokeOrDefault(new PluginsDescriptor()), cancellationToken);
		public RecoveryResponse Recovery(IRecoveryRequest request) => DoRequest<IRecoveryRequest, RecoveryResponse>(request, request.RequestParameters);
		public Task<RecoveryResponse> RecoveryAsync(IRecoveryRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<IRecoveryRequest, RecoveryResponse>(request, request.RequestParameters, cancellationToken);
		public RecoveryResponse Recovery(Func<RecoveryDescriptor, IRecoveryRequest> selector = null) => Recovery(selector.InvokeOrDefault(new RecoveryDescriptor()));
		public Task<RecoveryResponse> RecoveryAsync(Func<RecoveryDescriptor, IRecoveryRequest> selector = null, CancellationToken cancellationToken = default) => RecoveryAsync(selector.InvokeOrDefault(new RecoveryDescriptor()), cancellationToken);
		public RepositoriesResponse Repositories(IRepositoriesRequest request) => DoRequest<IRepositoriesRequest, RepositoriesResponse>(request, request.RequestParameters);
		public Task<RepositoriesResponse> RepositoriesAsync(IRepositoriesRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<IRepositoriesRequest, RepositoriesResponse>(request, request.RequestParameters, cancellationToken);
		public RepositoriesResponse Repositories(Func<RepositoriesDescriptor, IRepositoriesRequest> selector = null) => Repositories(selector.InvokeOrDefault(new RepositoriesDescriptor()));
		public Task<RepositoriesResponse> RepositoriesAsync(Func<RepositoriesDescriptor, IRepositoriesRequest> selector = null, CancellationToken cancellationToken = default) => RepositoriesAsync(selector.InvokeOrDefault(new RepositoriesDescriptor()), cancellationToken);
		public SegmentsResponse Segments(ISegmentsRequest request) => DoRequest<ISegmentsRequest, SegmentsResponse>(request, request.RequestParameters);
		public Task<SegmentsResponse> SegmentsAsync(ISegmentsRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<ISegmentsRequest, SegmentsResponse>(request, request.RequestParameters, cancellationToken);
		public SegmentsResponse Segments(Func<SegmentsDescriptor, ISegmentsRequest> selector = null) => Segments(selector.InvokeOrDefault(new SegmentsDescriptor()));
		public Task<SegmentsResponse> SegmentsAsync(Func<SegmentsDescriptor, ISegmentsRequest> selector = null, CancellationToken cancellationToken = default) => SegmentsAsync(selector.InvokeOrDefault(new SegmentsDescriptor()), cancellationToken);
		public ShardsResponse Shards(IShardsRequest request) => DoRequest<IShardsRequest, ShardsResponse>(request, request.RequestParameters);
		public Task<ShardsResponse> ShardsAsync(IShardsRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<IShardsRequest, ShardsResponse>(request, request.RequestParameters, cancellationToken);
		public ShardsResponse Shards(Func<ShardsDescriptor, IShardsRequest> selector = null) => Shards(selector.InvokeOrDefault(new ShardsDescriptor()));
		public Task<ShardsResponse> ShardsAsync(Func<ShardsDescriptor, IShardsRequest> selector = null, CancellationToken cancellationToken = default) => ShardsAsync(selector.InvokeOrDefault(new ShardsDescriptor()), cancellationToken);
		public SnapshotsResponse Snapshots(ISnapshotsRequest request) => DoRequest<ISnapshotsRequest, SnapshotsResponse>(request, request.RequestParameters);
		public Task<SnapshotsResponse> SnapshotsAsync(ISnapshotsRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<ISnapshotsRequest, SnapshotsResponse>(request, request.RequestParameters, cancellationToken);
		public SnapshotsResponse Snapshots(Func<SnapshotsDescriptor, ISnapshotsRequest> selector = null) => Snapshots(selector.InvokeOrDefault(new SnapshotsDescriptor()));
		public Task<SnapshotsResponse> SnapshotsAsync(Func<SnapshotsDescriptor, ISnapshotsRequest> selector = null, CancellationToken cancellationToken = default) => SnapshotsAsync(selector.InvokeOrDefault(new SnapshotsDescriptor()), cancellationToken);
		public TasksResponse Tasks(ITasksRequest request) => DoRequest<ITasksRequest, TasksResponse>(request, request.RequestParameters);
		public Task<TasksResponse> TasksAsync(ITasksRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<ITasksRequest, TasksResponse>(request, request.RequestParameters, cancellationToken);
		public TasksResponse Tasks(Func<TasksDescriptor, ITasksRequest> selector = null) => Tasks(selector.InvokeOrDefault(new TasksDescriptor()));
		public Task<TasksResponse> TasksAsync(Func<TasksDescriptor, ITasksRequest> selector = null, CancellationToken cancellationToken = default) => TasksAsync(selector.InvokeOrDefault(new TasksDescriptor()), cancellationToken);
		public TemplatesResponse Templates(ITemplatesRequest request) => DoRequest<ITemplatesRequest, TemplatesResponse>(request, request.RequestParameters);
		public Task<TemplatesResponse> TemplatesAsync(ITemplatesRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<ITemplatesRequest, TemplatesResponse>(request, request.RequestParameters, cancellationToken);
		public TemplatesResponse Templates(Func<TemplatesDescriptor, ITemplatesRequest> selector = null) => Templates(selector.InvokeOrDefault(new TemplatesDescriptor()));
		public Task<TemplatesResponse> TemplatesAsync(Func<TemplatesDescriptor, ITemplatesRequest> selector = null, CancellationToken cancellationToken = default) => TemplatesAsync(selector.InvokeOrDefault(new TemplatesDescriptor()), cancellationToken);
		public ThreadPoolResponse ThreadPool(IThreadPoolRequest request) => DoRequest<IThreadPoolRequest, ThreadPoolResponse>(request, request.RequestParameters);
		public Task<ThreadPoolResponse> ThreadPoolAsync(IThreadPoolRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<IThreadPoolRequest, ThreadPoolResponse>(request, request.RequestParameters, cancellationToken);
		public ThreadPoolResponse ThreadPool(Func<ThreadPoolDescriptor, IThreadPoolRequest> selector = null) => ThreadPool(selector.InvokeOrDefault(new ThreadPoolDescriptor()));
		public Task<ThreadPoolResponse> ThreadPoolAsync(Func<ThreadPoolDescriptor, IThreadPoolRequest> selector = null, CancellationToken cancellationToken = default) => ThreadPoolAsync(selector.InvokeOrDefault(new ThreadPoolDescriptor()), cancellationToken);
		public TransformsResponse Transforms(ITransformsRequest request) => DoRequest<ITransformsRequest, TransformsResponse>(request, request.RequestParameters);
		public Task<TransformsResponse> TransformsAsync(ITransformsRequest request, CancellationToken cancellationToken = default) => DoRequestAsync<ITransformsRequest, TransformsResponse>(request, request.RequestParameters, cancellationToken);
		public TransformsResponse Transforms(Func<TransformsDescriptor, ITransformsRequest> selector = null) => Transforms(selector.InvokeOrDefault(new TransformsDescriptor()));
		public Task<TransformsResponse> TransformsAsync(Func<TransformsDescriptor, ITransformsRequest> selector = null, CancellationToken cancellationToken = default) => TransformsAsync(selector.InvokeOrDefault(new TransformsDescriptor()), cancellationToken);
	}
}