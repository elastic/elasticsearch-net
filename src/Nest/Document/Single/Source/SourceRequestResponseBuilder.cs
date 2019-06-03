using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public class SourceRequestResponseBuilder<TDocument> : CustomResponseBuilderBase
	{
		public static SourceRequestResponseBuilder<TDocument> Instance { get; } = new SourceRequestResponseBuilder<TDocument>();

		public override object DeserializeResponse(IElasticsearchSerializer builtInSerializer, IApiCallDetails response, Stream stream)
		{
			var source = builtInSerializer.Deserialize<TDocument>(stream);
			return new SourceResponse<TDocument> { Body = source, };
		}

		public override Task<object> DeserializeResponseAsync(IElasticsearchSerializer builtInSerializer, IApiCallDetails response, Stream stream, CancellationToken ctx = default) => throw new System.NotImplementedException();
	}
}
