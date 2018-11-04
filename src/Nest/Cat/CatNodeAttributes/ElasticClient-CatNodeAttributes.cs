using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		ICatResponse<CatNodeAttributesRecord> CatNodeAttributes(Func<CatNodeAttributesDescriptor, ICatNodeAttributesRequest> selector = null);

		/// <inheritdoc />
		ICatResponse<CatNodeAttributesRecord> CatNodeAttributes(ICatNodeAttributesRequest request);

		/// <inheritdoc />
		Task<ICatResponse<CatNodeAttributesRecord>> CatNodeAttributesAsync(
			Func<CatNodeAttributesDescriptor, ICatNodeAttributesRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<ICatResponse<CatNodeAttributesRecord>> CatNodeAttributesAsync(ICatNodeAttributesRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICatResponse<CatNodeAttributesRecord>
			CatNodeAttributes(Func<CatNodeAttributesDescriptor, ICatNodeAttributesRequest> selector = null) =>
			CatNodeAttributes(selector.InvokeOrDefault(new CatNodeAttributesDescriptor()));

		/// <inheritdoc />
		public ICatResponse<CatNodeAttributesRecord> CatNodeAttributes(ICatNodeAttributesRequest request) =>
			DoCat<ICatNodeAttributesRequest, CatNodeAttributesRequestParameters, CatNodeAttributesRecord>(request,
				LowLevelDispatch.CatNodeattrsDispatch<CatResponse<CatNodeAttributesRecord>>);

		/// <inheritdoc />
		public Task<ICatResponse<CatNodeAttributesRecord>> CatNodeAttributesAsync(
			Func<CatNodeAttributesDescriptor, ICatNodeAttributesRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) => CatNodeAttributesAsync(selector.InvokeOrDefault(new CatNodeAttributesDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<ICatResponse<CatNodeAttributesRecord>> CatNodeAttributesAsync(ICatNodeAttributesRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			DoCatAsync<ICatNodeAttributesRequest, CatNodeAttributesRequestParameters, CatNodeAttributesRecord>(request, cancellationToken,
				LowLevelDispatch.CatNodeattrsDispatchAsync<CatResponse<CatNodeAttributesRecord>>);
	}
}
