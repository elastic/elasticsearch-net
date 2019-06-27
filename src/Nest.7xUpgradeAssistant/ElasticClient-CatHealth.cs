using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Cat.Health(), please update this usage.")]
		public static CatResponse<CatHealthRecord> CatHealth(this IElasticClient client, Func<CatHealthDescriptor, ICatHealthRequest> selector = null)
			=> client.Cat.Health(selector);

		[Obsolete("Moved to client.Cat.Health(), please update this usage.")]
		public static CatResponse<CatHealthRecord> CatHealth(this IElasticClient client, ICatHealthRequest request)
			=> client.Cat.Health(request);

		[Obsolete("Moved to client.Cat.HealthAsync(), please update this usage.")]
		public static Task<CatResponse<CatHealthRecord>> CatHealthAsync(this IElasticClient client,
			Func<CatHealthDescriptor, ICatHealthRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cat.HealthAsync(selector, ct);

		[Obsolete("Moved to client.Cat.HealthAsync(), please update this usage.")]
		public static Task<CatResponse<CatHealthRecord>> CatHealthAsync(this IElasticClient client, ICatHealthRequest request,
			CancellationToken ct = default
		)
			=> client.Cat.HealthAsync(request, ct);
	}
}
