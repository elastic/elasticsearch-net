using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static CatResponse<CatPluginsRecord> CatPlugins(this IElasticClient client,Func<CatPluginsDescriptor, ICatPluginsRequest> selector = null);

		/// <inheritdoc />
		public static CatResponse<CatPluginsRecord> CatPlugins(this IElasticClient client,ICatPluginsRequest request);

		/// <inheritdoc />
		public static Task<CatResponse<CatPluginsRecord>> CatPluginsAsync(this IElasticClient client,Func<CatPluginsDescriptor, ICatPluginsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<CatResponse<CatPluginsRecord>> CatPluginsAsync(this IElasticClient client,ICatPluginsRequest request,
			CancellationToken ct = default
		);
	}

}
