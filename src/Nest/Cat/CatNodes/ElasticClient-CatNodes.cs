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
			CancellationToken ct = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<ICatResponse<CatNodesRecord>> CatNodesAsync(ICatNodesRequest request, CancellationToken ct = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICatResponse<CatNodesRecord> CatNodes(Func<CatNodesDescriptor, ICatNodesRequest> selector = null) =>
			CatNodes(selector.InvokeOrDefault(new CatNodesDescriptor()));

		/// <inheritdoc />
		public ICatResponse<CatNodesRecord> CatNodes(ICatNodesRequest request) =>
			DoCat<ICatNodesRequest, CatNodesRequestParameters, CatNodesRecord>(request);

		/// <inheritdoc />
		public Task<ICatResponse<CatNodesRecord>> CatNodesAsync(Func<CatNodesDescriptor, ICatNodesRequest> selector = null,
			CancellationToken ct = default
		) =>
			CatNodesAsync(selector.InvokeOrDefault(new CatNodesDescriptor()), ct);

		/// <inheritdoc />
		public Task<ICatResponse<CatNodesRecord>> CatNodesAsync(ICatNodesRequest request, CancellationToken ct = default) =>
			DoCatAsync<ICatNodesRequest, CatNodesRequestParameters, CatNodesRecord>(request, ct);
	}
}
