using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CatResponse<CatPluginsRecord> CatPlugins(this IElasticClient client,
			Func<CatPluginsDescriptor, ICatPluginsRequest> selector = null
		)
			=> client.Cat.Plugins(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CatResponse<CatPluginsRecord> CatPlugins(this IElasticClient client, ICatPluginsRequest request)
			=> client.Cat.Plugins(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CatResponse<CatPluginsRecord>> CatPluginsAsync(this IElasticClient client,
			Func<CatPluginsDescriptor, ICatPluginsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cat.PluginsAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CatResponse<CatPluginsRecord>> CatPluginsAsync(this IElasticClient client, ICatPluginsRequest request,
			CancellationToken ct = default
		)
			=> client.Cat.PluginsAsync(request, ct);
	}
}
