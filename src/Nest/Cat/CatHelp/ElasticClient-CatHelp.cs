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
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<ICatResponse<CatHelpRecord>> CatHelpAsync(ICatHelpRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICatResponse<CatHelpRecord> CatHelp(Func<CatHelpDescriptor, ICatHelpRequest> selector = null) =>
			CatHelp(selector.InvokeOrDefault(new CatHelpDescriptor()));

		/// <inheritdoc />
		public ICatResponse<CatHelpRecord> CatHelp(ICatHelpRequest request) =>
			Dispatcher.Dispatch<ICatHelpRequest, CatHelpRequestParameters, CatResponse<CatHelpRecord>>(
				request,
				new Func<IApiCallDetails, Stream, CatResponse<CatHelpRecord>>(DeserializeCatHelpResponse),
				(p, d) => LowLevelDispatch.CatHelpDispatch<CatResponse<CatHelpRecord>>(p)
			);

		/// <inheritdoc />
		public Task<ICatResponse<CatHelpRecord>> CatHelpAsync(Func<CatHelpDescriptor, ICatHelpRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			CatHelpAsync(selector.InvokeOrDefault(new CatHelpDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<ICatResponse<CatHelpRecord>> CatHelpAsync(ICatHelpRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<ICatHelpRequest, CatHelpRequestParameters, CatResponse<CatHelpRecord>, ICatResponse<CatHelpRecord>>(
				request,
				cancellationToken,
				new Func<IApiCallDetails, Stream, CatResponse<CatHelpRecord>>(DeserializeCatHelpResponse),
				(p, d, c) => LowLevelDispatch.CatHelpDispatchAsync<CatResponse<CatHelpRecord>>(p, c)
			);

		private CatResponse<CatHelpRecord> DeserializeCatHelpResponse(IApiCallDetails response, Stream stream)
		{
			using (stream)
			using (var ms = new MemoryStream())
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
