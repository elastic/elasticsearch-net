using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Elasticsearch.Net;

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
		INodesHotThreadsResponse NodesHotThreads(INodesHotThreadsRequest request);

		/// <inheritdoc/>
		Task<INodesHotThreadsResponse> NodesHotThreadsAsync(Func<NodesHotThreadsDescriptor, INodesHotThreadsRequest> selector = null);

		/// <inheritdoc/>
		Task<INodesHotThreadsResponse> NodesHotThreadsAsync(INodesHotThreadsRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public INodesHotThreadsResponse NodesHotThreads(Func<NodesHotThreadsDescriptor, INodesHotThreadsRequest> selector = null) =>
			this.NodesHotThreads(selector.InvokeOrDefault(new NodesHotThreadsDescriptor()));

		/// <inheritdoc/>
		public INodesHotThreadsResponse NodesHotThreads(INodesHotThreadsRequest request) =>
			this.Dispatcher.Dispatch<INodesHotThreadsRequest, NodesHotThreadsRequestParameters, NodesHotThreadsResponse>(
				request,
				new NodesHotThreadConverter(DeserializeNodesHotThreadResponse),
				(p, d) => this.LowLevelDispatch.NodesHotThreadsDispatch<NodesHotThreadsResponse>(p)
			);

		/// <inheritdoc/>
		public Task<INodesHotThreadsResponse> NodesHotThreadsAsync(Func<NodesHotThreadsDescriptor, INodesHotThreadsRequest> selector = null) =>
			this.NodesHotThreadsAsync(selector.InvokeOrDefault(new NodesHotThreadsDescriptor()));

		/// <inheritdoc/>
		public Task<INodesHotThreadsResponse> NodesHotThreadsAsync(INodesHotThreadsRequest request) =>
			this.Dispatcher.DispatchAsync<INodesHotThreadsRequest, NodesHotThreadsRequestParameters, NodesHotThreadsResponse, INodesHotThreadsResponse>(
				request,
				new NodesHotThreadConverter(DeserializeNodesHotThreadResponse),
				(p, d) => this.LowLevelDispatch.NodesHotThreadsDispatchAsync<NodesHotThreadsResponse>(p)
			);


		//::: {Dragonfly}{lvtIV72sRIWBGik7ulbuaw}{127.0.0.1}{127.0.0.1:9300}
		private static Regex NodeRegex = new Regex(@"^\s\{(?<name>.+?)\}\{(?<id>.+?)\}(?<hosts>.+)\n");


		/// <summary>
		/// Because the nodes.hot_threads endpoint returns plain text instead of JSON, we have to
		/// manually parse the response text into a typed response object.
		/// </summary>
		private NodesHotThreadsResponse DeserializeNodesHotThreadResponse(IApiCallDetails response, Stream stream)
		{
			using (stream)
			using (var sr = new StreamReader(stream, Encoding.UTF8))
			{
				var plainTextResponse = sr.ReadToEnd();

				// If the response doesn't start with :::, which is the pattern that delimits
				// each node section in the response, then the response format isn't recognized.
				// Just return an empty response object. This is especially useful when unit
				// testing against an in-memory connection where you won't get a real response.
				if (!plainTextResponse.StartsWith(":::", StringComparison.Ordinal))
					return new NodesHotThreadsResponse();

				var sections = plainTextResponse.Split(new string[] { ":::" }, StringSplitOptions.RemoveEmptyEntries);
				var info =
					from section in sections
					select section.Split(new string[] { "\n   \n" }, StringSplitOptions.None)
					into sectionLines
					where sectionLines.Length > 0
					let nodeLine = sectionLines.FirstOrDefault()
					where nodeLine != null
					let matches = NodeRegex.Match(nodeLine)
					where matches.Success
					let node = matches.Groups["name"].Value
					let nodeId = matches.Groups["id"].Value
					let hosts = matches.Groups["hosts"].Value.Split(new[] { '{', '}' }, StringSplitOptions.RemoveEmptyEntries)
					let threads = sectionLines.Skip(1).Take(sectionLines.Length - 1).ToList()
					select new HotThreadInformation
					{
						NodeName = node,
						NodeId = nodeId,
						Threads = threads,
						Hosts = hosts
					};
				return new NodesHotThreadsResponse(info.ToList());
			}
		}

	}
}