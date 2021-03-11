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
    public class GraphNamespace : NamespacedClientProxy
    {
        internal GraphNamespace(ElasticClient client): base(client)
        {
        }

        public GraphExploreResponse Explore(IGraphExploreRequest request)
        {
            return DoRequest<IGraphExploreRequest, GraphExploreResponse>(request, request.RequestParameters);
        }

        public Task<GraphExploreResponse> ExploreAsync(IGraphExploreRequest request, CancellationToken cancellationToken = default)
        {
            return DoRequestAsync<IGraphExploreRequest, GraphExploreResponse>(request, request.RequestParameters, cancellationToken);
        }
    }
}