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
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<ICatResponse<CatMasterRecord>> CatMasterAsync(ICatMasterRequest request, CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICatResponse<CatMasterRecord> CatMaster(Func<CatMasterDescriptor, ICatMasterRequest> selector = null) =>
			CatMaster(selector.InvokeOrDefault(new CatMasterDescriptor()));

		/// <inheritdoc />
		public ICatResponse<CatMasterRecord> CatMaster(ICatMasterRequest request) =>
			DoCat<ICatMasterRequest, CatMasterRequestParameters, CatMasterRecord>(request,
				LowLevelDispatch.CatMasterDispatch<CatResponse<CatMasterRecord>>);

		/// <inheritdoc />
		public Task<ICatResponse<CatMasterRecord>> CatMasterAsync(Func<CatMasterDescriptor, ICatMasterRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			CatMasterAsync(selector.InvokeOrDefault(new CatMasterDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<ICatResponse<CatMasterRecord>> CatMasterAsync(ICatMasterRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			DoCatAsync<ICatMasterRequest, CatMasterRequestParameters, CatMasterRecord>(request, cancellationToken,
				LowLevelDispatch.CatMasterDispatchAsync<CatResponse<CatMasterRecord>>);
	}
}
