using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		ICatResponse<CatPluginsRecord> CatPlugins(Func<CatPluginsDescriptor, ICatPluginsRequest> selector = null);

		/// <inheritdoc />
		ICatResponse<CatPluginsRecord> CatPlugins(ICatPluginsRequest request);

		/// <inheritdoc />
		Task<ICatResponse<CatPluginsRecord>> CatPluginsAsync(Func<CatPluginsDescriptor, ICatPluginsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<ICatResponse<CatPluginsRecord>> CatPluginsAsync(ICatPluginsRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICatResponse<CatPluginsRecord> CatPlugins(Func<CatPluginsDescriptor, ICatPluginsRequest> selector = null) =>
			CatPlugins(selector.InvokeOrDefault(new CatPluginsDescriptor()));

		/// <inheritdoc />
		public ICatResponse<CatPluginsRecord> CatPlugins(ICatPluginsRequest request) =>
			DoCat<ICatPluginsRequest, CatPluginsRequestParameters, CatPluginsRecord>(request,
				LowLevelDispatch.CatPluginsDispatch<CatResponse<CatPluginsRecord>>);

		/// <inheritdoc />
		public Task<ICatResponse<CatPluginsRecord>> CatPluginsAsync(
			Func<CatPluginsDescriptor, ICatPluginsRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)
		) => CatPluginsAsync(selector.InvokeOrDefault(new CatPluginsDescriptor()), cancellationToken);

		public Task<ICatResponse<CatPluginsRecord>> CatPluginsAsync(ICatPluginsRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			DoCatAsync<ICatPluginsRequest, CatPluginsRequestParameters, CatPluginsRecord>(request, cancellationToken,
				LowLevelDispatch.CatPluginsDispatchAsync<CatResponse<CatPluginsRecord>>);
	}
}
