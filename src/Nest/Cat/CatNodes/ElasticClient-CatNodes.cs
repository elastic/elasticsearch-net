using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Elasticsearch.Net.Api.Cat;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		CatResponse<CatNodesRecord> CatNodes(Func<CatNodesDescriptor, ICatNodesRequest> selector = null);

		/// <inheritdoc />
		CatResponse<CatNodesRecord> CatNodes(ICatNodesRequest request);

		/// <inheritdoc />
		Task<CatResponse<CatNodesRecord>> CatNodesAsync(Func<CatNodesDescriptor, ICatNodesRequest> selector = null,
			CancellationToken ct = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<CatResponse<CatNodesRecord>> CatNodesAsync(ICatNodesRequest request, CancellationToken ct = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public CatResponse<CatNodesRecord> CatNodes(Func<CatNodesDescriptor, ICatNodesRequest> selector = null) =>
			CatNodes(selector.InvokeOrDefault(new CatNodesDescriptor()));

		/// <inheritdoc />
		public CatResponse<CatNodesRecord> CatNodes(ICatNodesRequest request) =>
			DoCat<ICatNodesRequest, CatNodesRequestParameters, CatNodesRecord>(request);

		/// <inheritdoc />
		public Task<CatResponse<CatNodesRecord>> CatNodesAsync(Func<CatNodesDescriptor, ICatNodesRequest> selector = null,
			CancellationToken ct = default
		) =>
			CatNodesAsync(selector.InvokeOrDefault(new CatNodesDescriptor()), ct);

		/// <inheritdoc />
		public Task<CatResponse<CatNodesRecord>> CatNodesAsync(ICatNodesRequest request, CancellationToken ct = default) =>
			DoCatAsync<ICatNodesRequest, CatNodesRequestParameters, CatNodesRecord>(request, ct);
	}
}
