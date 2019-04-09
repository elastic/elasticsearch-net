using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		ICatResponse<CatMasterRecord> CatMaster(Func<CatMasterDescriptor, ICatMasterRequest> selector = null);

		/// <inheritdoc />
		ICatResponse<CatMasterRecord> CatMaster(ICatMasterRequest request);

		/// <inheritdoc />
		Task<ICatResponse<CatMasterRecord>> CatMasterAsync(
			Func<CatMasterDescriptor, ICatMasterRequest> selector = null,
			CancellationToken ct = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<ICatResponse<CatMasterRecord>> CatMasterAsync(ICatMasterRequest request, CancellationToken ct = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICatResponse<CatMasterRecord> CatMaster(Func<CatMasterDescriptor, ICatMasterRequest> selector = null) =>
			CatMaster(selector.InvokeOrDefault(new CatMasterDescriptor()));

		/// <inheritdoc />
		public ICatResponse<CatMasterRecord> CatMaster(ICatMasterRequest request) =>
			DoCat<ICatMasterRequest, CatMasterRequestParameters, CatMasterRecord>(request);

		/// <inheritdoc />
		public Task<ICatResponse<CatMasterRecord>> CatMasterAsync(
			Func<CatMasterDescriptor, ICatMasterRequest> selector = null,
			CancellationToken ct = default
		) =>
			CatMasterAsync(selector.InvokeOrDefault(new CatMasterDescriptor()), ct);

		/// <inheritdoc />
		public Task<ICatResponse<CatMasterRecord>> CatMasterAsync(ICatMasterRequest request, CancellationToken ct = default) =>
			DoCatAsync<ICatMasterRequest, CatMasterRequestParameters, CatMasterRecord>(request, ct);
	}
}
