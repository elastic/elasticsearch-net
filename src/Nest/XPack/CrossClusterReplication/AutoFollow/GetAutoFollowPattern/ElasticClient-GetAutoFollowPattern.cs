using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary> This API gets configured auto-follow patterns. This API will return the specified auto-follow pattern collection. </summary>
		IGetAutoFollowPatternResponse GetAutoFollowPattern(Func<GetAutoFollowPatternDescriptor, IGetAutoFollowPatternRequest> selector = null);

		/// <inheritdoc cref="GetAutoFollowPattern(System.Func{Nest.GetAutoFollowPatternDescriptor,Nest.IGetAutoFollowPatternRequest})" />
		IGetAutoFollowPatternResponse GetAutoFollowPattern(IGetAutoFollowPatternRequest request);

		/// <inheritdoc cref="GetAutoFollowPattern(System.Func{Nest.GetAutoFollowPatternDescriptor,Nest.IGetAutoFollowPatternRequest})" />
		Task<IGetAutoFollowPatternResponse> GetAutoFollowPatternAsync(Func<GetAutoFollowPatternDescriptor, IGetAutoFollowPatternRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="GetAutoFollowPattern(System.Func{Nest.GetAutoFollowPatternDescriptor,Nest.IGetAutoFollowPatternRequest})" />
		Task<IGetAutoFollowPatternResponse> GetAutoFollowPatternAsync(IGetAutoFollowPatternRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="GetAutoFollowPattern(System.Func{Nest.GetAutoFollowPatternDescriptor,Nest.IGetAutoFollowPatternRequest})" />
		public IGetAutoFollowPatternResponse GetAutoFollowPattern(Func<GetAutoFollowPatternDescriptor, IGetAutoFollowPatternRequest> selector = null) =>
			GetAutoFollowPattern(selector.InvokeOrDefault(new GetAutoFollowPatternDescriptor()));

		/// <inheritdoc cref="GetAutoFollowPattern(System.Func{Nest.GetAutoFollowPatternDescriptor,Nest.IGetAutoFollowPatternRequest})" />
		public IGetAutoFollowPatternResponse GetAutoFollowPattern(IGetAutoFollowPatternRequest request) =>
			Dispatcher.Dispatch<IGetAutoFollowPatternRequest, GetAutoFollowPatternRequestParameters, GetAutoFollowPatternResponse>(
				request,
				(p, d) => LowLevelDispatch.CcrGetAutoFollowPatternDispatch<GetAutoFollowPatternResponse>(p)
			);

		/// <inheritdoc cref="GetAutoFollowPattern(System.Func{Nest.GetAutoFollowPatternDescriptor,Nest.IGetAutoFollowPatternRequest})" />
		public Task<IGetAutoFollowPatternResponse> GetAutoFollowPatternAsync(Func<GetAutoFollowPatternDescriptor, IGetAutoFollowPatternRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			GetAutoFollowPatternAsync(selector.InvokeOrDefault(new GetAutoFollowPatternDescriptor()), cancellationToken);

		/// <inheritdoc cref="GetAutoFollowPattern(System.Func{Nest.GetAutoFollowPatternDescriptor,Nest.IGetAutoFollowPatternRequest})" />
		public Task<IGetAutoFollowPatternResponse> GetAutoFollowPatternAsync(IGetAutoFollowPatternRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			Dispatcher.DispatchAsync<IGetAutoFollowPatternRequest, GetAutoFollowPatternRequestParameters, GetAutoFollowPatternResponse, IGetAutoFollowPatternResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.CcrGetAutoFollowPatternDispatchAsync<GetAutoFollowPatternResponse>(p, c)
			);
	}
}
