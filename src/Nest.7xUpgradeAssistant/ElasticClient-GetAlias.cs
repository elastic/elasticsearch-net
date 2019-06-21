using System;
using System.Threading;
using System.Threading.Tasks;
using static Nest.Infer;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Indices.GetAlias(), please update this usage.")]
		public static GetAliasResponse GetAlias(this IElasticClient client, Func<GetAliasDescriptor, IGetAliasRequest> selector = null)
			=> client.Indices.GetAlias(AllIndices, selector);

		[Obsolete("Moved to client.Indices.GetAlias(), please update this usage.")]
		public static GetAliasResponse GetAlias(this IElasticClient client, IGetAliasRequest request)
			=> client.Indices.GetAlias(request);

		[Obsolete("Moved to client.Indices.GetAliasAsync(), please update this usage.")]
		public static Task<GetAliasResponse> GetAliasAsync(this IElasticClient client,
			Func<GetAliasDescriptor, IGetAliasRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.GetAliasAsync(AllIndices, selector, ct);

		[Obsolete("Moved to client.Indices.GetAliasAsync(), please update this usage.")]
		public static Task<GetAliasResponse> GetAliasAsync(this IElasticClient client, IGetAliasRequest request, CancellationToken ct = default)
			=> client.Indices.GetAliasAsync(request, ct);
	}
}
