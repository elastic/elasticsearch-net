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
using System;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
    public partial class ElasticClient : IElasticClient
    {
        public ClusterNamespace Cluster { get; private set; }

        public IndicesNamespace Indices { get; private set; }

        private partial void SetupNamespaces()
        {
            Cluster = new ClusterNamespace(this);
            Indices = new IndicesNamespace(this);
        }

        public DeleteResponse Delete(IDeleteRequest request)
        {
            return DoRequest<IDeleteRequest, DeleteResponse>(request, request.RequestParameters);
        }

        public Task<DeleteResponse> DeleteAsync(IDeleteRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IDeleteRequest, DeleteResponse>(request, request.RequestParameters, cancellationToken);
        }

        public DeleteResponse Delete(IndexName index, Id id, Func<DeleteDescriptor, IDeleteRequest> selector = null)
        {
            return Delete(selector.InvokeOrDefault(new DeleteDescriptor(index, id)));
        }

        public Task<DeleteResponse> DeleteAsync(IndexName index, Id id, Func<DeleteDescriptor, IDeleteRequest> selector = null, CancellationToken cancellationToken = default)
        {
            return DeleteAsync(selector.InvokeOrDefault(new DeleteDescriptor(index, id)), cancellationToken);
        }

        public ExistsResponse Exists(IExistsRequest request)
        {
            return DoRequest<IExistsRequest, ExistsResponse>(request, request.RequestParameters);
        }

        public Task<ExistsResponse> ExistsAsync(IExistsRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IExistsRequest, ExistsResponse>(request, request.RequestParameters, cancellationToken);
        }

        public ExistsResponse Exists(IndexName index, Id id, Func<ExistsDescriptor, IExistsRequest> selector = null)
        {
            return Exists(selector.InvokeOrDefault(new ExistsDescriptor(index, id)));
        }

        public Task<ExistsResponse> ExistsAsync(IndexName index, Id id, Func<ExistsDescriptor, IExistsRequest> selector = null, CancellationToken cancellationToken = default)
        {
            return ExistsAsync(selector.InvokeOrDefault(new ExistsDescriptor(index, id)), cancellationToken);
        }

        public GetResponse<TDocument> Get<TDocument>(IGetRequest request)
        {
            return DoRequest<IGetRequest, GetResponse<TDocument>>(request, request.RequestParameters);
        }

        public Task<GetResponse<TDocument>> GetAsync<TDocument>(IGetRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IGetRequest, GetResponse<TDocument>>(request, request.RequestParameters, cancellationToken);
        }

        public GetResponse<TDocument> Get<TDocument>(IndexName index, Id id, Func<GetDescriptor, IGetRequest> selector = null)
        {
            return Get<TDocument>(selector.InvokeOrDefault(new GetDescriptor(index, id)));
        }

        public Task<GetResponse<TDocument>> GetAsync<TDocument>(IndexName index, Id id, Func<GetDescriptor, IGetRequest> selector = null, CancellationToken cancellationToken = default)
        {
            return GetAsync<TDocument>(selector.InvokeOrDefault(new GetDescriptor(index, id)), cancellationToken);
        }

        public IndexResponse Index<TDocument>(IIndexRequest<TDocument> request)
        {
            return DoRequest<IIndexRequest<TDocument>, IndexResponse>(request, request.RequestParameters);
        }

        public Task<IndexResponse> IndexAsync<TDocument>(IIndexRequest<TDocument> request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IIndexRequest<TDocument>, IndexResponse>(request, request.RequestParameters, cancellationToken);
        }

        public IndexResponse Index<TDocument>(TDocument document, IndexName index, Func<IndexDescriptor<TDocument>, IIndexRequest<TDocument>> selector = null)
        {
            return Index(selector.InvokeOrDefault(new IndexDescriptor<TDocument>(index)));
        }

        public Task<IndexResponse> IndexAsync<TDocument>(TDocument document, IndexName index, Func<IndexDescriptor<TDocument>, IIndexRequest<TDocument>> selector = null, CancellationToken cancellationToken = default)
        {
            return IndexAsync(selector.InvokeOrDefault(new IndexDescriptor<TDocument>(index)), cancellationToken);
        }

        public PingResponse Ping(IPingRequest request)
        {
            return DoRequest<IPingRequest, PingResponse>(request, request.RequestParameters);
        }

        public Task<PingResponse> PingAsync(IPingRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IPingRequest, PingResponse>(request, request.RequestParameters, cancellationToken);
        }

        public PingResponse Ping(Func<PingDescriptor, IPingRequest> selector = null)
        {
            return Ping(selector.InvokeOrDefault(new PingDescriptor()));
        }

        public Task<PingResponse> PingAsync(Func<PingDescriptor, IPingRequest> selector = null, CancellationToken cancellationToken = default)
        {
            return PingAsync(selector.InvokeOrDefault(new PingDescriptor()), cancellationToken);
        }

        public SearchResponse<TDocument> Search<TDocument>(ISearchRequest request)
        {
            return DoRequest<ISearchRequest, SearchResponse<TDocument>>(request, request.RequestParameters);
        }

        public Task<SearchResponse<TDocument>> SearchAsync<TDocument>(ISearchRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<ISearchRequest, SearchResponse<TDocument>>(request, request.RequestParameters, cancellationToken);
        }

        public SearchResponse<TDocument> Search<TDocument>(Func<SearchDescriptor, ISearchRequest> selector = null)
        {
            return Search<TDocument>(selector.InvokeOrDefault(new SearchDescriptor()));
        }

        public Task<SearchResponse<TDocument>> SearchAsync<TDocument>(Func<SearchDescriptor, ISearchRequest> selector = null, CancellationToken cancellationToken = default)
        {
            return SearchAsync<TDocument>(selector.InvokeOrDefault(new SearchDescriptor()), cancellationToken);
        }
    }
}