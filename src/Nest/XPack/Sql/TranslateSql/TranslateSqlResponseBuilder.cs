using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	internal class TranslateSqlResponseBuilder : CustomResponseBuilderBase
	{
		public static TranslateSqlResponseBuilder Instance { get; } = new TranslateSqlResponseBuilder();

		public override object DeserializeResponse(IElasticsearchSerializer builtInSerializer, IApiCallDetails response, Stream stream) =>
			response.Success
				? new TranslateSqlResponse { Result = builtInSerializer.Deserialize<ISearchRequest>(stream) }
				: new TranslateSqlResponse();

		public override async Task<object> DeserializeResponseAsync(
			IElasticsearchSerializer builtInSerializer,
			IApiCallDetails response,
			Stream stream,
			CancellationToken ctx = default
		) =>
			response.Success
				? new TranslateSqlResponse
				{
					Result = await builtInSerializer.DeserializeAsync<ISearchRequest>(stream, ctx).ConfigureAwait(false)
				}
				: new TranslateSqlResponse();
	}
}
