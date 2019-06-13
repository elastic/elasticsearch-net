using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static ClearCachedRealmsResponse ClearCachedRealms(this IElasticClient client,Names realms, Func<ClearCachedRealmsDescriptor, IClearCachedRealmsRequest> selector = null);

		/// <inheritdoc />
		public static ClearCachedRealmsResponse ClearCachedRealms(this IElasticClient client,IClearCachedRealmsRequest request);

		/// <inheritdoc />
		public static Task<ClearCachedRealmsResponse> ClearCachedRealmsAsync(this IElasticClient client,Names realms,
			Func<ClearCachedRealmsDescriptor, IClearCachedRealmsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<ClearCachedRealmsResponse> ClearCachedRealmsAsync(this IElasticClient client,IClearCachedRealmsRequest request,
			CancellationToken ct = default
		);
	}

}
