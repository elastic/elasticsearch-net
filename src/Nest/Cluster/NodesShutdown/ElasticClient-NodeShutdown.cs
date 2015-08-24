using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Nest
{
	using NodesHotThreadConverter = Func<IApiCallDetails, Stream, NodesHotThreadsResponse>;

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public INodesShutdownResponse NodesShutdown(Func<NodesShutdownDescriptor, NodesShutdownDescriptor> nodesShutdownSelector = null)
		{
			nodesShutdownSelector = nodesShutdownSelector ?? (s => s);
			return this.Dispatcher.Dispatch<NodesShutdownDescriptor, NodesShutdownRequestParameters, NodesShutdownResponse>(
				nodesShutdownSelector,
				(p, d) => this.LowLevelDispatch.NodesShutdownDispatch<NodesShutdownResponse>(p)
			);
		}

		/// <inheritdoc/>
		public Task<INodesShutdownResponse> NodesShutdownAsync(Func<NodesShutdownDescriptor, NodesShutdownDescriptor> nodesShutdownSelector = null)
		{
			nodesShutdownSelector = nodesShutdownSelector ?? (s => s);
			return this.Dispatcher.DispatchAsync<NodesShutdownDescriptor, NodesShutdownRequestParameters, NodesShutdownResponse, INodesShutdownResponse>(
				nodesShutdownSelector,
				(p, d) => this.LowLevelDispatch.NodesShutdownDispatchAsync<NodesShutdownResponse>(p)
			);
		}

		/// <inheritdoc/>
		public INodesShutdownResponse NodesShutdown(INodesShutdownRequest nodesShutdownRequest)
		{
			return this.Dispatcher.Dispatch<INodesShutdownRequest, NodesShutdownRequestParameters, NodesShutdownResponse>(
				nodesShutdownRequest,
				(p, d) => this.LowLevelDispatch.NodesShutdownDispatch<NodesShutdownResponse>(p)
			);
		}

		/// <inheritdoc/>
		public Task<INodesShutdownResponse> NodesShutdownAsync(INodesShutdownRequest nodesShutdownRequest)
		{
			return this.Dispatcher.DispatchAsync<INodesShutdownRequest, NodesShutdownRequestParameters, NodesShutdownResponse, INodesShutdownResponse>(
				nodesShutdownRequest,
				(p, d) => this.LowLevelDispatch.NodesShutdownDispatchAsync<NodesShutdownResponse>(p)
			);
		}

	}
}