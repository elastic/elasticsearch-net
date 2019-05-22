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
		CatResponse<CatMasterRecord> CatMaster(Func<CatMasterDescriptor, ICatMasterRequest> selector = null);

		/// <inheritdoc />
		CatResponse<CatMasterRecord> CatMaster(ICatMasterRequest request);

		/// <inheritdoc />
		Task<CatResponse<CatMasterRecord>> CatMasterAsync(
			Func<CatMasterDescriptor, ICatMasterRequest> selector = null,
			CancellationToken ct = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<CatResponse<CatMasterRecord>> CatMasterAsync(ICatMasterRequest request, CancellationToken ct = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public CatResponse<CatMasterRecord> CatMaster(Func<CatMasterDescriptor, ICatMasterRequest> selector = null) =>
			CatMaster(selector.InvokeOrDefault(new CatMasterDescriptor()));

		/// <inheritdoc />
		public CatResponse<CatMasterRecord> CatMaster(ICatMasterRequest request) =>
			DoCat<ICatMasterRequest, CatMasterRequestParameters, CatMasterRecord>(request);

		/// <inheritdoc />
		public Task<CatResponse<CatMasterRecord>> CatMasterAsync(
			Func<CatMasterDescriptor, ICatMasterRequest> selector = null,
			CancellationToken ct = default
		) =>
			CatMasterAsync(selector.InvokeOrDefault(new CatMasterDescriptor()), ct);

		/// <inheritdoc />
		public Task<CatResponse<CatMasterRecord>> CatMasterAsync(ICatMasterRequest request, CancellationToken ct = default) =>
			DoCatAsync<ICatMasterRequest, CatMasterRequestParameters, CatMasterRecord>(request, ct);
	}
}
