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
		/// <inheritdoc />
		public INodesHotThreadsResponse NodesHotThreads(Func<NodesHotThreadsDescriptor, NodesHotThreadsDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatcher.Dispatch<NodesHotThreadsDescriptor, NodesHotThreadsRequestParameters, NodesHotThreadsResponse>(
				selector,
				(p, d) => this.LowLevelDispatch.NodesHotThreadsDispatch<NodesHotThreadsResponse>(
					p.DeserializationState(new NodesHotThreadConverter(DeserializeNodesHotThreadResponse)))
				);
		}

		/// <inheritdoc />
		public INodesHotThreadsResponse NodesHotThreads(INodesHotThreadsRequest nodesHotThreadsRequest)
		{
			return this.Dispatcher.Dispatch<INodesHotThreadsRequest, NodesHotThreadsRequestParameters, NodesHotThreadsResponse>(
				nodesHotThreadsRequest,
				(p, d) => this.LowLevelDispatch.NodesHotThreadsDispatch<NodesHotThreadsResponse>(
					p.DeserializationState(new NodesHotThreadConverter(DeserializeNodesHotThreadResponse)))
			);
		}

		/// <inheritdoc />
		public Task<INodesHotThreadsResponse> NodesHotThreadsAsync(Func<NodesHotThreadsDescriptor, NodesHotThreadsDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatcher.DispatchAsync<NodesHotThreadsDescriptor, NodesHotThreadsRequestParameters, NodesHotThreadsResponse, INodesHotThreadsResponse>(
				selector,
				(p, d) => this.LowLevelDispatch.NodesHotThreadsDispatchAsync<NodesHotThreadsResponse>(
					p.DeserializationState(new NodesHotThreadConverter(DeserializeNodesHotThreadResponse)))
			);
		}

		/// <inheritdoc />
		public Task<INodesHotThreadsResponse> NodesHotThreadsAsync(INodesHotThreadsRequest nodesHotThreadsRequest)
		{
			return this.Dispatcher.DispatchAsync<INodesHotThreadsRequest, NodesHotThreadsRequestParameters, NodesHotThreadsResponse, INodesHotThreadsResponse>(
				nodesHotThreadsRequest,
				(p, d) => this.LowLevelDispatch.NodesHotThreadsDispatchAsync<NodesHotThreadsResponse>(
					p.DeserializationState(new NodesHotThreadConverter(DeserializeNodesHotThreadResponse)))
			);
		}

		/// <summary>
		/// Because the nodes.hot_threads endpoint returns plain text instead of JSON, we have to
		/// manually parse the response text into a typed response object.
		/// </summary>
		private NodesHotThreadsResponse DeserializeNodesHotThreadResponse(IApiCallDetails response, Stream stream)
		{
			var typedResponse = new NodesHotThreadsResponse();
			var plainTextResponse = Encoding.UTF8.GetString(response.ResponseBodyInBytes);

			// If the response doesn't start with :::, which is the pattern that delimits
			// each node section in the response, then the response format isn't recognized.
			// Just return an empty response object. This is especially useful when unit
			// testing against an in-memory connection where you won't get a real response.
			if (!plainTextResponse.StartsWith(":::"))
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