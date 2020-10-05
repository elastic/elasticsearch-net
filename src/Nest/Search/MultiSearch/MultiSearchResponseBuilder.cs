// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;

namespace Nest
{
	internal class MultiSearchResponseBuilder : CustomResponseBuilderBase
	{
		public MultiSearchResponseBuilder(IRequest request) => Formatter = new MultiSearchResponseFormatter(request);

		private MultiSearchResponseFormatter Formatter { get; }

		public override object DeserializeResponse(ITransportSerializer builtInSerializer, IApiCallDetails response, Stream stream) =>
			response.Success
				? builtInSerializer.CreateStateful(Formatter).Deserialize<MultiSearchResponse>(stream)
				: new MultiSearchResponse();

		public override async Task<object> DeserializeResponseAsync(
			ITransportSerializer builtInSerializer,
			IApiCallDetails response,
			Stream stream,
			CancellationToken ctx = default
		) =>
			response.Success
				? await builtInSerializer.CreateStateful(Formatter)
					.DeserializeAsync<MultiSearchResponse>(stream, ctx)
					.ConfigureAwait(false)
				: new MultiSearchResponse();
	}
}
