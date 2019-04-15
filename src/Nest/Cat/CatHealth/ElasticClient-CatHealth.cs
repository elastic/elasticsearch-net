using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		CatResponse<CatHealthRecord> CatHealth(Func<CatHealthDescriptor, ICatHealthRequest> selector = null);

		/// <inheritdoc />
		CatResponse<CatHealthRecord> CatHealth(ICatHealthRequest request);

		/// <inheritdoc />
		Task<CatResponse<CatHealthRecord>> CatHealthAsync(
			Func<CatHealthDescriptor, ICatHealthRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<CatResponse<CatHealthRecord>> CatHealthAsync(ICatHealthRequest request, CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public CatResponse<CatHealthRecord> CatHealth(Func<CatHealthDescriptor, ICatHealthRequest> selector = null) =>
			CatHealth(selector.InvokeOrDefault(new CatHealthDescriptor()));

		/// <inheritdoc />
		public CatResponse<CatHealthRecord> CatHealth(ICatHealthRequest request) =>
			DoCat<ICatHealthRequest, CatHealthRequestParameters, CatHealthRecord>(request);

		/// <inheritdoc />
		public Task<CatResponse<CatHealthRecord>> CatHealthAsync(
			Func<CatHealthDescriptor, ICatHealthRequest> selector = null,
			CancellationToken ct = default
		) => CatHealthAsync(selector.InvokeOrDefault(new CatHealthDescriptor()), ct);

		/// <inheritdoc />
		public Task<CatResponse<CatHealthRecord>> CatHealthAsync(ICatHealthRequest request, CancellationToken ct = default) =>
			DoCatAsync<ICatHealthRequest, CatHealthRequestParameters, CatHealthRecord>(request, ct);
	}
}
