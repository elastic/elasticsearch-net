using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		ICatResponse<CatAliasesRecord> CatAliases(Func<CatAliasesDescriptor, ICatAliasesRequest> selector = null);

		/// <inheritdoc />
		ICatResponse<CatAliasesRecord> CatAliases(ICatAliasesRequest request);

		/// <inheritdoc />
		Task<ICatResponse<CatAliasesRecord>> CatAliasesAsync(Func<CatAliasesDescriptor, ICatAliasesRequest> selector = null,
			CancellationToken ct = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<ICatResponse<CatAliasesRecord>> CatAliasesAsync(ICatAliasesRequest request,
			CancellationToken ct = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICatResponse<CatAliasesRecord> CatAliases(Func<CatAliasesDescriptor, ICatAliasesRequest> selector = null) =>
			CatAliases(selector.InvokeOrDefault(new CatAliasesDescriptor()));

		/// <inheritdoc />
		public ICatResponse<CatAliasesRecord> CatAliases(ICatAliasesRequest request) =>
			DoCat<ICatAliasesRequest, CatAliasesRequestParameters, CatAliasesRecord>(request);

		/// <inheritdoc />
		public Task<ICatResponse<CatAliasesRecord>> CatAliasesAsync(
			Func<CatAliasesDescriptor, ICatAliasesRequest> selector = null,
			CancellationToken ct = default
		) =>
			CatAliasesAsync(selector.InvokeOrDefault(new CatAliasesDescriptor()), ct);

		/// <inheritdoc />
		public Task<ICatResponse<CatAliasesRecord>> CatAliasesAsync(ICatAliasesRequest request, CancellationToken ct = default) =>
			DoCatAsync<ICatAliasesRequest, CatAliasesRequestParameters, CatAliasesRecord>(request, ct);
	}
}
