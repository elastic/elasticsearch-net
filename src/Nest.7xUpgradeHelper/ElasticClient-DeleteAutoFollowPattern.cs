using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>Deletes a configured collection of auto-follow patterns.</summary>
		public static DeleteAutoFollowPatternResponse DeleteAutoFollowPattern(this IElasticClient client,Name name, Func<DeleteAutoFollowPatternDescriptor, IDeleteAutoFollowPatternRequest> selector = null);

		/// <inheritdoc cref="DeleteAutoFollowPattern(Name,System.Func{Nest.DeleteAutoFollowPatternDescriptor,Nest.IDeleteAutoFollowPatternRequest})" />
		public static DeleteAutoFollowPatternResponse DeleteAutoFollowPattern(this IElasticClient client,IDeleteAutoFollowPatternRequest request);

		/// <inheritdoc cref="DeleteAutoFollowPattern(Name,System.Func{Nest.DeleteAutoFollowPatternDescriptor,Nest.IDeleteAutoFollowPatternRequest})" />
		public static Task<DeleteAutoFollowPatternResponse> DeleteAutoFollowPatternAsync(this IElasticClient client,Name name, Func<DeleteAutoFollowPatternDescriptor, IDeleteAutoFollowPatternRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="DeleteAutoFollowPattern(Name,System.Func{Nest.DeleteAutoFollowPatternDescriptor,Nest.IDeleteAutoFollowPatternRequest})" />
		public static Task<DeleteAutoFollowPatternResponse> DeleteAutoFollowPatternAsync(this IElasticClient client,IDeleteAutoFollowPatternRequest request, CancellationToken ct = default);
	}

}
