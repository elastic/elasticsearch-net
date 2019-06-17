using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Indices.GetFieldMapping(), please update this usage.")]
		public static GetFieldMappingResponse GetFieldMapping<T>(this IElasticClient client, Fields fields,
			Func<GetFieldMappingDescriptor<T>, IGetFieldMappingRequest> selector = null
		)
			where T : class => client.Indices.GetFieldMapping(fields, selector);

		[Obsolete("Moved to client.Indices.GetFieldMapping(), please update this usage.")]
		public static GetFieldMappingResponse GetFieldMapping(this IElasticClient client, IGetFieldMappingRequest request)
			=> client.Indices.GetFieldMapping(request);

		[Obsolete("Moved to client.Indices.GetFieldMappingAsync(), please update this usage.")]
		public static Task<GetFieldMappingResponse> GetFieldMappingAsync<T>(this IElasticClient client, Fields fields,
			Func<GetFieldMappingDescriptor<T>, IGetFieldMappingRequest> selector = null,
			CancellationToken ct = default
		)
			where T : class => client.Indices.GetFieldMappingAsync(fields, selector, ct);

		[Obsolete("Moved to client.Indices.GetFieldMappingAsync(), please update this usage.")]
		public static Task<GetFieldMappingResponse> GetFieldMappingAsync(this IElasticClient client, IGetFieldMappingRequest request,
			CancellationToken ct = default
		)
			=> client.Indices.GetFieldMappingAsync(request, ct);
	}
}
