using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public class MultiSearchResponseBuilder : CustomResponseBuilderBase
	{
		public MultiSearchResponseBuilder(IRequest request) => Formatter = new MultiSearchResponseFormatter(request);

		private MultiSearchResponseFormatter Formatter { get; }

		public override object DeserializeResponse(IElasticsearchSerializer builtInSerializer, IApiCallDetails response, Stream stream)
		{
			var statefulSerializer = builtInSerializer.CreateStateful(Formatter);
			return statefulSerializer.Deserialize<MultiSearchResponse>(stream);
		}

		public override async Task<object> DeserializeResponseAsync(
			IElasticsearchSerializer builtInSerializer,
			IApiCallDetails response,
			Stream stream,
			CancellationToken ctx = default
		)
		{
			var statefulSerializer = builtInSerializer.CreateStateful(Formatter);
			return await statefulSerializer.DeserializeAsync<MultiSearchResponse>(stream, ctx);
		}
	}
}
