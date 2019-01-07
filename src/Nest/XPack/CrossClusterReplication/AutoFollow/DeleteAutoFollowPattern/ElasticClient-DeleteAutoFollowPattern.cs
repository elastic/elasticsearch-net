using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>This API deletes a configured collection of auto-follow patterns.</summary>
		IDeleteAutoFollowPatternResponse DeleteAutoFollowPattern(Name name, Func<DeleteAutoFollowPatternDescriptor, IDeleteAutoFollowPatternRequest> selector = null);

		/// <inheritdoc cref="DeleteAutoFollowPattern(Name,System.Func{Nest.DeleteAutoFollowPatternDescriptor,Nest.IDeleteAutoFollowPatternRequest})" />
		IDeleteAutoFollowPatternResponse DeleteAutoFollowPattern(IDeleteAutoFollowPatternRequest request);

		/// <inheritdoc cref="DeleteAutoFollowPattern(Name,System.Func{Nest.DeleteAutoFollowPatternDescriptor,Nest.IDeleteAutoFollowPatternRequest})" />
		Task<IDeleteAutoFollowPatternResponse> DeleteAutoFollowPatternAsync(Name name, Func<DeleteAutoFollowPatternDescriptor, IDeleteAutoFollowPatternRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc cref="DeleteAutoFollowPattern(Name,System.Func{Nest.DeleteAutoFollowPatternDescriptor,Nest.IDeleteAutoFollowPatternRequest})" />
		Task<IDeleteAutoFollowPatternResponse> DeleteAutoFollowPatternAsync(IDeleteAutoFollowPatternRequest request, CancellationToken cancellationToken = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="DeleteAutoFollowPattern(Name,System.Func{Nest.DeleteAutoFollowPatternDescriptor,Nest.IDeleteAutoFollowPatternRequest})" />
		public IDeleteAutoFollowPatternResponse DeleteAutoFollowPattern(Name name, Func<DeleteAutoFollowPatternDescriptor, IDeleteAutoFollowPatternRequest> selector = null) =>
			DeleteAutoFollowPattern(selector.InvokeOrDefault(new DeleteAutoFollowPatternDescriptor(name)));

		/// <inheritdoc cref="DeleteAutoFollowPattern(Name,System.Func{Nest.DeleteAutoFollowPatternDescriptor,Nest.IDeleteAutoFollowPatternRequest})" />
		public IDeleteAutoFollowPatternResponse DeleteAutoFollowPattern(IDeleteAutoFollowPatternRequest request) =>
			Dispatcher.Dispatch<IDeleteAutoFollowPatternRequest, DeleteAutoFollowPatternRequestParameters, DeleteAutoFollowPatternResponse>(
				request,
				(p, d) => LowLevelDispatch.CcrDeleteAutoFollowPatternDispatch<DeleteAutoFollowPatternResponse>(p)
			);

		/// <inheritdoc cref="DeleteAutoFollowPattern(Name,System.Func{Nest.DeleteAutoFollowPatternDescriptor,Nest.IDeleteAutoFollowPatternRequest})" />
		public Task<IDeleteAutoFollowPatternResponse> DeleteAutoFollowPatternAsync(Name name, Func<DeleteAutoFollowPatternDescriptor, IDeleteAutoFollowPatternRequest> selector = null,
			CancellationToken cancellationToken = default
		) =>
			DeleteAutoFollowPatternAsync(selector.InvokeOrDefault(new DeleteAutoFollowPatternDescriptor(name)), cancellationToken);

		/// <inheritdoc cref="DeleteAutoFollowPattern(Name,System.Func{Nest.DeleteAutoFollowPatternDescriptor,Nest.IDeleteAutoFollowPatternRequest})" />
		public Task<IDeleteAutoFollowPatternResponse> DeleteAutoFollowPatternAsync(IDeleteAutoFollowPatternRequest request, CancellationToken cancellationToken = default) =>
			Dispatcher.DispatchAsync<IDeleteAutoFollowPatternRequest, DeleteAutoFollowPatternRequestParameters, DeleteAutoFollowPatternResponse, IDeleteAutoFollowPatternResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.CcrDeleteAutoFollowPatternDispatchAsync<DeleteAutoFollowPatternResponse>(p, c)
			);
	}
}
