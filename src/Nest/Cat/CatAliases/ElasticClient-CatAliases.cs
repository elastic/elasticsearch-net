using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICatResponse<CatAliasesRecord> CatAliases(Func<CatAliasesDescriptor, CatAliasesDescriptor> selector = null) =>
			this.DoCat<CatAliasesDescriptor, CatAliasesRequestParameters, CatAliasesRecord>(selector, this.RawDispatch.CatAliasesDispatch<CatResponse<CatAliasesRecord>>);

		public ICatResponse<CatAliasesRecord> CatAliases(ICatAliasesRequest request) =>
			this.DoCat<ICatAliasesRequest, CatAliasesRequestParameters, CatAliasesRecord>(request, this.RawDispatch.CatAliasesDispatch<CatResponse<CatAliasesRecord>>);

		public Task<ICatResponse<CatAliasesRecord>> CatAliasesAsync(Func<CatAliasesDescriptor, CatAliasesDescriptor> selector = null) =>
			this.DoCatAsync<CatAliasesDescriptor, CatAliasesRequestParameters, CatAliasesRecord>(selector, this.RawDispatch.CatAliasesDispatchAsync<CatResponse<CatAliasesRecord>>);

		public Task<ICatResponse<CatAliasesRecord>> CatAliasesAsync(ICatAliasesRequest request) =>
			this.DoCatAsync<ICatAliasesRequest, CatAliasesRequestParameters, CatAliasesRecord>(request, this.RawDispatch.CatAliasesDispatchAsync<CatResponse<CatAliasesRecord>>);

	}
}