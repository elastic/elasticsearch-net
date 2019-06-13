using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// The put mapping API allows to register specific mapping definition for a specific type.
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-put-mapping.html
		/// </summary>
		/// <typeparam name="T">The type we want to map in elasticsearch</typeparam>
		/// <param name="selector">A descriptor to describe the mapping of our type</param>
		public static PutMappingResponse Map<T>(this IElasticClient client,Func<PutMappingDescriptor<T>, IPutMappingRequest> selector)
			where T : class;

		/// <inheritdoc />
		public static PutMappingResponse Map(this IElasticClient client,IPutMappingRequest request);

		/// <inheritdoc />
		public static Task<PutMappingResponse> MapAsync<T>(this IElasticClient client,Func<PutMappingDescriptor<T>, IPutMappingRequest> selector,
			CancellationToken ct = default
		)
			where T : class;

		/// <inheritdoc />
		public static Task<PutMappingResponse> MapAsync(this IElasticClient client,IPutMappingRequest request, CancellationToken ct = default);
	}

}
