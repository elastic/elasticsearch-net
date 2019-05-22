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
		CatResponse<CatTemplatesRecord> CatTemplates(Func<CatTemplatesDescriptor, ICatTemplatesRequest> selector = null);

		/// <inheritdoc />
		CatResponse<CatTemplatesRecord> CatTemplates(ICatTemplatesRequest request);

		/// <inheritdoc />
		Task<CatResponse<CatTemplatesRecord>> CatTemplatesAsync(
			Func<CatTemplatesDescriptor, ICatTemplatesRequest> selector = null,
			CancellationToken ct = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<CatResponse<CatTemplatesRecord>> CatTemplatesAsync(ICatTemplatesRequest request,
			CancellationToken ct = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public CatResponse<CatTemplatesRecord> CatTemplates(Func<CatTemplatesDescriptor, ICatTemplatesRequest> selector = null) =>
			CatTemplates(selector.InvokeOrDefault(new CatTemplatesDescriptor()));

		/// <inheritdoc />
		public CatResponse<CatTemplatesRecord> CatTemplates(ICatTemplatesRequest request) =>
			DoCat<ICatTemplatesRequest, CatTemplatesRequestParameters, CatTemplatesRecord>(request);

		/// <inheritdoc />
		public Task<CatResponse<CatTemplatesRecord>> CatTemplatesAsync(
			Func<CatTemplatesDescriptor, ICatTemplatesRequest> selector = null,
			CancellationToken ct = default
		) => CatTemplatesAsync(selector.InvokeOrDefault(new CatTemplatesDescriptor()), ct);

		/// <inheritdoc />
		public Task<CatResponse<CatTemplatesRecord>> CatTemplatesAsync(ICatTemplatesRequest request,
			CancellationToken ct = default
		) =>
			DoCatAsync<ICatTemplatesRequest, CatTemplatesRequestParameters, CatTemplatesRecord>(request, ct);
	}
}
