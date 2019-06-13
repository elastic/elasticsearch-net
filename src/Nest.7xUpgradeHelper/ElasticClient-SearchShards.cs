using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static SearchShardsResponse SearchShards<T>(this IElasticClient client,Func<SearchShardsDescriptor<T>, ISearchShardsRequest> selector) where T : class;

		/// <inheritdoc />
		public static SearchShardsResponse SearchShards(this IElasticClient client,ISearchShardsRequest request);

		/// <inheritdoc />
		public static Task<SearchShardsResponse> SearchShardsAsync<T>(this IElasticClient client,Func<SearchShardsDescriptor<T>, ISearchShardsRequest> selector,
			CancellationToken ct = default
		)
			where T : class;

		/// <inheritdoc />
		public static Task<SearchShardsResponse> SearchShardsAsync(this IElasticClient client,ISearchShardsRequest request, CancellationToken ct = default);
	}

}
