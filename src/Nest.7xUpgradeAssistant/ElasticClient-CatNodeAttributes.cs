using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Cat.NodeAttributes(), please update this usage.")]
		public static CatResponse<CatNodeAttributesRecord> CatNodeAttributes(this IElasticClient client,
			Func<CatNodeAttributesDescriptor, ICatNodeAttributesRequest> selector = null
		)
			=> client.Cat.NodeAttributes(selector);

		[Obsolete("Moved to client.Cat.NodeAttributes(), please update this usage.")]
		public static CatResponse<CatNodeAttributesRecord> CatNodeAttributes(this IElasticClient client, ICatNodeAttributesRequest request)
			=> client.Cat.NodeAttributes(request);

		[Obsolete("Moved to client.Cat.NodeAttributesAsync(), please update this usage.")]
		public static Task<CatResponse<CatNodeAttributesRecord>> CatNodeAttributesAsync(this IElasticClient client,
			Func<CatNodeAttributesDescriptor, ICatNodeAttributesRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cat.NodeAttributesAsync(selector, ct);

		[Obsolete("Moved to client.Cat.NodeAttributesAsync(), please update this usage.")]
		public static Task<CatResponse<CatNodeAttributesRecord>> CatNodeAttributesAsync(this IElasticClient client, ICatNodeAttributesRequest request,
			CancellationToken ct = default
		)
			=> client.Cat.NodeAttributesAsync(request, ct);
	}
}
