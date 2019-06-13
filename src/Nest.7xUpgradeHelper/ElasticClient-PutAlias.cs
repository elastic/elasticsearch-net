using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Add a single index alias
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-aliases.html#alias-adding
		/// </summary>
		/// <param name="request">A descriptor that describes the put alias request</param>
		public static PutAliasResponse PutAlias(this IElasticClient client,IPutAliasRequest request);

		/// <inheritdoc />
		public static Task<PutAliasResponse> PutAliasAsync(this IElasticClient client,IPutAliasRequest request, CancellationToken ct = default);

		/// <inheritdoc />
		public static PutAliasResponse PutAlias(this IElasticClient client,Indices indices, Name alias, Func<PutAliasDescriptor, IPutAliasRequest> selector = null);

		/// <inheritdoc />
		public static Task<PutAliasResponse> PutAliasAsync(this IElasticClient client,
			Indices indices,
			Name alias,
			Func<PutAliasDescriptor, IPutAliasRequest> selector = null,
			CancellationToken ct = default
		);
	}

}
