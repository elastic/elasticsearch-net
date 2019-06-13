using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Creates a new named collection of auto-follow patterns against the remote cluster specified
		/// in the request body. Newly created indices on the remote cluster matching any of the specified patterns
		/// will be automatically configured as follower indices.
		/// </summary>
		public static CreateAutoFollowPatternResponse CreateAutoFollowPattern(this IElasticClient client,Name name, Func<CreateAutoFollowPatternDescriptor, ICreateAutoFollowPatternRequest> selector);

		/// <inheritdoc cref="CreateAutoFollowPattern(Name, System.Func{Nest.CreateAutoFollowPatternDescriptor,Nest.ICreateAutoFollowPatternRequest})" />
		public static CreateAutoFollowPatternResponse CreateAutoFollowPattern(this IElasticClient client,ICreateAutoFollowPatternRequest request);

		/// <inheritdoc cref="CreateAutoFollowPattern(Name, System.Func{Nest.CreateAutoFollowPatternDescriptor,Nest.ICreateAutoFollowPatternRequest})" />
		public static Task<CreateAutoFollowPatternResponse> CreateAutoFollowPatternAsync(this IElasticClient client,Name name, Func<CreateAutoFollowPatternDescriptor, ICreateAutoFollowPatternRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="CreateAutoFollowPattern(Name, System.Func{Nest.CreateAutoFollowPatternDescriptor,Nest.ICreateAutoFollowPatternRequest})" />
		public static Task<CreateAutoFollowPatternResponse> CreateAutoFollowPatternAsync(this IElasticClient client,ICreateAutoFollowPatternRequest request, CancellationToken ct = default);
	}

}
