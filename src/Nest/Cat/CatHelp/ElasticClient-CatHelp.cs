using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		ICatResponse<CatHelpRecord> CatHelp(Func<CatHelpDescriptor, ICatHelpRequest> selector = null);

		/// <inheritdoc />
		ICatResponse<CatHelpRecord> CatHelp(ICatHelpRequest request);

		/// <inheritdoc />
		Task<ICatResponse<CatHelpRecord>> CatHelpAsync(Func<CatHelpDescriptor, ICatHelpRequest> selector = null,
			CancellationToken ct = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<ICatResponse<CatHelpRecord>> CatHelpAsync(ICatHelpRequest request, CancellationToken ct = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICatResponse<CatHelpRecord> CatHelp(Func<CatHelpDescriptor, ICatHelpRequest> selector = null) =>
			CatHelp(selector.InvokeOrDefault(new CatHelpDescriptor()));

		/// <inheritdoc />
		public ICatResponse<CatHelpRecord> CatHelp(ICatHelpRequest request)
		{
			request.RequestParameters.DeserializationOverride = DeserializeCatHelpResponse;
			return DoRequest<ICatHelpRequest, CatResponse<CatHelpRecord>>(request, request.RequestParameters);
		}

		/// <inheritdoc />
		public Task<ICatResponse<CatHelpRecord>> CatHelpAsync(Func<CatHelpDescriptor, ICatHelpRequest> selector = null, CancellationToken ct = default) =>
			CatHelpAsync(selector.InvokeOrDefault(new CatHelpDescriptor()), ct);

		/// <inheritdoc />
		public Task<ICatResponse<CatHelpRecord>> CatHelpAsync(ICatHelpRequest request, CancellationToken ct = default)
		{
			request.RequestParameters.DeserializationOverride = DeserializeCatHelpResponse;
			return DoRequestAsync<ICatHelpRequest, ICatResponse<CatHelpRecord>, CatResponse<CatHelpRecord>>(request, request.RequestParameters, ct);
		}

		private static CatResponse<CatHelpRecord> DeserializeCatHelpResponse(IApiCallDetails response, Stream stream)
		{
			using (stream)
			using (var ms = response.ConnectionConfiguration.MemoryStreamFactory.Create())
			{
				stream.CopyTo(ms);
				var body = ms.ToArray().Utf8String();
				return new CatResponse<CatHelpRecord>
				{
					Records = body.Split('\n')
						.Skip(1)
						.Select(f => new CatHelpRecord { Endpoint = f.Trim() })
						.ToList()
				};
			}
		}
	}
}
