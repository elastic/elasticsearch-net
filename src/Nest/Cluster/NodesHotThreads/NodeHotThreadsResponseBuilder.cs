// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	internal class NodeHotThreadsResponseBuilder : CustomResponseBuilderBase
	{
		public static NodeHotThreadsResponseBuilder Instance { get; } = new NodeHotThreadsResponseBuilder();

		//::: {Dragonfly}{lvtIV72sRIWBGik7ulbuaw}{127.0.0.1}{127.0.0.1:9300}
		private static readonly Regex NodeRegex = new Regex(@"^\s\{(?<name>.+?)\}\{(?<id>.+?)\}(?<hosts>.+)\n");

		private static NodesHotThreadsResponse Parse(string plainTextResponse)
		{
			// If the response doesn't start with :::, which is the pattern that delimits
			// each node section in the response, then the response format isn't recognized.
			// Just return an empty response object. This is especially useful when unit
			// testing against an in-memory connection where you won't get a real response.
			if (!plainTextResponse.StartsWith(":::", StringComparison.Ordinal))
				return new NodesHotThreadsResponse();

			var sections = plainTextResponse.Split(new[] { ":::" }, StringSplitOptions.RemoveEmptyEntries);
			var info =
				from section in sections
				select section.Split(new[] { "\n   \n" }, StringSplitOptions.None)
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

		public override object DeserializeResponse(IElasticsearchSerializer builtInSerializer, IApiCallDetails response, Stream stream)
		{
			if (!response.Success)
				return new NodesHotThreadsResponse();

			using (stream)
			using (var sr = new StreamReader(stream, Encoding.UTF8))
			{
				var plainTextResponse = sr.ReadToEnd();
				return Parse(plainTextResponse);
			}
		}

		public override async Task<object> DeserializeResponseAsync(
			IElasticsearchSerializer builtInSerializer,
			IApiCallDetails response,
			Stream stream,
			CancellationToken ctx = default
		)
		{
			if (!response.Success)
				return new NodesHotThreadsResponse();

			using (stream)
			using (var sr = new StreamReader(stream, Encoding.UTF8))
			{
				var plainTextResponse = await sr.ReadToEndAsync().ConfigureAwait(false);
				return Parse(plainTextResponse);
			}
		}
	}
}
