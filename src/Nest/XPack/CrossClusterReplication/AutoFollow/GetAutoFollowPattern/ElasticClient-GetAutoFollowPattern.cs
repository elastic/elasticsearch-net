using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary> Gets configured auto-follow patterns. Returns the specified auto-follow pattern collection. </summary>
		GetAutoFollowPatternResponse GetAutoFollowPattern(Func<GetAutoFollowPatternDescriptor, IGetAutoFollowPatternRequest> selector = null);

		/// <inheritdoc cref="GetAutoFollowPattern(System.Func{Nest.GetAutoFollowPatternDescriptor,Nest.IGetAutoFollowPatternRequest})" />
		GetAutoFollowPatternResponse GetAutoFollowPattern(IGetAutoFollowPatternRequest request);

		/// <inheritdoc cref="GetAutoFollowPattern(System.Func{Nest.GetAutoFollowPatternDescriptor,Nest.IGetAutoFollowPatternRequest})" />
		Task<GetAutoFollowPatternResponse> GetAutoFollowPatternAsync(Func<GetAutoFollowPatternDescriptor, IGetAutoFollowPatternRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="GetAutoFollowPattern(System.Func{Nest.GetAutoFollowPatternDescriptor,Nest.IGetAutoFollowPatternRequest})" />
		Task<GetAutoFollowPatternResponse> GetAutoFollowPatternAsync(IGetAutoFollowPatternRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="GetAutoFollowPattern(System.Func{Nest.GetAutoFollowPatternDescriptor,Nest.IGetAutoFollowPatternRequest})" />
		public GetAutoFollowPatternResponse GetAutoFollowPattern(Func<GetAutoFollowPatternDescriptor, IGetAutoFollowPatternRequest> selector = null) =>
			GetAutoFollowPattern(selector.InvokeOrDefault(new GetAutoFollowPatternDescriptor()));

		/// <inheritdoc cref="GetAutoFollowPattern(System.Func{Nest.GetAutoFollowPatternDescriptor,Nest.IGetAutoFollowPatternRequest})" />
		public GetAutoFollowPatternResponse GetAutoFollowPattern(IGetAutoFollowPatternRequest request) =>
			DoRequest<IGetAutoFollowPatternRequest, GetAutoFollowPatternResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="GetAutoFollowPattern(System.Func{Nest.GetAutoFollowPatternDescriptor,Nest.IGetAutoFollowPatternRequest})" />
		public Task<GetAutoFollowPatternResponse> GetAutoFollowPatternAsync(
			Func<GetAutoFollowPatternDescriptor, IGetAutoFollowPatternRequest> selector = null,
			CancellationToken ct = default
		) => GetAutoFollowPatternAsync(selector.InvokeOrDefault(new GetAutoFollowPatternDescriptor()), ct);

		/// <inheritdoc cref="GetAutoFollowPattern(System.Func{Nest.GetAutoFollowPatternDescriptor,Nest.IGetAutoFollowPatternRequest})" />
		public Task<GetAutoFollowPatternResponse> GetAutoFollowPatternAsync(IGetAutoFollowPatternRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetAutoFollowPatternRequest, GetAutoFollowPatternResponse>(request, request.RequestParameters, ct);
	}
}
