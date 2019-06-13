using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Deletes a registered scroll request on the cluster
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-request-scroll.html
		/// </summary>
		/// <param name="selector">Specify the scroll id as well as request specific configuration</param>
		public static ClearScrollResponse ClearScroll(this IElasticClient client,Func<ClearScrollDescriptor, IClearScrollRequest> selector);

		/// <inheritdoc />
		public static ClearScrollResponse ClearScroll(this IElasticClient client,IClearScrollRequest request);

		/// <inheritdoc />
		public static Task<ClearScrollResponse> ClearScrollAsync(this IElasticClient client,Func<ClearScrollDescriptor, IClearScrollRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<ClearScrollResponse> ClearScrollAsync(this IElasticClient client,IClearScrollRequest request, CancellationToken ct = default);
	}

}
