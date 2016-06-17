using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		ICatResponse<CatNodeAttributesRecord> CatNodeAttributes(Func<CatNodeAttributesDescriptor, ICatNodeAttributesRequest> selector = null);

		/// <inheritdoc/>
		ICatResponse<CatNodeAttributesRecord> CatNodeAttributes(ICatNodeAttributesRequest request);

		/// <inheritdoc/>
		Task<ICatResponse<CatNodeAttributesRecord>> CatNodeAttributesAsync(
			Func<CatNodeAttributesDescriptor, ICatNodeAttributesRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc/>
		Task<ICatResponse<CatNodeAttributesRecord>> CatNodeAttributesAsync(ICatNodeAttributesRequest request, CancellationToken cancellationToken = default(CancellationToken));

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ICatResponse<CatNodeAttributesRecord> CatNodeAttributes(Func<CatNodeAttributesDescriptor, ICatNodeAttributesRequest> selector = null) =>
			this.CatNodeAttributes(selector.InvokeOrDefault(new CatNodeAttributesDescriptor()));

		/// <inheritdoc/>
		public ICatResponse<CatNodeAttributesRecord> CatNodeAttributes(ICatNodeAttributesRequest request) =>
			this.DoCat<ICatNodeAttributesRequest, CatNodeAttributesRequestParameters, CatNodeAttributesRecord>(request, this.LowLevelDispatch.CatNodeattrsDispatch<CatResponse<CatNodeAttributesRecord>>);

		/// <inheritdoc/>
		public Task<ICatResponse<CatNodeAttributesRecord>> CatNodeAttributesAsync(
			Func<CatNodeAttributesDescriptor, ICatNodeAttributesRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) => this.CatNodeAttributesAsync(selector.InvokeOrDefault(new CatNodeAttributesDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<ICatResponse<CatNodeAttributesRecord>> CatNodeAttributesAsync(ICatNodeAttributesRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.DoCatAsync<ICatNodeAttributesRequest, CatNodeAttributesRequestParameters, CatNodeAttributesRecord>(request, cancellationToken, this.LowLevelDispatch.CatNodeattrsDispatchAsync<CatResponse<CatNodeAttributesRecord>>);

	}
}
