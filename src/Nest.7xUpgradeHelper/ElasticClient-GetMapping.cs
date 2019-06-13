using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// The get mapping API allows to retrieve mapping definitions for an index or index/type.
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-get-mapping.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the get mapping operation</param>
		public static GetMappingResponse GetMapping<T>(this IElasticClient client,Func<GetMappingDescriptor<T>, IGetMappingRequest> selector = null) where T : class;

		/// <inheritdoc />
		public static GetMappingResponse GetMapping(this IElasticClient client,IGetMappingRequest request);

		/// <inheritdoc />
		public static Task<GetMappingResponse> GetMappingAsync<T>(this IElasticClient client,Func<GetMappingDescriptor<T>, IGetMappingRequest> selector = null,
			CancellationToken ct = default
		)
			where T : class;

		/// <inheritdoc />
		public static Task<GetMappingResponse> GetMappingAsync(this IElasticClient client,IGetMappingRequest request, CancellationToken ct = default);
	}

}
