using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// The get index alias api allows to filter by alias name and index name. This api redirects to the master and fetches
		/// the requested index aliases, if available. This api only serialises the found index aliases.
		/// <para> Difference with GetAlias is that this call will also return indices without aliases set</para>
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-aliases.html#alias-retrieving
		/// </summary>
		/// <param name="selector">A descriptor that describes which aliases/indexes we are interested int</param>
		public static GetAliasResponse GetAlias(this IElasticClient client,Func<GetAliasDescriptor, IGetAliasRequest> selector = null);

		/// <inheritdoc />
		public static GetAliasResponse GetAlias(this IElasticClient client,IGetAliasRequest request);

		/// <inheritdoc />
		public static Task<GetAliasResponse> GetAliasAsync(this IElasticClient client,
			Func<GetAliasDescriptor, IGetAliasRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<GetAliasResponse> GetAliasAsync(this IElasticClient client,IGetAliasRequest request, CancellationToken ct = default);
	}

}
