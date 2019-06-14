using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	internal class MultiGetResponseBuilder : CustomResponseBuilderBase
	{
		public MultiGetResponseBuilder(IMultiGetRequest request) => Formatter = new MultiGetResponseFormatter(request);

		private MultiGetResponseFormatter Formatter { get; }

		public override object DeserializeResponse(IElasticsearchSerializer builtInSerializer, IApiCallDetails response, Stream stream)
		{
			return response.Success
				? builtInSerializer.CreateStateful(Formatter).Deserialize<MultiGetResponse>(stream)
				: new MultiGetResponse();
		}

		public override async Task<object> DeserializeResponseAsync(
			IElasticsearchSerializer builtInSerializer,
			IApiCallDetails response,
			Stream stream,
			CancellationToken ctx = default
		)
		{
			return response.Success
				? await builtInSerializer.CreateStateful(Formatter).DeserializeAsync<MultiGetResponse>(stream, ctx)
				: new MultiGetResponse();
		}
	}
}
