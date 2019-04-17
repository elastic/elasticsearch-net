using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		CatResponse<CatFielddataRecord> CatFielddata(Func<CatFielddataDescriptor, ICatFielddataRequest> selector = null);

		/// <inheritdoc />
		CatResponse<CatFielddataRecord> CatFielddata(ICatFielddataRequest request);

		/// <inheritdoc />
		Task<CatResponse<CatFielddataRecord>> CatFielddataAsync(
			Func<CatFielddataDescriptor, ICatFielddataRequest> selector = null,
			CancellationToken ct = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<CatResponse<CatFielddataRecord>> CatFielddataAsync(ICatFielddataRequest request,
			CancellationToken ct = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public CatResponse<CatFielddataRecord> CatFielddata(Func<CatFielddataDescriptor, ICatFielddataRequest> selector = null) =>
			CatFielddata(selector.InvokeOrDefault(new CatFielddataDescriptor()));

		/// <inheritdoc />
		public CatResponse<CatFielddataRecord> CatFielddata(ICatFielddataRequest request) =>
			DoCat<ICatFielddataRequest, CatFielddataRequestParameters, CatFielddataRecord>(request);

		/// <inheritdoc />
		public Task<CatResponse<CatFielddataRecord>> CatFielddataAsync(
			Func<CatFielddataDescriptor, ICatFielddataRequest> selector = null,
			CancellationToken ct = default
		) => CatFielddataAsync(selector.InvokeOrDefault(new CatFielddataDescriptor()), ct);

		/// <inheritdoc />
		public Task<CatResponse<CatFielddataRecord>> CatFielddataAsync(ICatFielddataRequest request, CancellationToken ct = default) =>
			DoCatAsync<ICatFielddataRequest, CatFielddataRequestParameters, CatFielddataRecord>(request, ct);
	}
}
