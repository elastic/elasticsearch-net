using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		ICatResponse<CatPluginsRecord> CatPlugins(Func<CatPluginsDescriptor, ICatPluginsRequest> selector = null);

		/// <inheritdoc/>
		ICatResponse<CatPluginsRecord> CatPlugins(ICatPluginsRequest request);

		/// <inheritdoc/>
		Task<ICatResponse<CatPluginsRecord>> CatPluginsAsync(Func<CatPluginsDescriptor, ICatPluginsRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<ICatResponse<CatPluginsRecord>> CatPluginsAsync(ICatPluginsRequest request, CancellationToken cancellationToken = default(CancellationToken));

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ICatResponse<CatPluginsRecord> CatPlugins(Func<CatPluginsDescriptor, ICatPluginsRequest> selector = null) =>
			this.CatPlugins(selector.InvokeOrDefault(new CatPluginsDescriptor()));

		/// <inheritdoc/>
		public ICatResponse<CatPluginsRecord> CatPlugins(ICatPluginsRequest request) =>
			this.DoCat<ICatPluginsRequest, CatPluginsRequestParameters, CatPluginsRecord>(request, this.LowLevelDispatch.CatPluginsDispatch<CatResponse<CatPluginsRecord>>);

		/// <inheritdoc/>
		public Task<ICatResponse<CatPluginsRecord>> CatPluginsAsync(
			Func<CatPluginsDescriptor, ICatPluginsRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)
		) => this.CatPluginsAsync(selector.InvokeOrDefault(new CatPluginsDescriptor()), cancellationToken);

		public Task<ICatResponse<CatPluginsRecord>> CatPluginsAsync(ICatPluginsRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.DoCatAsync<ICatPluginsRequest, CatPluginsRequestParameters, CatPluginsRecord>(request, cancellationToken, this.LowLevelDispatch.CatPluginsDispatchAsync<CatResponse<CatPluginsRecord>>);

	}
}
