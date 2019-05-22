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
		CatResponse<CatAliasesRecord> CatAliases(Func<CatAliasesDescriptor, ICatAliasesRequest> selector = null);

		/// <inheritdoc />
		CatResponse<CatAliasesRecord> CatAliases(ICatAliasesRequest request);

		/// <inheritdoc />
		Task<CatResponse<CatAliasesRecord>> CatAliasesAsync(Func<CatAliasesDescriptor, ICatAliasesRequest> selector = null,
			CancellationToken ct = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<CatResponse<CatAliasesRecord>> CatAliasesAsync(ICatAliasesRequest request,
			CancellationToken ct = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public CatResponse<CatAliasesRecord> CatAliases(Func<CatAliasesDescriptor, ICatAliasesRequest> selector = null) =>
			CatAliases(selector.InvokeOrDefault(new CatAliasesDescriptor()));

		/// <inheritdoc />
		public CatResponse<CatAliasesRecord> CatAliases(ICatAliasesRequest request) =>
			DoCat<ICatAliasesRequest, CatAliasesRequestParameters, CatAliasesRecord>(request);

		/// <inheritdoc />
		public Task<CatResponse<CatAliasesRecord>> CatAliasesAsync(
			Func<CatAliasesDescriptor, ICatAliasesRequest> selector = null,
			CancellationToken ct = default
		) =>
			CatAliasesAsync(selector.InvokeOrDefault(new CatAliasesDescriptor()), ct);

		/// <inheritdoc />
		public Task<CatResponse<CatAliasesRecord>> CatAliasesAsync(ICatAliasesRequest request, CancellationToken ct = default) =>
			DoCatAsync<ICatAliasesRequest, CatAliasesRequestParameters, CatAliasesRecord>(request, ct);
	}
}
