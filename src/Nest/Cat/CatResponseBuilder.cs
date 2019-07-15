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
			if (!response.Success)
				return new CatResponse<TCatRecord>();

			if (response.HttpStatusCode == 404)
				return builtInSerializer.Deserialize<CatResponse<TCatRecord>>(stream);

			var catResponse = new CatResponse<TCatRecord>();
			var records = builtInSerializer.Deserialize<IReadOnlyCollection<TCatRecord>>(stream);
			catResponse.Records = records;
			return catResponse;
		}

		public override async Task<object> DeserializeResponseAsync(IElasticsearchSerializer builtInSerializer, IApiCallDetails response, Stream stream, CancellationToken ctx = default)
		{
			if (!response.Success)
				return new CatResponse<TCatRecord>();

			if (response.HttpStatusCode == 404)
				return await builtInSerializer.DeserializeAsync<CatResponse<TCatRecord>>(stream, ctx).ConfigureAwait(false);

			var catResponse = new CatResponse<TCatRecord>();
			var records = await builtInSerializer.DeserializeAsync<IReadOnlyCollection<TCatRecord>>(stream, ctx).ConfigureAwait(false);
			catResponse.Records = records;
			return catResponse;
		}
	}
}
