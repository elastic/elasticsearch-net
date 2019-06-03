using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest 
{
	internal class CatResponseBuilder<TCatRecord> : CustomResponseBuilderBase where TCatRecord : ICatRecord
	{
		public static CatResponseBuilder<TCatRecord> Instance { get; } = new CatResponseBuilder<TCatRecord>();
		
		public override object DeserializeResponse(IElasticsearchSerializer builtInSerializer, IApiCallDetails response, Stream stream)
		{
			var catResponse = new CatResponse<TCatRecord>();

			if (!response.Success) return catResponse;

			var records = builtInSerializer.Deserialize<IReadOnlyCollection<TCatRecord>>(stream);
			catResponse.Records = records;

			return catResponse;
			
		}

		
		public override async Task<object> DeserializeResponseAsync(IElasticsearchSerializer builtInSerializer, IApiCallDetails response, Stream stream, CancellationToken ctx = default)
		{
			var catResponse = new CatResponse<TCatRecord>();

			if (!response.Success) return catResponse;

			var records = await builtInSerializer.DeserializeAsync<IReadOnlyCollection<TCatRecord>>(stream, ctx);
			catResponse.Records = records;

			return catResponse;
			
		}

	}
}