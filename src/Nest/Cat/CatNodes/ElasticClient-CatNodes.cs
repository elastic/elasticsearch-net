using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		ICatResponse<CatNodesRecord> CatNodes(Func<CatNodesDescriptor, ICatNodesRequest> selector = null);

		/// <inheritdoc />
		ICatResponse<CatNodesRecord> CatNodes(ICatNodesRequest request);

		/// <inheritdoc />
		Task<ICatResponse<CatNodesRecord>> CatNodesAsync(Func<CatNodesDescriptor, ICatNodesRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<ICatResponse<CatNodesRecord>> CatNodesAsync(ICatNodesRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICatResponse<CatNodesRecord> CatNodes(Func<CatNodesDescriptor, ICatNodesRequest> selector = null) =>
			CatNodes(selector.InvokeOrDefault(new CatNodesDescriptor()));

		/// <inheritdoc />
		public ICatResponse<CatNodesRecord> CatNodes(ICatNodesRequest request) =>
			DoCat<ICatNodesRequest, CatNodesRequestParameters, CatNodesRecord>(request,
				LowLevelDispatch.CatNodesDispatch<CatResponse<CatNodesRecord>>);

		/// <inheritdoc />
		public Task<ICatResponse<CatNodesRecord>> CatNodesAsync(Func<CatNodesDescriptor, ICatNodesRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			CatNodesAsync(selector.InvokeOrDefault(new CatNodesDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<ICatResponse<CatNodesRecord>> CatNodesAsync(ICatNodesRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			DoCatAsync<ICatNodesRequest, CatNodesRequestParameters, CatNodesRecord>(request, cancellationToken,
				LowLevelDispatch.CatNodesDispatchAsync<CatResponse<CatNodesRecord>>);
	}
}
