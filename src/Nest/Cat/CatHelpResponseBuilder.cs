// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;

namespace Nest
{
	internal class CatHelpResponseBuilder : CustomResponseBuilderBase
	{
		public static CatHelpResponseBuilder Instance { get; } = new CatHelpResponseBuilder();

		public override object DeserializeResponse(ITransportSerializer builtInSerializer, IApiCallDetails response, Stream stream)
		{
			var catResponse = new CatResponse<CatHelpRecord>();

			if (!response.Success)
				return catResponse;

			using (stream)
			using (var ms = response.ConnectionConfiguration.MemoryStreamFactory.Create())
			{
				stream.CopyTo(ms);
				var body = ms.ToArray().Utf8String();
				Parse(catResponse, body);
			}

			return catResponse;
		}

		public override async Task<object> DeserializeResponseAsync(ITransportSerializer builtInSerializer, IApiCallDetails response, Stream stream,
			CancellationToken ctx = default)
		{
			var catResponse = new CatResponse<CatHelpRecord>();

			if (!response.Success)
				return catResponse;

			using (stream)
			using (var ms = response.ConnectionConfiguration.MemoryStreamFactory.Create())
			{
				await stream.CopyToAsync(ms, 81920, ctx).ConfigureAwait(false);
				var body = ms.ToArray().Utf8String();
				Parse(catResponse, body);
			}

			return catResponse;
		}

		private static void Parse(CatResponse<CatHelpRecord> catResponse, string body) =>
			catResponse.Records = body.Split('\n')
				.Skip(1)
				.Select(f => new CatHelpRecord { Endpoint = f.Trim() })
				.ToList();
	}
}
