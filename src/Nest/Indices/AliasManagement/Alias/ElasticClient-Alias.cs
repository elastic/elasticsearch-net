using System;
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
		/// <param name="selector">A descriptor that describes the parameters for the alias operation</param>
		IBulkAliasResponse Alias(Func<BulkAliasDescriptor, IBulkAliasRequest> selector);

		/// <inheritdoc/>
		IBulkAliasResponse Alias(IBulkAliasRequest request);

		/// <inheritdoc/>
		Task<IBulkAliasResponse> AliasAsync(Func<BulkAliasDescriptor, IBulkAliasRequest> selector);

		/// <inheritdoc/>
		Task<IBulkAliasResponse> AliasAsync(IBulkAliasRequest request);
	}
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IBulkAliasResponse Alias(IBulkAliasRequest request) => 
			this.Dispatcher.Dispatch<IBulkAliasRequest, BulkAliasRequestParameters, BulkAliasResponse>(
				request,
				this.LowLevelDispatch.IndicesUpdateAliasesDispatch<BulkAliasResponse>
			);

		/// <inheritdoc/>
		public IBulkAliasResponse Alias(Func<BulkAliasDescriptor, IBulkAliasRequest> selector) =>
			this.Alias(selector?.Invoke(new BulkAliasDescriptor()));

		/// <inheritdoc/>
		public Task<IBulkAliasResponse> AliasAsync(IBulkAliasRequest request) => 
			this.Dispatcher.DispatchAsync<IBulkAliasRequest, BulkAliasRequestParameters, BulkAliasResponse, IBulkAliasResponse>(
				request,
				this.LowLevelDispatch.IndicesUpdateAliasesDispatchAsync<BulkAliasResponse>
			);

		/// <inheritdoc/>
		public Task<IBulkAliasResponse> AliasAsync(Func<BulkAliasDescriptor, IBulkAliasRequest> selector)=>
			this.AliasAsync(selector?.Invoke(new BulkAliasDescriptor()));

	}
}