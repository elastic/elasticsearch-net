using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public abstract class CustomResponseBuilderBase
	{
		public abstract object DeserializeResponse(IElasticsearchSerializer builtInSerializer, IApiCallDetails response, Stream stream);

		public abstract Task<object> DeserializeResponseAsync(IElasticsearchSerializer builtInSerializer, IApiCallDetails response, Stream stream, CancellationToken ctx = default);
	}
}
