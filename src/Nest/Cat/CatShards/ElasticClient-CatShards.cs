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
			CancellationToken ct = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<ICatResponse<CatShardsRecord>> CatShardsAsync(ICatShardsRequest request, CancellationToken ct = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICatResponse<CatShardsRecord> CatShards(Func<CatShardsDescriptor, ICatShardsRequest> selector = null) =>
			CatShards(selector.InvokeOrDefault(new CatShardsDescriptor()));

		/// <inheritdoc />
		public ICatResponse<CatShardsRecord> CatShards(ICatShardsRequest request) =>
			DoCat<ICatShardsRequest, CatShardsRequestParameters, CatShardsRecord>(request);

		/// <inheritdoc />
		public Task<ICatResponse<CatShardsRecord>> CatShardsAsync(
			Func<CatShardsDescriptor, ICatShardsRequest> selector = null,
			CancellationToken ct = default
		) =>
			CatShardsAsync(selector.InvokeOrDefault(new CatShardsDescriptor()), ct);

		public Task<ICatResponse<CatShardsRecord>> CatShardsAsync(ICatShardsRequest request, CancellationToken ct = default) =>
			DoCatAsync<ICatShardsRequest, CatShardsRequestParameters, CatShardsRecord>(request, ct);
	}
}
