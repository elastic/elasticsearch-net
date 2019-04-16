using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		ICatResponse<CatTemplatesRecord> CatTemplates(Func<CatTemplatesDescriptor, ICatTemplatesRequest> selector = null);

		/// <inheritdoc />
		ICatResponse<CatTemplatesRecord> CatTemplates(ICatTemplatesRequest request);

		/// <inheritdoc />
		Task<ICatResponse<CatTemplatesRecord>> CatTemplatesAsync(
			Func<CatTemplatesDescriptor, ICatTemplatesRequest> selector = null,
			CancellationToken ct = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<ICatResponse<CatTemplatesRecord>> CatTemplatesAsync(ICatTemplatesRequest request,
			CancellationToken ct = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICatResponse<CatTemplatesRecord> CatTemplates(Func<CatTemplatesDescriptor, ICatTemplatesRequest> selector = null) =>
			CatTemplates(selector.InvokeOrDefault(new CatTemplatesDescriptor()));

		/// <inheritdoc />
		public ICatResponse<CatTemplatesRecord> CatTemplates(ICatTemplatesRequest request) =>
			DoCat<ICatTemplatesRequest, CatTemplatesRequestParameters, CatTemplatesRecord>(request);

		/// <inheritdoc />
		public Task<ICatResponse<CatTemplatesRecord>> CatTemplatesAsync(
			Func<CatTemplatesDescriptor, ICatTemplatesRequest> selector = null,
			CancellationToken ct = default
		) => CatTemplatesAsync(selector.InvokeOrDefault(new CatTemplatesDescriptor()), ct);

		/// <inheritdoc />
		public Task<ICatResponse<CatTemplatesRecord>> CatTemplatesAsync(ICatTemplatesRequest request,
			CancellationToken ct = default
		) =>
			DoCatAsync<ICatTemplatesRequest, CatTemplatesRequestParameters, CatTemplatesRecord>(request, ct);
	}
}
