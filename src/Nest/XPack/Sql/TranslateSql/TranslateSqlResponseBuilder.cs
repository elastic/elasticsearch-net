using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	internal class TranslateSqlResponseBuilder : CustomResponseBuilderBase
	{
		public static TranslateSqlResponseBuilder Instance { get; } = new TranslateSqlResponseBuilder();

		public override object DeserializeResponse(IElasticsearchSerializer builtInSerializer, IApiCallDetails response, Stream stream)
		{
			var result = builtInSerializer.Deserialize<ISearchRequest>(stream);
			return new TranslateSqlResponse { Result = result };
		}

		public override async Task<object> DeserializeResponseAsync(
			IElasticsearchSerializer builtInSerializer,
			IApiCallDetails response,
			Stream stream,
			CancellationToken ctx = default
		)
		{
			var result = await builtInSerializer.DeserializeAsync<ISearchRequest>(stream);
			return new TranslateSqlResponse { Result = result };
		}
	}
}
