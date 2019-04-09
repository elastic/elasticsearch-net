using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		ICatResponse<CatHealthRecord> CatHealth(Func<CatHealthDescriptor, ICatHealthRequest> selector = null);

		/// <inheritdoc />
		ICatResponse<CatHealthRecord> CatHealth(ICatHealthRequest request);

		/// <inheritdoc />
		Task<ICatResponse<CatHealthRecord>> CatHealthAsync(
			Func<CatHealthDescriptor, ICatHealthRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<ICatResponse<CatHealthRecord>> CatHealthAsync(ICatHealthRequest request, CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICatResponse<CatHealthRecord> CatHealth(Func<CatHealthDescriptor, ICatHealthRequest> selector = null) =>
			CatHealth(selector.InvokeOrDefault(new CatHealthDescriptor()));

		/// <inheritdoc />
		public ICatResponse<CatHealthRecord> CatHealth(ICatHealthRequest request) =>
			DoCat<ICatHealthRequest, CatHealthRequestParameters, CatHealthRecord>(request);

		/// <inheritdoc />
		public Task<ICatResponse<CatHealthRecord>> CatHealthAsync(
			Func<CatHealthDescriptor, ICatHealthRequest> selector = null,
			CancellationToken ct = default
		) => CatHealthAsync(selector.InvokeOrDefault(new CatHealthDescriptor()), ct);

		/// <inheritdoc />
		public Task<ICatResponse<CatHealthRecord>> CatHealthAsync(ICatHealthRequest request, CancellationToken ct = default) =>
			DoCatAsync<ICatHealthRequest, CatHealthRequestParameters, CatHealthRecord>(request, ct);
	}
}
