using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		ICatResponse<CatTasksRecord> CatTasks(Func<CatTasksDescriptor, ICatTasksRequest> selector = null);

		/// <inheritdoc />
		ICatResponse<CatTasksRecord> CatTasks(ICatTasksRequest request);

		/// <inheritdoc />
		Task<ICatResponse<CatTasksRecord>> CatTasksAsync(
			Func<CatTasksDescriptor, ICatTasksRequest> selector = null,
			CancellationToken ct = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<ICatResponse<CatTasksRecord>> CatTasksAsync(ICatTasksRequest request, CancellationToken ct = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICatResponse<CatTasksRecord> CatTasks(Func<CatTasksDescriptor, ICatTasksRequest> selector = null) =>
			CatTasks(selector.InvokeOrDefault(new CatTasksDescriptor()));

		/// <inheritdoc />
		public ICatResponse<CatTasksRecord> CatTasks(ICatTasksRequest request) =>
			DoCat<ICatTasksRequest, CatTasksRequestParameters, CatTasksRecord>(request);

		/// <inheritdoc />
		public Task<ICatResponse<CatTasksRecord>> CatTasksAsync(
			Func<CatTasksDescriptor, ICatTasksRequest> selector = null,
			CancellationToken ct = default
		) => CatTasksAsync(selector.InvokeOrDefault(new CatTasksDescriptor()));

		/// <inheritdoc />
		public Task<ICatResponse<CatTasksRecord>> CatTasksAsync(ICatTasksRequest request, CancellationToken ct = default) =>
			DoCatAsync<ICatTasksRequest, CatTasksRequestParameters, CatTasksRecord>(request, ct);
	}
}
