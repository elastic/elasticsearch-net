using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary> Gets configured auto-follow patterns. Returns the specified auto-follow pattern collection. </summary>
		public static GetAutoFollowPatternResponse GetAutoFollowPattern(this IElasticClient client,Func<GetAutoFollowPatternDescriptor, IGetAutoFollowPatternRequest> selector = null);

		/// <inheritdoc cref="GetAutoFollowPattern(System.Func{Nest.GetAutoFollowPatternDescriptor,Nest.IGetAutoFollowPatternRequest})" />
		public static GetAutoFollowPatternResponse GetAutoFollowPattern(this IElasticClient client,IGetAutoFollowPatternRequest request);

		/// <inheritdoc cref="GetAutoFollowPattern(System.Func{Nest.GetAutoFollowPatternDescriptor,Nest.IGetAutoFollowPatternRequest})" />
		public static Task<GetAutoFollowPatternResponse> GetAutoFollowPatternAsync(this IElasticClient client,Func<GetAutoFollowPatternDescriptor, IGetAutoFollowPatternRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="GetAutoFollowPattern(System.Func{Nest.GetAutoFollowPatternDescriptor,Nest.IGetAutoFollowPatternRequest})" />
		public static Task<GetAutoFollowPatternResponse> GetAutoFollowPatternAsync(this IElasticClient client,IGetAutoFollowPatternRequest request, CancellationToken ct = default);
	}

}
