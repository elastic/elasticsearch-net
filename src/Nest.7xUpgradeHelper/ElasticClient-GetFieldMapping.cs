using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static GetFieldMappingResponse GetFieldMapping<T>(this IElasticClient client,Fields fields, Func<GetFieldMappingDescriptor<T>, IGetFieldMappingRequest> selector = null)
			where T : class;

		/// <inheritdoc />
		public static GetFieldMappingResponse GetFieldMapping(this IElasticClient client,IGetFieldMappingRequest request);

		/// <inheritdoc />
		public static Task<GetFieldMappingResponse> GetFieldMappingAsync<T>(this IElasticClient client,Fields fields,
			Func<GetFieldMappingDescriptor<T>, IGetFieldMappingRequest> selector = null,
			CancellationToken ct = default
		)
			where T : class;

		/// <inheritdoc />
		public static Task<GetFieldMappingResponse> GetFieldMappingAsync(this IElasticClient client,IGetFieldMappingRequest request,
			CancellationToken ct = default
		);
	}

}
