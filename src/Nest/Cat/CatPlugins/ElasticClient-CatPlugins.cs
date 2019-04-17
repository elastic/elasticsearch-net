using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		CatResponse<CatPluginsRecord> CatPlugins(Func<CatPluginsDescriptor, ICatPluginsRequest> selector = null);

		/// <inheritdoc />
		CatResponse<CatPluginsRecord> CatPlugins(ICatPluginsRequest request);

		/// <inheritdoc />
		Task<CatResponse<CatPluginsRecord>> CatPluginsAsync(Func<CatPluginsDescriptor, ICatPluginsRequest> selector = null,
			CancellationToken ct = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<CatResponse<CatPluginsRecord>> CatPluginsAsync(ICatPluginsRequest request,
			CancellationToken ct = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public CatResponse<CatPluginsRecord> CatPlugins(Func<CatPluginsDescriptor, ICatPluginsRequest> selector = null) =>
			CatPlugins(selector.InvokeOrDefault(new CatPluginsDescriptor()));

		/// <inheritdoc />
		public CatResponse<CatPluginsRecord> CatPlugins(ICatPluginsRequest request) =>
			DoCat<ICatPluginsRequest, CatPluginsRequestParameters, CatPluginsRecord>(request);

		/// <inheritdoc />
		public Task<CatResponse<CatPluginsRecord>> CatPluginsAsync(
			Func<CatPluginsDescriptor, ICatPluginsRequest> selector = null,
			CancellationToken ct = default
		) => CatPluginsAsync(selector.InvokeOrDefault(new CatPluginsDescriptor()), ct);

		public Task<CatResponse<CatPluginsRecord>> CatPluginsAsync(ICatPluginsRequest request,
			CancellationToken ct = default
		) =>
			DoCatAsync<ICatPluginsRequest, CatPluginsRequestParameters, CatPluginsRecord>(request, ct);
	}
}
