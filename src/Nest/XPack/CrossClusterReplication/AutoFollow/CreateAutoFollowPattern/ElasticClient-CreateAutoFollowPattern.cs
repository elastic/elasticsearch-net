using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Creates a new named collection of auto-follow patterns against the remote cluster specified
		/// in the request body. Newly created indices on the remote cluster matching any of the specified patterns
		/// will be automatically configured as follower indices.
		/// </summary>
		ICreateAutoFollowPatternResponse CreateAutoFollowPattern(Name name, Func<CreateAutoFollowPatternDescriptor, ICreateAutoFollowPatternRequest> selector);

		/// <inheritdoc cref="CreateAutoFollowPattern(Name, System.Func{Nest.CreateAutoFollowPatternDescriptor,Nest.ICreateAutoFollowPatternRequest})" />
		ICreateAutoFollowPatternResponse CreateAutoFollowPattern(ICreateAutoFollowPatternRequest request);

		/// <inheritdoc cref="CreateAutoFollowPattern(Name, System.Func{Nest.CreateAutoFollowPatternDescriptor,Nest.ICreateAutoFollowPatternRequest})" />
		Task<ICreateAutoFollowPatternResponse> CreateAutoFollowPatternAsync(Name name, Func<CreateAutoFollowPatternDescriptor, ICreateAutoFollowPatternRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="CreateAutoFollowPattern(Name, System.Func{Nest.CreateAutoFollowPatternDescriptor,Nest.ICreateAutoFollowPatternRequest})" />
		Task<ICreateAutoFollowPatternResponse> CreateAutoFollowPatternAsync(ICreateAutoFollowPatternRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="CreateAutoFollowPattern(Name, System.Func{Nest.CreateAutoFollowPatternDescriptor,Nest.ICreateAutoFollowPatternRequest})" />
		public ICreateAutoFollowPatternResponse CreateAutoFollowPattern(Name name, Func<CreateAutoFollowPatternDescriptor, ICreateAutoFollowPatternRequest> selector) =>
			CreateAutoFollowPattern(selector.InvokeOrDefault(new CreateAutoFollowPatternDescriptor(name)));

		/// <inheritdoc cref="CreateAutoFollowPattern(Name, System.Func{Nest.CreateAutoFollowPatternDescriptor,Nest.ICreateAutoFollowPatternRequest})" />
		public ICreateAutoFollowPatternResponse CreateAutoFollowPattern(ICreateAutoFollowPatternRequest request) =>
			Dispatch2<ICreateAutoFollowPatternRequest, CreateAutoFollowPatternResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="CreateAutoFollowPattern(Name, System.Func{Nest.CreateAutoFollowPatternDescriptor,Nest.ICreateAutoFollowPatternRequest})" />
		public Task<ICreateAutoFollowPatternResponse> CreateAutoFollowPatternAsync(
			Name name,
			Func<CreateAutoFollowPatternDescriptor, ICreateAutoFollowPatternRequest> selector,
			CancellationToken ct = default
		) =>
			CreateAutoFollowPatternAsync(selector.InvokeOrDefault(new CreateAutoFollowPatternDescriptor(name)), ct);

		/// <inheritdoc cref="CreateAutoFollowPattern(Name, System.Func{Nest.CreateAutoFollowPatternDescriptor,Nest.ICreateAutoFollowPatternRequest})" />
		public Task<ICreateAutoFollowPatternResponse> CreateAutoFollowPatternAsync(ICreateAutoFollowPatternRequest request, CancellationToken ct = default) =>
			Dispatch2Async<ICreateAutoFollowPatternRequest, ICreateAutoFollowPatternResponse, CreateAutoFollowPatternResponse>(request, request.RequestParameters, ct);
	}
}
