using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		private CatResponse<TCatRecord> DeserializeCatResponse<TCatRecord>(IApiCallDetails response, Stream stream)
			where TCatRecord : ICatRecord
		{
			var catResponse = new CatResponse<TCatRecord>();

			if (!response.Success) return catResponse;

			var records = RequestResponseSerializer.Deserialize<IReadOnlyCollection<TCatRecord>>(stream);
			catResponse.Records = records;

			return catResponse;
		}

		private CatResponse<TCatRecord> DoCat<TRequest, TParams, TCatRecord>(TRequest request)
			where TCatRecord : ICatRecord
			where TParams : RequestParameters<TParams>, new()
			where TRequest : class, IRequest<TParams>
		{
			request.RequestParameters.DeserializationOverride = DeserializeCatResponse<TCatRecord>;
			return DoRequest<TRequest, CatResponse<TCatRecord>>(request, request.RequestParameters, r => ForceJson(r));
		}

		private Task<CatResponse<TCatRecord>> DoCatAsync<TRequest, TParams, TCatRecord>(TRequest request, CancellationToken ct)
			where TCatRecord : ICatRecord
			where TParams : RequestParameters<TParams>, new()
			where TRequest : class, IRequest<TParams>
		{
			request.RequestParameters.DeserializationOverride = DeserializeCatResponse<TCatRecord>;
			return DoRequestAsync<TRequest, CatResponse<TCatRecord>>(request, request.RequestParameters, ct, r => ForceJson(r));
		}
	}
}
