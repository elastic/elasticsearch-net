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
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<ICatResponse<CatAliasesRecord>> CatAliasesAsync(ICatAliasesRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICatResponse<CatAliasesRecord> CatAliases(Func<CatAliasesDescriptor, ICatAliasesRequest> selector = null) =>
			CatAliases(selector.InvokeOrDefault(new CatAliasesDescriptor()));

		/// <inheritdoc />
		public ICatResponse<CatAliasesRecord> CatAliases(ICatAliasesRequest request) =>
			DoCat<ICatAliasesRequest, CatAliasesRequestParameters, CatAliasesRecord>(request,
				LowLevelDispatch.CatAliasesDispatch<CatResponse<CatAliasesRecord>>);

		/// <inheritdoc />
		public Task<ICatResponse<CatAliasesRecord>> CatAliasesAsync(Func<CatAliasesDescriptor, ICatAliasesRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			CatAliasesAsync(selector.InvokeOrDefault(new CatAliasesDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<ICatResponse<CatAliasesRecord>> CatAliasesAsync(ICatAliasesRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			DoCatAsync<ICatAliasesRequest, CatAliasesRequestParameters, CatAliasesRecord>(request, cancellationToken,
				LowLevelDispatch.CatAliasesDispatchAsync<CatResponse<CatAliasesRecord>>);
	}
}
