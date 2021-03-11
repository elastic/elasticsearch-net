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
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;

namespace Nest
{
    public class SnapshotLifecycleManagementNamespace : NamespacedClientProxy
    {
        internal SnapshotLifecycleManagementNamespace(ElasticClient client): base(client)
        {
        }

        public DeleteSnapshotLifecycleResponse DeleteSnapshotLifecycle(IDeleteSnapshotLifecycleRequest request)
        {
            return DoRequest<IDeleteSnapshotLifecycleRequest, DeleteSnapshotLifecycleResponse>(request, request.RequestParameters);
        }

        public Task<DeleteSnapshotLifecycleResponse> DeleteSnapshotLifecycleAsync(IDeleteSnapshotLifecycleRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IDeleteSnapshotLifecycleRequest, DeleteSnapshotLifecycleResponse>(request, request.RequestParameters, cancellationToken);
        }

        public ExecuteSnapshotLifecycleResponse ExecuteSnapshotLifecycle(IExecuteSnapshotLifecycleRequest request)
        {
            return DoRequest<IExecuteSnapshotLifecycleRequest, ExecuteSnapshotLifecycleResponse>(request, request.RequestParameters);
        }

        public Task<ExecuteSnapshotLifecycleResponse> ExecuteSnapshotLifecycleAsync(IExecuteSnapshotLifecycleRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IExecuteSnapshotLifecycleRequest, ExecuteSnapshotLifecycleResponse>(request, request.RequestParameters, cancellationToken);
        }

        public ExecuteRetentionResponse ExecuteRetention(IExecuteRetentionRequest request)
        {
            return DoRequest<IExecuteRetentionRequest, ExecuteRetentionResponse>(request, request.RequestParameters);
        }

        public Task<ExecuteRetentionResponse> ExecuteRetentionAsync(IExecuteRetentionRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IExecuteRetentionRequest, ExecuteRetentionResponse>(request, request.RequestParameters, cancellationToken);
        }

        public GetSnapshotLifecycleResponse GetSnapshotLifecycle(IGetSnapshotLifecycleRequest request)
        {
            return DoRequest<IGetSnapshotLifecycleRequest, GetSnapshotLifecycleResponse>(request, request.RequestParameters);
        }

        public Task<GetSnapshotLifecycleResponse> GetSnapshotLifecycleAsync(IGetSnapshotLifecycleRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IGetSnapshotLifecycleRequest, GetSnapshotLifecycleResponse>(request, request.RequestParameters, cancellationToken);
        }

        public GetSnapshotLifecycleStatsResponse GetSnapshotLifecycleStats(IGetSnapshotLifecycleStatsRequest request)
        {
            return DoRequest<IGetSnapshotLifecycleStatsRequest, GetSnapshotLifecycleStatsResponse>(request, request.RequestParameters);
        }

        public Task<GetSnapshotLifecycleStatsResponse> GetSnapshotLifecycleStatsAsync(IGetSnapshotLifecycleStatsRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IGetSnapshotLifecycleStatsRequest, GetSnapshotLifecycleStatsResponse>(request, request.RequestParameters, cancellationToken);
        }

        public GetSnapshotLifecycleManagementStatusResponse GetSnapshotLifecycleManagementStatus(IGetSnapshotLifecycleManagementStatusRequest request)
        {
            return DoRequest<IGetSnapshotLifecycleManagementStatusRequest, GetSnapshotLifecycleManagementStatusResponse>(request, request.RequestParameters);
        }

        public Task<GetSnapshotLifecycleManagementStatusResponse> GetSnapshotLifecycleManagementStatusAsync(IGetSnapshotLifecycleManagementStatusRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IGetSnapshotLifecycleManagementStatusRequest, GetSnapshotLifecycleManagementStatusResponse>(request, request.RequestParameters, cancellationToken);
        }

        public PutSnapshotLifecycleResponse PutSnapshotLifecycle(IPutSnapshotLifecycleRequest request)
        {
            return DoRequest<IPutSnapshotLifecycleRequest, PutSnapshotLifecycleResponse>(request, request.RequestParameters);
        }

        public Task<PutSnapshotLifecycleResponse> PutSnapshotLifecycleAsync(IPutSnapshotLifecycleRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IPutSnapshotLifecycleRequest, PutSnapshotLifecycleResponse>(request, request.RequestParameters, cancellationToken);
        }

        public StartSnapshotLifecycleManagementResponse StartSnapshotLifecycleManagement(IStartSnapshotLifecycleManagementRequest request)
        {
            return DoRequest<IStartSnapshotLifecycleManagementRequest, StartSnapshotLifecycleManagementResponse>(request, request.RequestParameters);
        }

        public Task<StartSnapshotLifecycleManagementResponse> StartSnapshotLifecycleManagementAsync(IStartSnapshotLifecycleManagementRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IStartSnapshotLifecycleManagementRequest, StartSnapshotLifecycleManagementResponse>(request, request.RequestParameters, cancellationToken);
        }

        public StopSnapshotLifecycleManagementResponse StopSnapshotLifecycleManagement(IStopSnapshotLifecycleManagementRequest request)
        {
            return DoRequest<IStopSnapshotLifecycleManagementRequest, StopSnapshotLifecycleManagementResponse>(request, request.RequestParameters);
        }

        public Task<StopSnapshotLifecycleManagementResponse> StopSnapshotLifecycleManagementAsync(IStopSnapshotLifecycleManagementRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IStopSnapshotLifecycleManagementRequest, StopSnapshotLifecycleManagementResponse>(request, request.RequestParameters, cancellationToken);
        }
    }
}