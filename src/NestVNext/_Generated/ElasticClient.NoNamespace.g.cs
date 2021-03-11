using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
    public partial class ElasticClient : IElasticClient
    {
        public AsyncSearchNamespace AsyncSearch { get; private set; }

        public AutoscalingNamespace Autoscaling { get; private set; }

        public CatNamespace Cat { get; private set; }

        public CrossClusterReplicationNamespace CrossClusterReplication { get; private set; }

        public ClusterNamespace Cluster { get; private set; }

        public DanglingIndicesNamespace DanglingIndices { get; private set; }

        public DataFrameTransformDeprecatedNamespace DataFrameTransformDeprecated { get; private set; }

        public EnrichNamespace Enrich { get; private set; }

        public EqlNamespace Eql { get; private set; }

        public FeaturesNamespace Features { get; private set; }

        public GraphNamespace Graph { get; private set; }

        public IndexLifecycleManagementNamespace IndexLifecycleManagement { get; private set; }

        public IndicesNamespace Indices { get; private set; }

        public IngestNamespace Ingest { get; private set; }

        public LicenseNamespace License { get; private set; }

        public LogstashNamespace Logstash { get; private set; }

        public MigrationNamespace Migration { get; private set; }

        public MachineLearningNamespace MachineLearning { get; private set; }

        public MonitoringNamespace Monitoring { get; private set; }

        public NodesNamespace Nodes { get; private set; }

        public RollupNamespace Rollup { get; private set; }

        public SearchableSnapshotsNamespace SearchableSnapshots { get; private set; }

        public SecurityNamespace Security { get; private set; }

        public SnapshotLifecycleManagementNamespace SnapshotLifecycleManagement { get; private set; }

        public SnapshotNamespace Snapshot { get; private set; }

        public SqlNamespace Sql { get; private set; }

        public TasksNamespace Tasks { get; private set; }

        public TextStructureNamespace TextStructure { get; private set; }

        public TransformNamespace Transform { get; private set; }

        public WatcherNamespace Watcher { get; private set; }

        public XPackNamespace XPack { get; private set; }

        private partial void SetupNamespaces()
        {
            AsyncSearch = new AsyncSearchNamespace(this);
            Autoscaling = new AutoscalingNamespace(this);
            Cat = new CatNamespace(this);
            CrossClusterReplication = new CrossClusterReplicationNamespace(this);
            Cluster = new ClusterNamespace(this);
            DanglingIndices = new DanglingIndicesNamespace(this);
            DataFrameTransformDeprecated = new DataFrameTransformDeprecatedNamespace(this);
            Enrich = new EnrichNamespace(this);
            Eql = new EqlNamespace(this);
            Features = new FeaturesNamespace(this);
            Graph = new GraphNamespace(this);
            IndexLifecycleManagement = new IndexLifecycleManagementNamespace(this);
            Indices = new IndicesNamespace(this);
            Ingest = new IngestNamespace(this);
            License = new LicenseNamespace(this);
            Logstash = new LogstashNamespace(this);
            Migration = new MigrationNamespace(this);
            MachineLearning = new MachineLearningNamespace(this);
            Monitoring = new MonitoringNamespace(this);
            Nodes = new NodesNamespace(this);
            Rollup = new RollupNamespace(this);
            SearchableSnapshots = new SearchableSnapshotsNamespace(this);
            Security = new SecurityNamespace(this);
            SnapshotLifecycleManagement = new SnapshotLifecycleManagementNamespace(this);
            Snapshot = new SnapshotNamespace(this);
            Sql = new SqlNamespace(this);
            Tasks = new TasksNamespace(this);
            TextStructure = new TextStructureNamespace(this);
            Transform = new TransformNamespace(this);
            Watcher = new WatcherNamespace(this);
            XPack = new XPackNamespace(this);
        }

        public BulkResponse Bulk(IBulkRequest request)
        {
            return DoRequest<IBulkRequest, BulkResponse>(request, request.RequestParameters);
        }

        public Task<BulkResponse> BulkAsync(IBulkRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IBulkRequest, BulkResponse>(request, request.RequestParameters, cancellationToken);
        }

        public ClearScrollResponse ClearScroll(IClearScrollRequest request)
        {
            return DoRequest<IClearScrollRequest, ClearScrollResponse>(request, request.RequestParameters);
        }

        public Task<ClearScrollResponse> ClearScrollAsync(IClearScrollRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IClearScrollRequest, ClearScrollResponse>(request, request.RequestParameters, cancellationToken);
        }

        public CountResponse Count(ICountRequest request)
        {
            return DoRequest<ICountRequest, CountResponse>(request, request.RequestParameters);
        }

        public Task<CountResponse> CountAsync(ICountRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<ICountRequest, CountResponse>(request, request.RequestParameters, cancellationToken);
        }

        public CreateResponse Create(ICreateRequest request)
        {
            return DoRequest<ICreateRequest, CreateResponse>(request, request.RequestParameters);
        }

        public Task<CreateResponse> CreateAsync(ICreateRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<ICreateRequest, CreateResponse>(request, request.RequestParameters, cancellationToken);
        }

        public DeleteResponse Delete(IDeleteRequest request)
        {
            return DoRequest<IDeleteRequest, DeleteResponse>(request, request.RequestParameters);
        }

        public Task<DeleteResponse> DeleteAsync(IDeleteRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IDeleteRequest, DeleteResponse>(request, request.RequestParameters, cancellationToken);
        }

        public DeleteByQueryResponse DeleteByQuery(IDeleteByQueryRequest request)
        {
            return DoRequest<IDeleteByQueryRequest, DeleteByQueryResponse>(request, request.RequestParameters);
        }

        public Task<DeleteByQueryResponse> DeleteByQueryAsync(IDeleteByQueryRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IDeleteByQueryRequest, DeleteByQueryResponse>(request, request.RequestParameters, cancellationToken);
        }

        public DeleteByQueryRethrottleResponse DeleteByQueryRethrottle(IDeleteByQueryRethrottleRequest request)
        {
            return DoRequest<IDeleteByQueryRethrottleRequest, DeleteByQueryRethrottleResponse>(request, request.RequestParameters);
        }

        public Task<DeleteByQueryRethrottleResponse> DeleteByQueryRethrottleAsync(IDeleteByQueryRethrottleRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IDeleteByQueryRethrottleRequest, DeleteByQueryRethrottleResponse>(request, request.RequestParameters, cancellationToken);
        }

        public DeleteScriptResponse DeleteScript(IDeleteScriptRequest request)
        {
            return DoRequest<IDeleteScriptRequest, DeleteScriptResponse>(request, request.RequestParameters);
        }

        public Task<DeleteScriptResponse> DeleteScriptAsync(IDeleteScriptRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IDeleteScriptRequest, DeleteScriptResponse>(request, request.RequestParameters, cancellationToken);
        }

        public DocumentExistsResponse DocumentExists(IDocumentExistsRequest request)
        {
            return DoRequest<IDocumentExistsRequest, DocumentExistsResponse>(request, request.RequestParameters);
        }

        public Task<DocumentExistsResponse> DocumentExistsAsync(IDocumentExistsRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IDocumentExistsRequest, DocumentExistsResponse>(request, request.RequestParameters, cancellationToken);
        }

        public SourceExistsResponse SourceExists(ISourceExistsRequest request)
        {
            return DoRequest<ISourceExistsRequest, SourceExistsResponse>(request, request.RequestParameters);
        }

        public Task<SourceExistsResponse> SourceExistsAsync(ISourceExistsRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<ISourceExistsRequest, SourceExistsResponse>(request, request.RequestParameters, cancellationToken);
        }

        public ExplainResponse Explain(IExplainRequest request)
        {
            return DoRequest<IExplainRequest, ExplainResponse>(request, request.RequestParameters);
        }

        public Task<ExplainResponse> ExplainAsync(IExplainRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IExplainRequest, ExplainResponse>(request, request.RequestParameters, cancellationToken);
        }

        public FieldCapabilitiesResponse FieldCapabilities(IFieldCapabilitiesRequest request)
        {
            return DoRequest<IFieldCapabilitiesRequest, FieldCapabilitiesResponse>(request, request.RequestParameters);
        }

        public Task<FieldCapabilitiesResponse> FieldCapabilitiesAsync(IFieldCapabilitiesRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IFieldCapabilitiesRequest, FieldCapabilitiesResponse>(request, request.RequestParameters, cancellationToken);
        }

        public GetResponse Get(IGetRequest request)
        {
            return DoRequest<IGetRequest, GetResponse>(request, request.RequestParameters);
        }

        public Task<GetResponse> GetAsync(IGetRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IGetRequest, GetResponse>(request, request.RequestParameters, cancellationToken);
        }

        public GetScriptResponse GetScript(IGetScriptRequest request)
        {
            return DoRequest<IGetScriptRequest, GetScriptResponse>(request, request.RequestParameters);
        }

        public Task<GetScriptResponse> GetScriptAsync(IGetScriptRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IGetScriptRequest, GetScriptResponse>(request, request.RequestParameters, cancellationToken);
        }

        public SourceResponse Source(ISourceRequest request)
        {
            return DoRequest<ISourceRequest, SourceResponse>(request, request.RequestParameters);
        }

        public Task<SourceResponse> SourceAsync(ISourceRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<ISourceRequest, SourceResponse>(request, request.RequestParameters, cancellationToken);
        }

        public IndexResponse Index(IIndexRequest request)
        {
            return DoRequest<IIndexRequest, IndexResponse>(request, request.RequestParameters);
        }

        public Task<IndexResponse> IndexAsync(IIndexRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IIndexRequest, IndexResponse>(request, request.RequestParameters, cancellationToken);
        }

        public RootNodeInfoResponse RootNodeInfo(IRootNodeInfoRequest request)
        {
            return DoRequest<IRootNodeInfoRequest, RootNodeInfoResponse>(request, request.RequestParameters);
        }

        public Task<RootNodeInfoResponse> RootNodeInfoAsync(IRootNodeInfoRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IRootNodeInfoRequest, RootNodeInfoResponse>(request, request.RequestParameters, cancellationToken);
        }

        public MultiSearchResponse MultiSearch(IMultiSearchRequest request)
        {
            return DoRequest<IMultiSearchRequest, MultiSearchResponse>(request, request.RequestParameters);
        }

        public Task<MultiSearchResponse> MultiSearchAsync(IMultiSearchRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IMultiSearchRequest, MultiSearchResponse>(request, request.RequestParameters, cancellationToken);
        }

        public MultiTermVectorsResponse MultiTermVectors(IMultiTermVectorsRequest request)
        {
            return DoRequest<IMultiTermVectorsRequest, MultiTermVectorsResponse>(request, request.RequestParameters);
        }

        public Task<MultiTermVectorsResponse> MultiTermVectorsAsync(IMultiTermVectorsRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IMultiTermVectorsRequest, MultiTermVectorsResponse>(request, request.RequestParameters, cancellationToken);
        }

        public PingResponse Ping(IPingRequest request)
        {
            return DoRequest<IPingRequest, PingResponse>(request, request.RequestParameters);
        }

        public Task<PingResponse> PingAsync(IPingRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IPingRequest, PingResponse>(request, request.RequestParameters, cancellationToken);
        }

        public PutScriptResponse PutScript(IPutScriptRequest request)
        {
            return DoRequest<IPutScriptRequest, PutScriptResponse>(request, request.RequestParameters);
        }

        public Task<PutScriptResponse> PutScriptAsync(IPutScriptRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IPutScriptRequest, PutScriptResponse>(request, request.RequestParameters, cancellationToken);
        }

        public ReindexResponse Reindex(IReindexRequest request)
        {
            return DoRequest<IReindexRequest, ReindexResponse>(request, request.RequestParameters);
        }

        public Task<ReindexResponse> ReindexAsync(IReindexRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IReindexRequest, ReindexResponse>(request, request.RequestParameters, cancellationToken);
        }

        public ReindexRethrottleResponse ReindexRethrottle(IReindexRethrottleRequest request)
        {
            return DoRequest<IReindexRethrottleRequest, ReindexRethrottleResponse>(request, request.RequestParameters);
        }

        public Task<ReindexRethrottleResponse> ReindexRethrottleAsync(IReindexRethrottleRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IReindexRethrottleRequest, ReindexRethrottleResponse>(request, request.RequestParameters, cancellationToken);
        }

        public RenderSearchTemplateResponse RenderSearchTemplate(IRenderSearchTemplateRequest request)
        {
            return DoRequest<IRenderSearchTemplateRequest, RenderSearchTemplateResponse>(request, request.RequestParameters);
        }

        public Task<RenderSearchTemplateResponse> RenderSearchTemplateAsync(IRenderSearchTemplateRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IRenderSearchTemplateRequest, RenderSearchTemplateResponse>(request, request.RequestParameters, cancellationToken);
        }

        public ExecutePainlessScriptResponse ExecutePainlessScript(IExecutePainlessScriptRequest request)
        {
            return DoRequest<IExecutePainlessScriptRequest, ExecutePainlessScriptResponse>(request, request.RequestParameters);
        }

        public Task<ExecutePainlessScriptResponse> ExecutePainlessScriptAsync(IExecutePainlessScriptRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IExecutePainlessScriptRequest, ExecutePainlessScriptResponse>(request, request.RequestParameters, cancellationToken);
        }

        public ScrollResponse Scroll(IScrollRequest request)
        {
            return DoRequest<IScrollRequest, ScrollResponse>(request, request.RequestParameters);
        }

        public Task<ScrollResponse> ScrollAsync(IScrollRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IScrollRequest, ScrollResponse>(request, request.RequestParameters, cancellationToken);
        }

        public SearchResponse Search(ISearchRequest request)
        {
            return DoRequest<ISearchRequest, SearchResponse>(request, request.RequestParameters);
        }

        public Task<SearchResponse> SearchAsync(ISearchRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<ISearchRequest, SearchResponse>(request, request.RequestParameters, cancellationToken);
        }

        public SearchShardsResponse SearchShards(ISearchShardsRequest request)
        {
            return DoRequest<ISearchShardsRequest, SearchShardsResponse>(request, request.RequestParameters);
        }

        public Task<SearchShardsResponse> SearchShardsAsync(ISearchShardsRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<ISearchShardsRequest, SearchShardsResponse>(request, request.RequestParameters, cancellationToken);
        }

        public TermVectorsResponse TermVectors(ITermVectorsRequest request)
        {
            return DoRequest<ITermVectorsRequest, TermVectorsResponse>(request, request.RequestParameters);
        }

        public Task<TermVectorsResponse> TermVectorsAsync(ITermVectorsRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<ITermVectorsRequest, TermVectorsResponse>(request, request.RequestParameters, cancellationToken);
        }

        public UpdateResponse Update(IUpdateRequest request)
        {
            return DoRequest<IUpdateRequest, UpdateResponse>(request, request.RequestParameters);
        }

        public Task<UpdateResponse> UpdateAsync(IUpdateRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IUpdateRequest, UpdateResponse>(request, request.RequestParameters, cancellationToken);
        }

        public UpdateByQueryResponse UpdateByQuery(IUpdateByQueryRequest request)
        {
            return DoRequest<IUpdateByQueryRequest, UpdateByQueryResponse>(request, request.RequestParameters);
        }

        public Task<UpdateByQueryResponse> UpdateByQueryAsync(IUpdateByQueryRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IUpdateByQueryRequest, UpdateByQueryResponse>(request, request.RequestParameters, cancellationToken);
        }
    }
}