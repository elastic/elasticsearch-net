using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		ICatResponse<CatTemplatesRecord> CatTemplates(Func<CatTemplatesDescriptor, ICatTemplatesRequest> selector = null);

		/// <inheritdoc/>
		ICatResponse<CatTemplatesRecord> CatTemplates(ICatTemplatesRequest request);

		/// <inheritdoc/>
		Task<ICatResponse<CatTemplatesRecord>> CatTemplatesAsync(
			Func<CatTemplatesDescriptor, ICatTemplatesRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc/>
		Task<ICatResponse<CatTemplatesRecord>> CatTemplatesAsync(ICatTemplatesRequest request, CancellationToken cancellationToken = default(CancellationToken));

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ICatResponse<CatTemplatesRecord> CatTemplates(Func<CatTemplatesDescriptor, ICatTemplatesRequest> selector = null) =>
			this.CatTemplates(selector.InvokeOrDefault(new CatTemplatesDescriptor()));

		/// <inheritdoc/>
		public ICatResponse<CatTemplatesRecord> CatTemplates(ICatTemplatesRequest request) =>
			this.DoCat<ICatTemplatesRequest, CatTemplatesRequestParameters, CatTemplatesRecord>(request, this.LowLevelDispatch.CatTemplatesDispatch<CatResponse<CatTemplatesRecord>>);

		/// <inheritdoc/>
		public Task<ICatResponse<CatTemplatesRecord>> CatTemplatesAsync(
			Func<CatTemplatesDescriptor, ICatTemplatesRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) => this.CatTemplatesAsync(selector.InvokeOrDefault(new CatTemplatesDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<ICatResponse<CatTemplatesRecord>> CatTemplatesAsync(ICatTemplatesRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.DoCatAsync<ICatTemplatesRequest, CatTemplatesRequestParameters, CatTemplatesRecord>(request, cancellationToken, this.LowLevelDispatch.CatTemplatesDispatchAsync<CatResponse<CatTemplatesRecord>>);

	}
}
