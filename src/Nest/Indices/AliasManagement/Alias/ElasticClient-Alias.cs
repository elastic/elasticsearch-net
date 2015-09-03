using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// APIs in elasticsearch accept an index name when working against a specific index, and several indices when applicable. 
		/// <para>The index aliases API allow to alias an index with a name, with all APIs automatically converting the alias name to the 
		/// actual index name.</para><para> An alias can also be mapped to more than one index, and when specifying it, the alias 
		/// will automatically expand to the aliases indices.i</para><para> An alias can also be associated with a filter that will 
		/// automatically be applied when searching, and routing values.</para>
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-aliases.html
		/// </summary>
		/// <param name="aliasSelector">A desriptor that describes the parameters for the alias operation</param>
		IIndicesOperationResponse Alias(Func<BulkAliasDescriptor, IBulkAliasRequest> aliasSelector);

		/// <inheritdoc/>
		IIndicesOperationResponse Alias(IBulkAliasRequest aliasRequest);

		/// <inheritdoc/>
		Task<IIndicesOperationResponse> AliasAsync(Func<BulkAliasDescriptor, IBulkAliasRequest> aliasSelector);

		/// <inheritdoc/>
		Task<IIndicesOperationResponse> AliasAsync(IBulkAliasRequest aliasRequest);
	}
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IIndicesOperationResponse Alias(IBulkAliasRequest aliasRequest) => 
			this.Dispatcher.Dispatch<IBulkAliasRequest, BulkAliasRequestParameters, IndicesOperationResponse>(
				aliasRequest,
				this.LowLevelDispatch.IndicesUpdateAliasesDispatch<IndicesOperationResponse>
			);

		/// <inheritdoc/>
		public IIndicesOperationResponse Alias(Func<BulkAliasDescriptor, IBulkAliasRequest> aliasSelector) =>
			this.Alias(aliasSelector?.Invoke(new BulkAliasDescriptor()));

		/// <inheritdoc/>
		public Task<IIndicesOperationResponse> AliasAsync(IBulkAliasRequest aliasRequest) => 
			this.Dispatcher.DispatchAsync<IBulkAliasRequest, BulkAliasRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
				aliasRequest,
				this.LowLevelDispatch.IndicesUpdateAliasesDispatchAsync<IndicesOperationResponse>
			);

		/// <inheritdoc/>
		public Task<IIndicesOperationResponse> AliasAsync(Func<BulkAliasDescriptor, IBulkAliasRequest> aliasSelector)=>
			this.AliasAsync(aliasSelector?.Invoke(new BulkAliasDescriptor()));

	}
}