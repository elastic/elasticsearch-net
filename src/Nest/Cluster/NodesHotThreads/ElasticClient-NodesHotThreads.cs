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

	public partial interface IElasticClient
	{
		/// <summary>
		/// An API allowing to get the current hot threads on each node in the cluster.
		/// </summary>
		/// <param name="selector"></param>
		/// <returns>An optional descriptor to further describe the nodes hot threads operation</returns>
		INodesHotThreadsResponse NodesHotThreads(Func<NodesHotThreadsDescriptor, INodesHotThreadsRequest> selector = null);

		/// <inheritdoc/>
		INodesHotThreadsResponse NodesHotThreads(INodesHotThreadsRequest nodesHotThreadsRequest);

		/// <inheritdoc/>
		Task<INodesHotThreadsResponse> NodesHotThreadsAsync(Func<NodesHotThreadsDescriptor, INodesHotThreadsRequest> selector = null);

		/// <inheritdoc/>
		Task<INodesHotThreadsResponse> NodesHotThreadsAsync(INodesHotThreadsRequest nodesHotThreadsRequest);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public INodesHotThreadsResponse NodesHotThreads(Func<NodesHotThreadsDescriptor, INodesHotThreadsRequest> selector = null) =>
			this.NodesHotThreads(selector.InvokeOrDefault(new NodesHotThreadsDescriptor()));

		/// <inheritdoc/>
		public INodesHotThreadsResponse NodesHotThreads(INodesHotThreadsRequest nodesHotThreadsRequest) => 
			this.Dispatcher.Dispatch<INodesHotThreadsRequest, NodesHotThreadsRequestParameters, NodesHotThreadsResponse>(
				nodesHotThreadsRequest,
				new NodesHotThreadConverter(DeserializeNodesHotThreadResponse),
				(p, d) => this.LowLevelDispatch.NodesHotThreadsDispatch<NodesHotThreadsResponse>(p)
			);

		/// <inheritdoc/>
		public Task<INodesHotThreadsResponse> NodesHotThreadsAsync(Func<NodesHotThreadsDescriptor, INodesHotThreadsRequest> selector = null) =>
			this.NodesHotThreadsAsync(selector.InvokeOrDefault(new NodesHotThreadsDescriptor()));

		/// <inheritdoc/>
		public Task<INodesHotThreadsResponse> NodesHotThreadsAsync(INodesHotThreadsRequest nodesHotThreadsRequest) => 
			this.Dispatcher.DispatchAsync<INodesHotThreadsRequest, NodesHotThreadsRequestParameters, NodesHotThreadsResponse, INodesHotThreadsResponse>(
				nodesHotThreadsRequest,
				new NodesHotThreadConverter(DeserializeNodesHotThreadResponse),
				(p, d) => this.LowLevelDispatch.NodesHotThreadsDispatchAsync<NodesHotThreadsResponse>(p)
			);

		/// <summary>
		/// Because the nodes.hot_threads endpoint returns plain text instead of JSON, we have to
		/// manually parse the response text into a typed response object.
		/// </summary>
		private NodesHotThreadsResponse DeserializeNodesHotThreadResponse(IApiCallDetails response, Stream stream)
		{
			var typedResponse = new NodesHotThreadsResponse();
			var plainTextResponse = response.ResponseBodyInBytes.Utf8String();

			// If the response doesn't start with :::, which is the pattern that delimits
			// each node section in the response, then the response format isn't recognized.
			// Just return an empty response object. This is especially useful when unit
			// testing against an in-memory connection where you won't get a real response.
			if (!plainTextResponse.StartsWith(":::", StringComparison.Ordinal))
				return typedResponse;

			var sections = plainTextResponse.Split(new string[] { ":::" }, StringSplitOptions.RemoveEmptyEntries);

			foreach (var section in sections)
			{
				var sectionLines = section.Split(new string[] { "\n   \n" }, StringSplitOptions.None);

				if (sectionLines.Length > 0)
				{
					var hotThreadInfo = new HotThreadInformation
					{
						// First line contains the node name between [ ]
						Node = sectionLines.First().Split('[')[1].TrimEnd(']'),
						// The rest of the lines are hot threads
						Threads = sectionLines.Skip(1).Take(sectionLines.Length - 1).ToList()
					};

					typedResponse.HotThreads.Add(hotThreadInfo);
				}
			}

			return typedResponse;
		}

	}
}