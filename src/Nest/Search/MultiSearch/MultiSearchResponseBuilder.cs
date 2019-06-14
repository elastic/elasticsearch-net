using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	internal class MultiSearchResponseBuilder : CustomResponseBuilderBase
	{
		public MultiSearchResponseBuilder(IRequest request) => Formatter = new MultiSearchResponseFormatter(request);

		private MultiSearchResponseFormatter Formatter { get; }

		public override object DeserializeResponse(IElasticsearchSerializer builtInSerializer, IApiCallDetails response, Stream stream)
		{
			return response.Success
				? builtInSerializer.CreateStateful(Formatter).Deserialize<MultiSearchResponse>(stream)
				: null;
		}

		public override async Task<object> DeserializeResponseAsync(
			IElasticsearchSerializer builtInSerializer,
			IApiCallDetails response,
			Stream stream,
			CancellationToken ctx = default
		)
		{
			return response.Success
				? await builtInSerializer.CreateStateful(Formatter).DeserializeAsync<MultiSearchResponse>(stream, ctx)
				: null;
		}
	}
}
