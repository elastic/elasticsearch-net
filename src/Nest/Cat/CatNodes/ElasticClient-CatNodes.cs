using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		ICatResponse<CatNodesRecord> CatNodes(Func<CatNodesDescriptor, ICatNodesRequest> selector = null);

		/// <inheritdoc/>
		ICatResponse<CatNodesRecord> CatNodes(ICatNodesRequest request);

		/// <inheritdoc/>
		Task<ICatResponse<CatNodesRecord>> CatNodesAsync(Func<CatNodesDescriptor, ICatNodesRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<ICatResponse<CatNodesRecord>> CatNodesAsync(ICatNodesRequest request, CancellationToken cancellationToken = default(CancellationToken));

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ICatResponse<CatNodesRecord> CatNodes(Func<CatNodesDescriptor, ICatNodesRequest> selector = null) =>
			this.CatNodes(selector.InvokeOrDefault(new CatNodesDescriptor()));

		/// <inheritdoc/>
		public ICatResponse<CatNodesRecord> CatNodes(ICatNodesRequest request)=>
			this.DoCat<ICatNodesRequest, CatNodesRequestParameters, CatNodesRecord>(request, this.LowLevelDispatch.CatNodesDispatch<CatResponse<CatNodesRecord>>);

		/// <inheritdoc/>
		public Task<ICatResponse<CatNodesRecord>> CatNodesAsync(Func<CatNodesDescriptor, ICatNodesRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.CatNodesAsync(selector.InvokeOrDefault(new CatNodesDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<ICatResponse<CatNodesRecord>> CatNodesAsync(ICatNodesRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.DoCatAsync<ICatNodesRequest, CatNodesRequestParameters, CatNodesRecord>(request, cancellationToken, this.LowLevelDispatch.CatNodesDispatchAsync<CatResponse<CatNodesRecord>>);
	}
}
