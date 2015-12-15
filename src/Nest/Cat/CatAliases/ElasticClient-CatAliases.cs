using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		ICatResponse<CatAliasesRecord> CatAliases(Func<CatAliasesDescriptor, ICatAliasesRequest> selector = null);

		/// <inheritdoc/>
		ICatResponse<CatAliasesRecord> CatAliases(ICatAliasesRequest request);
		
		/// <inheritdoc/>
		Task<ICatResponse<CatAliasesRecord>> CatAliasesAsync(Func<CatAliasesDescriptor, ICatAliasesRequest> selector = null);
		
		/// <inheritdoc/>
		Task<ICatResponse<CatAliasesRecord>> CatAliasesAsync(ICatAliasesRequest request);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ICatResponse<CatAliasesRecord> CatAliases(Func<CatAliasesDescriptor, ICatAliasesRequest> selector = null) =>
			this.CatAliases(selector.InvokeOrDefault(new CatAliasesDescriptor()));

		/// <inheritdoc/>
		public ICatResponse<CatAliasesRecord> CatAliases(ICatAliasesRequest request) =>
			this.DoCat<ICatAliasesRequest, CatAliasesRequestParameters, CatAliasesRecord>(request, this.LowLevelDispatch.CatAliasesDispatch<CatResponse<CatAliasesRecord>>);

		/// <inheritdoc/>
		public Task<ICatResponse<CatAliasesRecord>> CatAliasesAsync(Func<CatAliasesDescriptor, ICatAliasesRequest> selector = null) =>
			this.CatAliasesAsync(selector.InvokeOrDefault(new CatAliasesDescriptor()));

		/// <inheritdoc/>
		public Task<ICatResponse<CatAliasesRecord>> CatAliasesAsync(ICatAliasesRequest request) =>
			this.DoCatAsync<ICatAliasesRequest, CatAliasesRequestParameters, CatAliasesRecord>(request, this.LowLevelDispatch.CatAliasesDispatchAsync<CatResponse<CatAliasesRecord>>);

	}
}