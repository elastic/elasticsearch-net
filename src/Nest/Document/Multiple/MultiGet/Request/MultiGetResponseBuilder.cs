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
			var statefulSerializer = builtInSerializer.CreateStateful(Formatter);
			return statefulSerializer.Deserialize<MultiGetResponse>(stream);
		}

		public override async Task<object> DeserializeResponseAsync(
			IElasticsearchSerializer builtInSerializer,
			IApiCallDetails response,
			Stream stream,
			CancellationToken ctx = default
		)
		{
			var statefulSerializer = builtInSerializer.CreateStateful(Formatter);
			return await statefulSerializer.DeserializeAsync<MultiGetResponse>(stream, ctx);
		}
	}
}
