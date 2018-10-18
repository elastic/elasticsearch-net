using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Gets the rollup capabilities that have been configured for an index or index pattern.
		/// This API is useful because a rollup job is often configured to rollup only a subset of fields from the
		/// source index. Furthermore, only certain aggregations can be configured for various fields,
		/// leading to a limited subset of functionality depending on that configuration.
		/// <para />
		/// <para />
		/// This API will allow you to inspect an index and determine:
		/// <para />
		/// 1. Does this index have associated rollup data somewhere in the cluster?
		/// <para />
		/// 2. If yes to the first question, what fields were rolled up, what aggregations can be performed, and where does the data live?
		/// </summary>
		IGetRollupCapabilitiesResponse GetRollupCapabilities(Func<GetRollupCapabilitiesDescriptor, IGetRollupCapabilitiesRequest> selector = null);

		/// <inheritdoc cref="GetRollupCapabilities(System.Func{Nest.GetRollupCapabilitiesDescriptor,Nest.IGetRollupCapabilitiesRequest})"/>
		IGetRollupCapabilitiesResponse GetRollupCapabilities(IGetRollupCapabilitiesRequest request);

		/// <inheritdoc cref="GetRollupCapabilities(System.Func{Nest.GetRollupCapabilitiesDescriptor,Nest.IGetRollupCapabilitiesRequest})"/>
		Task<IGetRollupCapabilitiesResponse> GetRollupCapabilitiesAsync(
			Func<GetRollupCapabilitiesDescriptor, IGetRollupCapabilitiesRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc cref="GetRollupCapabilities(System.Func{Nest.GetRollupCapabilitiesDescriptor,Nest.IGetRollupCapabilitiesRequest})"/>
		Task<IGetRollupCapabilitiesResponse> GetRollupCapabilitiesAsync(IGetRollupCapabilitiesRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IGetRollupCapabilitiesResponse GetRollupCapabilities(Func<GetRollupCapabilitiesDescriptor, IGetRollupCapabilitiesRequest> selector = null) =>
			this.GetRollupCapabilities(selector.InvokeOrDefault(new GetRollupCapabilitiesDescriptor()));

		/// <inheritdoc/>
		public IGetRollupCapabilitiesResponse GetRollupCapabilities(IGetRollupCapabilitiesRequest request) =>
			this.Dispatcher.Dispatch<IGetRollupCapabilitiesRequest, GetRollupCapabilitiesRequestParameters, GetRollupCapabilitiesResponse>(
				request,
				(p, d) => this.LowLevelDispatch.XpackRollupGetRollupCapsDispatch<GetRollupCapabilitiesResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IGetRollupCapabilitiesResponse> GetRollupCapabilitiesAsync(
			Func<GetRollupCapabilitiesDescriptor, IGetRollupCapabilitiesRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken))  =>
			this.GetRollupCapabilitiesAsync(selector.InvokeOrDefault(new GetRollupCapabilitiesDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<IGetRollupCapabilitiesResponse> GetRollupCapabilitiesAsync(IGetRollupCapabilitiesRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IGetRollupCapabilitiesRequest, GetRollupCapabilitiesRequestParameters, GetRollupCapabilitiesResponse, IGetRollupCapabilitiesResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackRollupGetRollupCapsDispatchAsync<GetRollupCapabilitiesResponse>(p, c)
			);
	}
}
