using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Elasticsearch.Net.Api.Cat;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		CatResponse<CatNodeAttributesRecord> CatNodeAttributes(Func<CatNodeAttributesDescriptor, ICatNodeAttributesRequest> selector = null);

		/// <inheritdoc />
		CatResponse<CatNodeAttributesRecord> CatNodeAttributes(ICatNodeAttributesRequest request);

		/// <inheritdoc />
		Task<CatResponse<CatNodeAttributesRecord>> CatNodeAttributesAsync(
			Func<CatNodeAttributesDescriptor, ICatNodeAttributesRequest> selector = null,
			CancellationToken ct = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<CatResponse<CatNodeAttributesRecord>> CatNodeAttributesAsync(ICatNodeAttributesRequest request,
			CancellationToken ct = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public CatResponse<CatNodeAttributesRecord>
			CatNodeAttributes(Func<CatNodeAttributesDescriptor, ICatNodeAttributesRequest> selector = null) =>
			CatNodeAttributes(selector.InvokeOrDefault(new CatNodeAttributesDescriptor()));

		/// <inheritdoc />
		public CatResponse<CatNodeAttributesRecord> CatNodeAttributes(ICatNodeAttributesRequest request) =>
			DoCat<ICatNodeAttributesRequest, CatNodeAttributesRequestParameters, CatNodeAttributesRecord>(request);

		/// <inheritdoc />
		public Task<CatResponse<CatNodeAttributesRecord>> CatNodeAttributesAsync(
			Func<CatNodeAttributesDescriptor, ICatNodeAttributesRequest> selector = null,
			CancellationToken ct = default
		) => CatNodeAttributesAsync(selector.InvokeOrDefault(new CatNodeAttributesDescriptor()), ct);

		/// <inheritdoc />
		public Task<CatResponse<CatNodeAttributesRecord>>CatNodeAttributesAsync(
			ICatNodeAttributesRequest request,
			CancellationToken ct = default
		) => DoCatAsync<ICatNodeAttributesRequest, CatNodeAttributesRequestParameters, CatNodeAttributesRecord>(request, ct);
	}
}
