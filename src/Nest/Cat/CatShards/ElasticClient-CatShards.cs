using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		ICatResponse<CatShardsRecord> CatShards(Func<CatShardsDescriptor, ICatShardsRequest> selector = null);

		/// <inheritdoc />
		ICatResponse<CatShardsRecord> CatShards(ICatShardsRequest request);

		/// <inheritdoc />
		Task<ICatResponse<CatShardsRecord>> CatShardsAsync(Func<CatShardsDescriptor, ICatShardsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<ICatResponse<CatShardsRecord>> CatShardsAsync(ICatShardsRequest request, CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICatResponse<CatShardsRecord> CatShards(Func<CatShardsDescriptor, ICatShardsRequest> selector = null) =>
			CatShards(selector.InvokeOrDefault(new CatShardsDescriptor()));

		/// <inheritdoc />
		public ICatResponse<CatShardsRecord> CatShards(ICatShardsRequest request) =>
			DoCat<ICatShardsRequest, CatShardsRequestParameters, CatShardsRecord>(request,
				LowLevelDispatch.CatShardsDispatch<CatResponse<CatShardsRecord>>);

		/// <inheritdoc />
		public Task<ICatResponse<CatShardsRecord>> CatShardsAsync(Func<CatShardsDescriptor, ICatShardsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			CatShardsAsync(selector.InvokeOrDefault(new CatShardsDescriptor()), cancellationToken);

		public Task<ICatResponse<CatShardsRecord>> CatShardsAsync(ICatShardsRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			DoCatAsync<ICatShardsRequest, CatShardsRequestParameters, CatShardsRecord>(request, cancellationToken,
				LowLevelDispatch.CatShardsDispatchAsync<CatResponse<CatShardsRecord>>);
	}
}
