using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		ICatResponse<CatHelpRecord> CatHelp(Func<CatHelpDescriptor, ICatHelpRequest> selector = null);

		/// <inheritdoc/>
		ICatResponse<CatHelpRecord> CatHelp(ICatHelpRequest request);

		/// <inheritdoc/>
		Task<ICatResponse<CatHelpRecord>> CatHelpAsync(Func<CatHelpDescriptor, ICatHelpRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<ICatResponse<CatHelpRecord>> CatHelpAsync(ICatHelpRequest request, CancellationToken cancellationToken = default(CancellationToken));

	}

	public partial class ElasticClient
	{

		private CatResponse<CatHelpRecord> DeserializeCatHelpResponse(IApiCallDetails response, Stream stream)
		{
			using (stream)
			using (var ms = new MemoryStream())
			{
				stream.CopyTo(ms);
				var body = ms.ToArray().Utf8String();
				return new CatResponse<CatHelpRecord>
				{
					Records = body.Split('\n').Skip(1)
						.Select(f => new CatHelpRecord { Endpoint = f.Trim() })
						.ToList()
				};
			}
		}

		/// <inheritdoc/>
		public ICatResponse<CatHelpRecord> CatHelp(Func<CatHelpDescriptor, ICatHelpRequest> selector = null) =>
			this.CatHelp(selector.InvokeOrDefault(new CatHelpDescriptor()));

		/// <inheritdoc/>
		public ICatResponse<CatHelpRecord> CatHelp(ICatHelpRequest request) =>
			this.Dispatcher.Dispatch<ICatHelpRequest, CatHelpRequestParameters, CatResponse<CatHelpRecord>>(
				request,
				new Func<IApiCallDetails, Stream, CatResponse<CatHelpRecord>>(this.DeserializeCatHelpResponse),
				(p, d) => this.LowLevelDispatch.CatHelpDispatch<CatResponse<CatHelpRecord>>(p)
			);

		/// <inheritdoc/>
		public Task<ICatResponse<CatHelpRecord>> CatHelpAsync(Func<CatHelpDescriptor, ICatHelpRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.CatHelpAsync(selector.InvokeOrDefault(new CatHelpDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<ICatResponse<CatHelpRecord>> CatHelpAsync(ICatHelpRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<ICatHelpRequest, CatHelpRequestParameters, CatResponse<CatHelpRecord>, ICatResponse<CatHelpRecord>>(
				request,
				cancellationToken,
				new Func<IApiCallDetails, Stream, CatResponse<CatHelpRecord>>(this.DeserializeCatHelpResponse),
				(p, d, c) => this.LowLevelDispatch.CatHelpDispatchAsync<CatResponse<CatHelpRecord>>(p, c)
			);

	}
}
