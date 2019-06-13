using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// The update by query API allows to update documents from one or more indices and one or more types based on a query.
		/// </summary>
		/// <typeparam name="T">
		/// The type used to infer the default index and typename as well as describe the strongly
		///  typed parts of the query
		/// </typeparam>
		/// <param name="selector">An optional descriptor to further describe the update by query operation</param>
		public static UpdateByQueryResponse UpdateByQuery<T>(this IElasticClient client,Func<UpdateByQueryDescriptor<T>, IUpdateByQueryRequest> selector)
			where T : class;

		/// <inheritdoc />
		public static UpdateByQueryResponse UpdateByQuery(this IElasticClient client,IUpdateByQueryRequest request);

		/// <inheritdoc />
		public static Task<UpdateByQueryResponse> UpdateByQueryAsync<T>(this IElasticClient client,Func<UpdateByQueryDescriptor<T>, IUpdateByQueryRequest> selector,
			CancellationToken ct = default
		)
			where T : class;

		/// <inheritdoc />
		public static Task<UpdateByQueryResponse> UpdateByQueryAsync(this IElasticClient client,IUpdateByQueryRequest request,
			CancellationToken ct = default
		);
	}

}
