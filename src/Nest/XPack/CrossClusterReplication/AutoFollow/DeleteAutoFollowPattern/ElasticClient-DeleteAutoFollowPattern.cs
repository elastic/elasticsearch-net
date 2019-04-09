using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>Deletes a configured collection of auto-follow patterns.</summary>
		IDeleteAutoFollowPatternResponse DeleteAutoFollowPattern(Name name, Func<DeleteAutoFollowPatternDescriptor, IDeleteAutoFollowPatternRequest> selector = null);

		/// <inheritdoc cref="DeleteAutoFollowPattern(Name,System.Func{Nest.DeleteAutoFollowPatternDescriptor,Nest.IDeleteAutoFollowPatternRequest})" />
		IDeleteAutoFollowPatternResponse DeleteAutoFollowPattern(IDeleteAutoFollowPatternRequest request);

		/// <inheritdoc cref="DeleteAutoFollowPattern(Name,System.Func{Nest.DeleteAutoFollowPatternDescriptor,Nest.IDeleteAutoFollowPatternRequest})" />
		Task<IDeleteAutoFollowPatternResponse> DeleteAutoFollowPatternAsync(Name name, Func<DeleteAutoFollowPatternDescriptor, IDeleteAutoFollowPatternRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="DeleteAutoFollowPattern(Name,System.Func{Nest.DeleteAutoFollowPatternDescriptor,Nest.IDeleteAutoFollowPatternRequest})" />
		Task<IDeleteAutoFollowPatternResponse> DeleteAutoFollowPatternAsync(IDeleteAutoFollowPatternRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="DeleteAutoFollowPattern(Name,System.Func{Nest.DeleteAutoFollowPatternDescriptor,Nest.IDeleteAutoFollowPatternRequest})" />
		public IDeleteAutoFollowPatternResponse DeleteAutoFollowPattern(Name name, Func<DeleteAutoFollowPatternDescriptor, IDeleteAutoFollowPatternRequest> selector = null) =>
			DeleteAutoFollowPattern(selector.InvokeOrDefault(new DeleteAutoFollowPatternDescriptor(name)));

		/// <inheritdoc cref="DeleteAutoFollowPattern(Name,System.Func{Nest.DeleteAutoFollowPatternDescriptor,Nest.IDeleteAutoFollowPatternRequest})" />
		public IDeleteAutoFollowPatternResponse DeleteAutoFollowPattern(IDeleteAutoFollowPatternRequest request) =>
			Dispatch2<IDeleteAutoFollowPatternRequest, DeleteAutoFollowPatternResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="DeleteAutoFollowPattern(Name,System.Func{Nest.DeleteAutoFollowPatternDescriptor,Nest.IDeleteAutoFollowPatternRequest})" />
		public Task<IDeleteAutoFollowPatternResponse> DeleteAutoFollowPatternAsync(
			Name name,
			Func<DeleteAutoFollowPatternDescriptor, IDeleteAutoFollowPatternRequest> selector = null,
			CancellationToken ct = default
		) => DeleteAutoFollowPatternAsync(selector.InvokeOrDefault(new DeleteAutoFollowPatternDescriptor(name)), ct);

		/// <inheritdoc cref="DeleteAutoFollowPattern(Name,System.Func{Nest.DeleteAutoFollowPatternDescriptor,Nest.IDeleteAutoFollowPatternRequest})" />
		public Task<IDeleteAutoFollowPatternResponse> DeleteAutoFollowPatternAsync(IDeleteAutoFollowPatternRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IDeleteAutoFollowPatternRequest, IDeleteAutoFollowPatternResponse, DeleteAutoFollowPatternResponse>(request, request.RequestParameters, ct);
	}
}
