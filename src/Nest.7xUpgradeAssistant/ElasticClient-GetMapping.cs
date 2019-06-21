using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Indices.GetMapping(), please update this usage.")]
		public static GetMappingResponse GetMapping<T>(this IElasticClient client, Func<GetMappingDescriptor<T>, IGetMappingRequest> selector = null)
			where T : class => client.Indices.GetMapping<T>(selector);

		[Obsolete("Moved to client.Indices.GetMapping(), please update this usage.")]
		public static GetMappingResponse GetMapping(this IElasticClient client, IGetMappingRequest request)
			=> client.Indices.GetMapping(request);

		[Obsolete("Moved to client.Indices.GetMappingAsync(), please update this usage.")]
		public static Task<GetMappingResponse> GetMappingAsync<T>(this IElasticClient client,
			Func<GetMappingDescriptor<T>, IGetMappingRequest> selector = null,
			CancellationToken ct = default
		)
			where T : class => client.Indices.GetMappingAsync<T>(selector, ct);

		[Obsolete("Moved to client.Indices.GetMappingAsync(), please update this usage.")]
		public static Task<GetMappingResponse> GetMappingAsync(this IElasticClient client, IGetMappingRequest request, CancellationToken ct = default)
			=> client.Indices.GetMappingAsync(request, ct);
	}
}
