using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		ICatResponse<CatFielddataRecord> CatFielddata(Func<CatFielddataDescriptor, ICatFielddataRequest> selector = null);

		/// <inheritdoc />
		ICatResponse<CatFielddataRecord> CatFielddata(ICatFielddataRequest request);

		/// <inheritdoc />
		Task<ICatResponse<CatFielddataRecord>> CatFielddataAsync(
			Func<CatFielddataDescriptor, ICatFielddataRequest> selector = null,
			CancellationToken ct = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<ICatResponse<CatFielddataRecord>> CatFielddataAsync(ICatFielddataRequest request,
			CancellationToken ct = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICatResponse<CatFielddataRecord> CatFielddata(Func<CatFielddataDescriptor, ICatFielddataRequest> selector = null) =>
			CatFielddata(selector.InvokeOrDefault(new CatFielddataDescriptor()));

		/// <inheritdoc />
		public ICatResponse<CatFielddataRecord> CatFielddata(ICatFielddataRequest request) =>
			DoCat<ICatFielddataRequest, CatFielddataRequestParameters, CatFielddataRecord>(request);

		/// <inheritdoc />
		public Task<ICatResponse<CatFielddataRecord>> CatFielddataAsync(
			Func<CatFielddataDescriptor, ICatFielddataRequest> selector = null,
			CancellationToken ct = default
		) => CatFielddataAsync(selector.InvokeOrDefault(new CatFielddataDescriptor()), ct);

		/// <inheritdoc />
		public Task<ICatResponse<CatFielddataRecord>> CatFielddataAsync(ICatFielddataRequest request, CancellationToken ct = default) =>
			DoCatAsync<ICatFielddataRequest, CatFielddataRequestParameters, CatFielddataRecord>(request, ct);
	}
}
