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

		private ICatResponse<TCatRecord> DoCat<TRequest, TParams, TCatRecord>(TRequest request)
			where TCatRecord : ICatRecord
			where TParams : RequestParameters<TParams>, new()
			where TRequest : class, IRequest<TParams>
		{
			ForceConfiguration(request, c =>
			{
				c.Accept = RequestData.MimeType;
				c.ContentType = RequestData.MimeType;
			});
			request.RequestParameters.DeserializationOverride = DeserializeCatResponse<TCatRecord>;
			return Dispatch2<TRequest, CatResponse<TCatRecord>>(request, request.RequestParameters);
		}

		private Task<ICatResponse<TCatRecord>> DoCatAsync<TRequest, TParams, TCatRecord>(TRequest request, CancellationToken ct)
			where TCatRecord : ICatRecord
			where TParams : RequestParameters<TParams>, new()
			where TRequest : class, IRequest<TParams>
		{
			ForceConfiguration(request, c =>
			{
				c.Accept = RequestData.MimeType;
				c.ContentType = RequestData.MimeType;
			});
			request.RequestParameters.DeserializationOverride = DeserializeCatHelpResponse;
			return Dispatch2Async<TRequest, ICatResponse<TCatRecord>, CatResponse<TCatRecord>>(request, request.RequestParameters, ct);
		}
	}
}
